using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    private bool _enteringScene = false;
    private float _entranceSpeed = 3f;

    private Camera _mainCamera;
    private bool _cameraShakeActive = false;

    [SerializeField]
    private GameObject _bossFightBackground;

    private bool _moveLeft = false;
    private int _movementDirection = 0;
    [SerializeField] private float _movementSpeed = 5f;
    private bool _beginBattleMovement = false;

    private int _laserToFire;
    [SerializeField] private GameObject _innerLaserPrefab;
    [SerializeField] private GameObject _middleLaserPrefab;
    [SerializeField] private GameObject _outerLaserPrefab;
    private float _fireRate = 0.75f;
    private float _canFire = -1.0f;

    [SerializeField] private GameObject _leftLightningPrefab;
    [SerializeField] private GameObject _rightLightningPrefab;
    private float _fireRateLightning = 3.0f;
    private float _canFireLightning = -1.0f;
    private int _lightningToFire;

    [SerializeField] private int _bossHealth = 24;
    [SerializeField] private GameObject _bossHealth75, _bossHealth50, _bossHealth25;
    private bool _isDead = false;
    [SerializeField] private Animator _explosionAnim;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manger is NULL");
        }
        transform.position = new Vector3(0, 18, 0);
        _enteringScene = true;
        _movementDirection = Random.Range(0, 2);
        if (_movementDirection == 1)
        {
            _moveLeft = true;
        }
        else
        {
            _moveLeft = false;
        }
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _mainCamera = camera;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead == false)
        {
            EnteringSceneMovement();
            BossMovement();
            StartCoroutine(FireLaserRoutine());
            StartCoroutine(LightningAttack());
            switch (_bossHealth)
            {
                case 18:
                    _bossHealth75.gameObject.SetActive(true);
                    break;
                case 12:
                    _bossHealth50.gameObject.SetActive(true);
                    break;
                case 6:
                    _bossHealth25.gameObject.SetActive(true);
                    break;
                case 0:
                    Instantiate(_explosionAnim, transform.position, Quaternion.identity);
                    Destroy(this.gameObject, 1.75f);
                    _isDead = true;
                    _uiManager.GameWon();
                    break;
                default:
                    break;
            }
        }
        
    }

    private void EnteringSceneMovement()
    {
        if (_enteringScene == true)
        {
            transform.Translate(Vector3.down * _entranceSpeed * Time.deltaTime);
            if (transform.position.y <= 9)
            {
                _enteringScene = false;
                _cameraShakeActive = true;
                StartCoroutine(CameraShakeRoutine());
            }
        }
    }

    IEnumerator CameraShakeRoutine()
    {
        while (_cameraShakeActive == true)
        {
            float _shakeSpeed = 0.5f;
            WaitForSeconds _waitTime = new WaitForSeconds(0.1f);
            for (int i = 0; i < 9; i++)
            {
                yield return _waitTime;
                _mainCamera.transform.Translate(Vector3.right * _shakeSpeed);
                yield return _waitTime;
                _mainCamera.transform.Translate(Vector3.left * _shakeSpeed);
            }
            _cameraShakeActive = false;
            Instantiate(_bossFightBackground, Vector3.zero, Quaternion.identity);
            _beginBattleMovement = true;
        }
    }

    private void BossMovement()
    {
        if (_moveLeft == true && _beginBattleMovement == true)
        {
            transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);
            if (transform.position.x <= -12f)
            {
                _moveLeft = false;
            }
        }
        if (_moveLeft == false && _beginBattleMovement == true)
        {
            transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
            if (transform.position.x >= 12f)
            {
                _moveLeft = true;
            }
        }
    }

    IEnumerator FireLaserRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while (_beginBattleMovement == true && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            yield return new WaitForSeconds(1.0f);
            FireLaser();
        }
    }

    private void FireLaser()
    {
        Vector3 offset = new Vector3(0, -3.75f, 0);
        _laserToFire = Random.Range(0, 3);
        switch (_laserToFire)
        {
            case 0:
                Instantiate(_innerLaserPrefab, transform.position + offset, Quaternion.identity);
                Laser[] laserInner = _innerLaserPrefab.GetComponentsInChildren<Laser>();
                for (int i = 0; i < laserInner.Length; i++)
                {
                    laserInner[i].AssignEnemyLaser();
                }
                break;
            case 1:
                Instantiate(_middleLaserPrefab, transform.position + offset, Quaternion.identity);
                Laser[] laserMid = _middleLaserPrefab.GetComponentsInChildren<Laser>();
                for (int i = 0; i < laserMid.Length; i++)
                {
                    laserMid[i].AssignEnemyLaser();
                }
                break;
            case 2:
                Instantiate(_outerLaserPrefab, transform.position + offset, Quaternion.identity);
                Laser[] laserOuter = _outerLaserPrefab.GetComponentsInChildren<Laser>();
                for (int i = 0; i < laserOuter.Length; i++)
                {
                    laserOuter[i].AssignEnemyLaser();
                }
                break;
            default:
                break;
        }
    }

    IEnumerator LightningAttack()
    {
        while (_beginBattleMovement == true && Time.time > _canFireLightning)
        {
            _canFireLightning = Time.time + _fireRateLightning;
            _leftLightningPrefab.gameObject.SetActive(false);
            _rightLightningPrefab.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            FireLightning();
        }
    }

    private void FireLightning()
    {
        _lightningToFire = Random.Range(0, 3);
        switch (_lightningToFire)
        {
            case 0:
                _leftLightningPrefab.gameObject.SetActive(true);
                break;
            case 1:
                _rightLightningPrefab.gameObject.SetActive(true);
                break;
            case 2:
                _leftLightningPrefab.gameObject.SetActive(true);
                _rightLightningPrefab.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _bossHealth -= 1;
        }
    }
}
