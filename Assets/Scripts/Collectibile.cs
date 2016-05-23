using UnityEngine;
using System.Collections;

public class Collectibile : MonoBehaviour {
    private GameManager manager; 

    void Start()
    {
        manager = GameObject.Find("Character").GetComponent<GameManager>();
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        print("collides with collectible");
        manager.collected = true;
        Destroy(this.gameObject);
    }
}
