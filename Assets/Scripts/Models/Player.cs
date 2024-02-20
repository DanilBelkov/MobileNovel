namespace Assets.Scripts.Models
{
    public class Player
    {
        private static  Player _player;

        public delegate void Update();
        public static event Update OnUpdate;
        public int CurrentDialogStepId { get; set; }
        public int MoodValue { get => _moodValue; set { _moodValue = value; OnUpdate(); } }
        public int Money { get => _money; set { _money = value; OnUpdate(); } }

        private int _moodValue;
        private int _money = 534;
        public static Player InitializePlayer()
        {
            if(_player == null)
                _player = new Player();
            return _player;
        }


    }
}
