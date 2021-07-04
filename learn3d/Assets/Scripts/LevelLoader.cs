using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
  
    int currentsceneindex;
    // Start is called before the first frame update
    void Start()
    {
        currentsceneindex = SceneManager.GetActiveScene().buildIndex;
       
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentsceneindex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentsceneindex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
