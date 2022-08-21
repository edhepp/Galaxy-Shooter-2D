using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : MonoBehaviour
{

    private Player _player;

    private void Start()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        _player = player;
        if (_player == null)
        {
            Debug.Log("Player is NULL");
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _player != null && other.GetType() == typeof(BoxCollider2D))
        {
            _player.Damage();
        }
    }
}
