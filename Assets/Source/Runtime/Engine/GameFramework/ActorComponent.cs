using Epic.Core;

namespace Epic.Engine.GameFramework
{
	[System.Serializable]
	public abstract class ActorComponent : UnrealObject
	{
		public FActorComponentTickFunction primaryComponentTick;
	}

}