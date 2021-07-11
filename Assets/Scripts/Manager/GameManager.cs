using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver = false;
    private UIManager _uiManager;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;

        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver == true)
        {
            _uiManager.SetPreviousScore();
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.M) && _gameOver == true) 
        {
            Cursor.lockState = CursorLockMode.None;
            _uiManager.SetPreviousScore();
            SceneManager.LoadScene(0);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}
