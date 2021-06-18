using UnityEngine;
using System;
using System.Collections;

//singleton
public class Control : MonoBehaviour
{
    public bool _playerHasControl;

    #region Key_Delegates
    public Action OnSwitchKey;
    public Action OnAttackKeys;
    public Action<bool> OnHorizontalMoveKeys; //bool false when left
    public Action<bool> OnVerticalMoveKeys; //bool false when down
    #endregion Key_Delegates

    #region Key_Variables
    [SerializeField] private KeyCode _switchingKey = KeyCode.Space;
    [SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode _moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode _moveUpKey = KeyCode.W;
    [SerializeField] private KeyCode _moveDownKey = KeyCode.S;
    #endregion Key_Variables

    #region Key_Variables_alt 
    [SerializeField] private KeyCode _attackKeyAlt;
    [SerializeField] private KeyCode _moveLeftKeyAlt;
    [SerializeField] private KeyCode _moveRightKeyAlt;
    [SerializeField, Range(0f, -2f)] private float _scrollOffsetDown;
    [SerializeField, Range(0f, 2f)] private float _scrollOffsetUp;
    #endregion Key_Variables_alt

    #region Key_Delay_Variables
    private bool _switchKeyInDelay;
    private bool _shootable = true;
    [SerializeField, Range(0.51f, 2f)] private float _switchKeyDelayInSeconds = 0.55f;
    [Range(1, 5)] public float _shootWaitingtime = 2;
    #endregion Key_Delay_Variables

    public void GameStarter(bool doStart) => _playerHasControl = doStart;
    public static Control Instance; //singleton
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        _playerHasControl = false;
    }

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
            if(!_shootable) return;
            OnAttackKeys?.Invoke();
            StartCoroutine(ShootingDelay());

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
    
    private IEnumerator ShootingDelay()
    {
        _shootable = false;

        yield return new WaitForSeconds(_shootWaitingtime);

        _shootable = true;
        
        yield return null;
    }
}
