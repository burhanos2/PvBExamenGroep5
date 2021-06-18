using System;
using SoundSystem;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField] [Range(1, 20)]
    private int _gunRotateSpeed = 4;
    [SerializeField]
    public GameObject _gunObject;
    [SerializeField]
    public GameObject _barrelObject; // anchorpoint!!!
    [SerializeField]
    private GameObject _powerObject;

    private readonly float _powerMax = -69.69f; //:)
    private readonly float _powerMin = 45f;

    //0.712 is ong 90 degrees
    [SerializeField] private float _maxHorizontalTurn = 0.9f;
    [SerializeField] private float _minHorizontalTurn = -0.9f;
    
    [SerializeField] private float _maxVerticalTurn = 0.05f;
    [SerializeField] private float _minVerticalTurn = -0.193f;

    public Action Shoot;
    
    //added for sound 
    private bool _isMoving;
    private float _soundWait;
    private const float SoundWaitDefault = 0.1f;

    [NonSerialized] public Vector3 _verticalRotateAxis = Vector3.zero;
    
    private void Start()
    {
        Control.Instance.OnAttackKeys += InvokeShoot;
        Control.Instance.OnHorizontalMoveKeys += TurnHorizontal;
        Control.Instance.OnVerticalMoveKeys += TurnVertical;
        _soundWait = 0;
    }

    private void Update()
    {
        if (!enabled) return;
        
        _soundWait -= Time.deltaTime;
        
        if (_soundWait < 0)
        {
            SetMoveBool(false);
        }
    }

    private void InvokeShoot()
    {
            Shoot?.Invoke();
    }

    private void TurnHorizontal(bool isRight)
    {
        if (!enabled) return;
        if (isRight)
        {
            if (!(_gunObject.transform.rotation.y <= _maxHorizontalTurn)) return; //right
            _gunObject.transform.Rotate(0, yAngle: +_gunRotateSpeed * Time.deltaTime, 0);
            SetMoveBool(true);
            _soundWait = SoundWaitDefault;
        }
        else
        {
            if (!(_gunObject.transform.rotation.y >= _minHorizontalTurn)) return; //left
            _gunObject.transform.Rotate(0, yAngle: -_gunRotateSpeed * Time.deltaTime, 0);
            SetMoveBool(true);
            _soundWait = SoundWaitDefault;
        }
    }

    private void TurnVertical(bool isUp)
    {
        if (!enabled) return;
        
        var localRotation = _barrelObject.transform.localRotation;
        var meterPercentage = Mathf.InverseLerp(_minVerticalTurn, _maxVerticalTurn, (int)_verticalRotateAxis.x == 1 ? localRotation.x : localRotation.z);

        if(isUp) //up
        {
            if (_barrelObject.transform.localRotation.x <= _maxVerticalTurn && _barrelObject.transform.localRotation.z <= _maxVerticalTurn)
            {
                _barrelObject.transform.Rotate(
                    _verticalRotateAxis.x * +_gunRotateSpeed * Time.deltaTime,
                    yAngle: 0,
                    _verticalRotateAxis.z * +_gunRotateSpeed * Time.deltaTime,
                    Space.Self);
                SetMoveBool(true);
                _soundWait = SoundWaitDefault;   
            }
        }
        else
        {
            if (_barrelObject.transform.localRotation.x >= _minVerticalTurn && _barrelObject.transform.localRotation.z >= _minVerticalTurn)
            {
                _barrelObject.transform.Rotate(
                    _verticalRotateAxis.x * -_gunRotateSpeed * Time.deltaTime,
                    yAngle: 0, 
                    _verticalRotateAxis.z * -_gunRotateSpeed * Time.deltaTime, 
                    Space.Self);
                SetMoveBool(true);
                _soundWait = SoundWaitDefault;
            }
        }

        _powerObject.transform.localPosition = new Vector3(-209f,  Mathf.Lerp(_powerMin, _powerMax, meterPercentage), 0);

    }
    
    
    //added to script for sounds
    public void SetMoveBool(bool newVal)
    {
        if (newVal == _isMoving) return;
        if (newVal)
        {
            _isMoving = true;
            PlayLoop();
        }
        else
        {
            _isMoving = false;
            StopPlayingLoop();
        }
    }
    private static void StopPlayingLoop()
    {
        AudioManager.Instance.StopSfxLoop();
    }

    private void PlayLoop()
    {
        if (_isMoving)
        {
            AudioManager.Instance.PlaySfxLooped(SfxTypes.CannonMoveLoop);
        }
        else
        {
            StopPlayingLoop();
        }
    }
}
