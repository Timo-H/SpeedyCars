using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{

    float timer = 0f;
    [SerializeField] float lightRotationTime = 6f;

    Renderer redLight;
    Renderer orangeLight;
    Renderer greenLight;

    public float rotation;
    public string direction;
    public bool green;

    public float stopPosition;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateDirection();

        redLight = transform.Find("Red Light").gameObject.GetComponent<Renderer>();
        orangeLight = transform.Find("Orange Light").gameObject.GetComponent<Renderer>();
        greenLight = transform.Find("Green Light").gameObject.GetComponent<Renderer>();

        green = true;

        greenLight.material.SetColor("_Color", Color.grey);
        orangeLight.material.SetColor("_Color", Color.grey);
        redLight.material.SetColor("_Color", Color.grey);

        GreenLight(greenLight, redLight);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > lightRotationTime)
        {
            timer -= lightRotationTime;

            green = !green;

            GreenLight(greenLight, redLight);
            if (!green) { 
                StartCoroutine(OrangeToRedLight(orangeLight, redLight));
            }
        };
    }

    private IEnumerator OrangeToRedLight(Renderer orangeLight, Renderer redLight)
    {
        if (!green) {
            orangeLight.material.SetColor("_Color", Color.yellow);

            yield return new WaitForSeconds(2);

            orangeLight.material.SetColor("_Color", Color.grey);
            redLight.material.SetColor("_Color", Color.red);
        } else
        {
            redLight.material.SetColor("_Color", Color.grey);
        }
    }

    private void GreenLight(Renderer greenLight, Renderer redLight)
    {
        if (green)
        {
            redLight.material.SetColor("_Color", Color.grey);
            greenLight.material.SetColor("_Color", Color.green);
        } else
        {
            greenLight.material.SetColor("_Color", Color.grey);
        }
    }

    private void UpdateDirection()
    {
        rotation = transform.rotation.eulerAngles.z;
        float stopDistance = 1;
        if(rotation == 0)
        {
            direction = "north";
            stopPosition = transform.position.y - stopDistance;
        } else if (rotation == 90)
        {
            direction = "west";
            stopPosition = transform.position.x + stopDistance;
        }
        else if (rotation == 180)
        {
            direction = "south";
            stopPosition = transform.position.y + stopDistance;
        }
        else if (rotation == 270)
        {
            direction = "east";
            stopPosition = transform.position.x - stopDistance;
        }
    }
}
