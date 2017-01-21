using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using u3dExtensions.Engine.Runtime;

public interface IPlayer
{

}

public class Player : Entity, IPlayer {
	
	[BindingProvider(DependencyCount=1)]
	public static IPlayer Get(IPlataformerInput plataformerInput,Camera camera,Vector3 spawnPoint)
	{
		var prefab = Resources.Load<GameObject> ("Player");

		var go  = GameObject.Instantiate (prefab, spawnPoint, Quaternion.identity);

		var p =  go.GetComponent<Player> ();

		var context = p.Context;
		context.Bind<IPlataformerInput> ().To (() => plataformerInput);
		camera.transform.SetParent (p.transform);
		return p;

	}
}
