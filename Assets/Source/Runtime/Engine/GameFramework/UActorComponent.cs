using System;
using Epic.Core;
using UnityEngine;

namespace Epic.Engine.GameFramework
{
	[System.Serializable]
	public abstract class UActorComponent : UnrealObject
	{
		public FActorComponentTickFunction primaryComponentTick;
		public Action globalCreatePhysicsDelegate;
		public Action globalDestroyPhysicsDelegate;
		public string[] componentTags;

		int markedForEndOfFrameUpdateArrayIndex;

		protected int registered;
		protected int renderStateCreated;
		protected int physicsStateCreated;
		protected int replicates;
		protected int netAddressable;
		
		int renderStateDirty;
		int renderTransformDirty;
		int renderDynamicDataDirty;
		int routedPostRename;

		public bool autoRegister;
		protected bool allowReregistration;
		
		// UE4.24.1::Line:164

		bool canUseCachedOwner;
		AActor ownerPrivate;

		#region Interface

		public virtual void OnRegister()
		{
			
		}

		public virtual void TickComponent(float deltaTime, ELevelTick tickType, FActorComponentTickFunction thisTickFunction)
		{
			
		}

		public virtual void PostLoad()
		{
			
		}

		public virtual void ApplyWorldOffset(Vector3 inOffset, bool worldShift)
		{
			
		}
		
		#endregion

		protected virtual Quaternion GetComponentRotation()
		{
			return transform.rotation;
		}

		protected AActor GetOwner()
		{
#if UNITY_EDITOR
			if (canUseCachedOwner)
			{
				return ownerPrivate;
			}
			else
			{
				return GetActorOwnerNoninline();
			}
#else
			return ownerPrivate;
#endif
		}

		AActor GetActorOwnerNoninline()
		{
			return null;
		}
	}
}