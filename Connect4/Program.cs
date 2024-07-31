using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

public enum GridPosition{
    EMPTY = 0,
    YELLOW = 1,
    RED = 2
}

class Grid
{
    public int ROWS;
    public int COLS;
    public GridPosition[,] grid;

    public Grid(int Rows, int Cols)
    {
        ROWS = Rows;
        COLS = Cols;

        grid = new GridPosition[ROWS, COLS];

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                grid[i, j] = GridPosition.EMPTY;
            }
        }
    }

    public GridPosition[,] GetGrid()
    {
        return grid;
    }

    public int PlacePiece(int column, GridPosition piece)
    {
        if (column < 0 || column >= COLS)
        {
            throw new Exception("Invalid Column");
        }
        if (piece == GridPosition.EMPTY)
        {
            throw new Exception("Invalid Piece");
        }

        for (int row = 0; row <= ROWS; row++)
        {
            if (grid[row, column] == GridPosition.EMPTY)
            {
                grid[row, column] = piece;
            }
            return row;
        }
        return 0;
    }

    public bool CheckWin(int connectN, int row, int col, GridPosition piece)
    {
        int count = 0;
        // check horizontal
        for (int i = 0; i <= col; i++)
        {
            if (grid[row, i] == piece)
            {
                count++;
            }
            else
            {
                count = 0;
            }

            if (count == connectN) return true;
        }

        // check vertical
        count = 0;
        for (int i = 0; i < ROWS; i++)
        {
            if (grid[i, col] == piece)
            {
                count++;
            }
            else
            {
                count = 0;
            }

            if (count == connectN) return true;
        }

        // check diagonals
        count = 0;
        for (int i = 0; i < ROWS; i++)
        {
            int c = row + col - i;

            if (c >= 0 & c < COLS & grid[i, c] == piece) count++;
            else count = 0;
            
            if (count == connectN) return true;
        }

        // check anti-diagonals
        count = 0;
        for (int i = 0; i < ROWS; i++)
        {
            int c = col - row + i;

            if (c >= 0 & c < COLS & grid[c, i] == piece) count++;
            else count = 0;

            if (count == connectN) return true;
        }

        return false;
    }

}

class Player
{
    public string name { get; private set; }
    public GridPosition pieceColour { get; private set; }
    public int score { get; private set; }

    public Player(string Name, GridPosition PieceColour)
    {
        Name = name;
        PieceColour = pieceColour;
        score = 0;
    }

    public void IncrementScore() => score++;

}

class Game
{
    public Grid grid { get; private set; }
    public int connectN { get; private set; }
    public int targetScore { get; private set; }

    public Player[] players { get; private set; }

    public Game(Grid Grid, int ConnectN, int TargetScore)
    {
        Grid = new Grid(6, 6);
        ConnectN = connectN;
        TargetScore = targetScore;

        Player[] players = { new Player("Player 1", GridPosition.YELLOW), new Player("Player 2", GridPosition.RED) };
    }

    public void PrintBoard()
    {
        Console.WriteLine("Board:");

        for (int i = 0; i < grid.ROWS; i++)
        {
            string row = "";
            foreach (GridPosition piece in grid.grid)
            {
                if (piece == GridPosition.YELLOW) row += "Y";
                else if (piece == GridPosition.RED) row += "R";
                else  row += "0";
            }
            Console.WriteLine(row);
        }
        Console.WriteLine();
    }

    public (int, int) PlayMove(Player player)
    {
        PrintBoard();
        Console.WriteLine($"{player.name} turn");

        int moveColumn;
        Console.WriteLine($"Enter column between 0 and {grid.COLS - 1} to add piece:");
        int.TryParse(Console.ReadLine(), out moveColumn);

        int moveRow = grid.PlacePiece(moveColumn, player.pieceColour);

        return (moveRow, moveColumn);
    }

    public Player playRound()
    {
        while (true)
        {
            foreach (Player player in players)
            {
                (int, int) playPosition = PlayMove(player);
                int row = playPosition.Item1;
                int col = playPosition.Item2;
                
                if (grid.CheckWin(connectN, row, col, player.pieceColour))
                {
                    player.IncrementScore();
                }
                return player;
            }
        }
    }

    public void Play() 
    { 
        targetScore = 1;
        Player? winner = null;
        
        while(true)
        {
            winner = playRound();
            
            
            foreach (Player player in players)
            {
                if (player.score == targetScore)
                {
                    Console.WriteLine($"{player.name} won!");
                }
            }

        }



    }

}

class IPlay
{

}