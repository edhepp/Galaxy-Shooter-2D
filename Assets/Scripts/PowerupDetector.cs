using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDetector : MonoBehaviour
{

    private bool _powerupDetected = false;

    [SerializeField]
    private Enemy _enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

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
            Debug.Log("Powerup detected");
            _enemy.FireAtPowerup();
            _powerupDetected = false;
        }
    }
}
