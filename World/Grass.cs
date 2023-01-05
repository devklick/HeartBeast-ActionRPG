using Godot;

namespace ActionRPG.World
{
    public class Grass : Node2D
    {
        #region Privates
        private Sprite _grassSprite;
        private AnimatedSprite _destroyAnimation;
        #endregion

        #region Overrides
        public override void _Ready()
        {
            _grassSprite = GetNode<Sprite>("Sprite");
            _destroyAnimation = GetNode<AnimatedSprite>("DestroyAnimation");
        }
        #endregion

        #region Internal Helper Functions
        private void Destroy()
        {
            _grassSprite.Visible = false;
            _destroyAnimation.Visible = true;
            _destroyAnimation.Play();
        }
        #endregion

        #region Event Handlers
#pragma warning disable IDE0051, IDE1006, IDE0060
        private void _on_DestroyAnimation_animation_finished()
        {
            _destroyAnimation.QueueFree();
            QueueFree();
        }

        private void _on_HurtBox_area_entered(Area2D area)
        {
            Destroy();
        }
#pragma warning restore IDE0051, IDE1006, IDE0060
        #endregion
    }
}