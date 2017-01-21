using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	float m_deadRadius=2.0f;

	[SerializeField]
	float m_lerpSpeed=5.0f;

	Transform m_target;

	void Update ()
	{
		var pos1 = (Vector2)transform.position;

		var pos2 = (Vector2)m_target.position;
		var delta = (pos2 - pos1);

		if (delta.sqrMagnitude > m_deadRadius*m_deadRadius) 
		{
			pos2 = new Vector2 (pos2.x*1.05f,pos2.y);
			var target=Vector2.Lerp (pos1, pos2,Time.deltaTime * m_lerpSpeed) ;

			//transform.Translate (delta* Time.deltaTime * m_lerpSpeed);

			transform.position = new Vector3 (target.x, target.y, transform.position.z);
		}
	}

	public void SetTarget(Transform target)
	{
		m_target = target;
	}
}
