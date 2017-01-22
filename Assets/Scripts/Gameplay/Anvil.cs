using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour {

	Vector2 m_respawnPoint;
	void Start()
	{
		m_respawnPoint = transform.position;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag ("killAnvil")) {

			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			GetComponent<Rigidbody2D> ().angularVelocity = 0.0f;
			transform.position = m_respawnPoint;
		}
	}
}
