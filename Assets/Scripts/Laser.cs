using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;

    public bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y >= 12)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);
        if (transform.position.y <= -12)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true && other.GetType() == typeof(BoxCollider2D))
        {
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player is NULL");
            }
            else
            {
                player.Damage();
            }
        }
        if (other.tag == "Turret" && other.GetType() == typeof(CircleCollider2D))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }

        if (other.tag == "Powerup_Pickup" && _isEnemyLaser)
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
