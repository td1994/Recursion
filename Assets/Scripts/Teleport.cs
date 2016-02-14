using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public GameObject target;
    public int recursionCount = 1;

    void OnTriggerEnter2D(Collider2D coll)
    {
        print(recursionCount);
        if (coll.gameObject.tag == "Player")
        {
            recursionCount++;
            coll.transform.position = new Vector3(1f, coll.transform.position.y, 
                coll.transform.position.z);
        }
        
    }
}
