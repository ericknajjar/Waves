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

	Animator m_animator;

	public bool IsGrounded {
		get;
		private set;
	}

	public void Start()
	{
		m_animator = GetComponent<Animator> ();
		m_collider = GetComponent<Collider2D> ();

		UpdateGrounded ();

		m_rigidBody = GetComponent<Rigidbody2D> ();
		m_input = Context.Get<IPlataformerInput> ();

		var lister = new DelegateEventListener (OnJump, () => this == null);
		m_input.OnJumpClick.Register (lister);
	}

	void UpdateGrounded()
	{
		var bounds = m_collider.bounds;
		bounds.Expand(new Vector3(1.1f,1.0f,1.0f));
		var allHits = Physics2D.BoxCastAll (transform.position, bounds.extents,0.0f, Vector2.down, m_collider.bounds.extents.y/2, LayerMask.GetMask ("Ground"));

		bool grounded = false;
		foreach (var hit in allHits) {
			

			var angle = Vector2.Angle(Vector2.up,((Vector2)transform.position - hit.point).normalized); 
			grounded = angle < 30.0f;
		
			if (grounded)
				break;
		}

		IsGrounded = grounded;
	}

	void FixedUpdate()
	{
		UpdateGrounded ();

		var joystick = m_input.Joystick.normalized;
		var speedX = 0.0f;

		if(joystick.x > 0)
		{
			

			m_animator.SetFloat ("walkLeft",joystick.x);
			m_animator.SetFloat ("walkRight",0);
		}
		else
		{
			m_animator.SetFloat ("walkRight",Mathf.Abs (joystick.x));
			m_animator.SetFloat ("walkLeft",0);
		}

		if ( Mathf.Abs (joystick.x) > 0.5f)
		{
			speedX = m_moveSpeed * Mathf.Sign (joystick.x);
		}
			
		if(IsGrounded)
			m_rigidBody.velocity = new Vector2 (speedX,m_rigidBody.velocity.y);
		else
			if(Mathf.Abs (speedX)> 0.5f)
				m_rigidBody.velocity = new Vector2 (speedX,m_rigidBody.velocity.y);
	}

	void OnJump()
	{
		if(IsGrounded)
			m_rigidBody.AddForce (new Vector2 (0, m_jumpForce));
	}
}


