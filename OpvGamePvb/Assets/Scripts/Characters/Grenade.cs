using UnityEngine;
using BlinkingAnimation;
using Vector3 = UnityEngine.Vector3;
using SoundSystem;

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
        // update controls here and start processes
        _valSetter.OnBlind -= ChangeFoV;
        
        _active = true;
        
        SetArrayOfGO(_objectsToEnable, true);
        _gunMovement.enabled = true;
        Control.Instance._shootWaitingtime = 0.7f;
        _gunMovement._verticalRotateAxis = Vector3.forward; // (0, 0, 1)
    }
    
    public override void Leave()
    {
        //halt controls here and halt processes
        _valSetter.OnBlind += ChangeFoV;
        
        AudioManager.Instance.StopSfxLoop();
        SetArrayOfGO(_objectsToDisable, false);
        _gunMovement.SetMoveBool(false);
        _gunMovement.enabled = false;
        
        _active = false;
    }

    private void ChangeFoV()
    {
        // to captain
        _camera.fieldOfView = 60; 
        CharacterCornerSprite.Instance.SetSprite(1);
        ConeActiveHandler.Instance.ChangeActivity(0);
    }
}