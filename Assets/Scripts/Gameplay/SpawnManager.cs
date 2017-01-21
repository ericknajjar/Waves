using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField]
	GameObject m_enemy1;

	int m_currentWave = 1;

	[SerializeField]
	GameObject[] m_spawnPoints;

	void Start ()
	{
		StartCoroutine (Wave());
	}


	IEnumerator Wave()
	{
		yield return new WaitForSeconds (3.0f);

		for (int i=0;i<NextSapawnIn;++i) 
		{
			SpawnOne ();
			yield return new WaitForSeconds (NextSapawnIn);
		}

		m_currentWave++;

		StartCoroutine (Wave());
	}

	void SpawnOne()
	{
		var spawnPoint = m_spawnPoints [Random.Range (0, m_spawnPoints.Length)];
		GameObject.Instantiate (m_enemy1,spawnPoint.transform.position,Quaternion.identity);
	}

	float NextSapawnIn
	{
		get {
			return 1.0f/Mathf.Sqrt (m_currentWave);
		}
	}
		
	int AmmountPerWave
	{
		get{ 
		
			return (int)(m_currentWave*1.75f)+5;
		}
	}
}
