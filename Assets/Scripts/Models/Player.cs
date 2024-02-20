using System;

namespace Assets.Scripts.Models
{
    public class Player
    {
        public static event Action OnUpdate;
        public int CurrentDialogStepId { get; set; }
        public int MoodValue { get => _moodValue; set { _moodValue = value; OnUpdate(); } }
        public int Money { get => _money; set { _money = value; OnUpdate(); } }

        private int _moodValue;
        private int _money;

        public Player()
        {
            SetOnDefaultValue();
        }
        public void SetOnDefaultValue()
        {
            CurrentDialogStepId = 0;
            MoodValue = 0;
            Money = 534;
        }
    }
}
