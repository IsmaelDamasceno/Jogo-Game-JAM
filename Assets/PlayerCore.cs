using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCore : MonoBehaviour
{
	public StateMachine stateMachine;
	void Start()
	{
		stateMachine = GetComponent<StateMachine>();

		BaseState moveState = GetComponent<MovementState>();
		stateMachine.RegisterState("Move", moveState);
		stateMachine.ChangeState("Move");
	}
}
