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

    public Grid()
    {
        grid = new GridPosition[ROWS, COLS];

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                grid[i, j] = GridPosition.EMPTY;
            }
        }
    }

    public GridPosition[,] getGrid()
    {
        return grid;
    }

    public void placePiece(int column, GridPosition piece)
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
        }
    }

    public bool checkWin(int connectN, int row, int col, GridPosition piece)
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
        for (int i = 0; i < ROWS; i++)
        {

            

        }

    }

}

class Player
{

}

class Game
{

}

class IPlay
{

}