using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed = 8.0f;

    private float _yBounds = 6.0f;

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y >= _yBounds)
        {
            Destroy(gameObject);
        }

    }




} // class



























