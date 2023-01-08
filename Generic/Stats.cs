using System;
using Godot;

namespace ActionRPG.Generic
{
    public class Stats : Node
    {
        #region Exports
        [Export]
        public readonly int MaxHealth = 1;

        [Export]
        public int Health { get => health; set => SetHealth(value); }
        #endregion

        #region Signals
        [Signal]
        public delegate void HealthDecreasedAndDepleted();
        public static readonly string HealthDecreasedAndDepletedSignalName = nameof(HealthDecreasedAndDepleted);
        [Signal]
        public delegate void HealthDecreasedButNotDepleted();
        public static readonly string HealthDecreasedButNotDepletedSignalName = nameof(HealthDecreasedButNotDepleted);
        #endregion

        #region Privates
        private int health;
        #endregion

        #region Overrides
        public override void _Ready()
        {
            Health = MaxHealth;
            GD.Print("Health is ", health);
        }
        #endregion

        #region Internal Helper Functions
        private void SetHealth(int value)
        {
            GD.Print("Health updating to ", value);

            if (value < health)
            {
                if (value <= 0) EmitSignal(HealthDecreasedAndDepletedSignalName);
                else EmitSignal(HealthDecreasedButNotDepletedSignalName);
            }

            health = value;
        }
        #endregion
    }
}
