using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;

    private Animator _explosionAnim;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _fireRate = 3.0f;

    private AudioSource _audioSource;

    private float _canFire = -1;

    int _randomMovement = 0;
    Vector3 startPosition;

    [SerializeField]
    private GameObject _shieldSprite;
    private bool _isShieldActive = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(Random.Range(-18f, 18f), 11, 0);
        transform.position = startPosition;
        _randomMovement = Random.Range(0, 2);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explosionAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        EnemyShieldActive();

        if (_explosionAnim == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if (_randomMovement == 0 && startPosition.x < -1)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _enemySpeed);
        }
        else if (_randomMovement == 1 && startPosition.x > 1)
        {
            transform.Translate(Vector3.left * Time.deltaTime * _enemySpeed);
        }

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(Random.Range(-18f, 18f), 11, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            if (_isShieldActive == false)
            {
                _explosionAnim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _audioSource.Play();
                _shieldSprite.gameObject.SetActive(false);
                Destroy(this.gameObject, 2.8f);
            }
            else
            {
                _isShieldActive = false;
                _shieldSprite.gameObject.SetActive(false);
            }

        }

        else if (other.transform.tag == "Laser")
        {
            if (_isShieldActive == false)
            {
                Destroy(other.gameObject);
                _player.AddScore(10);
                _explosionAnim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _audioSource.Play();
                _shieldSprite.gameObject.SetActive(false);
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.8f);
            }
            else
            {
                _isShieldActive = false;
                Destroy(other.gameObject);
                _shieldSprite.gameObject.SetActive(false);
            }

        }

        else if (other.transform.tag == "Kitten")
        {
            if (_isShieldActive == false)
            {
                Destroy(other.gameObject);
                _player.AddScore(10);
                _explosionAnim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _audioSource.Play();
                _shieldSprite.gameObject.SetActive(false);
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.8f);
            }
            else
            {
                _isShieldActive = false;
                Destroy(other.gameObject);
                _shieldSprite.gameObject.SetActive(false);
            }
        }
    }

    public void EnemyShieldActive()
    {
        int _randomShield = Random.Range(0, 4);
        if (_randomShield == 2)
        {
            _isShieldActive = true;
            _shieldSprite.gameObject.SetActive(true);
        }
        
    }
}
