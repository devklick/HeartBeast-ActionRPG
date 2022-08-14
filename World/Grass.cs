using Godot;

namespace ActionRPG.World
{
    public class Grass : Node2D
    {
        private Sprite _grassSprite;
        private AnimatedSprite _destroyAnimation;

        public override void _Ready()
        {
            _grassSprite = GetNode<Sprite>("Sprite");
            _destroyAnimation = GetNode<AnimatedSprite>("DestroyAnimation");
        }

        private void Destroy()
        {
            _grassSprite.Visible = false;
            _destroyAnimation.Visible = true;
            _destroyAnimation.Play();
        }

        private void _on_DestroyAnimation_animation_finished()
        {
            _destroyAnimation.QueueFree();
            QueueFree();
        }

        private void _on_HurtBox_area_entered(Area2D area)
        {
            Destroy();
        }
    }
}