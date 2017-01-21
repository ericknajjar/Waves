using UnityEngine;
using System.Collections;

public class MagnetismResponder2 : MonoBehaviour,IMagnectiResponder
{

	public bool Affect(IMagneticResponse response,Vector2 direction, float power)
	{

		Debug.Log ("aqui");
		GetComponent<Rigidbody2D> ().AddForce (direction * power * Time.deltaTime);
		//response.ApplyForce (-direction * power);

		return true;
	}

}

