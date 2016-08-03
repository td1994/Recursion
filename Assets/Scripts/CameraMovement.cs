using UnityEngine;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {

    private float lastCameraPosX;
    private float lastCameraPosY;
    private const float INIT_DIST_X = 19.2f;
    private const float INIT_DIST_Y = 10.8f;
    private GameObject player;
    private GameObject border;
    private GameObject[] enclosedBarriers;
    private float borderSpace;
    private int locationAt = 1;

    void Start()
    {
        player = GameObject.Find("Character");
        border = GameObject.Find("Barrier");
        List<GameObject> barrierList = new List<GameObject>();
        int i = 1;
        barrierList.Add(GameObject.Find("EnclosedBarrier0"));
        while(GameObject.Find("EnclosedBarrier" + i) != null)
        {
            barrierList.Add(GameObject.Find("EnclosedBarrier" + i));
            i++;
        }
        enclosedBarriers = barrierList.ToArray();
        borderSpace = (this.GetComponent<BoxCollider2D>().bounds.size.y - border.GetComponent<BoxCollider2D>().bounds.size.y) / 2;
    }

    void Update()
    {
        if (GameObject.Find("Character") != null)
        {
            // get the coordinates of the player
            float posX = player.transform.position.x, posY = this.transform.position.y;
            PolygonCollider2D collider = enclosedBarriers[player.GetComponent<GameManager>().atSection - 1].GetComponent<PolygonCollider2D>();

            // check if the player has teleported
            if (locationAt != player.GetComponent<GameManager>().atSection)
            {
                locationAt = player.GetComponent<GameManager>().atSection;
                posY = collider.transform.position.y - collider.bounds.size.y / 2 + INIT_DIST_Y;
            }

            // check if the player has moved outside of the barrier region
            if(player.transform.position.y + (player.GetComponent<Renderer>().bounds.size.y / 2) >= posY + (border.GetComponent<BoxCollider2D>().bounds.size.y / 2)) // top region
            {
                Debug.Log("player is moving up");
                posY = (player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 2) - border.GetComponent<BoxCollider2D>().bounds.size.y / 2;
                //Debug.Log(posY);
            }
            else if(player.transform.position.y - player.GetComponent<Renderer>().bounds.size.y / 2 <= posY - border.GetComponent<BoxCollider2D>().bounds.size.y / 2) // bottom region
            {
                Debug.Log("player is moving down");
                posY = player.transform.position.y - player.GetComponent<Renderer>().bounds.size.y / 2 + border.GetComponent<BoxCollider2D>().bounds.size.y / 2;
                //Debug.Log(posY);
            }

            // checks if the camera has collided with an enclosed barrier
            if ((collider.transform.position.x + collider.bounds.size.x / 2) - player.transform.position.x <= INIT_DIST_X)
            {
                // collided with right side of the border
                Debug.Log("camera collided with the right side of the border");
                posX = (collider.transform.position.x + collider.bounds.size.x / 2) - INIT_DIST_X;
            }
            if (player.transform.position.x - (collider.transform.position.x - collider.bounds.size.x / 2) < INIT_DIST_X)
            {
                // collided with left side of the border
                Debug.Log("camera collided with the left side of the border");
                posX = (collider.transform.position.x - collider.bounds.size.x / 2) + INIT_DIST_X;
            }
            if ((collider.transform.position.y + collider.bounds.size.y / 2) - (player.transform.position.y + player.GetComponent<SpriteRenderer>().bounds.size.y / 2) <= borderSpace)
            {
                // collided with the top of the border
                Debug.Log("camera collided with the top side of the border");
                posY = (collider.transform.position.y + collider.bounds.size.y / 2) - INIT_DIST_Y;
            }
            if ((player.transform.position.y - player.GetComponent<SpriteRenderer>().bounds.size.y / 2) - (collider.transform.position.y - collider.bounds.size.y / 2) <= borderSpace)
            {
                // collided with the top of the border
                Debug.Log("camera collided with the bottom side of the border");
                posY = (collider.transform.position.y - collider.bounds.size.y / 2) + INIT_DIST_Y;
            }

            //Debug.Log("X axis: " + posX + " Y axis: " + posY);
            this.transform.position = new Vector3(posX, posY, this.transform.position.z);
        }
    }
}
