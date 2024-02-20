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
        private int _money = 534;

        public void SetOnDefaultValue()
        {
            CurrentDialogStepId = 0;
            _moodValue = 0;
            _money = 534;
            OnUpdate();
        }

    }
}
