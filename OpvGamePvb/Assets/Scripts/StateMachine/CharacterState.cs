using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
	public virtual void Enter()
	{
		// update controls here and start processes
	}

	public virtual void Leave()
	{
		//halt controls here and halt processes
	}
}

