using UnityEngine;
using System;

public class NewCameraBarrier2 : MonoBehaviour {

    private float lastCameraPosX;
    private float lastCameraPosY;
    private const float INIT_DIST_X = 19.2f;
    private const float INIT_DIST_Y = 10.8f;
    private GameObject player;
    private GameObject border;
    private GameObject[] enclosedBarriers;

    void Start()
    {
        player = GameObject.Find("Character");
        border = GameObject.Find("Barrier");
        enclosedBarriers = GameObject.FindGameObjectsWithTag("border");
    }

    void Update()
    {
        if (GameObject.Find("Character") != null)
        {
            // get the coordinates of the player
            float posX = player.transform.position.x, posY = this.transform.position.y;
            
            // check if the player has moved outside of the barrier region
            if(player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 2 > this.transform.position.y + border.GetComponent<BoxCollider2D>().bounds.size.y / 2) // top region
            {
                posY = player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 2 - border.GetComponent<BoxCollider2D>().bounds.size.y / 2;
            }
            else if(player.transform.position.y - player.GetComponent<Renderer>().bounds.size.y / 2 < this.transform.position.y - border.GetComponent<BoxCollider2D>().bounds.size.y / 2) // bottom region
            {
                posY = player.transform.position.y - player.GetComponent<Renderer>().bounds.size.y / 2 + border.GetComponent<BoxCollider2D>().bounds.size.y / 2;
            }

            // checks if the camera has collided with an enclosed barrier
        }
    }
}
