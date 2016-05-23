using UnityEngine;
using System;

public class NewCameraBarrier : MonoBehaviour {
    private GameObject player;
    private GameObject border;
    public GameObject[] floor;
    private bool collided = false;
    private float lastCollidedY;
    private float lastX = 0f;
    private float halfCameraY = 10.8f;
    private int cameraAt = 1;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Character");
        border = GameObject.Find("Barrier");
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Character") != null)
        {
            float posX = player.transform.position.x;
            float posY = this.transform.position.y;

            //This code calculates the y axis of the camera
            //if the camera collided with the floor
            if(Math.Abs(posX - lastX) >= 20f)
            {
                if(cameraAt != player.GetComponent<GameManager>().atSection)
                {
                    posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2;
                    cameraAt = player.GetComponent<GameManager>().atSection;
                }
            }
            else if (collided)
            {
                if (player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y
                    < border.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2)
                {
                    print("Camera is staying where it is, collided");
                    posY = floor[player.GetComponent<GameManager>().atSection - 1].transform.position.y + floor[player.GetComponent<GameManager>().atSection - 1].GetComponent<Renderer>().bounds.size.y / 2 + halfCameraY;
                }
                else
                {
                    print("Camera is moving away from the floor");
                    posY = player.transform.position.y - border.GetComponent<Renderer>().bounds.size.y / 2;
                    collided = false;
                }
            }
            else if (player.transform.position.y - player.GetComponent<Renderer>().bounds.size.y / 2
                    < border.transform.position.y - border.GetComponent<Renderer>().bounds.size.y / 2)
            {
                print("Camera is moving down, not collided");
                posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2;
            } else if (player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 2
                    > border.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2)
            {
                print("Camera is moving up, not collided");
                posY = player.transform.position.y - border.GetComponent<Renderer>().bounds.size.y / 2;
            }

            print("X: " + posX + " Y: " + posY);
            this.transform.position = new Vector3(posX, posY, this.transform.position.z);
            lastX = posX;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "border")
        {
            print("it hit floor");
            collided = true;
            lastCollidedY = this.transform.position.y;
        }
    }
}
