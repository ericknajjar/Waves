using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using u3dExtensions;
using u3dExtensions.IOC.extensions;
using u3dExtensions.IOC;
using u3dExtensions.Engine.Runtime;
using u3dExtensions.Events;

public struct PowerStick
{

	public static PowerStick Construt(int s,Vector2 dir)
	{
		PowerStick ret;
		ret.PowerSwitch = s;
		ret.PowerDirection = dir;

		return ret;
	}


	public int PowerSwitch ;
	public Vector2 PowerDirection;
}

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

	public PowerStick GetPower(Vector2 myPos){

		PowerStick power = PowerStick.Construt (0, Vector2.zero);
		var mousePos = Input.mousePosition;
		mousePos.z = 0.0f;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		var vector = (Vector2)mousePos - myPos;

		int powerButton = 0;

		if(Input.GetMouseButton(0))
		{
			powerButton = 1;
		}

		if (Input.GetMouseButton (1)) {
			powerButton = -1;
		}

		if (vector.sqrMagnitude > 0.01f) {

			power = PowerStick.Construt (powerButton, vector.normalized);
		}
			
		return power;
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
