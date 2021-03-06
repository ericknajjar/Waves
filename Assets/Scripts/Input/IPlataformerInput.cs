﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using u3dExtensions.Events;

public interface IPlataformerInput  {
	
	IEventRegister OnJumpClick{ get;}

	Vector2 Joystick{ get;}

	PowerStick GetPower (Vector2 myPos);
}
