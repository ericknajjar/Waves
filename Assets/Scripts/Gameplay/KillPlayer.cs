using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : Entity {

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag ("killPlayer")) {
			SceneManager.LoadScene ("scene");
		}
	}
}
