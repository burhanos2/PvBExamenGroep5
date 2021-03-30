using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField]
    private int GunRotateSpeed = 4;
    [SerializeField]
    public GameObject Gun;
    [SerializeField]
    public GameObject Barrel;

    public Action Shoot;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Gun.transform.Rotate(0 ,yAngle: + GunRotateSpeed*Time.deltaTime ,0);
        } 
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Gun.transform.Rotate(0 ,yAngle: - GunRotateSpeed*Time.deltaTime ,0);
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Barrel.transform.Rotate(+ GunRotateSpeed*Time.deltaTime ,yAngle:0  ,0);
        } 
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Barrel.transform.Rotate( - GunRotateSpeed*Time.deltaTime  ,yAngle:0,0);
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Shoot?.Invoke();
        }
    }
}
