using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private GameObject background;
    private TextMesh status;
    private GameObject[] stars_empty;
    private GameObject[] stars_full;
    private TextMesh recNotice;
    public GameObject[] repeatingTeleporters;
    public int[] minDepth;
    private GameObject retry;
    private GameObject mainMenu;
    private GameObject nextLevel;
    private GameObject player;
    private GameObject collectible;

    public float speed;
    public float jump;
    private bool grounded = true;
    private bool dead = false;
    public int atSection = 1;
    public bool beginning = true;
    public bool collected = false;

    void Start()
    {
        player = GameObject.Find("Character");
        background = GameObject.Find("Background");
        status = GameObject.Find("Level Complete").GetComponent<TextMesh>();
        stars_empty = new GameObject[3];
        stars_empty[0] = GameObject.Find("Star1");
        stars_empty[1] = GameObject.Find("Star2");
        stars_empty[2] = GameObject.Find("Star3");
        stars_full = new GameObject[3];
        stars_full[0] = GameObject.Find("StarColored1");
        stars_full[1] = GameObject.Find("StarColored2");
        stars_full[2] = GameObject.Find("StarColored3");
        recNotice = GameObject.Find("Loops").GetComponent<TextMesh>();
        background.GetComponent<SpriteRenderer>().enabled = false;
        status.GetComponent<MeshRenderer>().enabled = false;
        retry = GameObject.Find("Retry");
        mainMenu = GameObject.Find("Main Menu");
        nextLevel = GameObject.Find("Next Level");
        collectible = GameObject.Find("Collectible");

        for (int i = 0; i < stars_empty.Length; i++)
        {
            stars_empty[i].GetComponent<SpriteRenderer>().enabled = false;
            //print(stars_empty[i].GetComponent<SpriteRenderer>().enabled);
            stars_full[i].GetComponent<SpriteRenderer>().enabled = false;
            //print(stars_full[i].GetComponent<SpriteRenderer>().enabled);
        }
        retry.SetActive(false);
        mainMenu.SetActive(false);
        nextLevel.SetActive(false);

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    void Update()
    {
        if (repeatingTeleporters.GetLength(0) > 0)
        {
            recNotice.text = "Section " + atSection + " Loops: " + repeatingTeleporters[atSection - 1].GetComponent<Teleport>().recursionCount;
        }
        if (Input.GetAxis("Jump") > 0f && grounded)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, jump);
            grounded = false;
        }

        if (this.GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            dead = true;
            Destroy(this.gameObject);
        }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "floor")
        {
            grounded = true;
        } else if(coll.gameObject.tag == "door")
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        if (dead)
        {
            background.GetComponent<SpriteRenderer>().enabled = true;
            status.text = "Level Failed!";
            status.GetComponent<MeshRenderer>().enabled = true;
            retry.SetActive(true);
            mainMenu.SetActive(true);
        } else
        {
            background.GetComponent<SpriteRenderer>().enabled = true;
            status.GetComponent<MeshRenderer>().enabled = true;
            /*for (int i = 0; i < stars_empty.Length; i++)
            {
                stars_empty[i].GetComponent<SpriteRenderer>().enabled = true;
                stars_full[i].GetComponent<SpriteRenderer>().enabled = true;
            }*/
            stars_full[0].GetComponent<SpriteRenderer>().enabled = true;
            for (int i = 0; i < repeatingTeleporters.Length; i++)
            {
                if (repeatingTeleporters[i].GetComponent<Teleport>().recursionCount > minDepth[i])
                {
                    stars_empty[1].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            if (!stars_empty[1].GetComponent<SpriteRenderer>().enabled)
            {
                stars_full[1].GetComponent<SpriteRenderer>().enabled = true;
            }

            if (collected)
            {
                stars_full[2].GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                stars_empty[2].GetComponent<SpriteRenderer>().enabled = true;
            }
            recNotice.GetComponent<MeshRenderer>().enabled = false;
            retry.SetActive(true);
            mainMenu.SetActive(true);
            nextLevel.SetActive(true);
        }
    }
}
