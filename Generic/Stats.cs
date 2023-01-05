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
        public delegate void NoHealth();
        public static readonly string NoHealthSignalName = nameof(NoHealth);
        #endregion

        #region Privates
        private int health;
        #endregion

        #region Overrides
        public override void _Ready()
        {
            Health = MaxHealth;
        }
        #endregion

        #region Internal Helper Functions
        private void SetHealth(int value)
        {
            health = value;

            if (health <= 0)
            {
                EmitSignal(NoHealthSignalName);
            }
        }
        #endregion
    }
}
