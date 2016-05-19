using UnityEngine;
using System;

public class CameraBarrier : MonoBehaviour {

    private bool rightWall, ceil;
    private bool collidedX = true,  collidedY = true;
    private float lastCollidedX, lastCollidedY;
    private float initDistX = 19.2f;
    private float initDistY = 8.1f;
    public GameObject player;
    public GameObject border;
    private float lastX = 0f;
    private bool teleported;


	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Character") != null)
        {
            float posX = this.transform.position.x, posY = this.transform.position.y;

            //Control the camera on the X axis
            if (collidedX)
            {
                if (!rightWall && player.transform.position.x > this.transform.position.x)
                {
                    print("Moving away from left wall");
                    posX = player.transform.position.x;
                    collidedX = false;
                } else if (rightWall && player.transform.position.x < lastCollidedX)
                {
                    print("Moving away from right wall");
                    posX = player.transform.position.x;
                    collidedX = false;
                    rightWall = false;
                    if (Math.Abs(posX - lastX) > 20f)
                    {
                        print("teleported");
                        teleported = true;
                        posX = player.transform.position.x + initDistX - player.GetComponent<Renderer>().bounds.size.x / 2;
                        collidedX = true;
                    }
                }           
            } else
            {
                posX = player.transform.position.x;
                if (Math.Abs(posX - lastX) > 20f)
                {
                    print("teleported");
                    teleported = true;
                    collidedX = true;
                    if (player.GetComponent<Movement>().beginning)
                    {
                        posX = player.transform.position.x + initDistX - player.GetComponent<Renderer>().bounds.size.x / 2;     
                    } else
                    {
                        posX = player.transform.position.x;
                    }
                }
            }

            //Controls the camera on the Y axis
            if (ceil && player.transform.position.y < lastCollidedY - (border.GetComponent<Renderer>().bounds.size.y / 2) + (player.GetComponent<Renderer>().bounds.size.x / 2))
            {
                print("Moving away from ceiling");
                posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2 - (player.GetComponent<Renderer>().bounds.size.x / 2);
            } else if(!ceil && player.transform.position.y > lastCollidedY + (border.GetComponent<Renderer>().bounds.size.y / 2) - (player.GetComponent<Renderer>().bounds.size.x / 2))
            {
                print("Moving away from floor");
                posY = player.transform.position.y - (border.GetComponent<Renderer>().bounds.size.y / 2) + (player.GetComponent<Renderer>().bounds.size.x / 2);
            }
            else if (teleported)
            {
                print("changing y value");
                posY = player.transform.position.y + (border.GetComponent<Renderer>().bounds.size.y / 2) - (player.GetComponent<Renderer>().bounds.size.x / 2);
                teleported = false;
            }
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
                print("it hit right wall");
                rightWall = true;
                collidedX = true;
                lastCollidedX = this.transform.position.x;
            }
            //hits barrier to the left
            else if (coll.gameObject.name.Contains("Left Border"))
            {
                print("it hit left wall");
                rightWall = false;
                collidedX = true;
                lastCollidedX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x / 2;
            }
            //hits barrier to the top
            if(coll.gameObject.name.Contains("Top Border"))
            {
                print("it hit ceiling");
                ceil = true;
                collidedY = true;
                lastCollidedY = this.transform.position.y;
            }
            //hits barrier to the bottom
            else
            {
                print("it hit floor");
                ceil = false;
                collidedY = true;
                lastCollidedY = this.transform.position.y;
            }
        }
    }
}
