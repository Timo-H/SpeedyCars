using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] float spawnTime = 2f;

    [SerializeField] GameObject carPrefab;
    int numCars = 0;

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > spawnTime)
        {
            timer -= spawnTime;
            
            // Spawn Car
            Instantiate(carPrefab, new Vector3(22f, 0.5f, 0f), Quaternion.Euler(0, 0, 90)).name = $"Car {++numCars}";
        }
    }
}
