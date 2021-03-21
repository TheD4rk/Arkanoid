using System.Collections;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int currentLevel;
    private int brickCount;
    public GameObject[] levels;
    private Ball ball;

    public GameObject winScreen;
    public TMP_Text finalScore;

    private void Start()
    {
        currentLevel = 1;
        brickCount = 9;
        ball = FindObjectOfType<Ball>();
    }

    public void DecrementBricks()
    {
        brickCount--;
        if (brickCount <= 0)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        if (levels == null)
        {
            return;
        }
        currentLevel++;
        if (currentLevel <= 5)
        {
            ball.ResetPosition();
            StartCoroutine(WaitForNextLevel());
        }
        else
        {
            WinningScreen();
            Time.timeScale = 0f;
        }
    }

    private IEnumerator WaitForNextLevel()
    {
        yield return new WaitForSeconds(0.1f);
        levels[currentLevel - 2].SetActive(false);
        levels[currentLevel - 1].SetActive(true);
        brickCount = levels[currentLevel - 1].transform.childCount;
    }

    private void WinningScreen()
    {
        winScreen.SetActive(true);
        finalScore.text = FindObjectOfType<Ball>().scoreText.text;
    }
    
}
