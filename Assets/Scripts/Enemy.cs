using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8f, 9f), 6, 0);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);

        if (transform.position.y < -5.5)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 6, 0);
        }
    }
}
