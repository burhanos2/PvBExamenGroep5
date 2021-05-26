using UnityEngine;
using System;
using System.Collections;

public class Control : MonoBehaviour
{
    public static bool _playerHasControl = true;

    #region Key_Delegates
    public static Action OnSwitchKey;
    public static Action OnAttackKeys;
    public static Action<bool> OnHorizontalMoveKeys; //bool false when left
    public static Action<bool> OnVerticalMoveKeys; //bool false when down
    #endregion Key_Delegates

    #region Key_Variables
    [SerializeField] private KeyCode _switchingKey = KeyCode.Space;
    [SerializeField] private KeyCode _attackKey = KeyCode.Tab;
    [SerializeField] private KeyCode _moveLeftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _moveRightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode _moveUpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode _moveDownKey = KeyCode.DownArrow;
    #endregion Key_Variables

    #region Key_Variables_alt 
    [SerializeField] private KeyCode _attackKeyAlt = KeyCode.Mouse2;
    [SerializeField] private KeyCode _moveLeftKeyAlt = KeyCode.Mouse0;
    [SerializeField] private KeyCode _moveRightKeyAlt = KeyCode.Mouse1;
    [SerializeField, Range(0f, -2f)] private float _scrollOffsetDown = 0f;
    [SerializeField, Range(0f, 2f)] private float _scrollOffsetUp = 0f;
    #endregion Key_Variables_alt

    #region Key_Delay_Variables
    private bool _switchKeyInDelay;
    private bool _shootable = true;
    [SerializeField, Range(0.51f, 2f)] private float _switchKeyDelayInSeconds = 0.55f;
    [SerializeField, Range(1, 5)] private float _waitingtime = 2;
    #endregion Key_Delay_Variables

    public void GameStarter(bool doStart) => _playerHasControl = doStart;
    private void Update()
    {
        if (!_playerHasControl) return;
        CheckKeys();
    }

    private void CheckKeys()
    {
        //switch key
        if (Input.GetKeyDown(_switchingKey) && !_switchKeyInDelay) //the animation lasts half a second
        {
            _switchKeyInDelay = true;
            OnSwitchKey?.Invoke();
            StartCoroutine(SwitchKeyWait());
        }
        //attack button
        if (Input.GetKeyDown(_attackKey) || Input.GetKeyDown(_attackKeyAlt))
        {
            Debug.Log(_shootable);
            if(!_shootable) return;
            OnAttackKeys?.Invoke();
            StartCoroutine(inputDelay());

        }
        //Horizontal keys, false is left
        if (Input.GetKey(_moveLeftKey) || Input.GetKey(_moveLeftKeyAlt))
        {
            OnHorizontalMoveKeys?.Invoke(false);
        }
        else if (Input.GetKey(_moveRightKey) || Input.GetKey(_moveRightKeyAlt))
        {
            OnHorizontalMoveKeys?.Invoke(true);
        }
        //Vertical keys, false is down
        if (Input.GetKey(_moveDownKey) || Input.GetAxis("Mouse ScrollWheel") < _scrollOffsetDown)
        {
            OnVerticalMoveKeys?.Invoke(false);
        }
        else if (Input.GetKey(_moveUpKey) || Input.GetAxis("Mouse ScrollWheel") > _scrollOffsetUp)
        {
            OnVerticalMoveKeys?.Invoke(true); 
        }
    }

    private IEnumerator SwitchKeyWait()
    {
        yield return new WaitForSecondsRealtime(_switchKeyDelayInSeconds);
        _switchKeyInDelay = false;
    }
    
    private IEnumerator inputDelay()
    {
        _shootable = false;

        yield return new WaitForSeconds(_waitingtime);

        _shootable = true;
        
        yield return null;
    }
}
