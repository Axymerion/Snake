using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int maxFood = 1;
    public float foodSpawnDelay;
    public GameObject foodPrefab;
    private static int currFood;

    public static void FoodEaten()
    {
        currFood--;
    }

    void SpawnFood()
    {
        if(currFood < maxFood)
        {
            int x = Random.Range(-12, 12) * 2;
            int z = Random.Range(-12, 12) * 2;
            GameObject temp = Instantiate(foodPrefab, new Vector3(x, 0.5f, z), Quaternion.identity, transform);
            currFood++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currFood = 0;
        InvokeRepeating("SpawnFood", foodSpawnDelay, foodSpawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        currFood = transform.childCount;
    }
}
