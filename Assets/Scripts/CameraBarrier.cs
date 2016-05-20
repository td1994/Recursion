using UnityEngine;
using System;

public class CameraBarrier : MonoBehaviour {

    private bool rightWall, ceil;
    private bool collidedX = true,  collidedY = true;
    private float lastCollidedX, lastCollidedY;
    private float initDistX = 19.2f;
    public GameObject player;
    public GameObject border;
    private float lastX = 0f;
    private bool teleported;
    public GameObject[] ceilings;
    private int atSection = 1;


	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Character") != null)
        {
            float posX = this.transform.position.x, posY = this.transform.position.y;

            //Control the camera on the X axis
            if (collidedX)
            {
                //if the camera is moving away from the left wall
                if (!rightWall && player.transform.position.x > this.transform.position.x)
                {
                    //print("Moving away from left wall");
                    posX = player.transform.position.x;
                    collidedX = false;
                } else if (rightWall && player.transform.position.x < lastCollidedX)
                //if the camera is moving away from the right wall
                {
                    //print("Moving away from right wall");
                    posX = player.transform.position.x;
                    collidedX = false;
                    rightWall = false;
                    //if the player teleported (aka is repeating the section)
                    if (Math.Abs(posX - lastX) > 20f)
                    {
                        //print("teleported");
                        teleported = true;
                        posX = player.transform.position.x + initDistX - player.GetComponent<Renderer>().bounds.size.x / 2;
                        collidedX = true;
                    }
                }           
            } else //if the player is just traversing the level
            {
                posX = player.transform.position.x;
                //if they teleport mid level
                if (Math.Abs(posX - lastX) > 20f)
                {
                    //print("teleported");
                    teleported = true;
                    collidedX = true;
                    //if the teleporter takes them to the beginning of another section
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
            //if the camera is moving away from the ceiling
                if (ceil && player.transform.position.y < lastCollidedY - (border.GetComponent<Renderer>().bounds.size.y / 2) + (player.GetComponent<Renderer>().bounds.size.x / 2))
                {
                    //print("Moving away from ceiling");
                    posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2 - (player.GetComponent<Renderer>().bounds.size.x / 2);
                    collidedY = false;
                }
                else if (!ceil && player.transform.position.y > lastCollidedY + (border.GetComponent<Renderer>().bounds.size.y / 2) - (player.GetComponent<Renderer>().bounds.size.x / 2))
                //if the camera is moving away from the floor
                {
                    //print("Moving away from floor");
                    posY = player.transform.position.y - (border.GetComponent<Renderer>().bounds.size.y / 2) + (player.GetComponent<Renderer>().bounds.size.x / 2);
                    collidedY = false;
                }
            
            //if they teleported
            if (teleported)
            {
                
                print(player.GetComponent<Movement>().atSection);
                //if they're moving to the beginning of a level
                if (player.GetComponent<Movement>().beginning)
                {
                    if(atSection != player.GetComponent<Movement>().atSection)
                    {
                        print("teleporting to beginning of new section");
                        posY = player.transform.position.y + (border.GetComponent<Renderer>().bounds.size.y / 2) - (player.GetComponent<Renderer>().bounds.size.x / 2);
                        atSection = player.GetComponent<Movement>().atSection;
                        print(atSection);
                    } else
                    {
                        print("teleporting to beginning of same section");
                        posY = this.transform.position.y;
                    }
                }
                else
                {
                    //if the player has spawned close to the ceiling, set the camera to the top of the ceiling
                    if(ceilings[player.GetComponent<Movement>().atSection - 1].transform.position.y
                        - (ceilings[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.y / 2) 
                        - player.transform.position.y - player.GetComponent<Renderer>().bounds.size.y / 2 
                        < 21.6f - (10.8f - border.GetComponent<Renderer>().bounds.size.y / 2))
                    {
                        posY = ceilings[player.GetComponent<Movement>().atSection - 1].transform.position.y
                        - (ceilings[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.y / 2) - 10.8f;
                    } else //else, spawn normally
                    {
                        posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2 - (player.GetComponent<Renderer>().bounds.size.x / 2);
                    }
                    atSection = player.GetComponent<Movement>().atSection;
                }
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
                //print("it hit right wall");
                rightWall = true;
                collidedX = true;
                lastCollidedX = this.transform.position.x;
            }
            //hits barrier to the left
            else if (coll.gameObject.name.Contains("Left Border"))
            {
                //print("it hit left wall");
                rightWall = false;
                collidedX = true;
                lastCollidedX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x / 2;
            }
            //hits barrier to the top
            if(coll.gameObject.name.Contains("Top Border"))
            {
                //print("it hit ceiling");
                ceil = true;
                collidedY = true;
                lastCollidedY = this.transform.position.y;
            }
            //hits barrier to the bottom
            else
            {
                //print("it hit floor");
                ceil = false;
                collidedY = true;
                lastCollidedY = this.transform.position.y;
            }
        }
    }
}
