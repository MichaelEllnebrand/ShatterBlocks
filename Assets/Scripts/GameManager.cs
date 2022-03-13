using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Text gameOverText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timerText;

    private int score=0;
    private float time;

    public bool IsGameRunning { get; private set; } = false;

    private void Update()
    {
        if (IsGameRunning)
        {
            time = Time.time;
            timerText.text = $"Time: {time}";


        }
    }

    public void StartGame()
    {
        IsGameRunning = true;
        Vector3 startingPostion = new Vector3(0f, 12f, 0f);
        Instantiate(ball, startingPostion, Quaternion.identity);
    }

    public void GameOver(string reasonText)
    {
        IsGameRunning = false;

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            ball.gameObject.SetActive(false);
        }

        gameOverCanvas.SetActive(true);
        gameOverText.text = reasonText;
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;

    }
}
