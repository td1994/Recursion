using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultMenuControls : MonoBehaviour {
    public string currentLevel;
    public string nextLevel;
    public string mainMenu;

	public void Retry()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
