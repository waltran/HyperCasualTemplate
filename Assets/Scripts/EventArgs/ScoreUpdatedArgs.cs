using System;

namespace Game.Core
{
    public class ScoreUpdatedArgs : EventArgs
    {
        public int NewScore { get; set; }
    }
}