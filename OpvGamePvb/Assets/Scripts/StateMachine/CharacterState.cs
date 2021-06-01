using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
	[SerializeField] protected GameObject[] _objectsToEnable;
	[SerializeField] protected GameObject[] _objectsToDisable;

	protected void SetArrayOfGO(GameObject[] array, bool makeEnabled)
	{
		if (makeEnabled)
		{
			foreach (var gameObj in array)
			{
				gameObj.SetActive(true);
			}
		}
		else
		{
			foreach (var gameObj in array)
			{
				gameObj.SetActive(false);
			}
		}
	}
	public virtual void Enter()
	{
		// update controls here and start processes
	}

	public virtual void Leave()
	{
		//halt controls here and halt processes
	}
}

