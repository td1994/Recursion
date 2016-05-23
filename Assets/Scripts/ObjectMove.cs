using UnityEngine;
using System.Collections;
using System;

public class ObjectMove : MonoBehaviour {
    public GameObject[] positions;
    public GameObject button;
    public float velocity;
    private int count = 0;
    private double angle;

	// Update is called once per frame
	void Update () {
	    if(button.GetComponent<Button>().triggered)
        {
            if (count < positions.Length)
            {   if(Math.Abs(positions[count].transform.position.x - this.transform.position.x) <= 1f &&
                    Math.Abs(positions[count].transform.position.y - this.transform.position.y) <= 1f)
                {
                    count++;
                    this.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);

                }
                else
                {
                    angle = Math.Atan((positions[count].transform.position.x - this.transform.position.x) / (positions[count].transform.position.y - this.transform.position.y));
                    this.GetComponent<Rigidbody2D>().velocity = new Vector3(velocity * 
                        (float)Math.Sin(angle), velocity * (float)Math.Cos(angle), 0f);
                }
            }
        }
	}
}
