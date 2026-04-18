using Shooter.FiniteStateMachine;

namespace Shooter.PlayerControl
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public PlayerIdlingState IdlingState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }

        public PlayerMovementStateMachine()
        {
            IdlingState = new();
            WalkingState = new();
            RunningState = new();
        }
    }
}