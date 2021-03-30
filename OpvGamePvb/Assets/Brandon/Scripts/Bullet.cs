using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody RB;
    [SerializeField]
    private float BS = 2; //BS = Bullet Speed
    
    private GameObject aim;
    private Transform target;
    
    
    // Start is called before the first frame update
    void Start()
    {
        aim = GameObject.FindWithTag("BarrelEnd");
        target = GameObject.FindWithTag("BarrelBegin").transform;
        Vector3 direction = (aim.transform.position - target.transform.position).normalized;
        RB.velocity = direction * (BS*Time.deltaTime);
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
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
