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

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-18f, 18f), 11, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explosionAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_explosionAnim == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        StartCoroutine(MovementRoutine());

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
            _explosionAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);

        }

        else if (other.transform.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            _explosionAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);

        }

        else if (other.transform.tag == "Kitten")
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            _explosionAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            transform.Translate(Vector3.right * Time.deltaTime * _enemySpeed);
            yield return new WaitForSeconds(1f);
            transform.Translate(Vector3.left * Time.deltaTime * _enemySpeed);
        } 
    }

}
