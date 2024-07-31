namespace Connect4
{
    class Player
    {
        public string name { get; private set; }
        public GridPosition pieceColour { get; private set; }
        public int score { get; private set; }

        public Player(string Name, GridPosition PieceColour)
        {
            name = Name;
            pieceColour = PieceColour;
            score = 0;
        }

        public void IncrementScore() => score++;
    }
}