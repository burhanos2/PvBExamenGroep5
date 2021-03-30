using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Control : MonoBehaviour
{
    [SerializeField] private KeyCode switchingKey = KeyCode.Space;
    public static Action OnSwitchKey;
    private void Update()
    {
        if (Input.GetKeyDown(switchingKey))
        {
            OnSwitchKey?.Invoke();
        }
    }
}
