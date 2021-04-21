using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    private EnemyMovement movement;

    [SerializeField]private GameObject player;

    [SerializeField] private GameObject bullet;

    private float timeBetweenFire;
    
    [SerializeField]
    private float FireRate;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("BoatModol");
        movement = gameObject.GetComponent<EnemyMovement>();
        bullet.GetComponent<BulletBehaviour>().SetObjectToMoveTo(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (movement == false)
        {
            timeBetweenFire += Time.deltaTime;
            
            if (timeBetweenFire >= FireRate)
            {   bullet.SetActive(true);
                bullet.GetComponent<BulletBehaviour>().SetActive(true);
                
                bullet.transform.position = transform.position;
                timeBetweenFire = 0;
            }
        } 
    }


}
