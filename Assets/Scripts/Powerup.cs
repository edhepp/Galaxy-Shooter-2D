using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 3f;

    private float _collectSpeed = 10f;

    [SerializeField]
    private int _powerupID;

    private float _distance;

    Rigidbody move;

    [SerializeField]
    private AudioClip _clip;

    private bool _moveTowardsPlayer = false;

    Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetKey(KeyCode.C) && _moveTowardsPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _collectSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetType() == typeof(BoxCollider2D))
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.AmmoPickup();
                        break;
                    case 4:
                        player.HealthPack();
                        break;
                    case 5:
                        player.KittenCannonballActive();
                        break;
                    case 6:
                        player.SlowdownEffect();
                        break;
                    case 7:
                        player.HomingMissileIsActive();
                        break;
                    default:
                        Debug.Log("Default case triggered");
                        break;
                }
            }
            Destroy(this.gameObject);
        }

        if (other.transform.tag == "Player" && other.GetType() == typeof(CircleCollider2D))
        {
            _moveTowardsPlayer = true;
        }
    }
}
