using UnityEngine;
using System;
using WaveSystem;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Range(2, 50)] private float _bulletSpeed = 2;
    [SerializeField] private Rigidbody _bulletRb;
    [SerializeField] private int _hitBonus;

    
    private GameObject _aim;
    private Transform _target;
    private GameObject _landingPlace;
    
    
    private void Start()
    {
        _landingPlace = GameObject.Find("LandingPlace");
        _aim = GameObject.FindWithTag("BarrelEnd");
        _target = GameObject.FindWithTag("BarrelBegin").transform;
        Vector3 direction = (_aim.transform.position - _target.transform.position).normalized;
        _bulletRb.velocity = (direction * _bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyShip": DeleteBullet();
                //Destroy(other.gameObject);
                PointInput.Instance.AddMultiplier(1);
                WavesManager.Instance.OnEnemyDeath.Invoke(1,other.gameObject);
                break;
            case "Wataa": DeleteBullet();
                PointInput.Instance.ResetMultiplier();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayArea"))
        {
            DeleteBullet();
        }
    }

    private void DeleteBullet()
    {
        _landingPlace.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
