using UnityEngine;
using System.Collections;
using u3dExtensions.IOC;
using u3dExtensions.Engine.Runtime;
using u3dExtensions.IOC.extensions;
using UnityEngine.SceneManagement;
using u3dExtensions.Events;
public class Bootstrap : MonoBehaviour 
{
	[SerializeField]
	Vector3 m_startPos = new Vector3 (-101.0f, 5.0f, 0.0f);
	[SerializeField]
	GameObject m_gameOverScreen;

	IBindingContext m_masterContext;

	void Start()
	{
		m_startPos = transform.position;
		//DontDestroyOnLoad (gameObject);

		var bindingFinder = new ReflectiveBindingFinder (GetType ().Assembly);

		m_masterContext = new ReflectiveBindingContextFactory (bindingFinder).CreateContext();

		var player = m_masterContext.Get<IPlayer> (InnerBindingNames.Empty,Camera.main.GetComponent<CameraFollow>(),m_startPos);

		player.OnDeath.Register (() => {
			Time.timeScale = 0.0f;
			m_gameOverScreen.SetActive(true);
		});
		Camera.main.transform.localPosition = new Vector3 (0.0f, 0.0f, Camera.main.transform.localPosition.z);
	}

	public void Restart()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("scene");
	}
}