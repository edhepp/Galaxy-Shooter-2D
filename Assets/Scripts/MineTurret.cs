using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTurret : MonoBehaviour
{
    private float _turretSpeed = 6f;
    private int _randomNum;
    private Vector3 _startPosition;
    private bool _turretMovement;

    [SerializeField]
    private GameObject _fireballPrefab;

    private Animator _anim;

    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = new Vector3(0, 10, 0);
        transform.position = _startPosition;
        _anim = GetComponent<Animator>();
        _anim.SetTrigger("OpenTurret");
        _randomNum = Random.Range(0, 2);
        if (_randomNum == 0)
        {
            _turretMovement = false;
        }
        else
        {
            _turretMovement = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_turretMovement == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _turretSpeed);
            if (transform.position.x <= -19)
            {
                _turretMovement = true;
            }
        }
        if (_turretMovement == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _turretSpeed);
            if (transform.position.x >= 19)
            {
                _turretMovement = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetType() == typeof(BoxCollider2D))
        {
            _offset = new Vector3(0, 2, 0);
            Instantiate(_fireballPrefab, transform.position - _offset, Quaternion.identity);
        }
    }
}
