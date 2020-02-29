using System;
using Epic.Engine.GameFramework;
using UnityEngine;

namespace Epic.Engine.GameFramework
{
	public abstract class USceneComponent : UActorComponent
	{
		protected bool autoActivate = true;
		protected bool tickInEditor = true;
		public bool absoluteRotation;
		
		protected virtual void Initialize()
		{
			primaryComponentTick = new FActorComponentTickFunction();
		}

		public bool IsUsingAbsoluteRotation()
		{
			return absoluteRotation;
		}

		public Quaternion GetRelativeRotation()
		{
			return transform.localRotation;
		}

		#region Interface

		public virtual bool HasAnySockets()
		{
			return false;
		}

		public virtual FTransform GetSocketTransform()
		{
			return new FTransform();
		}

		public virtual void QuerySupportedSockets(ref FComponentSocketDescription[] OutSockets)
		{
			
		}

		#endregion
	}
}