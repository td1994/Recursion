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

            //Control the camera on the X axis
            /*if (collidedX)
            {
                //if the camera is moving away from the left wall
                if (!rightWall && player.transform.position.x > this.transform.position.x)
                {
                    print("Moving away from left wall");
                    posX = player.transform.position.x;
                    collidedX = false;
                } else if (rightWall && player.transform.position.x < lastCollidedX)
                //if the camera is moving away from the right wall
                {
                    print("Moving away from right wall");
                    posX = player.transform.position.x;
                    collidedX = false;
                    rightWall = false;
                    //if the player teleported (aka is repeating the section)
                    if (Math.Abs(posX - lastX) > 20f)
                    {
                        print("teleported");
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
                    print("teleported");
                    teleported = true;
                    collidedX = true;
                    //if the teleporter takes them to the beginning of another section
                    if (player.GetComponent<Movement>().beginning)
                    {
                        posX = player.transform.position.x + initDistX - player.GetComponent<Renderer>().bounds.size.x / 2;     
                    } else
                    {
                        if ((player.transform.position.x - player.GetComponent<Renderer>().bounds.size.x / 2)
                            - (leftWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                            + (leftWalls[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2)) < 19.2f)
                        {
                            print("Camera collided with left wall");
                            posX = leftWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                                + (ceilings[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2) + 19.2f;
                        } else if ((rightWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                            - (rightWalls[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2))
                            - (player.transform.position.x + player.GetComponent<Renderer>().bounds.size.x / 2) < 19.2f)
                        {
                            print("Camera collided with right wall");
                            posX = rightWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                                - (ceilings[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2) - 19.2f;
                        } else
                        {
                            posX = player.transform.position.x;
                        }
                    }
                }
            }*/

            //This controls the placement of the x coordinate
            if(collidedX || Math.Abs(player.transform.position.x - lastX) >= 1f)
            {
                if(player.transform.position.x - (leftWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                        + leftWalls[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2) < initDistX)
                {
                    print("Collided with Left Wall");
                    posX = leftWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                        + leftWalls[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2 + initDistX;
                } else if ((rightWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                        - rightWalls[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2) - player.transform.position.x < initDistX)
                {
                    print("Collided with Right Wall");
                    posX = rightWalls[player.GetComponent<Movement>().atSection - 1].transform.position.x
                        - rightWalls[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.x / 2 - initDistX;
                }
            } else
            {
                print("moving away from walls");
                collidedX = false;
            }

            /*//Controls the camera on the Y axis
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
                //print(player.GetComponent<Movement>().atSection);
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
            }*/

            //This code calculates the y axis of the camera
            //if the camera collided with the floor
            if (Math.Abs(posX - lastX) >= 20f)
            {
                if (atSection != player.GetComponent<Movement>().atSection)
                {
                    posY = player.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2;
                    atSection = player.GetComponent<Movement>().atSection;
                }
            }
            else if (collidedY)
            {
                if (player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y
                    < border.transform.position.y + border.GetComponent<Renderer>().bounds.size.y / 2)
                {
                    //print("Camera is staying where it is, collided");
                    posY = floor[player.GetComponent<Movement>().atSection - 1].transform.position.y + floor[player.GetComponent<Movement>().atSection - 1].GetComponent<Renderer>().bounds.size.y / 2 + initDistY;
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

            print("X: " + posX + " Y: " + posY);
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
