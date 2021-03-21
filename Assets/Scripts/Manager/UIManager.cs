using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text _scoreText;
    [SerializeField] private Image _liveImage;
    [SerializeField] private Sprite[] _liveSprite;
    [SerializeField] private GameObject _gameOverPanel, _menuPanel;

    private Player player;

    private int _finalScore;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0f;

        _finalScore = 0;

        player = GameObject.Find("Player").GetComponent<Player>();

        _scoreText.text = "Score: " + _finalScore; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        if (_finalScore <= player.Score)
        {
            _finalScore = player.Score;
        }

        _scoreText.text = "Score: " + _finalScore;
    }

    public void UpdateLives(int currentLive)
    {
        _liveImage.sprite = _liveSprite[currentLive];
    }

    public void GameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void ResetButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void MenuButton()
    {
        _gameOverPanel.SetActive(false);
        _menuPanel.SetActive(true);
        _scoreText.gameObject.SetActive(false);
        _liveImage.gameObject.SetActive(false);
    }

    public void PlayButton()
    {
        _menuPanel.SetActive(false);
        _scoreText.gameObject.SetActive(true);
        _liveImage.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

} // class
















