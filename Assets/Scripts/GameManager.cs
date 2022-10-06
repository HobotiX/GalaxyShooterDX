using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    private bool _pauseActive = false;
    [SerializeField]
    private GameObject _pauseMenuPanel;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver) {
            SceneManager.LoadScene(1); // Current game scene
            _isGameOver = false;
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            ReturnMainMenu();
        } else if (Input.GetKeyDown(KeyCode.P)) {
            if (_pauseActive) {
                ResumeGame();
            } else {
                _pauseActive = true;
                _pauseMenuPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        
    }

    public void ResumeGame() 
    {
        _pauseActive = false;
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        if (_pauseActive) {
            _pauseActive = false;
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
