using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {

	[SerializeField]
	float m_time = 2.0f;

	IEnumerator Start ()
	{
		yield return new WaitForSeconds(m_time);
		
	}
	

}
