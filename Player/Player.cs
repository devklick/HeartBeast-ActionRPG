using Godot;
using System;

namespace ActionRPG.Player
{
    public class Player : KinematicBody2D
    {
        [Export]
        private float maxSpeed = 70;

        [Export]
        private float acceleration = 500;

        [Export]
        private float friction = 500;

        private AnimationTree _animationTree;
        private AnimationNodeStateMachinePlayback _animationPlaybackState;
        private Vector2 _currentVelocity = Vector2.Zero;
        private PlayerState _playerState = PlayerState.Move;

        public override void _Ready()
        {
            _animationTree = GetNode<AnimationTree>("AnimationTree");
            _animationPlaybackState = _animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;

            _animationTree.Active = true;
        }

        public override void _PhysicsProcess(float delta)
        {
            if (_playerState == PlayerState.Move) HandleMoveState(delta);
            else if (_playerState == PlayerState.Attack) HandleAttackState();
        }

        private void HandleMoveState(float delta)
        {
            var inputVector = GetInputVector();
            UpdateAnimation(inputVector);
            UpdateVelocity(inputVector, delta);
            _currentVelocity = MoveAndSlide(_currentVelocity);
            if (Input.IsActionJustPressed("attack")) _playerState = PlayerState.Attack;
        }

        private void HandleAttackState()
        {
            _animationPlaybackState.Travel(PlayerAnimation.Attack.ToString());
        }


        private void UpdateAnimation(Vector2 inputVector)
        {
            if (inputVector == Vector2.Zero)
                _animationPlaybackState.Travel(PlayerAnimation.Idle.ToString());
            else
            {
                UpdateAnimationTree(PlayerAnimation.Idle, inputVector);
                UpdateAnimationTree(PlayerAnimation.Run, inputVector);
                UpdateAnimationTree(PlayerAnimation.Attack, inputVector);
                _animationPlaybackState.Travel(PlayerAnimation.Run.ToString());
            }
        }

        private void UpdateAnimationTree(PlayerAnimation state, object value)
            => _animationTree.Set($"parameters/{state.ToString()}/blend_position", value);

        private Vector2 GetInputVector() => new Vector2(
                Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
                Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
            ).Normalized();

        private void UpdateVelocity(Vector2 inputVector, float delta)
        {
            if (inputVector == Vector2.Zero) _currentVelocity = SlowDown(_currentVelocity, delta);
            else _currentVelocity = SpeedUp(_currentVelocity, inputVector, delta);
        }

        private Vector2 SlowDown(Vector2 currentVelocity, float delta)
            => currentVelocity.MoveToward(Vector2.Zero, friction * delta);

        private Vector2 SpeedUp(Vector2 currentVelocity, Vector2 input, float delta)
            => currentVelocity.MoveToward(input * maxSpeed, acceleration * delta);

        private void Attack_Animation_Finished() => _playerState = PlayerState.Move;
    }
}


