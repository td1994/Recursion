using UnityEngine;
using System;

public class CameraBarrier : MonoBehaviour {

    private bool rightWall, ceil;
    private float lastCollidedX, lastCollidedY;
    public GameObject player;
    public GameObject border;

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Character") != null)
        {
            float posX = this.transform.position.x, posY = this.transform.position.y;
            if ((rightWall && lastCollidedX != 0 && player.transform.position.x < lastCollidedX)
                || (!rightWall && lastCollidedX != 0 && player.transform.position.x > lastCollidedX))
            {
                print("Moving away from wall");
                lastCollidedX = Int32.MinValue;
            }
            if ((ceil && lastCollidedY != Int32.MinValue && player.transform.position.y < lastCollidedY - (border.transform.position.y + border.GetComponent<BoxCollider2D>().size.y / 2))
                || (!ceil && lastCollidedY != Int32.MinValue && player.transform.position.y > lastCollidedY + (border.transform.position.y + border.GetComponent<BoxCollider2D>().size.y / 2)))
            {
                print("Moving away from ceiling");
                lastCollidedY = Int32.MinValue;
            }

            if (lastCollidedX == Int32.MinValue)
            {
                posX = player.transform.position.x;
            }
            else
            {
                posX = lastCollidedX;
            }
            if (lastCollidedY == Int32.MinValue
                || lastCollidedY < player.transform.position.y - 1.5f * this.GetComponent<BoxCollider2D>().size.y / 4
                || lastCollidedY > player.transform.position.y + 1.5f * this.GetComponent<BoxCollider2D>().size.y / 4)
            {
                if (ceil)
                {
                    print("Moving Up");
                    posY = player.transform.position.y + border.GetComponent<BoxCollider2D>().size.y / 2;
                }
                else
                {
                    print("Moving Down");
                    posY = player.transform.position.y - border.GetComponent<BoxCollider2D>().size.y / 2;
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
            if (coll.gameObject.name.Contains("Right Border"))
            {
                print("it hit right wall");
                rightWall = true;
                lastCollidedX = this.transform.position.x;
            }
            //hits barrier to the left
            else if (coll.gameObject.name.Contains("Left Border"))
            {
                print("it hit left wall");
                rightWall = false;
                lastCollidedX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x / 2;
            }
            //hits barrier to the top
            if(coll.gameObject.name.Contains("Top Border"))
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
