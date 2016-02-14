using UnityEngine;
using System.Collections;

public class ResultMenuControls : MonoBehaviour {

	public void Retry()
    {
        Application.LoadLevel("TestLevel");
    }

    public void ReturnToMenu()
    {
        Application.LoadLevel("TestLevel");
    }

    public void NextLevel()
    {
        Application.LoadLevel("TestLevel");
    }
}
