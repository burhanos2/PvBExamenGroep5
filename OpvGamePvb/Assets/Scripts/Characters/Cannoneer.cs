public class Cannoneer : CharacterState
{
    public override void Enter()
    {
        // update controls here and start processes
        SetArrayOfGO(_objectsToEnable, true);
    }

    public override void Leave()
    {
        //halt controls here and halt processes
        SetArrayOfGO(_objectsToDisable, false);
    }
}
