using System;

namespace Assets.Scripts.Models
{
    [Serializable]
    public struct DataGame
    {
        public int MoodValue { get ; set; }
        public int Money { get ; set; }
        public int DialogStepId { get; set; }
    }
}
