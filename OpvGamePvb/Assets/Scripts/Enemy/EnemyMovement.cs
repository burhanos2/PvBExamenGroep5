﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using UnityEngine;

using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 position;
    [SerializeField]
    private float speed = 0;
    private Vector3 newPosition;
    [SerializeField]
    private int minimalDivergent = 0;
    [SerializeField]
    private int maximumDivergent = 0;
    [SerializeField]
    private GameObject Player;

    private MeshRenderer playerMesh;
    [SerializeField]
    private float _DistanceHeldToPlayer = 0;

    private bool ismoving = false;
    private float waitingTimer = 0;
    [SerializeField]
    private float WaitingTime = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.Find("BoatModol");
       playerMesh =  Player.GetComponent<MeshRenderer>();
        
        position = transform.position;
        CalculateNewPosition();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position != newPosition &&  ismoving == true)
        {   

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            
        }
        else
        {   
            
            waitingTimer += Time.deltaTime;
            
            ismoving = false;
        }

        if (waitingTimer >= WaitingTime)
        {
            
            waitingTimer = 0;
            CalculateNewPosition();
        }
        
        
    }

    public void CalculateNewPosition()
    {   
        position = transform.position;
        newPosition = new Vector3(Random.Range(minimalDivergent, maximumDivergent),0,Random.Range(minimalDivergent, maximumDivergent));
        
        
        if(Vector3.Distance(newPosition,playerMesh.bounds.center) <= _DistanceHeldToPlayer)
        {
            CalculateNewPosition();
        }
        else
        {   
            
            ismoving = true;
        }



    }

    public bool getMovementStatus()
    {
        return ismoving;
    }

    
}
