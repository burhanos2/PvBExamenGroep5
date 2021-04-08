﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    private EnemyMovement movement;

    [SerializeField]private GameObject player;

    [SerializeField] private GameObject bullet;

    private float timeBetweenFire;
    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<EnemyMovement>();
        bullet.GetComponent<BulletBehaviour>().SetObjectToMoveTo(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (movement == false)
        {
            timeBetweenFire += Time.deltaTime;
            if (timeBetweenFire >= 10)
            {
                bullet.GetComponent<BulletBehaviour>().SetActive(true);
                
                bullet.transform.position = transform.position;
                timeBetweenFire = 0;
            }
        } 
    }


}