using UnityEngine;
using BlinkingAnimation;
using SoundSystem;

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
        Control.Instance._shootWaitingtime = 1.9f;
        _gunMovement._verticalRotateAxis = Vector3.right; // (1, 0, 0)
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
        AudioManager.Instance.StopSfxLoop();
    }
    private void ChangeFoV()
    {
        CharacterCornerSprite.Instance.SetSprite(0);
        _camera.fieldOfView = 30; // to grenade
        ConeActiveHandler.Instance.ChangeActivity(2);
    }
}
