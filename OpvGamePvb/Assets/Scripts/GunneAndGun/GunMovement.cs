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
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Mouse1))
        {
            gunObject.transform.Rotate(0 ,yAngle: + _gunRotateSpeed*Time.deltaTime ,0);
        } 
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Mouse0))
        {
            gunObject.transform.Rotate(0 ,yAngle: - _gunRotateSpeed*Time.deltaTime ,0);
        }
    }

    private void TurnVertical()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            barrelObject.transform.Rotate(+ _gunRotateSpeed*Time.deltaTime ,yAngle:0  ,0);
        } 
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            barrelObject.transform.Rotate( - _gunRotateSpeed*Time.deltaTime  ,yAngle:0,0);
        }
    }
}
