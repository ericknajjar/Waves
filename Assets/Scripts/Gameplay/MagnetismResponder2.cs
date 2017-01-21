using UnityEngine;
using System.Collections;

public class MagnetismResponder2 : MonoBehaviour,IMagnectiResponder
{

	public bool Affect(IMagneticResponse response,Vector2 direction, float power)
	{
		GetComponent<Rigidbody2D> ().AddForce (direction * power);
		//response.ApplyForce (-direction * power);

		return true;
	}

}

