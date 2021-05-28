using UnityEngine;
using BlinkingAnimation;
public class Grenade : CharacterState
{
    [SerializeField] private GunMovement _gunMovement;
    [SerializeField] private Camera camera;
    [SerializeField] private AnimatorValueSetter valSetter;


    private void Start()
    {
        _gunMovement = GetComponent<GunMovement>();
    }
    
    public override void Enter()
    {
        SetArrayOfGO(_objectsToEnable, true);
        valSetter.OnBlind -= ChangeFoV;
        
        _gunMovement.enabled = true;
        _active = true;
    }

    public override void Leave()
    {
        valSetter.OnBlind += ChangeFoV;
        //halt controls here and halt processes
        SetArrayOfGO(_objectsToDisable, false);
        _gunMovement.enabled = false;
        _active = false;
    }

    private void ChangeFoV()
    {
        
        camera.fieldOfView = 30;
    }
}