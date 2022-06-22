using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 5.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.15f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
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

        transform.Translate(Vector3.right * Time.deltaTime * _playerSpeed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * _playerSpeed * verticalInput);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }

        if (transform.position.x > 10.5f)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -10.5f)
        {
            transform.position = new Vector3(10.5f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Vector3 offset = new Vector3(0, 0.8f, 0);
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives -= 1;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
