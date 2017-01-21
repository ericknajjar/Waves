using UnityEngine;
using System.Collections;
using u3dExtensions.IOC;
using u3dExtensions.Engine.Runtime;
using u3dExtensions.IOC.extensions;

public class Bootstrap : MonoBehaviour 
{

	IBindingContext m_masterContext;

	void Start()
	{
		DontDestroyOnLoad (gameObject);

		var bindingFinder = new ReflectiveBindingFinder (GetType ().Assembly);

		m_masterContext = new ReflectiveBindingContextFactory (bindingFinder).CreateContext();

		m_masterContext.Get<IPlayer> (InnerBindingNames.Empty,Camera.main.transform, new Vector3(-101.0f,5.0f,0.0f));

	}
}