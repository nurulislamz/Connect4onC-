namespace Connect4
{
    class Program
    {
        static void Main()
        {
            Game game = new Game(6, 7, 4, 1);
            game.Play();
        }
    }
}
