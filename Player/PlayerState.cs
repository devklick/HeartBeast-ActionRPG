namespace ActionRPG.Player
{
    /// <summary>
    /// The different states that the player can transition between
    /// </summary>
    public enum PlayerState
    {
        Idle,
        /// <summary>
        /// Move consists of running and idling. It makes no sense, but that's just how it is.
        /// </summary>
        Run,

        Attack
    }
}