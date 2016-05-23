using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public GameObject target;
    public int recursionCount = 1;
    public int xPos = 0;
    public int toSection;
    public bool beginning;

    void OnTriggerEnter2D(Collider2D coll)
    {
        //print(recursionCount);
        if (coll.gameObject.tag == "Player")
        {
            if(target != null)
            {
                coll.transform.position = new Vector3(target.transform.position.x, target.transform.position.y,
                    coll.transform.position.z);
                coll.GetComponent<GameManager>().atSection = toSection;
                coll.GetComponent<GameManager>().beginning = beginning;
            } else
            {
                recursionCount++;
                coll.transform.position = new Vector3(xPos, coll.transform.position.y,
                    coll.transform.position.z);
                coll.GetComponent<GameManager>().beginning = true;
            }
        }
        
    }
}
