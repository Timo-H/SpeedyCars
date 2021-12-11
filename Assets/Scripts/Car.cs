using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.gameObject.name.Contains("Road"))
        {
            Road road = col.collider.GetComponent<Road>();
            setSpeed(road.GetMaxSpeed());
        }
    }

    void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
