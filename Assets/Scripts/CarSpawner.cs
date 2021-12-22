using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] float spawnTime = 2f;
    [SerializeField] string direction;

    [SerializeField] GameObject carPrefab;
    int numCars = 0;

    Dictionary<string, float> rotations = new Dictionary<string, float>();

    private void Start()
    {
        rotations.Add("north", 0);
        rotations.Add("east", 270);
        rotations.Add("south", 180);
        rotations.Add("west", 90);
    }
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > spawnTime)
        {
            timer -= spawnTime;

            // Spawn Car
            Instantiate(carPrefab, transform.position, Quaternion.Euler(0, 0, rotations[direction])).name = $"Car {++numCars}";
        }
    }
}
