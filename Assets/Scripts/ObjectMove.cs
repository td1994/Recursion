using UnityEngine;
using System.Collections;
using System;

public class ObjectMove : MonoBehaviour {
    public GameObject[] positions;
    public GameObject button;
    public float velocity;
    private int count = 0;
	
	// Update is called once per frame
	void Update () {
	    if(button.GetComponent<Button>().triggered)
        {
            if(count < positions.Length)
            {   if(Math.Abs(positions[count].transform.position.x - this.transform.position.x) <= 0.5f ||
                    Math.Abs(positions[count].transform.position.y - this.transform.position.y) <= 0.5f)
                {
                    count++;
                }

                if(Math.Abs(positions[count].transform.position.x - this.transform.position.x) > Math.Abs(positions[count].transform.position.y - this.transform.position.y))
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector3(velocity * 
                        (positions[count].transform.position.x - this.transform.position.x / 
                        Math.Abs(positions[count].transform.position.x - this.transform.position.x)), 0f, 0f);
                } else
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, velocity *
                        (positions[count].transform.position.y - this.transform.position.y /
                        Math.Abs(positions[count].transform.position.y - this.transform.position.y)), 0f);
                }
            }
        }
	}
}
