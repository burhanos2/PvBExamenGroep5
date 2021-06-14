using UnityEngine;
using BlinkingAnimation;
public class Grenade : CharacterState
{
    [SerializeField] private GunMovement _gunMovement;
    [SerializeField] private Camera _camera;
    [SerializeField] private AnimatorValueSetter _valSetter;


    private void Start()
    {
        _gunMovement = GetComponent<GunMovement>();
    }
    
    public override void Enter()
    {
        SetArrayOfGO(_objectsToEnable, true);
        _valSetter.OnBlind -= ChangeFoV;
        
        _gunMovement.enabled = true;
        _active = true;
        Control.Instance._shootWaitingtime = 0.7f;
    }

    public override void Leave()
    {
        _valSetter.OnBlind += ChangeFoV;
        //halt controls here and halt processes
        SetArrayOfGO(_objectsToDisable, false);
        _gunMovement.enabled = false;
        _active = false;
    }

    private void ChangeFoV()
    {
        
        _camera.fieldOfView = 30;
    }
}