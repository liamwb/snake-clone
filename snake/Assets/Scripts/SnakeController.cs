using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SnakeController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction movement;
    public FoodSpawnerController foodSpawner;
    public SnakeSpawnerController snakeSpawner;
    public IntScriptableObject scoreSO;
    public TextMeshProUGUI playButtonText;
    public UIManager uiManager;

    private Vector3 currentDirection;
    private List<Transform> bodyPieces;
    
    void Start()
    {
        // Set up Unity's input system
        playerInput = this.GetComponent<PlayerInput>();
        movement = playerInput.actions["Movement"];

        bodyPieces = new List<Transform>();
        bodyPieces.Add(this.transform);

        currentDirection = new Vector3(0, -1, 0);
        scoreSO.value = 0;  // Reset the score when a new snake is created
    }

    void FixedUpdate()
    {
        //Update the position of all the body pieces
        for (int i = bodyPieces.Count  - 1; i > 0; i--)        
        {
            bodyPieces[i].position = bodyPieces[i-1].position;
        }

        // Move the head
        Vector3 input = (Vector3) movement.ReadValue<Vector2>();
        currentDirection = SnakeMovement(input);
        this.transform.position += currentDirection;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food")
        {
            foodSpawner.SpawnFood();  // Spawn a new food piece
            
            Vector3 newSnakePosition = bodyPieces[bodyPieces.Count - 1].position;  // Add a body piece
            GameObject newSnake = snakeSpawner.spawnSnake(newSnakePosition);
            bodyPieces.Add(newSnake.transform);
            
            Destroy(other.gameObject);  // Destory the old piece

            scoreSO.value++;  // Update Score
        }

        else if (other.tag == "DeathCondition")
        {
            // Destroy all the pieces of the snake
            for (int i = bodyPieces.Count - 1; i >= 0; i--)
            {
                Destroy(bodyPieces[i].gameObject);
            }

            // Get rid of the food on the screen
            GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
            foreach (GameObject x in foods)
            {
                Destroy(x);
            }

            // Hide the play/pause button
            playButtonText.text = "";
        }
    }

    private Vector3 SnakeMovement(Vector3 input)
    {
        // no backwards movement
        if (input == -currentDirection)
        {
            return currentDirection;
        }   
        // accept simple movement
        if (Vector3.SqrMagnitude(input) == 1)
        {
            return input;
        }
        // preserve direction if nothing is pressed
        if (input == Vector3.zero)
        {
            return currentDirection;
        }
        // preserve direction if two buttons are pressed
        if ((currentDirection.x != 0 && currentDirection.x == input.x) || (currentDirection.y != 0 && currentDirection.y == input.y))
        {
            return currentDirection;
        }
        // else avoid doubling back
        else
        {
            if (currentDirection.x == 0)
            {
                return new Vector3(input.x, 0, 0);
            }
            else  // currentDirection.y == 0
            {
                return new Vector3(0, input.y, 0);
            }
        }
    }

    void OnPauseControl() // I don't know how to get the PlayerInput component to talk to the UIManager object directly...
    {
        uiManager.playPauseTask();
    }
}
