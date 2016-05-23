using UnityEngine;
using System;

public class CameraBarrier : MonoBehaviour {

    private bool collidedX = true,  collidedY = true;
    private float lastCollidedX, lastCollidedY;
    private float initDistX = 19.2f;
    private float initDistY = 10.8f;
    private GameObject player;
    private GameObject border;
    private float lastX = 0f;
    private bool teleported;
    public GameObject[] floor;
    public GameObject[] leftWalls;
    public GameObject[] rightWalls;
    private int atSection = 1;

    void Start ()
    {
        player = GameObject.Find("Character");
        border = GameObject.Find("Barrier");
    }

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Character") != null)
        {
            float posX = player.transform.position.x, posY = this.transform.position.y;

            //This controls the placement of the x coordinate
            if(collidedX || Math.Abs(player.transform.position.x - lastX) >= 1f)
            {
                if(player.transform.position.x - (leftWalls[player.GetComponent<GameManager>().atSection - 1].transform.position.x
                        + leftWalls[player.GetComponent<GameManager>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2) < initDistX)
                {
                    //print("Collided with Left Wall");
                    posX = leftWalls[player.GetComponent<GameManager>().atSection - 1].transform.position.x
                        + leftWalls[player.GetComponent<GameManager>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2 + initDistX;
                } else if ((rightWalls[player.GetComponent<GameManager>().atSection - 1].transform.position.x
                        - rightWalls[player.GetComponent<GameManager>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2) - player.transform.position.x < initDistX)
                {
                    //print("Collided with Right Wall");
                    posX = rightWalls[player.GetComponent<GameManager>().atSection - 1].transform.position.x
                        - rightWalls[player.GetComponent<GameManager>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2 - initDistX;
                }
            } else
            {
                //print("moving away from walls");
                collidedX = false;
            }

            //This code calculates the y axis of the camera
            //if the camera collided with the floor
            if (Math.Abs(posX - lastX) >= 1f)
            {
                if (atSection != player.GetComponent<GameManager>().atSection)
                {
                    posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2;
                    atSection = player.GetComponent<GameManager>().atSection;
                }
            }
            else if (collidedY)
            {
                if (player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 2
                    < border.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2)
                {
                    //print("Camera is staying where it is, collided");
                    posY = floor[player.GetComponent<GameManager>().atSection - 1].transform.position.y + floor[player.GetComponent<GameManager>().atSection - 1].GetComponent<Renderer>().bounds.size.y / 2 + initDistY;
                }
                else
                {
                    //print("Camera is moving away from the floor");
                    posY = player.transform.position.y - border.GetComponent<Renderer>().bounds.size.y / 2;
                    collidedY = false;
                }
            }
            else if (player.transform.position.y - player.GetComponent<Renderer>().bounds.size.y / 2
                    < border.transform.position.y - border.GetComponent<Renderer>().bounds.size.y / 2)
            {
                //print("Camera is moving down, not collided");
                posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2;
            }
            else if (player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 2
                  > border.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2)
            {
                //print("Camera is moving up, not collided");
                posY = player.transform.position.y - border.GetComponent<Renderer>().bounds.size.y / 2;
            }

            //print("X: " + posX + " Y: " + posY);
            this.transform.position = new Vector3(posX, posY, this.transform.position.z);
            lastX = posX;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "border")
        {
            //hits barrier to the right
            if (coll.gameObject.name.Contains("Right Border"))
            {
                //print("it hit right wall");
                collidedX = true;
                lastCollidedX = this.transform.position.x;
            }
            //hits barrier to the left
            else if (coll.gameObject.name.Contains("Left Border"))
            {
                //print("it hit left wall");
                collidedX = true;
                lastCollidedX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x / 2;
            }
            //hits barrier to the bottom
            else
            {
                //print("it hit floor");
                collidedY = true;
                lastCollidedY = this.transform.position.y;
            }
        }
    }
}
