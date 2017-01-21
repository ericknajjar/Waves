using UnityEngine;
using System.Collections;
using u3dExtensions.IOC;

public interface IGameEntity
{
	IBindingContext Context{ get;}

}

public class EntityContext : MonoBehaviour, IGameEntity
{
	IBindingContext m_context;


	public IBindingContext Context
	{
		get
		{
			if (m_context == null) 
			{
				m_context = BindingContext.Create ();

				m_context.Bind<IGameEntity> ().To (() => this);
			}

			return m_context;
		}
	}
}

