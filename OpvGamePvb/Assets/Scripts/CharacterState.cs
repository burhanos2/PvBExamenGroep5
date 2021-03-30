using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class CharacterState : MonoBehaviour {
	//need to find better way to get camera without creating 2 references
	public void Enter ()
	{
		Camera thisCam = gameObject.GetComponentInChildren<Camera>();
		UpdateControls();
		thisCam.enabled = true;
	}
	
	public void Leave ()
	{
		Camera thisCam = gameObject.GetComponentInChildren<Camera>();
		thisCam.enabled = false;
		DisableControls();
		PlaySwitchingEffect();
	}

	private void PlaySwitchingEffect()
	{
		//add switching effect here!
	}
	
	protected virtual void UpdateControls()
	{
		// update controls here and start processes
	}

	protected virtual void DisableControls()
	{
		//halt controls here and halt processes
	}
}

