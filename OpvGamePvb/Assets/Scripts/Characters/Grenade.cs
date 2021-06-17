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
        SetArrayOfGO(_objectsToEnable, true);
        _valSetter.OnBlind -= ChangeFoV;
        
        _gunMovement.enabled = true;
        _active = true;
        Control.Instance._shootWaitingtime = 0.7f;
        _gunMovement._verticalRotateAxis = Vector3.forward; // (0, 0, 1)
    }

    public override void Leave()
    {
        _valSetter.OnBlind += ChangeFoV;
        //halt controls here and halt processes
        SetArrayOfGO(_objectsToDisable, false);
        _gunMovement.enabled = false;
        _active = false;
        AudioManager.Instance.StopSfxLoop();
    }

    private void ChangeFoV()
    {
        CharacterCornerSprite.Instance.SetSprite(1);
        _camera.fieldOfView = 60; //to captain
        ConeActiveHandler.Instance.ChangeActivity(0);
    }
}