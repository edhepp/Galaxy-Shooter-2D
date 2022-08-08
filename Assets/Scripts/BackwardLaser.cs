using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardLaser : MonoBehaviour
{
    private float _laserSpeed = 8f;

    // Update is called once per frame
    void Update()
    {
        MoveUp();
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
                Destroy(this.gameObject);
                player.Damage();
            }
        }
    }
}
