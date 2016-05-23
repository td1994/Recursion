using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed;
    public float jump;
    private bool grounded = true;
    private bool dead = false;
    private GameObject background;
    private TextMesh status;
    private GameObject retry;
    private GameObject mainMenu;
    public int atSection = 1;
    public bool beginning = true;

    // Use this for initialization
    void Start () {
        background = GameObject.Find("Background");
        status = GameObject.Find("Level Complete").GetComponent<TextMesh>();
        retry = GameObject.Find("Retry");
        mainMenu = GameObject.Find("Main Menu");
        print(mainMenu != null);
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
