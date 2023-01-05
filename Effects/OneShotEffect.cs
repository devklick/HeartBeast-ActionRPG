using Godot;

namespace ActionRPG.Effects
{
    public class OneShotEffect : AnimatedSprite
    {
        #region Overrides
        public override void _Ready()
        {
            Connect("animation_finished", this, nameof(_on_animation_finished));
            Play("Animate");
        }
        #endregion

        #region Event Handlers
#pragma warning disable IDE1006
        private void _on_animation_finished() => QueueFree();
#pragma warning restore IDE1006
        #endregion
    }

}
