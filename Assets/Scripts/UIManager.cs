using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _healthImage;
    [SerializeField]
    private Sprite[] _healthSprite;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null) {
            Debug.LogError("ERROR: Game Manager is Null!");
        }
    }

    public void UpdateScore(int score) {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateHealth(int currentHealth)
    {
        _healthImage.sprite = _healthSprite[currentHealth];
        if (currentHealth == 0) {
            GameOverSequence();
        }
    }

    IEnumerator GameOverFlicker()
    {
        while(true) {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.1f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.1f);
        }
    }

    private void GameOverSequence() 
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _gameManager.GameOver();
    }

    public void ResumePlay() 
    {
        _gameManager.ResumeGame();
    }
    public void ToMainMenu() 
    {
        _gameManager.ReturnMainMenu();
    }
}
