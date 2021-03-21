using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _laserSound, _explosionSound, _powerupSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("The Audio Source is NULL!!");
        }
    }


    public void LaserSound()
    {
        _audioSource.PlayOneShot(_laserSound);
    }

    public void ExplosionSound()
    {
        _audioSource.PlayOneShot(_explosionSound);
    }

    public void PowerupSound()
    {
        _audioSource.PlayOneShot(_powerupSound);
    }


} // class




































