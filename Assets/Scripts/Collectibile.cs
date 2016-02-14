using UnityEngine;
using System.Collections;

public class Collectibile : MonoBehaviour {
    public GameObject manager; 

	void OnTriggerEnter2D(Collider2D coll)
    {
        print("collides with collectible");
        manager.GetComponent<GameManager>().collected = true;
        Destroy(this.gameObject);
    }
}
