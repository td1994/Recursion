using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject background;
    public TextMesh status;
    public GameObject[] stars_empty;
    public GameObject[] stars_full;
    public TextMesh[] recNotice;
    public GameObject[] teleporter;
    public int[] minDepth;
    private int depth = 0;
    public bool collected = false;
    public GameObject retry;
    public GameObject mainMenu;
    public GameObject nextLevel;

    void Start()
    {
        background.GetComponent<SpriteRenderer>().enabled = false;
        status.GetComponent<MeshRenderer>().enabled = false;
        for(int i = 0; i < stars_empty.Length; i++)
        {
            stars_empty[i].GetComponent<SpriteRenderer>().enabled = false;
            stars_full[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        retry.SetActive(false);
        mainMenu.SetActive(false);
        nextLevel.SetActive(false);
    }

    void Update()
    {
        recNotice[depth].text = "Loops: " + teleporter[depth].GetComponent<Teleport>().recursionCount;
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
        for (int i = 0; i < teleporter.Length; i++)
        {
            if(teleporter[i].GetComponent<Teleport>().recursionCount > minDepth[i])
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
        for (int i = 0; i < recNotice.Length; i++)
        {
            recNotice[i].GetComponent<MeshRenderer>().enabled = false;
        }
        retry.SetActive(true);
        mainMenu.SetActive(true);
        nextLevel.SetActive(true);
    }
}
