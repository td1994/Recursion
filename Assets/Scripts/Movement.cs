using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed;
    public float jump;
    private bool grounded = true;
    private bool dead = false;
    public GameObject background;
    public TextMesh status;
    public GameObject retry;
    public GameObject mainMenu;

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

        if(this.GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            dead = true;
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "floor")
        {
            grounded = true;
        }
    }

    void OnDestroy()
    {
        if(dead)
        {
            background.GetComponent<SpriteRenderer>().enabled = true;
            status.text = "Level Failed!";
            status.GetComponent<MeshRenderer>().enabled = true;
            retry.SetActive(true);
            mainMenu.SetActive(true);
        }
    }
}
