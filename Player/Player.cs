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

        public override void _Ready()
        {
            _animationTree = GetNode<AnimationTree>("AnimationTree");
            _animationPlaybackState = _animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;

            _animationTree.Active = true;
        }

        public override void _PhysicsProcess(float delta)
        {
            var inputVector = GetInputVector();
            var inputVelocity = GetInputVelocity(inputVector, delta);
            _currentVelocity = MoveAndSlide(inputVelocity);
            UpdateAnimation(inputVector);
        }

        private void Move(Vector2 inputVector, float delta)
        {
            var inputVelocity = GetInputVelocity(inputVector, delta);
            _currentVelocity = MoveAndSlide(inputVelocity);
        }

        private void UpdateAnimation(Vector2 inputVector)
        {
            if (inputVector == Vector2.Zero)
                _animationPlaybackState.Travel(PlayerState.Idle.ToString());
            else
            {
                UpdateAnimationTree(PlayerState.Idle, inputVector);
                UpdateAnimationTree(PlayerState.Run, inputVector);
                UpdateAnimationTree(PlayerState.Attack, inputVector);
                _animationPlaybackState.Travel(PlayerState.Run.ToString());
            }
        }

        private void UpdateAnimationTree(PlayerState state, object value)
            => _animationTree.Set($"parameters/{state.ToString()}/blend_position", value);

        private Vector2 GetInputVector() => new Vector2(
                Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
                Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
            ).Normalized();

        private Vector2 GetInputVelocity(Vector2 inputVector, float delta)
        {
            if (inputVector == Vector2.Zero) _currentVelocity = SlowDown(_currentVelocity, delta);
            else _currentVelocity = SpeedUp(_currentVelocity, inputVector, delta);

            return _currentVelocity;
        }

        private Vector2 SlowDown(Vector2 currentVelocity, float delta)
            => currentVelocity.MoveToward(Vector2.Zero, friction * delta);

        private Vector2 SpeedUp(Vector2 currentVelocity, Vector2 input, float delta)
            => currentVelocity.MoveToward(input * maxSpeed, acceleration * delta);
    }
}


