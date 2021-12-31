using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    public GameObject DestroyCar;
    public string variableName;

    private float updateNSeconds = 0.25f;
    private float lastUpdateTime = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        lastUpdateTime += Time.deltaTime;

        if (lastUpdateTime > updateNSeconds)
        {
            lastUpdateTime = 0f;

            Text myText = gameObject.GetComponent<Text>();
            myText.text = variableName + " = " + DestroyCar.GetComponent<DestroyCar>().carThroughput;
        } 
    }
}
