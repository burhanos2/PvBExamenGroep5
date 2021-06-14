using System;
using System.Collections;
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

    private readonly float _powerMax = -53;
    private readonly float _powerMin = 26;

    //0.712 is ong 90 graden
    private readonly float _maxHorizontal = 0.9f;
    private readonly float _minHorizontal = -0.9f;
    
    private readonly float _maxVertical = 0.05f;
    private readonly float _minVertical = -0.193f;

    public Action Shoot;
    
    //added for sound 
    private bool _isMoving;
    private float _soundWait;
    private const float SoundWaitDefault = 0.1f;
    
    private void Start()
    {
        Control.Instance.OnAttackKeys += InvokeShoot;
        Control.Instance.OnHorizontalMoveKeys += TurnHorizontal;
        Control.Instance.OnVerticalMoveKeys += TurnVertical;
        _soundWait = 0;
    }

    private void Update()
    {
        if (_soundWait < -0.5f) return;
        _soundWait -= Time.deltaTime;
        if(_soundWait < 0 )
        {SetMoveBool(false);}
    }

    private void InvokeShoot()
    {
            Shoot?.Invoke();
    }

    private void TurnHorizontal(bool isRight)
    {
        if(enabled){
            if(isRight && _gunObject.transform.rotation.y <= _maxHorizontal) //right
            {
                _gunObject.transform.Rotate(0, yAngle: +_gunRotateSpeed * Time.deltaTime, 0);
                SetMoveBool(true);
                _soundWait = SoundWaitDefault;
            }
            else if (!isRight && _gunObject.transform.rotation.y >= _minHorizontal) //left
            {
                _gunObject.transform.Rotate(0, yAngle: -_gunRotateSpeed * Time.deltaTime, 0);
                SetMoveBool(true);
                _soundWait = SoundWaitDefault;
            }
        }
        
    }

    private void TurnVertical(bool isUp)
    {
        if (enabled)
        {
            var meterPercentage = Mathf.InverseLerp(_minVertical, _maxVertical, _barrelObject.transform.rotation.x);

            if(isUp && _barrelObject.transform.rotation.x <= _maxVertical) //up
            {
                _barrelObject.transform.Rotate(+_gunRotateSpeed * Time.deltaTime, yAngle: 0, 0);
                SetMoveBool(true);
                _soundWait = SoundWaitDefault;
            }
            else if(!isUp && _barrelObject.transform.rotation.x >= _minVertical) //down
            {
                _barrelObject.transform.Rotate(-_gunRotateSpeed * Time.deltaTime, yAngle: 0, 0);
                SetMoveBool(true);
                _soundWait = SoundWaitDefault;
            }

            _powerObject.transform.localPosition = new Vector3(-209f,  Mathf.Lerp(_powerMin, _powerMax, meterPercentage), 0);
        }
        
    }
    
    
    //added to script for sounds
    private void SetMoveBool(bool newVal)
    {
        if (newVal == _isMoving) return;
        if (newVal)
        {
            _isMoving = true;
            PlayMoveSounds();
        }
        else
        {
            _isMoving = false;
            StopPlayingLoop();
        }
    }
    private void StopPlayingLoop()
    {
        AudioManager.Instance.StopSfxLoop();
    }
    private void PlayMoveSounds()
    {
        AudioManager.Instance.PlaySfxLoopStart(SfxTypes.CannonMoveBegin);
        StartCoroutine(PlayAfterDelay(1f));
    }

    private IEnumerator PlayAfterDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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
