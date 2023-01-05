using System;
using Godot;

namespace ActionRPG.Generic
{
    public class Stats : Node
    {
        [Export]
        public readonly int MaxHealth = 1;

        private int health;

        [Export]
        public int Health { get => health; set => SetHealth(value); }

        [Signal]
        public delegate void NoHealth();
        public static readonly string NoHealthSignalName = nameof(NoHealth);

        public override void _Ready()
        {
            Health = MaxHealth;
        }

        private void SetHealth(int value)
        {
            health = value;

            if (health <= 0)
            {
                EmitSignal(NoHealthSignalName);
            }
        }
    }
}
