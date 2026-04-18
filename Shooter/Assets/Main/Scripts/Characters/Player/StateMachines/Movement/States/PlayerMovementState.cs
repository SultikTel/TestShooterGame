using Shooter.FiniteStateMachine;
using UnityEngine;

namespace Shooter.PlayerControl
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine _stateMachine;

        protected Vector2 _movementInput;

        private float _speedModifier = 1f;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            _stateMachine = playerMovementStateMachine;
        }

        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }

        public virtual void Exit()
        {
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void Update()
        {
        }

        private void Move()
        {
            if (_movementInput == Vector2.zero) return;

            Vector3 movementDirection = GetMovementInputDirection();

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _stateMachine.PlayerMovement.RigidBody.AddForce(movementDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        private void ReadMovementInput()
        {
            _movementInput = _stateMachine.PlayerMovement.Input.PlayerControls.Move.ReadValue<Vector2>();
        }

        protected float GetMovementSpeed()
        {
            return _speedModifier * _stateMachine.PlayerMovement.PlayerMoveConfig.MoveSpeed;
        }

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(_movementInput.x, 0, _movementInput.y);
        }


        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = _stateMachine.PlayerMovement.RigidBody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }
    }
}