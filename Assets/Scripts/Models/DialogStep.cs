namespace Assets.Scripts
{
    public class DialogStep
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public Answer[] Answers { get; set; }
        public Answer SavedAnswer { get; set; }
        public bool HasTimer { get; set; }

    }
}
