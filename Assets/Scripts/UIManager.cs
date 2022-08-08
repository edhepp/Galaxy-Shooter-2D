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
    private TextMeshProUGUI _ammoText;

    [SerializeField]
    private Slider _thrusterSlider;

    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartLevelText;
    [SerializeField]
    private TextMeshProUGUI _newWaveText;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _ammoText.text = "Ammo: " + 15;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        Player player = GameObject.Find("Player").GetComponent<Player>();
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

    public void UpdateAmmo(int _ammoCount)
    {
        _ammoText.text = "Ammo: " + _ammoCount.ToString() + " / 15";
    }

    public void UpdateThruster(float _thrusterEnergy)
    {
        _thrusterSlider.value = _thrusterEnergy;
    }

    public void UpdateWave(float _currentWave)
    {
        _newWaveText.text = "Wave " + _currentWave;
        StartCoroutine(NewWaveRoutine());
    }

    IEnumerator NewWaveRoutine()
    {
        _newWaveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _newWaveText.gameObject.SetActive(false);
        StopCoroutine(NewWaveRoutine());
    }

}
