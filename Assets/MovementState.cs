using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementState : BaseState
{

	[Header("Movemento")]

	// Velocidade de movimento
	[SerializeField] private float moveSpeed;

	// Força do pulo
	[SerializeField] private float jumpStrength;
	// Força da gravidade
	[SerializeField] private float gravityScale;

	[Header("Agachar")]
	[SerializeField] private float crouchHeight;
	[SerializeField] private float crouchMoveSpeedMultiplier;
	[SerializeField] private float crouchTransitionDuration;

	private bool crouching = false;
	private float defaultHeight;
	private float camOffset;

	// Velocidade atual de movimento a ser aplicada
	private Vector3 velocity;
	[HideInInspector] public CharacterController charCtrl;
	private float gravity = 9.807f;

	public override void Enter()
	{
		// Procura um Rigidbody2D no Game Object, e atribui seu valor a vari�vel
		charCtrl = GetComponent<CharacterController>();

		defaultHeight = charCtrl.height;

		camOffset = Camera.main.transform.localPosition.y;

		Debug.Log("Enter Movement state");
	}
	public override void Exit()
	{
		Debug.Log("Exit Movement state");
	}

	public override void Step()
	{
		Vector3 input = new (
			Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")
		);

		Vector3 direction = (input.x * Camera.main.transform.right + input.z * Camera.main.transform.forward).normalized;
		float oldVel = velocity.y;

		velocity = moveSpeed * (crouching? crouchMoveSpeedMultiplier: 1f) * Time.deltaTime * direction;
		velocity.y = oldVel;

		if (Input.GetButtonDown("Jump") && charCtrl.isGrounded)
		{
			velocity.y = jumpStrength;
		}
		velocity.y -= gravity * gravityScale * Time.deltaTime * Time.deltaTime;

		charCtrl.Move(velocity);
		Crouch();
	}

	void Crouch()
	{
		crouching = Input.GetKey(KeyCode.LeftShift);

		float oldHeight = charCtrl.height;
		float heightTarget = crouching ? crouchHeight : defaultHeight;
		charCtrl.height = Mathf.Lerp(charCtrl.height, heightTarget, Time.fixedDeltaTime * crouchTransitionDuration);
		float deltaHeight = oldHeight - charCtrl.height;

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			Debug.Log(charCtrl.height);

			charCtrl.enabled = false;
			transform.position += Vector3.up * deltaHeight;
			charCtrl.enabled = true;
		}

		float newCamOffset = (defaultHeight - charCtrl.height) / 2f;
		Camera.main.transform.localPosition = new Vector3(0f, camOffset - newCamOffset, 0f);
	}

	public override void FixedStep()
	{
		
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		
	}
	private void OnDrawGizmos()
	{
		
	}
#endif
}
