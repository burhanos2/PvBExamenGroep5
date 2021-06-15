using UnityEngine;
using BlinkingAnimation;
public class Captain : CharacterState
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AnimatorValueSetter _valSetter;
    public override void Enter()
    {
        _valSetter.OnBlind -= ChangeFoV;
        _active = true;
        // update controls here and start processes
        SetArrayOfGO(_objectsToEnable, true);
    }

    public override void Leave()
    {
        _active = false;
        _valSetter.OnBlind += ChangeFoV;
        //halt controls here and halt processes
        SetArrayOfGO(_objectsToDisable, false);
        
    }

    private void ChangeFoV()
    {
        CharacterCornerSprite.Instance.SetSprite(2); 
        _camera.fieldOfView = 30; // to cannoneer
    }
}
