using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 5.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _speedBoostActive = false;
    [SerializeField]
    private bool _shieldsActive = false;
    [SerializeField]
    private GameObject _shieldSprite;
    [SerializeField]
    private int _score;

    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manger is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        FireLaser();   
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (_speedBoostActive == true)
        {
            StartCoroutine(SpeedBoostPowerDownRoutine());
            _playerSpeed = 15f;
            transform.Translate(Vector3.right * Time.deltaTime * _playerSpeed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _playerSpeed * verticalInput);
        }

        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _playerSpeed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _playerSpeed * verticalInput);
        }

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (transform.position.y <= -9.5f)
        {
            transform.position = new Vector3(transform.position.x, -9.5f, transform.position.z);
        }

        if (transform.position.x > 20f)
        {
            transform.position = new Vector3(-20f, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -20f)
        {
            transform.position = new Vector3(20f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            if (_tripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }

            else
            {
                Vector3 offset = new Vector3(0, 1.05f, 0);
                Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
            }
            
        }
    }

    public void Damage()
    {
        if (_shieldsActive == true)
        {
            _shieldsActive = false;
            _shieldSprite.gameObject.SetActive(false);
            return;
        }

        _lives -= 1;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uiManager.GameOver();
        }
    }

    public void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
        _playerSpeed = 5.5f;
    }

    public void ShieldActive()
    {
        _shieldsActive = true;
        _shieldSprite.gameObject.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
