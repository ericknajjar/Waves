using UnityEngine;
using System.Collections;

public interface IMagnectiResponder
{
	bool Affect (IMagneticResponse response,Vector2 direction, float power);
}

public class MagneticResponder : MonoBehaviour,IMagnectiResponder
{

	public bool Affect(IMagneticResponse response,Vector2 direction, float power)
	{

		response.ApplyForce (-direction * power);

		return true;
	}

}

