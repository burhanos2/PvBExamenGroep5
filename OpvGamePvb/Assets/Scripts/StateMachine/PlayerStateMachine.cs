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
		//Artillery = 3,
	}

	public class PlayerStateMachine : MonoBehaviour {
		private CharacterState _currentState;
		private int _stateIndex = 1;
		private Dictionary<CharactersEnum, CharacterState> statesDict = new Dictionary<CharactersEnum, CharacterState> ();
		private Camera _mainCamera;
		private Animator _blinkAnimator;
		private Transform _camSpot;
		private GameObject _eyelidObject;
		
		private readonly int _playBlink = Animator.StringToHash("PlayBlink");
		private readonly int _isBlind = Animator.StringToHash("isBlind");

		private void Start ()
		{
			_mainCamera = Camera.main;
			_eyelidObject = GameObject.Find("Camera_Eyelids");
			_blinkAnimator = _eyelidObject.GetComponent<Animator>();
 
			_eyelidObject.GetComponent<AnimatorValueSetter>().OnBlind += ActionOnBlind;
			Control.OnSwitchKey += SelectState;
			
			AddStates();
			
			LeaveAllStates(); //executing leave on every state first
			
			SetState((CharactersEnum)_stateIndex); //sets default state
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
			_stateIndex++;
			if (_stateIndex > statesDict.Count)
			{
				_stateIndex = 1;
			}
			else if(_stateIndex <= 0)
			{
				_stateIndex = statesDict.Count;
			}
			SetState((CharactersEnum)_stateIndex);
		}
	
		private void AddStates() {
			statesDict.Add( CharactersEnum.Captain,  FindObjectOfType<Captain>());
			statesDict.Add( CharactersEnum.Cannoneer, FindObjectOfType<Cannoneer>());
		//	statesDict.Add( CharactersEnum.Artillery, FindObjectOfType<Grenade>());
		}
	
		private void SetState(CharactersEnum stateId) {
			var selectedState = statesDict[stateId];
			if (!statesDict.ContainsKey(stateId) && selectedState == _currentState) //if key doesnt exist or selected state is already current, return
			{
				return;
			}
			_camSpot = selectedState.transform.Find("CameraPos").transform; //making this reference before the quick return is a bad idea

			if (_currentState)
			{
				_currentState.Leave();
				_blinkAnimator.SetTrigger(_playBlink);
			}
		
			_currentState = selectedState;
			_currentState.Enter();
		}

		private void ActionOnBlind()
		{
			_mainCamera.transform.SetPositionAndRotation(_camSpot.position, _camSpot.rotation);
			_mainCamera.transform.SetParent(_camSpot);
		}
	}
}