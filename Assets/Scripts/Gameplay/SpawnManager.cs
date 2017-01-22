using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using u3dExtensions.Events;


public class SpawnManager : MonoBehaviour {

	[SerializeField]
	GameObject[] m_enemies;

	[SerializeField]
	GameObject m_spawnFx;

	[SerializeField]
	Text m_score;

	[SerializeField]
	Text m_finalScore;

	int m_currentWave = 1;

	int m_scorePoints = 0;


	[SerializeField]
	GameObject[] m_spawnPoints;

	void Start ()
	{
		StartCoroutine (Wave());

		m_score.text = m_scorePoints.ToString ();
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
		var enemyPrefab = m_enemies[Random.Range (0, m_enemies.Length)];
		var enemy = GameObject.Instantiate (enemyPrefab,spawnPoint.transform.position,Quaternion.identity).GetComponent<EnemyGroundMovement>();
		enemy.OnDeath.Register(DelegateEventListeners.Once<Vector2>((pos)=>{


			m_finalScore.text = m_score.text = (++m_scorePoints).ToString();
		}));

		GameObject.Instantiate (m_spawnFx,spawnPoint.transform.position,Quaternion.identity);

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
