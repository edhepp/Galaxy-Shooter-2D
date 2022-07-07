using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartLevelText;
    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverFlickerRoutine());
        _restartLevelText.gameObject.SetActive(true);
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
        _gameManager.GameOverSequence();
    }

    private WaitForSeconds _flicker = new WaitForSeconds(0.3f);

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {

            _gameOverText.gameObject.SetActive(true);
            yield return _flicker;
            _gameOverText.gameObject.SetActive(false);
            yield return _flicker;

        }
    }


}
