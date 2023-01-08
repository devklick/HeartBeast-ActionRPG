using System;
using ActionRPG.Effects;
using ActionRPG.Generic;
using ActionRPG.Player;
using Godot;

namespace ActionRPG.Enemies
{
    public class Bat : KinematicBody2D
    {
        #region Exports
        [Export]
        private readonly float _friction = 200;
        #endregion

        #region Privates
        private Vector2 _knockBack = Vector2.Zero;
        private Stats _stats;
        private HurtBox _hurtBox;
        private static readonly PackedScene _enemyDestroyedScene =
            (PackedScene)ResourceLoader.Load("res://Effects/EnemyDestroyedEffect.tscn");
        private static readonly PackedScene _enemyDamagedEffectScene =
            (PackedScene)ResourceLoader.Load("res://Effects/EnemyDamagedEffect.tscn");

        #endregion

        #region Overrides
        public override void _Ready()
        {
            _stats = GetNode<Stats>("Stats");
            _hurtBox = GetNode<HurtBox>("HurtBox");

            SubscribeToStatsEvents(_stats);
            SubscribeToHurtBoxEvents(_hurtBox);
        }

        public override void _Process(float delta)
        {
            _knockBack = _knockBack.MoveToward(Vector2.Zero, _friction * delta);
            _knockBack = MoveAndSlide(_knockBack);
        }
        #endregion

        #region Internal Helper Functions
        private void SubscribeToStatsEvents(Stats stats)
        {
            stats.Connect(Stats.HealthDecreasedAndDepletedSignalName, this, nameof(_on_Stats_HealthDecreasedAndDepleted));
            stats.Connect(Stats.HealthDecreasedButNotDepletedSignalName, this, nameof(_on_Stats_HealthDecreasedButNotDepleted));
        }

        private void SubscribeToHurtBoxEvents(HurtBox hurtBox)
        {
            hurtBox.Connect("area_entered", this, nameof(_on_HurtBox_area_entered));
        }
        #endregion

        #region Event Handlers
#pragma warning disable IDE1006
        private void _on_HurtBox_area_entered(Area2D area)
        {
            if (!(area is SwordHitbox swordHitbox)) return;

            // TODO: Variable this magic number
            _knockBack = swordHitbox.KnockBackDirection * 120;
            _stats.Health -= swordHitbox.Damage;
        }

        private void _on_Stats_HealthDecreasedButNotDepleted()
        {
            var effect = _enemyDamagedEffectScene.Instance<OneShotEffect>();
            GetParent().AddChild(effect);
            effect.GlobalPosition = GlobalPosition - new Vector2(0, 8);
        }

        private void _on_Stats_HealthDecreasedAndDepleted()
        {
            var effect = _enemyDestroyedScene.Instance<OneShotEffect>();
            GetParent().AddChild(effect);
            effect.GlobalPosition = GlobalPosition;
            QueueFree();
        }
#pragma warning restore IDE1006
        #endregion
    }
}