using UnityEngine;
using BlinkingAnimation;

public class Cannoneer : CharacterState
{
    [SerializeField] private GunMovement _gunMovement;
    [SerializeField] private Camera _camera;
    [SerializeField] private AnimatorValueSetter _valSetter;
    public override void Enter()
    {
        // update controls here and start processes
        SetArrayOfGO(_objectsToEnable, true);
        _valSetter.OnBlind -= ChangeFoV;
        _gunMovement.enabled = true;
        _active = true;
    }

    private void Start()
    {
        _gunMovement = GetComponent<GunMovement>();
    }

    public override void Leave()
    {
        //halt controls here and halt processes
        SetArrayOfGO(_objectsToDisable, false);
        _gunMovement.enabled = false;
        
        _valSetter.OnBlind += ChangeFoV;
        _active = false;
    }
    private void ChangeFoV()
    {
        _camera.fieldOfView = 60;
    }
}
