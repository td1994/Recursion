using UnityEngine;
using System.Collections;

public class CameraBarrier : MonoBehaviour {

    private bool rightWall, ceil;
    private float lastCollidedX, lastCollidedY;
    public GameObject player;

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Character") != null)
        {
            float posX = this.transform.position.x, posY = this.transform.position.y;
            if ((rightWall && lastCollidedX != 0 && player.transform.position.x < lastCollidedX)
                || (!rightWall && lastCollidedX != 0 && player.transform.position.x > lastCollidedX))
            {
                print("Moving away from wall");
                lastCollidedX = 0;
            }
            if ((ceil && lastCollidedY != 0 && player.transform.position.y < lastCollidedY - (1.5f * this.GetComponent<BoxCollider2D>().size.y / 4))
                || (!ceil && lastCollidedY != 0 && player.transform.position.y > lastCollidedY + (1.5f * this.GetComponent<BoxCollider2D>().size.y / 4)))
            {
                print("Moving away from ceiling");
                lastCollidedY = 0;
            }

            if (lastCollidedX == 0)
            {
                posX = player.transform.position.x;
            }
            else
            {
                posX = lastCollidedX;
            }
            if (lastCollidedY == 0)
            {
                if (ceil)
                {
                    posY = player.transform.position.y - 1.5f * this.GetComponent<BoxCollider2D>().size.y / 4;
                }
                else
                {
                    posY = player.transform.position.y + 1.5f * this.GetComponent<BoxCollider2D>().size.y / 4;
                }
            }
            else
            {
                posY = lastCollidedY;
            }

            this.transform.position = new Vector3(posX, posY, this.transform.position.z);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "border")
        {
            //hits barrier to the right
            if (coll.transform.position.x > 0)
            {
                print("it hit right wall");
                rightWall = true;
                lastCollidedX = this.transform.position.x;
            }
            //hits barrier to the left
            else
            {
                print("it hit left wall");
                rightWall = false;
                lastCollidedX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x / 2;
            }
            //hits barrier to the top
            if(coll.transform.position.y > 0)
            {
                print("it hit ceiling");
                ceil = true;
                lastCollidedY = this.transform.position.y;
            }
            //hits barrier to the bottom
            else
            {
                print("it hit floor");
                ceil = false;
                lastCollidedY = this.transform.position.y;
            }
        }
    }
}
