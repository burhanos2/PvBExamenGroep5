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

    private float powerMax = 113;
    private float powerMin = -113;

    private float _maxHorizontal = 0.712f;
    private float _minHorizontal = -0.712f;
    
    private float _maxVertical = 0.6f;
    private float _minVertical = 0.1f;
    

    public Action Shoot;
    
    private void Update()
    {
        TurnHorizontal();
        
        TurnVertical();
        
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Mouse2))
        {
            Shoot?.Invoke();
        }
    }

    private void TurnHorizontal()
    {
        if (Input.GetKey(KeyCode.RightArrow) && gunObject.transform.rotation.y <= _maxHorizontal || Input.GetKey(KeyCode.Mouse1))
        {
            gunObject.transform.Rotate(0 ,yAngle: + _gunRotateSpeed*Time.deltaTime ,0);
        } 
        else if (Input.GetKey(KeyCode.LeftArrow) && gunObject.transform.rotation.y >= _minHorizontal || Input.GetKey(KeyCode.Mouse0))
        {
            gunObject.transform.Rotate(0 ,yAngle: - _gunRotateSpeed*Time.deltaTime ,0);
        }
    }

    private void TurnVertical()
    {
        var meterPercentage = Mathf.InverseLerp(_minVertical, _maxVertical, barrelObject.transform.rotation.x);
        if (Input.GetKey(KeyCode.UpArrow) && barrelObject.transform.rotation.x <= _maxVertical || Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            barrelObject.transform.Rotate(+_gunRotateSpeed * Time.deltaTime, yAngle: 0, 0);
        } 
        else if (Input.GetKey(KeyCode.DownArrow) && barrelObject.transform.rotation.x >= _minVertical || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            barrelObject.transform.Rotate( - _gunRotateSpeed*Time.deltaTime  ,yAngle:0,0);
        }
        
        powerObject.transform.localPosition = new Vector3(-175.2f,  Mathf.Lerp(powerMin, powerMax, meterPercentage), 0);
    }
}
