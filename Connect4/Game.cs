namespace Connect4
{
    class Game
    {
        public Grid grid { get; private set; }
        public int connectN { get; private set; }
        public int targetScore { get; private set; }

        public Player[] players { get; private set; }

        public Game(int rows, int cols, int ConnectN, int TargetScore)
        {
            grid = new Grid(rows, cols);
            connectN = ConnectN;
            targetScore = TargetScore;

            players = new Player[] { new Player("Player 1", GridPosition.YELLOW), new Player("Player 2", GridPosition.RED) };
        }

        public void PrintBoard()
        {
            Console.WriteLine("Board:");

            for (int i = 0; i < grid.ROWS; i++)
            {
                string row = "";
                for (int j = 0; j < grid.COLS; j++)
                {
                    if (grid.grid[i, j] == GridPosition.YELLOW) row += "Y";
                    else if (grid.grid[i, j] == GridPosition.RED) row += "R";
                    else row += "0";
                }
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }

        public (int, int) PlayMove(Player player)
        {
            PrintBoard();
            Console.WriteLine($"{player.name}'s turn");

            int moveColumn;
            while (true)
            {
                Console.WriteLine($"Enter column between 0 and {grid.COLS - 1} to add piece:");
                if (int.TryParse(Console.ReadLine(), out moveColumn) && moveColumn >= 0 && moveColumn < grid.COLS)
                {
                    try
                    {
                        int moveRow = grid.PlacePiece(moveColumn, player.pieceColour);
                        return (moveRow, moveColumn);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        public Player playRound()
        {
            while (true)
            {
                foreach (Player player in players)
                {
                    var playPosition = PlayMove(player);
                    int row = playPosition.Item1;
                    int col = playPosition.Item2;

                    if (grid.CheckWin(connectN, row, col, player.pieceColour))
                    {
                        player.IncrementScore();
                        return player;
                    }
                }
            }
        }

        public void Play()
        {
            Player winner = null;

            while (true)
            {
                winner = playRound();

                if (winner.score >= targetScore)
                {
                    Console.WriteLine($"{winner.name} won the game!");
                    PrintBoard();
                    break;
                }
            }
        }
    }
}