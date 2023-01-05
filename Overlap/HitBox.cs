using Godot;

namespace ActionRPG.Overlap
{
    public class HitBox : Area2D
    {
        #region Exports
        [Export]
        public int Damage = 1;
        #endregion
    }
}