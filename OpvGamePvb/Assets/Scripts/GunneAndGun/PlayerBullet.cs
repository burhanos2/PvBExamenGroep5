using UnityEngine;
using System;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _bulletRb;
    [SerializeField, Range(2, 50)]
    private float _bulletSpeed = 2;
    [SerializeField]
    private int HitBonus;

    
    private GameObject _aim;
    private Transform _target;
    private GameObject _landingPlace;
    public Action OnShotMiss;
    public Action<int>OnShotHit;
    
    
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
                Destroy(other.gameObject);
                OnShotHit(HitBonus);
                break;
            case "Wataa": DeleteBullet();
                OnShotMiss();
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
