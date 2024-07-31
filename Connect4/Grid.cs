namespace Connect4
{
    public class Grid
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

            for (int row = ROWS - 1; row >= 0; row--)
            {
                if (grid[row, column] == GridPosition.EMPTY)
                {
                    grid[row, column] = piece;
                    return row;
                }
            }

            throw new Exception("Column is full");
        }

        public bool CheckWin(int connectN, int row, int col, GridPosition piece)
        {
            // Check horizontal
            int count = 0;
            for (int i = Math.Max(0, col - connectN + 1); i < Math.Min(COLS, col + connectN); i++)
            {
                if (grid[row, i] == piece)
                {
                    count++;
                    if (count == connectN) return true;
                }
                else
                {
                    count = 0;
                }
            }

            // Check vertical
            count = 0;
            for (int i = Math.Max(0, row - connectN + 1); i < Math.Min(ROWS, row + connectN); i++)
            {
                if (grid[i, col] == piece)
                {
                    count++;
                    if (count == connectN) return true;
                }
                else
                {
                    count = 0;
                }
            }

            // Check diagonal (\)
            count = 0;
            for (int i = -connectN + 1; i < connectN; i++)
            {
                int r = row + i;
                int c = col + i;
                if (r >= 0 && r < ROWS && c >= 0 && c < COLS && grid[r, c] == piece)
                {
                    count++;
                    if (count == connectN) return true;
                }
                else
                {
                    count = 0;
                }
            }

            // Check anti-diagonal (/)
            count = 0;
            for (int i = -connectN + 1; i < connectN; i++)
            {
                int r = row + i;
                int c = col - i;
                if (r >= 0 && r < ROWS && c >= 0 && c < COLS && grid[r, c] == piece)
                {
                    count++;
                    if (count == connectN) return true;
                }
                else
                {
                    count = 0;
                }
            }

            return false;
        }
    }
}