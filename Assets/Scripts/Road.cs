using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] float maxSpeed = 50;

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
    
    public bool IsHorizontal()
    {
        return gameObject.transform.rotation.z == 0;
    }
}
