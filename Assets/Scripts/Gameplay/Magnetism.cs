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
	ParticleSystem m_pull;

	[SerializeField]
	ParticleSystem m_push;

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
	
		if (power.PowerSwitch != 0) {
			
			var arr = new string[]{ "Ground", "Wall", "Anvil" };
			var hits = Physics2D.RaycastAll (transform.position, power.PowerDirection, 20.0f, LayerMask.GetMask (arr));

			if (power.PowerSwitch == 1) {
				m_pull.gameObject.SetActive (true);
				m_pull.transform.forward = power.PowerDirection;

			} else {
				m_push.gameObject.SetActive (true);
				m_push.transform.forward = power.PowerDirection;
			}

			foreach (var hit in hits) {
				if (hit.collider != null && hit.collider.tag == "metal") {
					var responders = hit.collider.GetComponents<IMagnectiResponder> ();

					foreach (var responder in responders) {
						responder.Affect (this, power.PowerDirection * power.PowerSwitch, m_pullPower);
					}
				}
			}

		} else {
			m_pull.gameObject.SetActive (false);
			m_push.gameObject.SetActive (false);
		}
	}

	public Vector2 Position {
		get {
			return transform.position;
		}
	}
}
