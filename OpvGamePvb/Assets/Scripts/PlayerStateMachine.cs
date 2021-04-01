using System;
using System.Collections.Generic;
using System.Linq;
using BlinkingAnimation;
using UnityEngine;

public enum CharactersEnum
{
	NullStateId = 0,
	Captain = 1,
	Cannoneer = 2
}

public class PlayerStateMachine : MonoBehaviour {
	private CharacterState currentState;
	private int stateIndex = 1;
	private Dictionary<CharactersEnum, CharacterState> statesDict = new Dictionary<CharactersEnum, CharacterState> ();
	private Camera m_Cam;
	private Animator m_BlinkAnimator;
	private static readonly int PlayBlink = Animator.StringToHash("PlayBlink");
	private static readonly int IsBlind = Animator.StringToHash("isBlind");
	private Transform camSpot;

	private void Start () {
		m_BlinkAnimator = GameObject.Find("Camera_Eyelids").GetComponent<Animator>();
		Action OnBlind = GameObject.Find("Camera_Eyelids").GetComponent<AnimatorValueSetter>().OnBlind += ActionOnBlind;
		m_Cam = Camera.main;
		AddStates();
		SetState( CharactersEnum.Captain );

		Control.OnSwitchKey += StateSelection;
	}

	private void StateSelection()
	{
		stateIndex++;
		if (stateIndex > statesDict.Count)
		{
			stateIndex = 1;
		}
		else if(stateIndex <= 0)
		{
			stateIndex = statesDict.Count;
		}
		SetState((CharactersEnum)stateIndex);
	}
	
	private void AddStates() {
		statesDict.Add( CharactersEnum.Captain, GetComponentInChildren<Captain>());
		statesDict.Add( CharactersEnum.Cannoneer, GetComponentInChildren<Cannoneer>());
	}
	
	private void SetState(CharactersEnum stateId) {
		if (!statesDict.ContainsKey(stateId) && statesDict[stateId] == currentState) //if key doesnt exist or selected state is already current, return
		{
			return;
		}
		//making a references before a quick return would be redundant
		var selectedState = statesDict[stateId];
		camSpot = selectedState.transform.Find("CameraPos").transform;

		if (currentState) // if state is not empty, leave current state and blink
		{
			currentState.Leave();
			m_BlinkAnimator.SetTrigger(PlayBlink);
		}
		
		currentState = selectedState;
		currentState.Enter();
	}

	private void ActionOnBlind()
	{
		m_Cam.transform.SetPositionAndRotation(camSpot.position, camSpot.rotation);
	}
}