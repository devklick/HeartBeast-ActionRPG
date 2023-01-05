using ActionRPG.Overlap;
using Godot;

namespace ActionRPG.Player
{
    public class SwordHitbox : HitBox
    {
        #region Exports
        [Export]
        public Vector2 KnockBackDirection = Vector2.Zero;
        #endregion
    }
}