using System;
using ActionRPG.Generic;
using ActionRPG.Player;
using Godot;

namespace ActionRPG.Enemies
{
    public class Bat : KinematicBody2D
    {
        [Export] private readonly float _friction = 200;
        private Vector2 _knockBack = Vector2.Zero;

        private Stats _stats;

        public override void _Ready()
        {
            _stats = GetNode<Stats>("Stats");

            SubscribeToStatsEvents(_stats);
        }

        public override void _Process(float delta)
        {
            _knockBack = _knockBack.MoveToward(Vector2.Zero, _friction * delta);
            _knockBack = MoveAndSlide(_knockBack);
        }

        private void SubscribeToStatsEvents(Stats stats)
        {
            _stats.Connect(Stats.NoHealthSignalName, this, nameof(_on_Stats_no_health));
        }

        private void _on_HurtBox_area_entered(Area2D area)
        {
            if (!(area is SwordHitbox swordHitbox)) return;

            _knockBack = swordHitbox.KnockBackVector * 120;
            _stats.Health -= swordHitbox.Damage;
        }

        private void _on_Stats_no_health()
        {
            QueueFree();
        }
    }
}