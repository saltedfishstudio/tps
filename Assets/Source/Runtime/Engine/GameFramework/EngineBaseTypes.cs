namespace Epic.Engine.GameFramework
{
	public enum ETickingGroup
	{
		/** Any item that needs to be executed before physics simulation starts. */
		TG_PrePhysics ,// UMETA(DisplayName="Pre Physics"),

		/** Special tick group that starts physics simulation. */							
		TG_StartPhysics , // UMETA(Hidden, DisplayName="Start Physics"),

		/** Any item that can be run in parallel with our physics simulation work. */
		TG_DuringPhysics , // UMETA(DisplayName="During Physics"),

		/** Special tick group that ends physics simulation. */
		TG_EndPhysics , // UMETA(Hidden, DisplayName="End Physics"),

		/** Any item that needs rigid body and cloth simulation to be complete before being executed. */
		TG_PostPhysics , // UMETA(DisplayName="Post Physics"),

		/** Any item that needs the update work to be done before being ticked. */
		TG_PostUpdateWork , // UMETA(DisplayName="Post Update Work"),

		/** Catchall for anything demoted to the end. */
		TG_LastDemotable , // UMETA(Hidden, DisplayName = "Last Demotable"),

		/** Special tick group that is not actually a tick group. After every tick group this is repeatedly re-run until there are no more newly spawned items to run. */
		TG_NewlySpawned , // UMETA(Hidden, DisplayName="Newly Spawned"),

		TG_MAX,
	};
}