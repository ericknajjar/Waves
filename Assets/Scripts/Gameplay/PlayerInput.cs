using UnityEngine;
using System.Collections;
using u3dExtensions.IOC;
using u3dExtensions.Engine.Runtime;
using u3dExtensions.Events;

public class PlayerInput: Entity
{

	[SerializeField]
	float m_moveSpeed = 1.0f;

	[SerializeField]
	float m_jumpForce = 1.0f;

	IPlataformerInput m_input;
	Rigidbody2D m_rigidBody;
	Collider2D m_collider;
	RaycastHit2D[] m_raycasts = new RaycastHit2D[2];

	public bool IsGrounded {
		get;
		private set;
	}
		

	public void Start()
	{

		m_collider = GetComponent<Collider2D> ();

		UpdateGrounded ();

		m_rigidBody = GetComponent<Rigidbody2D> ();
		m_input = Context.Get<IPlataformerInput> ();
		m_input.OnJumpClick.Register (OnJump);
	}

	void UpdateGrounded()
	{
		IsGrounded = m_collider.Raycast (Vector2.down, m_raycasts, m_collider.bounds.extents.y*1.05f, LayerMask.GetMask ("Ground")) > 0;
	}

	void Update()
	{
		

	}

	void FixedUpdate()
	{
		UpdateGrounded ();
		Debug.Log (IsGrounded);
		var joystick = m_input.Joystick.normalized;
		var speedX = 0.0f;
		var speedY = 0.0f;

		if ( Mathf.Abs (joystick.x) > 0.5f)
		{
			speedX = m_moveSpeed * Mathf.Sign (joystick.x);
		}

		if(IsGrounded)
			m_rigidBody.velocity = new Vector2 (speedX,m_rigidBody.velocity.y);
	}

	void OnJump()
	{
		if(IsGrounded)
			m_rigidBody.AddForce (new Vector2 (0, m_jumpForce));
	}
}


