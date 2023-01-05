using Godot;

namespace ActionRPG.World
{
    public class Grass : Node2D
    {
        #region Privates
        private PackedScene _grassDestroyedScene;
        #endregion

        #region Overrides
        public override void _Ready()
        {
            _grassDestroyedScene = (PackedScene)ResourceLoader.Load("res://Effects/GrassDestroyedEffect.tscn");
        }
        #endregion

        #region Internal Helper Functions
        private void CreateGrassDestroyedEffect()
        {
            var grassDestroyedEffect = _grassDestroyedScene.Instance<Node2D>();
            GetParent().AddChild(grassDestroyedEffect);
            grassDestroyedEffect.GlobalPosition = GlobalPosition;
        }
        #endregion

        #region Event Handlers
#pragma warning disable IDE0051, IDE1006, IDE0060
        private void _on_HurtBox_area_entered(Area2D area)
        {
            CreateGrassDestroyedEffect();
            QueueFree();
        }
#pragma warning restore IDE0051, IDE1006, IDE0060
        #endregion
    }
}