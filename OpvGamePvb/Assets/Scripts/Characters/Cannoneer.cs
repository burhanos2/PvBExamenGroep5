using UnityEngine;
using BlinkingAnimation;
using Vector3 = UnityEngine.Vector3;
using SoundSystem;

public class Cannoneer : CharacterState
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
        Control.Instance._shootWaitingtime = 1.9f;
        _gunMovement._verticalRotateAxis = Vector3.right; // (1, 0, 0)
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
        // to grenade
        _camera.fieldOfView = 30; 
        CharacterCornerSprite.Instance.SetSprite(0);
        ConeActiveHandler.Instance.ChangeActivity(2);
    }
}