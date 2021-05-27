using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    private EnemyMovement movement;

    [SerializeField]private GameObject player;

    [SerializeField] private GameObject bullet;

    private float timeBetweenFire;
    private BulletBehaviour _bBehaviour;
    private MeshRenderer _renderer;
    
    [SerializeField]
    private float FireRate;

    [SerializeField] 
    private float _minimalDistanceToEnemy;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("BoatModol");
        _renderer = player.GetComponent<MeshRenderer>();
        movement = gameObject.GetComponent<EnemyMovement>();
        _bBehaviour = bullet.GetComponent<BulletBehaviour>();
        _bBehaviour.SetObjectToMoveTo(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.getMovementStatus() == false && Vector3.Distance(transform.position,_renderer.bounds.center) < _minimalDistanceToEnemy)
        {
           
            timeBetweenFire += Time.deltaTime;
            
            if (timeBetweenFire >= FireRate)
            {   if(_bBehaviour.GetActive() != true){
                    bullet.SetActive(true);
                    bullet.GetComponent<BulletBehaviour>().SetActive(true);
                
                    bullet.transform.position = transform.position;
                    timeBetweenFire = 0;
                }
                
            }
        } 
    }


}
