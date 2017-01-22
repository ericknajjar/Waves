using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using u3dExtensions.Engine.Runtime;
using UnityEngine.SceneManagement;
using u3dExtensions.Events;

public interface IPlayer
{
	IEventRegister OnDeath{ get;}
}

public class Player : Entity, IPlayer {

	EventSlot m_onDeath = new EventSlot ();

	#region IPlayer implementation

	public IEventRegister OnDeath {
		get {
			return m_onDeath;
		}
	}

	#endregion

	[BindingProvider(DependencyCount=1)]
	public static IPlayer Get(IPlataformerInput plataformerInput,CameraFollow camera,Vector3 spawnPoint)
	{
		var prefab = Resources.Load<GameObject> ("Player");

		var go  = GameObject.Instantiate (prefab, spawnPoint, Quaternion.identity);

		var p =  go.GetComponent<Player> ();

		var context = p.Context;
		context.Bind<IPlataformerInput> ().To (() => plataformerInput);

		camera.SetTarget (p.transform);

		return p;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag ("killPlayer")) {
			m_onDeath.Trigger ();
		}
	}
}
