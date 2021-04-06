using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody bulletRB;
    [SerializeField, Range(2, 50)]
    private float bulletSpeed = 2;
    
    private GameObject aim;
    private Transform target;
    private GameObject landingPlace;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        landingPlace = GameObject.Find("LandingPlace");
        aim = GameObject.FindWithTag("BarrelEnd");
        target = GameObject.FindWithTag("BarrelBegin").transform;
        Vector3 direction = (aim.transform.position - target.transform.position).normalized;
        bulletRB.velocity = (direction * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyShip"))
        {
            DeleteBullet();
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
        landingPlace.transform.position = gameObject.transform.position;
        Destroy(this.gameObject);
    }
}
