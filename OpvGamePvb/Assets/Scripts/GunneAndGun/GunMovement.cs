using System;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField] [Range(1, 20)]
    private int _gunRotateSpeed = 4;
    [SerializeField]
    public GameObject gunObject;
    [SerializeField]
    public GameObject barrelObject; // anchorpoint!!!
    [SerializeField]
    private GameObject powerObject;

    private float powerMax = -53;
    private float powerMin = 26;

    //0.712 is ong 90 graden
    private float _maxHorizontal = 0.9f;
    private float _minHorizontal = -0.9f;
    
    private float _maxVertical = 0.05f;
    private float _minVertical = -0.193f;
    

    public Action Shoot;
    
    private void Start()
    {
        Control.OnAttackKeys += InvokeShoot;
        Control.OnHorizontalMoveKeys += TurnHorizontal;
        Control.OnVerticalMoveKeys += TurnVertical;
    }

    private void InvokeShoot()
    {
        Shoot?.Invoke();
    }

    private void TurnHorizontal(bool isRight)
    {
        if(this.enabled == true){
            if(isRight && gunObject.transform.rotation.y <= _maxHorizontal) //right
            {
                gunObject.transform.Rotate(0, yAngle: +_gunRotateSpeed * Time.deltaTime, 0);
            }
            else if (!isRight && gunObject.transform.rotation.y >= _minHorizontal) //left
            {
                gunObject.transform.Rotate(0, yAngle: -_gunRotateSpeed * Time.deltaTime, 0);
            }
            
        }
        
    }

    private void TurnVertical(bool isUp)
    {
        if (this.enabled == true)
        {
            var meterPercentage = Mathf.InverseLerp(_minVertical, _maxVertical, barrelObject.transform.rotation.x);

            if(isUp && barrelObject.transform.rotation.x <= _maxVertical) //up
            {
                barrelObject.transform.Rotate(+_gunRotateSpeed * Time.deltaTime, yAngle: 0, 0);
            }
            else if(!isUp && barrelObject.transform.rotation.x >= _minVertical) //down
            {
                barrelObject.transform.Rotate(-_gunRotateSpeed * Time.deltaTime, yAngle: 0, 0);
            }
        
            powerObject.transform.localPosition = new Vector3(-209f,  Mathf.Lerp(powerMin, powerMax, meterPercentage), 0);
        }
        
    }
}
