using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private string enemyObjectTag;
    private string MeshPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == enemyObjectTag)
        {
            gameObject.GetComponent<MeshFilter>().mesh = Resources.Load(MeshPath) as Mesh;
        }
    }
}
