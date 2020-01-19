namespace Epic.Engine.GameFramework
{
	public enum EPlaneConstraintAxisSetting : sbyte
	{
		/** Lock movement to a user-defined axis. */
		Custom,
		/** Lock movement in the X axis. */
		X,
		/** Lock movement in the Y axis. */
		Y,
		/** Lock movement in the Z axis. */
		Z,
		/** Use the global physics project setting. */
		UseGlobalPhysicsSetting
	};

	public enum ELevelTick
	{
		/** Update the level time only. */
		LEVELTICK_TimeOnly = 0,

		/** Update time and viewports. */
		LEVELTICK_ViewportsOnly = 1,

		/** Update all. */
		LEVELTICK_All = 2,

		/** Delta time is zero, we are paused. Components don't tick. */
		LEVELTICK_PauseTick = 3,
	};
	
	public enum ECollisionChannel
	{

		ECC_WorldStatic , // World Static
		ECC_WorldDynamic , // World Dynamic
		ECC_Pawn , // Pawn
		ECC_Visibility, // Visibility
		ECC_Camera, // Camera
		ECC_PhysicsBody , // Physics Body
		ECC_Vehicle , // Vehicle
		ECC_Destructible , // Destructible

		/** Reserved for gizmo collision */
		ECC_EngineTraceChannel1 ,

		ECC_EngineTraceChannel2 ,
		ECC_EngineTraceChannel3 ,
		ECC_EngineTraceChannel4 , 
		ECC_EngineTraceChannel5 ,
		ECC_EngineTraceChannel6 ,

		ECC_GameTraceChannel1 ,
		ECC_GameTraceChannel2 ,
		ECC_GameTraceChannel3 ,
		ECC_GameTraceChannel4 ,
		ECC_GameTraceChannel5 ,
		ECC_GameTraceChannel6 ,
		ECC_GameTraceChannel7 ,
		ECC_GameTraceChannel8 ,
		ECC_GameTraceChannel9 ,
		ECC_GameTraceChannel10 ,
		ECC_GameTraceChannel11 ,
		ECC_GameTraceChannel12 ,
		ECC_GameTraceChannel13 ,
		ECC_GameTraceChannel14 ,
		ECC_GameTraceChannel15 ,
		ECC_GameTraceChannel16 ,
		ECC_GameTraceChannel17 ,
		ECC_GameTraceChannel18 ,
	
		/** Add new serializeable channels above here (i.e. entries that exist in FCollisionResponseContainer) */
		/** Add only nonserialized/transient flags below */

		// NOTE!!!! THESE ARE BEING DEPRECATED BUT STILL THERE FOR BLUEPRINT. PLEASE DO NOT USE THEM IN CODE

		ECC_OverlapAll_Deprecated ,
		ECC_MAX,
	};
	
	public enum ETeleportType : sbyte
	{
		/** Do not teleport physics body. This means velocity will reflect the movement between initial and final position, and collisions along the way will occur */
		None,

		/** Teleport physics body so that velocity remains the same and no collision occurs */
		TeleportPhysics,

		/** Teleport physics body and reset physics state completely */
		ResetPhysics,
	};
	
	public enum ERadialImpulseFalloff
	{
		/** Impulse is a constant strength, up to the limit of its range. */
		RIF_Constant,
		/** Impulse should get linearly weaker the further from origin. */
		RIF_Linear,
		RIF_MAX,
	};
}