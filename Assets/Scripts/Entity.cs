using UnityEngine;
using System.Collections;
using u3dExtensions.IOC;

public class Entity : MonoBehaviour
{
	EntityContext m_entityContext;

	public IBindingContext Context {
		get {

			if (m_entityContext == null)
			{
				m_entityContext = GetComponent<EntityContext> ();
				if (m_entityContext == null)
					m_entityContext = gameObject.AddComponent<EntityContext> ();
			}


			return m_entityContext.Context;
		}
	}


}

