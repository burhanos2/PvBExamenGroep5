using UnityEngine;
using BlinkingAnimation;
public class Captain : CharacterState
{
    [SerializeField] private Camera camera;
    [SerializeField] private AnimatorValueSetter valSetter;
    public override void Enter()
    {
        valSetter.OnBlind -= ChangeFoV;
        
        // update controls here and start processes
        SetArrayOfGO(_objectsToEnable, true);
        
    }

    public override void Leave()
    {
        valSetter.OnBlind += ChangeFoV;
        //halt controls here and halt processes
        SetArrayOfGO(_objectsToDisable, false);
        
    }

    private void ChangeFoV()
    {
        
        camera.fieldOfView = 30;
    }
}
