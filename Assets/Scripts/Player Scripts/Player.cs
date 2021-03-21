using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    private float _speedMultiplay = 2.0f;
    [SerializeField] private float _fireRate = 0.15f;
    [SerializeField] private int _lives = 3;

    private float _yBounds = 4.6f;
    private float _xBounds = 4f;
    private float _laserOffset = 0.9f;
    private float _canFire = -1.0f;

    [SerializeField] private GameObject _laserPrefab, _laserTripleShotPrefab;
    [SerializeField] private GameObject _playerShield;
    [SerializeField] private GameObject _playerDamageRight, _playerDamageLeft;

    private bool isTripleShotActive, isSpeedBoostActive, isShieldActive;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioManager _audioManager;

    private int _score;

    void Start()
    {
        _score = 0;

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        if(_uiManager == null)
        {
            Debug.LogError("UIManager is NULL");
        }

        if(_audioManager == null)
        {
            Debug.LogError("The Audio Manager is NULL");
        }

        //_uiManager.UpdateLives(_lives);

    }


    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Fire();
        }

    } // Update

    void Fire()
    {
        Vector3 laserPos = new Vector3(0, _laserOffset, 0) + transform.position;

        _canFire = Time.time + _fireRate;

        if (isTripleShotActive)
        {
            Instantiate(_laserTripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, laserPos, Quaternion.identity);
        }

        _audioManager.LaserSound();
        
    } // Fire

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        // use Mathf.Clamp to clamp the value of y between -_yBounds and _yBounds
        Vector3 yValue = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -_yBounds, _yBounds), transform.position.z);
        transform.position = yValue;

        if (transform.position.x >= _xBounds)
        {
            transform.position = new Vector3(-_xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -_xBounds)
        {
            transform.position = new Vector3(_xBounds, transform.position.y, transform.position.z);
        }
    } // PlayerMovement

    public void Damage()
    {
        if (isShieldActive)
        {
            _score += 15;
            return;
        }
        else
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                _audioManager.ExplosionSound();
                Destroy(this.gameObject);
                _uiManager.GameOverPanel();
            }

            if (_lives == 2)
            {
                _playerDamageRight.SetActive(true);
            }
            else if (_lives == 1)
            {
                _playerDamageLeft.SetActive(true);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Powerups powerup = target.transform.GetComponent<Powerups>();

        if (target.gameObject.CompareTag("Powerup"))
        {
            _audioManager.PowerupSound();
            switch (powerup.POWERUP_ID)
            {
                case 0:
                    isTripleShotActive = true;
                    StartCoroutine(PowerupsRoutine(target.gameObject));
                    break;
                case 1:
                    isSpeedBoostActive = true;
                    StartCoroutine(PowerupsRoutine(target.gameObject));
                    break;
                case 2:
                    isShieldActive = true;
                    StartCoroutine(PowerupsRoutine(target.gameObject));
                    break;
                default:
                    Debug.Log("Default value");
                    break;
            }

        }

        if (target.gameObject.CompareTag("EnemyLaser"))
        {
            Destroy(target.gameObject);
            Damage();
        }
    }

   /* IEnumerator TripleShot(GameObject target)
    {
        Destroy(target);
        isTripleShotActive = true;

        yield return new WaitForSeconds(5.0f);

        isTripleShotActive = false;
    }

    IEnumerator SpeedBooster(GameObject target)
    {
        Destroy(target);
        _speed = 8.0f;
        yield return new WaitForSeconds(5.0f);
        _speed = 3.5f;
    }*/

    IEnumerator PowerupsRoutine(GameObject powerup)
    {
        Destroy(powerup);
        if (isTripleShotActive)
        {
            yield return new WaitForSeconds(5.0f);
            isTripleShotActive = false;
        }
        else if (isSpeedBoostActive)
        {
            isSpeedBoostActive = false;
            _speed *= _speedMultiplay;
            yield return new WaitForSeconds(5.0f);
            _speed /= _speedMultiplay;
        } 
        else if (isShieldActive)
        {
            _playerShield.SetActive(true);
            yield return new WaitForSeconds(7.0f);
            _playerShield.SetActive(false);
            isShieldActive = false;
        }
    }


    public int Score { get { return _score; } set { _score = value; } }


} // class


















