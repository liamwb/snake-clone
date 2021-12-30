using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawnerController : MonoBehaviour
{
    public GameObject snakePrefab;
    public GameObject spawnSnake(Vector3 position)
    // Creates a new snake piece with position position and returns it
    {
        GameObject newSnake = Instantiate(snakePrefab);
        newSnake.transform.position = position;
        return newSnake;
    }
}
