using Shooter.FiniteStateMachine;
using UnityEngine;

namespace Shooter.PlayerControl
{
    public class PlayerMovementState : IState
    {
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }

        public virtual void Exit()
        {
        }

        public virtual void HandleInput()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Update()
        {
        }
    }
}