using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour {

    public float speed;
    public Vector3 direction;
    private int count;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        Debug.Log(count);
        if (count > 100) {
            direction.x *= -1;
            direction.y *= -1;
            direction.z *= -1;
            count = 0;
        }
        transform.Translate(direction * speed * Time.deltaTime);	
	}
}
