using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeath : MonoBehaviour
{   
    [SerializeField]
    private string _enemyObjectTag;
    [SerializeField]
    private List<GameObject> _inflatables;
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
        if (other.transform.tag == _enemyObjectTag)
        {
            int randomNumber = Random.Range(0, _inflatables.Count);
            Instantiate(_inflatables[randomNumber],transform.position,transform.rotation);
            Debug.Log("hit");
            Destroy(this.gameObject);
        }
    }
}
