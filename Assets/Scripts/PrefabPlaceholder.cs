using UnityEngine;
using System.Collections;

public class PrefabPlaceholder : MonoBehaviour
{

	[SerializeField]
	GameObject m_prefab;

	void Awake ()
	{
		if (transform.parent != null)
			GameObject.Instantiate (m_prefab, transform.position, Quaternion.identity, transform.parent);
		else
			GameObject.Instantiate (m_prefab, transform.position, Quaternion.identity);

		Destroy (gameObject);
	}

}

