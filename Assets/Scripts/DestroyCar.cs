using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCar : MonoBehaviour
{
    public int carThroughput = 0;
    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
        carThroughput++;
    }
}
