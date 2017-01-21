using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : Entity {

	[SerializeField]
	float m_pullPower=500;

	Rigidbody2D m_rigidBody;
	IPlataformerInput m_input;


	void Awake () {
		m_rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Start()
	{
		m_input = Context.Get<IPlataformerInput> ();
	}

	void FixedUpdate () 
	{

		var pull = -m_input.PowerStick * m_pullPower;

		if (pull.sqrMagnitude >= 0.01f)
		{
			var arr = new string[]{"Ground","Wall"};
			var hit = Physics2D.Raycast(transform.position,-m_input.PowerStick.normalized,20.0f,LayerMask.GetMask(arr));

			if (hit.collider!=null && hit.collider.tag=="metal")
			{
				m_rigidBody.AddForce (pull * Time.fixedDeltaTime);
				Debug.Log ("forceeee");
			}

		}
	}
}
