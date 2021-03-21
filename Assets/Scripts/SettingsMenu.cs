using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public Animator anim;

    public TMP_Text spaceTip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<Ball>().ThrowTheBall();
            spaceTip.enabled = false;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        anim.SetTrigger("Start");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
