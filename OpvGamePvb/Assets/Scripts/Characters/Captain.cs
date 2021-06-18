using UnityEngine;
using BlinkingAnimation;

public class Captain : CharacterState
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AnimatorValueSetter _valSetter;
    
    public override void Enter()
    {
        // update controls here and start processes
        _valSetter.OnBlind -= ChangeFoV;
        
        _active = true;
        
        SetArrayOfGO(_objectsToEnable, true);
    }

    public override void Leave()
    {
        //halt controls here and halt processes
        _valSetter.OnBlind += ChangeFoV;
        
        SetArrayOfGO(_objectsToDisable, false);
        
        _active = false;
    }

    private void ChangeFoV()
    {
        // to cannoneer
        _camera.fieldOfView = 30; 
        CharacterCornerSprite.Instance.SetSprite(2);
        ConeActiveHandler.Instance.ChangeActivity(1);
    }
}