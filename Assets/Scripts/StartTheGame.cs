using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour {

	public void OnStartClicked()
	{
		SceneManager.LoadScene ("scene");
	}
}
