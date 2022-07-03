using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-18f, 18f), 11, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(Random.Range(-18f, 18f), 11, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        else if (other.transform.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            Destroy(this.gameObject);
        }
    }

}
