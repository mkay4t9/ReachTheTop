using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiController : MonoBehaviour
{

    public GameObject resumePannel, gameOverPannel, gameFinishedPannel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameOver)
        {
            gameOverPannel.SetActive(true);
        }
        if(PlayerController.gameCompleted)
        {
            gameFinishedPannel.SetActive(true);
            gameOverPannel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        // gameOverPannel.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        resumePannel.SetActive(true);
        
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        resumePannel.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
