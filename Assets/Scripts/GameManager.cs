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
    private int depth = 1;
    public bool collected = false;
    private GameObject retry;
    private GameObject mainMenu;
    private GameObject nextLevel;
    public GameObject player;

    void Start()
    {
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

        for (int i = 0; i < stars_empty.Length; i++)
        {
            stars_empty[i].GetComponent<SpriteRenderer>().enabled = false;
            print(stars_empty[i].GetComponent<SpriteRenderer>().enabled);
            stars_full[i].GetComponent<SpriteRenderer>().enabled = false;
            print(stars_full[i].GetComponent<SpriteRenderer>().enabled);
        }
        retry.SetActive(false);
        mainMenu.SetActive(false);
        nextLevel.SetActive(false);
    }

    void Update()
    {
        if(player != null) 
            depth = player.GetComponent<Movement>().atSection;
        recNotice.text = "Section " + depth + " Loops: " + repeatingTeleporters[depth - 1].GetComponent<Teleport>().recursionCount;
        //Note: We need to come up with a way to set up adding teleporters in order to count for loops of other areas
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
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
        if(!stars_empty[1].GetComponent<SpriteRenderer>().enabled)
        {
            stars_full[1].GetComponent<SpriteRenderer>().enabled = true;
        }

        if(collected)
        {
            stars_full[2].GetComponent<SpriteRenderer>().enabled = true;
        } else
        {
            stars_empty[2].GetComponent<SpriteRenderer>().enabled = true;
        }
        recNotice.GetComponent<MeshRenderer>().enabled = false;
        retry.SetActive(true);
        mainMenu.SetActive(true);
        nextLevel.SetActive(true);
    }
}
