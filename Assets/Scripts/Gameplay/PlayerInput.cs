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


	public void Start()
	{
		m_rigidBody = GetComponent<Rigidbody2D> ();
		m_input = Context.Get<IPlataformerInput> ();
		Debug.Log ("player input");
		m_input.OnJumpClick.Register (OnJump);
	}

	void Update()
	{
		var joystick = m_input.Joystick.normalized;
		var speedX = 0.0f;
		var speedY = 0.0f;

		if ( Mathf.Abs (joystick.x) > 0.5f)
		{
			speedX = m_moveSpeed * Mathf.Sign (joystick.x);
		}
			

		m_rigidBody.velocity = new Vector2 (speedX,m_rigidBody.velocity.y);

		Debug.Log (speedX);
	}

	void OnJump()
	{
		m_rigidBody.AddForce (new Vector2 (0, m_jumpForce));
	}
}


