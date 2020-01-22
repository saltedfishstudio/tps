using UnityEngine;

namespace Epic.Engine.GameFramework
{
    public class Pawn : Actor, INavAgentInterface
    {
        public Vector3 GetNavAgentLocation()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetMoveGoalOffset(Actor movingActor)
        {
            throw new System.NotImplementedException();
        }

        public void GetMoveGoalReachTest(Actor movingActor, Vector3 moveOffset, Vector3 goalOffset, float goalRadius,
            float goalHalfHeight)
        {
            throw new System.NotImplementedException();
        }

        public bool ShouldPostponePathUpdates
        {
            get => false;
        }
        
        public bool IsFollowingAPath
        {
            get => false;
        }
        
        public IPathFollowingAgentInterface GetPathFollowingAgent
        {
            get => null;
        }
    }
}