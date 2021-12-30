using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPrefab;
    public void SpawnFood()
    {
        GameObject newFood = Instantiate(foodPrefab);
        int x = (int) Random.Range(-12, 12);
        int y = (int) Random.Range(-12, 12);
        newFood.transform.position = new Vector3(x, y, 0);
    }
    
}
