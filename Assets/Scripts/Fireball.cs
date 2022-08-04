using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float _fireballSpeed = 7;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _fireballSpeed);
        if (transform.position.y <= -12)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetType() == typeof(BoxCollider2D))
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
            Destroy(this.gameObject);
        }
    }
}
