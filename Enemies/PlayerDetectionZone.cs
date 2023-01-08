using Godot;
using System;

namespace ActionRPG.Enemies
{
    public class PlayerDetectionZone : Area2D
    {
        // TODO: Sort these horrible matching namespace / class
        public Player.Player Player;

        public override void _Ready()
        {
            Connect("body_entered", this, nameof(_on_PlayerDetectionZone_body_entered));
            Connect("body_exited", this, nameof(_on_PlayerDetectionZone_body_exited));
        }

        public bool PlayerDetected => Player != null;

        #region Event Handlers
        private void _on_PlayerDetectionZone_body_entered(Node body)
        {
            if (!(body is Player.Player p)) return;

            Player = p;
        }
        private void _on_PlayerDetectionZone_body_exited(Node body)
        {
            if (!(body is Player.Player)) return;

            Player = null;
        }
        #endregion
    }
}