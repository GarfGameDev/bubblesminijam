using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _highScoreText;
    [SerializeField]
    private Text _currentScoreText;
    [SerializeField]
    private Text _previousScoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _reloadText;
    [SerializeField]
    private Text _mainMenuText;
    private int _totalScore;
    //public static int _previousScore;
    //private int _previousScore;

    private GameManager _gameManager;

    private void Start() 
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateScore(int score) 
    {
        _totalScore += score;
        _scoreText.text = "Score: " + _totalScore;

    }

    

    public void GameOverScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        if (_totalScore > ScoreStats.HighScore)
        {
            ScoreStats.HighScore = _totalScore;
        }

        _highScoreText.text = "High Score: " + ScoreStats.HighScore;

        _currentScoreText.text = "Current Score: " + _totalScore;

        _previousScoreText.text = "Previous Score: " + ScoreStats.PreviousScore;
        
        
        _gameManager.GameOver();
        _scoreText.gameObject.SetActive(false);
        _mainMenuText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        _highScoreText.gameObject.SetActive(true);
        _currentScoreText.gameObject.SetActive(true);
        _previousScoreText.gameObject.SetActive(true);
        _reloadText.gameObject.SetActive(true);

    }

    public void SetPreviousScore()
    {
        ScoreStats.PreviousScore = _totalScore;
    }

}
