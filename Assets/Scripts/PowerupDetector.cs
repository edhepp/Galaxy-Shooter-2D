using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDetector : MonoBehaviour
{

    private bool _powerupDetected = false;

    [SerializeField]
    private Enemy _enemy;

    // Update is called once per frame
    void Update()
    {
        PowerupDetected();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Powerup_Pickup")
        {
            _powerupDetected = true;
        }
    }

    private void PowerupDetected()
    {
        if (_powerupDetected == true)
        {
            _enemy.FireAtPowerup();
            _powerupDetected = false;
        }
    }
}
