using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField] float maxSpawnTime = 2f;
    [SerializeField] string direction;

    [SerializeField] GameObject carPrefab;
    private int numCars = 0;
    public bool canSpawn = true;
    private float spawnTime;

    Dictionary<string, float> rotations = new Dictionary<string, float>();

    private void Start()
    {
        rotations.Add("north", 0);
        rotations.Add("east", 270);
        rotations.Add("south", 180);
        rotations.Add("west", 90);

        spawnTime = Random.Range(2, maxSpawnTime);
    }
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > spawnTime)
        {
            timer -= spawnTime;
            spawnTime = Random.Range(2, maxSpawnTime);
            // Spawn Car
            Instantiate(carPrefab, transform.position, Quaternion.Euler(0, 0, rotations[direction])).name = $"Car {++numCars}";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canSpawn = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canSpawn = true;
    }
}
