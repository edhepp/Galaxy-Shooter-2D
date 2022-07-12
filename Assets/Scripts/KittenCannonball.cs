using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenCannonball : MonoBehaviour
{

    private float _kittenSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        KittenMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(other);
        }

    }

    private void KittenMovement()
    {
        transform.Rotate(0, 0, 2f, Space.Self);
        transform.Translate(Vector3.up * _kittenSpeed * Time.deltaTime, Space.World);

        if (transform.position.y > 12)
        {
            Destroy(this.gameObject);
        }
    }
}
