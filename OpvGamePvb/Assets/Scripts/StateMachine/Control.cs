using UnityEngine;
using System;
using System.Collections;

public class Control : MonoBehaviour
{
    public static Action OnSwitchKey;
    public static bool _playerHasControl = true;

    [SerializeField] private KeyCode _switchingKey = KeyCode.Space;
    private bool _switchKeyInDelay;
    [SerializeField] private float _switchKeyDelayInSeconds = 0.55f;
    
    private void Update()
    {
        if (!_playerHasControl) return;
        CheckKeys();
    }

    private void CheckKeys()
    {
        if (Input.GetKeyDown(_switchingKey) && !_switchKeyInDelay) //the animation lasts half a second
        {
            _switchKeyInDelay = true;
            OnSwitchKey?.Invoke();
            StartCoroutine(SwitchKeyWait());
        } 
    }

    private IEnumerator SwitchKeyWait()
    {
        yield return new WaitForSecondsRealtime(_switchKeyDelayInSeconds);
        _switchKeyInDelay = false;
    }
}
