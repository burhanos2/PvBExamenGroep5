using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    private EnemyMovement _movement;

    [SerializeField]private GameObject _player;

    [SerializeField] private GameObject _bullet;

    private float _timeBetweenFire;
    private BulletBehaviour _bBehaviour;
    private MeshRenderer _renderer;
    
    [SerializeField]
    private float _fireRate;

    [SerializeField] 
    private float _minimalDistanceToEnemy;

    private void Start()
    {   
        _player = GameObject.Find("BoatModol");
        _renderer = _player.GetComponent<MeshRenderer>();
        _movement = gameObject.GetComponent<EnemyMovement>();
        _bBehaviour = _bullet.GetComponent<BulletBehaviour>();
        _bBehaviour.SetObjectToMoveTo(_player.transform.position);
    }


    private void Update()
    {   
        //Debug.Log("hit1");
        if (_movement.getMovementStatus() == false && Vector3.Distance(transform.position,_renderer.bounds.center) < _minimalDistanceToEnemy)
        {
            
            _timeBetweenFire += Time.deltaTime;
            
            if (_timeBetweenFire >= _fireRate)
     
            {   if(_bBehaviour.GetActive() != true){
                    _bullet.transform.position = transform.position;
  
                    _bullet.SetActive(true);
                    _bullet.GetComponent<BulletBehaviour>().SetActive(true);
                    
                    
                    _timeBetweenFire = 0;
                }
                
            }
        } 
    }


}
