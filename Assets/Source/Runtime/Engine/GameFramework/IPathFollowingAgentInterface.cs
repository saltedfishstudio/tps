using Epic.Core;

namespace Epic.Engine.GameFramework
{
	public interface IPathFollowingAgentInterface
	{
		void OnUnableToMove(UnrealObject instigator);
		void OnStartedFalling();
		void OnLanded();
		void OnMoveBlockedBy(HitResult blockingImpact);
	}
}