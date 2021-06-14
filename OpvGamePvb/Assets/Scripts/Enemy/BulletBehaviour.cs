using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private bool _isActive = false;
    private Vector3 _objectToMoveTo;
    [SerializeField]private float _speed;
    
    

    void Start()
    {
        
        gameObject.SetActive(false);
        
    }

    
    void Update()
    {
        if (!_isActive)return;
         float step = _speed * Time.deltaTime; 
         transform.position = Vector3.MoveTowards(transform.position, _objectToMoveTo, step);
        if (transform.position == _objectToMoveTo)
        {   PointInput.Instance.ResetMultiplier();

            _isActive = false;
            
            this.gameObject.SetActive(false);
        }
    }

    public void SetActive(bool active)
    {   
        
        _isActive = active;
    }

    public bool GetActive()
    {
        return _isActive;
    }

    public void SetObjectToMoveTo(Vector3 enemyPosition)
    {   
        
        _objectToMoveTo = enemyPosition;
    }
}
