using System;
using UnityEngine;

namespace Epic.Engine.GameFramework
{
	public interface INavAgentInterface
	{
		Vector3 GetNavAgentLocation();
		Vector3 GetMoveGoalOffset(AActor movingActor);
		void GetMoveGoalReachTest(AActor movingActor, Vector3 moveOffset, Vector3 goalOffset, float goalRadius,
			float goalHalfHeight);
		bool ShouldPostponePathUpdates { get; }
		bool IsFollowingAPath { get; }
		IPathFollowingAgentInterface GetPathFollowingAgent { get; }
	}
}