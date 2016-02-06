using UnityEngine;
using System.Collections;

public class ExitDoor : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
        //show a level complete and go to the next level
    }
}
