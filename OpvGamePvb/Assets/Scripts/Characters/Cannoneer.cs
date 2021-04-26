using UnityEngine;

public class Cannoneer : CharacterState
{
    [SerializeField] private GunMovement _gunMovement;
    public override void Enter()
    {
        // update controls here and start processes
        SetArrayOfGO(_objectsToEnable, true);
        _gunMovement.enabled = true;

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
    }
}
