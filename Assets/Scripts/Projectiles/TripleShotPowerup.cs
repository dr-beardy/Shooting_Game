using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{

    private float _speed = 3.0f;
    private float _boundsY = -8.0f;


    void Update()
    {
        MoveDown();
        DestroyGOBJ();
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void DestroyGOBJ()
    {
        if(transform.position.y <= _boundsY)
        {
            Destroy(gameObject);
        }
    }


} // class































