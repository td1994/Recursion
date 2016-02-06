using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed;
    public float jump;
    private bool grounded = true;
    // Use this for initialization
    void Start () {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Jump") > 0f && grounded)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, jump);
            grounded = false;
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "floor")
        {
            grounded = true;
        }
        else if (coll.gameObject.tag == "teleporter")
        {
            this.transform.position = new Vector3(1f, this.transform.position.y, this.transform.position.z);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
    }
}
