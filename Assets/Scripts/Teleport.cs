using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public GameObject target;

    void onCollisionStay2D(Collision coll)
    {
        print("It Works!");
        if(coll.gameObject.tag == "teleporter")
        {
            this.transform.position = target.transform.position;
        }
    }
}
