using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagneticResponse
{
	void ApplyForce (Vector2 force);
	Vector2 Position{ get;}
}

public enum ForceDirection
{
	Pull = -1, Push = 1
}
public class Magnetism : Entity,IMagneticResponse {

	[SerializeField]
	float m_pullPower=500;

	[SerializeField]
	Collider2D[] m_magneticAreas;

	Rigidbody2D m_rigidBody;
	IPlataformerInput m_input;


	void Awake () {
		m_rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Start()
	{
		m_input = Context.Get<IPlataformerInput> ();
	}

	public void ApplyForce(Vector2 force)
	{
		m_rigidBody.AddForce (force * Time.fixedDeltaTime);
	}

	void FixedUpdate () 
	{
		if (m_input.PowerStick.sqrMagnitude >= 0.01f)
		{
			var arr = new string[]{"Ground","Wall"};
			var hit = Physics2D.Raycast(transform.position,m_input.PowerStick.normalized,7.0f,LayerMask.GetMask(arr));

			Debug.Log (hit.collider);
			if (hit.collider!=null && hit.collider.tag=="metal")
			{
				var responder = hit.collider.GetComponent<IMagnectiResponder> ();

				if (responder != null)
					responder.Affect (this,m_input.PowerStick,m_pullPower);

			}

		}
	}

	public Vector2 Position {
		get {
			return transform.position;
		}
	}
}
