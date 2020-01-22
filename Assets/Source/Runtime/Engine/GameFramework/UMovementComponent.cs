using System;
using UnityEngine;

namespace Epic.Engine.GameFramework
{
	[System.Serializable]
	public abstract class UMovementComponent : UActorComponent
	{
		/** Current velocity of updated component. */
		public Vector3 velocity = default;

		protected Vector3 planeConstraintNormal = default;
		protected Vector3 planeConstraintOrigin = default;

		public sbyte updatedOnlyIfRendered = 1;
		public sbyte autoUpdateTickRegistration = 1;
		public sbyte tickBeforeOwner = 1;
		public sbyte autoRegisterUpdatedComponent = 1;
		public sbyte constrainToPlane = 1;
		public sbyte snapToPlaneAtStart = 1;
		public sbyte autoRegisterPhysicsVolumeUpdates = 1;
		public sbyte componentShouldUpdatePhysicsVolume = 1;

#if UNITY_EDITOR
		sbyte editorWarnedStaticMobilityMove = 1;
#endif
		bool inOnRegister = false;
		bool inInitializedComponent = false;
		EPlaneConstraintAxisSetting planeConstraintAxisSetting = default;

		public UMovementComponent()
		{
			// primaryComponentTick
		}

		/// <summary>
		/// Helper to compute the plane constraint axis from the current setting.
		/// </summary>
		/// <param name="axisSetting">AxisSetting Setting to use when computing the axis.</param>
		/// <returns>Plane constraint axis/normal.</returns>
		protected Vector3 GetPlaneConstraintNormalFromAxisSetting(EPlaneConstraintAxisSetting axisSetting)
		{
			throw new NotImplementedException();
		}

		#region Actor Component Interface

		public virtual void TickComponent(float deltaTime, ELevelTick ParticleSystemAnimationType,
			FActorComponentTickFunction thisTickFunction)
		{
			throw new NotImplementedException();
		}

		public virtual void RegisterComponentTickFunctions(bool register)
		{
		}

		public virtual void PostLoad()
		{
		}

		public virtual void Deactivate()
		{
		}

		public virtual void Serialize(Archive ar)
		{
		}

#if UNITY_EDITOR
		public virtual void PostEditChangeProperty(PropertyChangedEvent propertyChangedEvent)
		{
		}

		public static void PhysicsLockedAxisSettingChanged()
		{
		}

#endif

		#endregion

		public virtual float GetGravityZ()
		{
			throw new NotImplementedException();
		}

		public virtual float GetMaxSpeed()
		{
			return 0f;
		}

		public virtual float GetMaxSpeedModifier()
		{
			throw new NotImplementedException();
		}

		public virtual float K2_GetMaxSpeedModifier()
		{
			throw new NotImplementedException();
		}

		public virtual float GetModifiedMaxSpeed()
		{
			throw new NotImplementedException();
		}

		public virtual float K2_GetModifiedMaxSpeed()
		{
			throw new NotImplementedException();
		}

		public virtual bool IsExceedingMaxSpeed(float maxSpeed)
		{
			throw new NotImplementedException();
		}

		public virtual void StopMovementImmediately()
		{
			throw new NotImplementedException();
		}

		public virtual bool ShouldSkipUpdate(float deltaTime)
		{
			throw new NotImplementedException();
		}

		public virtual PhysicsVolume GetPhysicsVolume()
		{
			throw new NotImplementedException();
		}

		public virtual void PhysicsVolumeChanged(PhysicsVolume newVolume)
		{
			throw new NotImplementedException();
		}

		public virtual void SetUpdatedComponent(Component newUpdatedComponent)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsInWater()
		{
			throw new NotImplementedException();
		}

		public virtual void UpdateTickRegistration()
		{
			throw new NotImplementedException();
		}

		/** 
	 * Called for Blocking impact
	 * @param Hit: Describes the collision.
	 * @param TimeSlice: Time period for the simulation that produced this hit.  Useful for
	 *		  putting Hit.Time in context.  Can be zero in certain situations where it's not appropriate, 
	 *		  be sure to handle that.
	 * @param MoveDelta: Attempted move that resulted in the hit.
	 */
		public virtual void HandleImpact(FHitResult hit, float timeSlice, Vector3 moveDelta)
		{
			throw new NotImplementedException();
		}

		/** Update ComponentVelocity of UpdatedComponent. This needs to be called by derived classes at the end of an update whenever Velocity has changed.	 */
		public virtual void UpdateComponentVelocity()
		{
		}

		/** Initialize collision params appropriately based on our collision settings. Use this before any Line, Overlap, or Sweep tests. */
		public virtual void InitCollisionParams(CollisionQueryParams outParams, CollisionQueryParams outResponseParam)
		{
		}

		/** Return true if the given collision shape overlaps other geometry at the given location and rotation. The collision params are set by InitCollisionParams(). */
		public virtual bool OverlapTest(Vector3 location, Quaternion rotationQuat, ECollisionChannel CollisionChannel,
			CollisionShape CollisionShape, GameObject IgnoreActor)
		{
			throw new NotImplementedException();
		}

		/**
	 * Moves our UpdatedComponent by the given Delta, and sets rotation to NewRotation. Respects the plane constraint, if enabled.
	 * @note This simply calls the virtual MoveUpdatedComponentImpl() which can be overridden to implement custom behavior.
	 * @note The overload taking rotation as an Quaternion is slightly faster than the version using FRotator (which will be converted to an Quaternion).
	 * @note The 'Teleport' flag is currently always treated as 'None' (not teleporting) when used in an active FScopedMovementUpdate.
	 * @return True if some movement occurred, false if no movement occurred. Result of any impact will be stored in OutHit.
	 */
		public bool MoveUpdatedComponent(Vector3 delta, Quaternion newRotation, bool
			sweep, FHitResult outHit = default, ETeleportType teleport = ETeleportType.None)
		{
			throw new NotImplementedException();
		}

		public bool MoveUpdatedComponent(Vector3 delta, Rotator newRotation, bool
			sweep, FHitResult outHit = default, ETeleportType teleport = ETeleportType.None)
		{
			throw new NotImplementedException();
		}

		protected virtual bool MoveUpdatedComponentImpl(Vector3 Delta, Quaternion NewRotation, bool bSweep,
			FHitResult OutHit = default, ETeleportType Teleport = ETeleportType.None)
		{
			throw new NotImplementedException();
		}

		/**
	 * Moves our UpdatedComponent by the given Delta, and sets rotation to NewRotation.
	 * Respects the plane constraint, if enabled.
	 * @return True if some movement occurred, false if no movement occurred. Result of any impact will be stored in OutHit.
	 */
		public bool K2_MoveUpdatedComponent(Vector3 Delta, Rotator NewRotation, FHitResult OutHit, bool bSweep = true,
			bool bTeleport = false)
		{
			throw new NotImplementedException();
		}

		/**
	 * Calls MoveUpdatedComponent(), handling initial penetrations by calling ResolvePenetration().
	 * If this adjustment succeeds, the original movement will be attempted again.
	 * @note The overload taking rotation as an Quaternion is slightly faster than the version using FRotator (which will be converted to an Quaternion).
	 * @note The 'Teleport' flag is currently always treated as 'None' (not teleporting) when used in an active FScopedMovementUpdate.
	 * @return result of the final MoveUpdatedComponent() call.
	 */
		bool SafeMoveUpdatedComponent(Vector3 Delta, Quaternion NewRotation, bool bSweep, FHitResult OutHit,
			ETeleportType Teleport = ETeleportType.None)
		{
			throw new NotImplementedException();
		}

		bool SafeMoveUpdatedComponent(Vector3 Delta, Rotator NewRotation, bool bSweep, FHitResult OutHit,
			ETeleportType Teleport = ETeleportType.None)
		{
			throw new NotImplementedException();
		}

		/**
	 * Calculate a movement adjustment to try to move out of a penetration from a failed move.
	 * @param Hit the result of the failed move
	 * @return The adjustment to use after a failed move, or a zero vector if no attempt should be made.
	 */
		public virtual Vector3 GetPenetrationAdjustment(FHitResult Hit)
		{
			throw new NotImplementedException();
		}

		/**
	 * Try to move out of penetration in an object after a failed move. This function should respect the plane constraint if applicable.
	 * @note This simply calls the virtual ResolvePenetrationImpl() which can be overridden to implement custom behavior.
	 * @note The overload taking rotation as an Quaternion is slightly faster than the version using FRotator (which will be converted to an Quaternion)..
	 * @param Adjustment	The requested adjustment, usually from GetPenetrationAdjustment()
	 * @param Hit			The result of the failed move
	 * @return True if the adjustment was successful and the original move should be retried, or false if no repeated attempt should be made.
	 */
		bool ResolvePenetration(Vector3 Adjustment, FHitResult Hit, Quaternion NewRotation)
		{
			throw new NotImplementedException();
		}

		bool ResolvePenetration(Vector3 Adjustment, FHitResult Hit, Rotator NewRotation)
		{
			throw new NotImplementedException();
		}

		protected virtual bool ResolvePenetrationImpl(Vector3 Adjustment, FHitResult Hit, Quaternion NewRotation)
		{
			throw new NotImplementedException();
		}

// public here

		/**
	 * Compute a vector to slide along a surface, given an attempted move, time, and normal.
	 * @param Delta:	Attempted move.
	 * @param Time:		Amount of move to apply (between 0 and 1).
	 * @param Normal:	Normal opposed to movement. Not necessarily equal to Hit.Normal.
	 * @param Hit:		HitResult of the move that resulted in the slide.
	 */
		public virtual Vector3 ComputeSlideVector(Vector3 Delta, float Time, Vector3 Normal, FHitResult Hit)
		{
			throw new NotImplementedException();
		}

		/**
	 * Slide smoothly along a surface, and slide away from multiple impacts using TwoWallAdjust if necessary. Calls HandleImpact for each surface hit, if requested.
	 * Uses SafeMoveUpdatedComponent() for movement, and ComputeSlideVector() to determine the slide direction.
	 * @param Delta:	Attempted movement vector.
	 * @param Time:		Percent of Delta to apply (between 0 and 1). Usually equal to the remaining time after a collision: (1.0 - Hit.Time).
	 * @param Normal:	Normal opposing movement, along which we will slide.
	 * @param Hit:		[In] HitResult of the attempted move that resulted in the impact triggering the slide. [Out] HitResult of last attempted move.
	 * @param bHandleImpact:	Whether to call HandleImpact on each hit.
	 * @return The percentage of requested distance (Delta * Percent) actually applied (between 0 and 1). 0 if no movement occurred, non-zero if movement occurred.
	 */
		public virtual float SlideAlongSurface(Vector3 Delta, float Time, Vector3 Normal, FHitResult Hit,
			bool bHandleImpact = false)
		{
			throw new NotImplementedException();
		}

		/**
	 * Compute a movement direction when contacting two surfaces.
	 * @param Delta:		[In] Amount of move attempted before impact. [Out] Computed adjustment based on impacts.
	 * @param Hit:			Impact from last attempted move
	 * @param OldHitNormal:	Normal of impact before last attempted move
	 * @return Result in Delta that is the direction to move when contacting two surfaces.
	 */
		public virtual void TwoWallAdjust(Vector3 Delta, FHitResult Hit, Vector3 OldHitNormal)
		{
			throw new NotImplementedException();
		}

		/**
	 * Adds force from radial force components.
	 * Intended to be overridden by subclasses		{
			throw new NotImplementedException();
		} default implementation does nothing.
	 * @param	Origin		The origin of the force
	 * @param	Radius		The radius in which the force will be applied
	 * @param	Strength	The strength of the force
	 * @param	Falloff		The falloff from the force's origin
	 */
		public virtual void AddRadialForce(Vector3 Origin, float Radius, float Strength, ERadialImpulseFalloff Falloff)
		{
			throw new NotImplementedException();
		}

		/**
	 * Adds impulse from radial force components.
	 * Intended to be overridden by subclasses		{
			throw new NotImplementedException();
		} default implementation does nothing.
	 * @param	Origin		The origin of the force
	 * @param	Radius		The radius in which the force will be applied
	 * @param	Strength	The strength of the force
	 * @param	Falloff		The falloff from the force's origin
	 * @param	bVelChange	If true, the Strength is taken as a change in velocity instead of an impulse (ie. mass will have no effect).
	 */
		public virtual void AddRadialImpulse(Vector3 Origin, float Radius, float Strength,
			ERadialImpulseFalloff Falloff, bool bVelChange)
		{
			throw new NotImplementedException();
		}

		/**
	 * Set the plane constraint axis setting.
	 * Changing this setting will modify the current value of PlaneraintNormal.
	 * 
	 * @param  NewAxisSetting New plane constraint axis setting.
	 */
		public virtual void SetPlaneraintAxisSetting(EPlaneConstraintAxisSetting NewAxisSetting)
		{
			throw new NotImplementedException();
		}

		/**
	 * Get the plane constraint axis setting.
	 */
		EPlaneConstraintAxisSetting GetPlaneraintAxisSetting()
		{
			throw new NotImplementedException();
		}

		/**
	 * Sets the normal of the plane that rains movement, enforced if the plane constraint is enabled.
	 * Changing the normal automatically sets PlaneraintAxisSetting to "Custom".
	 *
	 * @param PlaneNormal	The normal of the plane. If non-zero in length, it will be normalized.
	 */
		public virtual void SetPlaneraintNormal(Vector3 PlaneNormal)
		{
			throw new NotImplementedException();
		}

		/** Uses the Forward and Up vectors to compute the plane that rains movement, enforced if the plane constraint is enabled. */
		public virtual void SetPlaneraintFromVectors(Vector3 Forward, Vector3 Up)
		{
			throw new NotImplementedException();
		}

		/** Sets the origin of the plane that rains movement, enforced if the plane constraint is enabled. */
		public virtual void SetPlaneraintOrigin(Vector3 PlaneOrigin)
		{
			throw new NotImplementedException();
		}

		/** Sets whether or not the plane constraint is enabled. */
		public virtual void SetPlaneraintEnabled(bool bEnabled)
		{
			throw new NotImplementedException();
		}

		/** Returns the normal of the plane that rains movement, enforced if the plane constraint is enabled. */
		Vector3 GetPlaneraintNormal()
		{
			throw new NotImplementedException();
		}

		/**
	 * Get the plane constraint origin. This defines the behavior of snapping a position to the plane, such as by SnapUpdatedComponentToPlane().
	 * @return The origin of the plane that rains movement, if the plane constraint is enabled.
	 */
		Vector3 GetPlaneraintOrigin()
		{
			throw new NotImplementedException();
		}

		/**
	 * rain a direction vector to the plane constraint, if enabled.
	 * @see SetPlaneraint
	 */
		public virtual Vector3 rainDirectionToPlane(Vector3 Direction)
		{
			throw new NotImplementedException();
		}

		/** rain a position vector to the plane constraint, if enabled. */
		public virtual Vector3 rainLocationToPlane(Vector3 Location)
		{
			throw new NotImplementedException();
		}

		/** rain a normal vector (of unit length) to the plane constraint, if enabled. */
		public virtual Vector3 rainNormalToPlane(Vector3 Normal)
		{
			throw new NotImplementedException();
		}

		/** Snap the updated component to the plane constraint, if enabled. */
		public virtual void SnapUpdatedComponentToPlane()
		{
			throw new NotImplementedException();
		}

		/** Called by owning Actor upon successful teleport from AActor::TeleportTo(). */
		public virtual void OnTeleported()
		{
			throw new NotImplementedException();
		}

		#region INLINE

		public EPlaneConstraintAxisSetting GetPlaneConstraintAxisSetting() => planeConstraintAxisSetting;

		#endregion
	}

	public struct FHitResult
	{
	}

	public struct Rotator
	{
		/** Rotation around the right axis (around Y axis), Looking up and down (0=Straight Ahead, +Up, -Down) */
		public float Pitch;

		/** Rotation around the up axis (around Z axis), Running in circles 0=East, +North, -South. */
		public float Yaw;

		/** Rotation around the forward axis (around X axis), Tilting your head, 0=Straight, +Clockwise, -CCW. */
		public float Roll;
	}

	public class FActorComponentTickFunction : FTickFunction
	{
		public UActorComponent target;
		
		
	}

	public class FTickTaskLevel
	{
		
	}

	public struct FTickPrerequisite
	{
		
	}

	public struct PropertyChangedEvent
	{
	}

	public class Archive
	{
	}

	public class PhysicsVolume
	{
	}

	public struct CollisionQueryParams
	{
	}

	public struct CollisionShape
	{
	}
}