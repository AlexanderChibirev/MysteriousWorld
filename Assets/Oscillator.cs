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

        speed = 0.000000005f;
        width = 1f;
        height = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;

        float y = Mathf.Sin(timeCounter) * height;
        float z = 0;
        Debug.Log("ДО: " + transform.position);
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
        Debug.Log("После: " + transform.position);
    }
}
