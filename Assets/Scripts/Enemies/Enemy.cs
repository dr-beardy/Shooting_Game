using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private GameObject _laserShotPrefab;

    private float _xRange = 2.5f;
    private float _ySpawnPos = 6.5f;

    public int _scoreToAdd = 10;

    Player player;

    private Animator _anim;

    private AudioManager _audioManager;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();

        if(player == null)
        {
            Debug.LogError("Player is NULL!");
        }

        if(_audioManager == null)
        {
            Debug.LogError("The Audio Manager is NULL!!");
        }

        _anim = GetComponent<Animator>();

        StartCoroutine(EnemyFire());

    }

    void Update()
    {
        EnemyMoveDown();
        if(transform.position.y <= -_ySpawnPos)
        {
            RespawnEnemy();
        }
    } // Update


    IEnumerator EnemyFire()
    {
        float randomShot = Random.Range(3f, 7f);
        Vector3 laserShotPos = new Vector3(0, -2.45f, 0) + transform.position;
        Instantiate(_laserShotPrefab, laserShotPos, Quaternion.identity);
        yield return new WaitForSeconds(randomShot);

    }


    void EnemyMoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    } // EnemyMoveDown

    void RespawnEnemy()
    {
        float new_x_position = Random.Range(-_xRange, _xRange);
        transform.position = new Vector3(new_x_position, _ySpawnPos, transform.position.z);
    } // RespawnEnemy

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            if(player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            StartCoroutine(WaitToDestroy());
        }

        if (target.gameObject.CompareTag("Laser"))
        {
            player.Score += _scoreToAdd;
            Destroy(target.gameObject);
            _speed = 0f;
            _anim.SetTrigger("OnEnemyDeath");
            StartCoroutine(WaitToDestroy());
        }
    } // OnTriggerEnter

    IEnumerator WaitToDestroy()
    {
        _audioManager.ExplosionSound();
        Destroy(GetComponent<Collider2D>());
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

} // class




























