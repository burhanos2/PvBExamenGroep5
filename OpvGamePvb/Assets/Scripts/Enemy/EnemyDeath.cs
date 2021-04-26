using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeath : MonoBehaviour
{   
    [SerializeField]
    private string enemyObjectTag;
    [SerializeField]
    private List<GameObject> inflatables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.transform.tag == enemyObjectTag)
        {
            int randomNumber = Random.Range(0, inflatables.Count);
            Instantiate(inflatables[randomNumber],transform.position,transform.rotation);
            Debug.Log("hit");
            Destroy(this.gameObject);
        }
    }
}
