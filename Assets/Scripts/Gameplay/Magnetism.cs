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
		var power = m_input.GetPower (transform.position);
		Debug.Log (power.PowerSwitch);
		if (power.PowerSwitch != 0)
		{
			var arr = new string[]{"Ground","Wall"};
			var hit = Physics2D.Raycast(transform.position,power.PowerDirection,7.0f,LayerMask.GetMask(arr));

			if (hit.collider!=null && hit.collider.tag=="metal")
			{
				var responders = hit.collider.GetComponents<IMagnectiResponder> ();

				foreach (var responder in responders)
				{
					responder.Affect (this,power.PowerDirection*power.PowerSwitch,m_pullPower);
				}
				Debug.Log (hit.collider);
			}
		}
	}

	public Vector2 Position {
		get {
			return transform.position;
		}
	}
}
