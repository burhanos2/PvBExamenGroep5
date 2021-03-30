using System;
using System.Collections.Generic;
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
	
	private void Start () {
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
		if (!statesDict.ContainsKey(stateId))
		{
			return;
		}

		if (currentState)
		{
			currentState.Leave();
		}

		currentState = statesDict[stateId];
		currentState.Enter();
	}
}