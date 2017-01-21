using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using u3dExtensions;
using u3dExtensions.IOC.extensions;
using u3dExtensions.IOC;
using u3dExtensions.Engine.Runtime;
using u3dExtensions.Events;

public class UnityPlatformInput : Entity, IPlataformerInput {

	EventSlot m_onJumpClick = new EventSlot();

	public UnityPlatformInput()
	{
		Joystick = Vector2.zero;
	}
		

	#region IPlataformerInput implementation
	public u3dExtensions.Events.IEventRegister OnJumpClick {
		get {
			return m_onJumpClick;
		}
	}
	public Vector2 Joystick {
		get;
		private set;
	}

	public Vector2 PowerStick {
		get;
		private set;
	}
	#endregion

	static UnityPlatformInput s_instance;

	void Update()
	{
		if (Input.GetButtonDown ("Jump")) {
			m_onJumpClick.Trigger ();
		}

		var x = Input.GetAxis ("Horizontal");

		Joystick = new Vector2 (x,0.0f);

		var xPower = Input.GetAxis ("HorizontalPower");
		var yPower = Input.GetAxis ("VerticalPower");

		PowerStick = new Vector2 (xPower, yPower);
	}

	[BindingProvider]
	public static IPlataformerInput Get()
	{
		if (s_instance == null) 
		{
			var go = new GameObject ("__input");

			DontDestroyOnLoad (go);

			s_instance = go.AddComponent<UnityPlatformInput> ();
		}

		return s_instance;
	}
}
