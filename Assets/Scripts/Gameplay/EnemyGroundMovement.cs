using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using u3dExtensions.Events;

public class EnemyGroundMovement : MonoBehaviour {

	bool m_canJump = true;
	[SerializeField]
	float m_topSpeed = 7.0f;

	[SerializeField]
	GameObject m_gore;

	[SerializeField]
	EventSlot<Vector2> m_onDeath = new EventSlot<Vector2>();

	public IEventRegister<Vector2> OnDeath
	{
		get {
			return m_onDeath;
		}
	}

	Vector2 m_walkDir = Vector2.left;

	public bool IsGrounded {
		get;
		private set;
	}

	IEnumerator Start()
	{
		for (;;) {

			var pos = transform.position;
			yield return new WaitForSeconds (0.3f);

			if (Vector2.Distance (pos, transform.position) < 0.1f) {
				m_walkDir *= -1;
				if (IsGrounded)
					Jump ();
				Debug.Log ("flip");
			}
		}
	}

	void Update () 
	{
		UpdateGrounded ();
		var rigidBody = GetComponent<Rigidbody2D> ();
		//if (IsGrounded) {
		var walkForce = IsGrounded?500:200;
		rigidBody.AddForce (m_walkDir * walkForce * Time.deltaTime);
		//}

		if (rigidBody.velocity.x > m_topSpeed)
			rigidBody.velocity = new Vector2 (m_topSpeed,rigidBody.velocity.y);
	}




	void UpdateGrounded()
	{
		var collider = GetComponent<Collider2D> ();
		var bounds = collider.bounds;
		bounds.Expand(new Vector3(1.1f,1.0f,1.0f));
		var allHits = Physics2D.BoxCastAll (transform.position, bounds.extents,0.0f, Vector2.down, collider.bounds.extents.y/2, LayerMask.GetMask ("Ground"));

		bool grounded = false;
		foreach (var hit in allHits) {


			var angle = Vector2.Angle(Vector2.up,((Vector2)transform.position - hit.point).normalized); 
			grounded = angle < 30.0f;

			if (grounded)
				break;
		}

		IsGrounded = grounded;
	}


	void OnCollisionEnter2D(Collision2D c)
	{
		
		if (c.collider.gameObject.name == "Anvil")
		{
			
			var rb = GetComponent<Rigidbody2D>();


			if (c.relativeVelocity.magnitude > GetComponent<Rigidbody2D> ().velocity.magnitude * 1.05f)
			{
				GameObject.Instantiate (m_gore,transform.position, Quaternion.identity);
				m_onDeath.Trigger (transform.position);
				Destroy (gameObject);
			}
		} 
		else if (m_canJump && IsGrounded)
		{
			var angle = Vector2.Angle (Vector2.up, c.contacts [0].normal);

			if (c.collider.CompareTag ("turn")) 
			{
				m_walkDir *= -1;
			}
			else if (angle > 10.0f) {
				Jump ();
			}

		}

		StartCoroutine (ResetCanJump());
	}

	void Jump()
	{
		GetComponent<Rigidbody2D> ().AddForce (Vector2.up*500);
		m_canJump = false;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag ("jump") && IsGrounded) 
		{
			
			if (Random.value > 0.5f) {
				Jump ();
			}
		}
	}

	IEnumerator ResetCanJump()
	{
		yield return new WaitForSeconds (3.0f);
		m_canJump = true;
	}
}
