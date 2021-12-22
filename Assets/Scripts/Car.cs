using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    public Road road;
    public bool waitingForTrafficLight = false;
    public bool slowingDown = false;
    public bool waitingForAnotherCar = false;
    public bool isCollidingWithRoad = false;
    public bool isCollidingWithVehicle = false;
    public bool goingMaxSpeed = false;
    public float position;
    public float rotation;
    public string direction;
    public float velocity;

    // Update is called once per frame
    private void Start()
    {
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        UpdateDirection();

        if (direction == "north" || direction == "south")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
            position = transform.position.y;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            position = transform.position.x;
        }

        SetTriggerColliderOffset(CarLength(Mathf.Abs(velocity), 2, 2));

        isCollidingWithRoad = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Car") && !isCollidingWithVehicle)
        {
            Car backCar = transform.GetComponent<Car>();
            if (!isCarInFront(position, col.GetComponent<Car>().position, direction))
            {
                Debug.Log(transform.name + " zit achter " + col.name);
                backCar = col.GetComponent<Car>();
            }
            if (direction == col.GetComponent<Car>().direction)
            {
                backCar.slowingDown = true;
                backCar.SetSpeed(col.GetComponent<Car>().speed);
                backCar.goingMaxSpeed = false;
                backCar.isCollidingWithVehicle = true;
                Debug.Log(transform.name + " is behind other car, slowing down!");
            }
        }
        if (col.gameObject.name.Contains("Road") && !isCollidingWithRoad)
        {
            road = col.GetComponent<Road>();
            if (!waitingForTrafficLight && !slowingDown && !waitingForAnotherCar && !isCollidingWithVehicle && !goingMaxSpeed)
            {
                SetSpeed(road.GetMaxSpeed());
                goingMaxSpeed = true;
                Debug.Log(transform.name + " entered new road, changing speed");
            }
            isCollidingWithRoad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Car") && isCollidingWithVehicle)
        {
            Car backCar = transform.GetComponent<Car>();
            if (!isCarInFront(position, col.GetComponent<Car>().position, direction))
            {
                Debug.Log(transform.name + " zit achter " + col.name);
                backCar = col.GetComponent<Car>();
            }
            if (direction == col.GetComponent<Car>().direction)
            {
                backCar.slowingDown = false;
                backCar.waitingForAnotherCar = false;
                Debug.Log(transform.name + " (direction: " + direction + ") is no longer slowing down or waiting");
                backCar.isCollidingWithVehicle = false;
                backCar.SetSpeed(road.GetMaxSpeed() * 0.75f);
                backCar.goingMaxSpeed = false;
                Debug.Log(transform.name + " too close to car in front, matching speed!");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Car"))
        {
            Car backCar = transform.GetComponent<Car>();
            if (!isCarInFront(position, col.GetComponent<Car>().position, direction))
            {
                Debug.Log(transform.name + " zit achter " + col.name);
                backCar = col.GetComponent<Car>();
            }
            if (direction == col.GetComponent<Car>().direction)
            {
                backCar.isCollidingWithVehicle = true;
                if (backCar.speed == 0)
                {
                    backCar.waitingForAnotherCar = true;
                    Debug.Log(transform.name + " (direction: " + direction + ") is waiting for another car");
                }
                else
                {
                    backCar.SetSpeed(road.GetMaxSpeed() * 0.75f);
                    Debug.Log(transform.name + " too close to car in front, slowing down!");
                }
            } 
        }
        if (col.gameObject.name.Contains("Road"))
        {
            Road road = col.GetComponent<Road>();
            if (!waitingForTrafficLight && !slowingDown && !waitingForAnotherCar && !isCollidingWithVehicle && !goingMaxSpeed)
            {
                SetSpeed(road.GetMaxSpeed());
                goingMaxSpeed = true;
            }

            if (goingMaxSpeed && velocity < road.GetMaxSpeed())
            {
                goingMaxSpeed = false;
            }
            checkForTrafficLight(road);
        }
    }

    private bool isCarInFront(float position1, float position2, string direction)
    {
        if (direction == "north" || direction == "east")
        {
            return position1 < position2;
        } else
        {
            return position1 > position2;
        }
    }
    private void SetSpeed(float newSpeed)
    {
        if (newSpeed >= 0)
        {
            if (direction == "west" || direction == "south")
            {
                speed = newSpeed * -1;
                Debug.Log(transform.name + " (direction: " + direction + ") Speed changed to " + speed);
            }
            else if (direction == "north" || direction == "east")
            {
                speed = newSpeed;
                Debug.Log(transform.name + " (direction: " + direction + ") Speed changed to " + speed);
            }
        }
    }

    private void UpdateDirection()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rotation = transform.rotation.eulerAngles.z;
        if (rotation == 0)
        {
            direction = "north";
            velocity = rb.velocity.y;
        } else if (rotation == 90)
        {
            direction = "west";
            velocity = rb.velocity.x;
        } else if (rotation == 180)
        {
            direction = "south";
            velocity = rb.velocity.y;
        } else if (rotation == -90)
        {
            direction = "east";
            velocity = rb.velocity.x;
        }
    }

    private void checkForTrafficLight(Road road)
    {
        int childCount = road.transform.childCount;
        if (childCount > 0)
        {
            for (var i = 0; i < childCount; ++i)
            {
                var child = road.transform.GetChild(i);
                if (child.gameObject.name.Contains("Traffic Light"))
                {
                    if (child.gameObject.GetComponent<TrafficLight>().direction == direction && !child.GetComponent<TrafficLight>().green) 
                    {
                        StopForTrafficLight(child);
                    } else
                    {
                        if (waitingForTrafficLight)
                        {
                            waitingForTrafficLight = false;
                            waitingForAnotherCar = false;
                        }
                        
                        if (!waitingForTrafficLight && !slowingDown && !waitingForAnotherCar && !isCollidingWithVehicle && velocity == 0 && !goingMaxSpeed)
                        {
                            Debug.Log(transform.name + " light green, continue!");
                            SetSpeed(road.GetMaxSpeed());
                            goingMaxSpeed = true;
                        }
                    }
                }
            }
        }
    }

    private void StopForTrafficLight(Transform child)
    {
        float stopPosition = child.GetComponent<TrafficLight>().stopPosition;
        float trafficLightDistance;
        if (direction == "north" || direction == "south")
        {
            trafficLightDistance = Math.Abs(stopPosition - gameObject.transform.position.y);
        } else
        {
            trafficLightDistance = Math.Abs(stopPosition - gameObject.transform.position.x);
        }

        if (trafficLightDistance < 1 && CanBreak(velocity, 20, trafficLightDistance) && !waitingForTrafficLight)
        {
            SetSpeed(0);
            Debug.Log(transform.name + " Stopped for traffic light");
            waitingForTrafficLight = true;
        }
    }

    private void SetTriggerColliderOffset(float size)
    {
        size = Mathf.Abs(size);
        if (size > 1)
        {
            float xOffset = 0;
            float yOffset = -((size / 2) + 1);
            transform.GetComponent<BoxCollider2D>().size = new Vector2(1, size);
            transform.GetComponent<BoxCollider2D>().offset = new Vector2(xOffset, yOffset);
        }
    }

    /// <summary>
    /// Calculates the effective length of a car object. By multiplying the velocity times how much distance in
    /// seconds each car is supposed to keep for their predecessor, plus their own car length.
    /// </summary>
    /// <param name="velocity"></param>
    /// The speed in m/s of the car.
    /// <param name="carLength"></param>
    /// The length of the car in m.
    /// <param name="distanceTime"></param>
    /// The time in seconds a car is to legally keep between themselves and the car in front of them.
    /// <returns></returns>
    /// The car's effective length in m.
    private static float CarLength(float velocity, float carLength, float distanceTime)
    {
        float result = (0 - velocity) * distanceTime + carLength; // m/s * s + m = m
        return result;
    }

    /// <summary>
    /// Determines whether a car has enough distance to target to be able to break.
    /// </summary>
    /// <param name="velocity"></param>
    /// Current speed in m/s of object.
    /// <param name="deAcceleration"></param>
    /// Object's ability to loose speed m/s^2.
    /// <param name="distance"></param>
    /// Distance between object and end.
    /// <returns></returns>
    /// true if object can succesfully come to a standstill if it starts de accelerating right now.
    /// false if object can not succesfully  come to a standstill if it starts de accelerating right now.
    private static bool CanBreak(float velocity, float deAcceleration, float distance)
    {
        float timeNeeded = velocity / deAcceleration;
        float traveled = velocity * timeNeeded - 0.5f * deAcceleration * timeNeeded * timeNeeded;
        if (traveled > distance)
        {
            return false;
        }
        else if (traveled <= distance)
        {
            return true;
        }
        else
        {
            throw new Exception("One of the values in CanBreak is not a number. (Neither float nor integer)");
        }
    }
}
