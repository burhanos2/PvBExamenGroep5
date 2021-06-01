using System;
using System.Collections.Generic;
using UnityEngine;
using BlinkingAnimation;

namespace StateMachine
{
	public enum CharactersEnum
	{
		NullStateId = 0,
		Captain = 1,
		Cannoneer = 2,
		Artillery = 3,
	}

	public class PlayerStateMachine : MonoBehaviour {
		private CharacterState _currentState;
		private int stateIndex = 1;
		private Dictionary<CharactersEnum, CharacterState> statesDict = new Dictionary<CharactersEnum, CharacterState> ();
		private Camera mainCamera;
		private Animator blinkAnimator;
		private Transform camSpot;
		private GameObject eyelidObject;
		
		private readonly int PlayBlink = Animator.StringToHash("PlayBlink");
		private readonly int IsBlind = Animator.StringToHash("isBlind");

		private void Start ()
		{
			mainCamera = Camera.main;
			eyelidObject = GameObject.Find("Camera_Eyelids");
			blinkAnimator = eyelidObject.GetComponent<Animator>();
 
			eyelidObject.GetComponent<AnimatorValueSetter>().OnBlind += ActionOnBlind;
			Control.OnSwitchKey += SelectState;
			
			AddStates();
			
			LeaveAllStates(); //executing leave on every state first
			
			SetState((CharactersEnum)stateIndex); //sets default state
		}

		private void LeaveAllStates()
		{
			for (int i = 1; i <= statesDict.Count; i++)
			{
				statesDict[(CharactersEnum)i].Leave();
			}
		}

		private void SelectState()
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
			statesDict.Add( CharactersEnum.Captain,  FindObjectOfType<Captain>());
			statesDict.Add( CharactersEnum.Cannoneer, FindObjectOfType<Cannoneer>());
			statesDict.Add( CharactersEnum.Artillery, FindObjectOfType<Grenade>());
		}
	
		private void SetState(CharactersEnum stateId) {
			var selectedState = statesDict[stateId];
			if (!statesDict.ContainsKey(stateId) && selectedState == _currentState) //if key doesnt exist or selected state is already current, return
			{
				return;
			}
			camSpot = selectedState.transform.Find("CameraPos").transform; //making this reference before the quick return is a bad idea

			if (_currentState)
			{
				_currentState.Leave();
				blinkAnimator.SetTrigger(PlayBlink);
			}
		
			_currentState = selectedState;
			_currentState.Enter();
		}

		private void ActionOnBlind()
		{
			mainCamera.transform.SetPositionAndRotation(camSpot.position, camSpot.rotation);
			mainCamera.transform.SetParent(camSpot);
		}
	}
}