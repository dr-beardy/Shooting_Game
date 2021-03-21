using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotationSpeed = 7.0f;

    [SerializeField] private GameObject _explotion;

    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
            Debug.LogError("The Spawn Manager is NULL!!");
    }

    void Update()
    {
        transform.Rotate(Vector3.back * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Laser"))
        {
            Instantiate(_explotion, transform.position, Quaternion.identity);
            Destroy(target.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.5f);
        }
    }


} // class






























