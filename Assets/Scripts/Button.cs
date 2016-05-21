using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {
    public bool triggered = false;
    private GameObject character;

    void Start ()
    {
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Character") != null)
        {
            if (Math.Abs(character.transform.position.x - this.transform.position.x) <= 5f
                && Math.Abs(character.transform.position.y - this.transform.position.y) <= 5f)
            {
                if (!triggered)
                {
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (Input.GetAxis("Use") > 0f)
                {
                    triggered = true;
                }
            }
            else if (!triggered)
            {
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
