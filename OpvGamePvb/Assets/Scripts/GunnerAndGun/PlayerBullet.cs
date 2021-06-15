using UnityEngine;
using System;
using WaveSystem;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Range(2, 100)] private float _bulletSpeed = 2;
    [SerializeField] private Rigidbody _bulletRb;
    [SerializeField] private int _hitBonus;
    
    private GameObject _barrelEnd;
    private GameObject _barrelBegin;
    private GameObject _landingPlace;
    [SerializeField]
    private GameObject _particle;

    [SerializeField] private string _barrelEndString;
    [SerializeField] private string _barrelBeginString;
    
    private void Start()
    {
        _landingPlace = GameObject.Find("LandingPlace");
        _barrelEnd = GameObject.FindWithTag(_barrelEndString);
        _barrelBegin = GameObject.FindWithTag(_barrelBeginString);
        Instantiate(_particle,_barrelEnd.transform.position,Quaternion.identity);
        Vector3 direction = (_barrelEnd.transform.position - _barrelBegin.transform.position).normalized;
        _bulletRb.velocity = (direction * _bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyShip": 
                DeleteBullet();
                //Destroy ship
                PointInput.Instance.AddMultiplier(1);
                WavesManager.Instance.OnEnemyDeath.Invoke(1,other.gameObject);
                break;
            case "Wataa": 
                DeleteBullet();
                PointInput.Instance.ResetMultiplier();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayArea"))
        {
            DeleteBullet();
            PointInput.Instance.ResetMultiplier();
        }
    }

    private void DeleteBullet()
    {
        _landingPlace.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
