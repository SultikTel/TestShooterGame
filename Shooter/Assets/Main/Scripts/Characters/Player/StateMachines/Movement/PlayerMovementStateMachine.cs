using Shooter.FiniteStateMachine;

namespace Shooter.PlayerControl
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public PlayerMovement PlayerMovement { get; }
        public PlayerIdlingState IdlingState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }

        public PlayerMovementStateMachine(PlayerMovement playerMovement)
        {
            PlayerMovement = playerMovement;

            IdlingState = new(this);

            WalkingState = new(this);
            RunningState = new(this);
        }
    }
}