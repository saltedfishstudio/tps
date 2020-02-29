using System;
using UnityEngine;

namespace Epic.Engine.GameFramework
{
	[Serializable]
	public class FTransform
	{
		public Quaternion rotation;
		public Vector3 translation;
		public Vector3 scale3d;
	}
}