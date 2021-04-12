using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 position;
    [SerializeField]
    private float speed;
    private Vector3 newPosition;
    [SerializeField]
    private int minimalDivergent = 0;
    [SerializeField]
    private int maximumDivergent = 0;
    [SerializeField]
    private GameObject Player;

    [SerializeField] private GameObject targetPrefab;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        CalculateNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateNewPosition()
    {   
        
        newPosition = new Vector3(Random.Range(minimalDivergent, maximumDivergent),0,Random.Range(minimalDivergent, maximumDivergent));
        GameObject target = Instantiate(targetPrefab,  newPosition,  transform.rotation, gameObject.transform) as GameObject;
        if((target.transform.position.x <= Player.GetComponent<MeshRenderer>().bounds.size.x /2f )&&(target.transform.position.x >= Player.GetComponent<MeshRenderer>().bounds.size.x *2f) && ((target.transform.position.z <= Player.GetComponent<MeshRenderer>().bounds.size.z / 2f)&&(target.transform.position.x >= Player.GetComponent<MeshRenderer>().bounds.size.x *2f)))
        {
            CalculateNewPosition();
        }
        
    }

    public void MoveTowardsNewPosition()
    {
        position = Vector3.MoveTowards(position,gameObject.GetComponentInChildren<Transform>().transform.position, speed);
        
    }
}
