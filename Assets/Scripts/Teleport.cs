using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public GameObject target;
    public int recursionCount = 1;
    private float xPos;
    public int toSection;
    public bool beginning;

    void Start()
    {
        int i = toSection - 1;
        PolygonCollider2D startAreaRef = GameObject.Find("EnclosedBarrier" + i).GetComponent<PolygonCollider2D>();
        GameObject player = GameObject.Find("Character");
        xPos = startAreaRef.transform.position.x - (startAreaRef.bounds.size.x / 2) + player.GetComponent<BoxCollider2D>().bounds.size.x / 2;
    }

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
