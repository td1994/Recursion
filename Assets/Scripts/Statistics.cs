using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {
    public int loops;
    public TextMesh loopCounter;
    public bool levelComplete;
    public int minLoops;

	// Use this for initialization
	void Start () {
        loops = 0;
        levelComplete = false;
	}

    void Update ()
    {
        loopCounter.text = "Loops: " + loops;
    }

    void Display()
    {
        // when the game reaches this point, the player has completed the level
        // this method will display the results screen and save the results
    }
}
