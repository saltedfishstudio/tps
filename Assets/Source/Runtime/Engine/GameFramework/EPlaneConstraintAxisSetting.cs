namespace Epic.Engine.GameFramework
{
	public enum EPlaneConstraintAxisSetting : uint
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
}