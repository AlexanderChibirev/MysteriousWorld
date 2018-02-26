using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    float timeCounter = 0;
    // Use this for initialization

    float speed;
    float width;
    float height;
    void Start()
    {

        speed = 0.5f;
        width = -3.21f;
        height = 2.636f;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;

        float y = Mathf.Sin(timeCounter) * height;
        float z = 6.819f;

        transform.position = new Vector3(x, y, z);
    }
}
