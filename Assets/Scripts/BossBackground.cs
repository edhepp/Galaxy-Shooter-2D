using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBackground : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }
}
