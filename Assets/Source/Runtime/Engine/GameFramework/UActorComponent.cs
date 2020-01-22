using Epic.Core;

namespace Epic.Engine.GameFramework
{
	[System.Serializable]
	public abstract class UActorComponent : UnrealObject
	{
		public FActorComponentTickFunction primaryComponentTick;
	}

}