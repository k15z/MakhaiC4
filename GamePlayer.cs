using System;

namespace MakhaiC4
{
    interface IGamePlayer
    {
        int NextMove(int[] state, Func<int, bool> valid);
    }

    class HumanPlayer : IGamePlayer
    {
        public int NextMove(int[] state, Func<int, bool> valid)
        {
            Console.Write("Your move (0-6): ");
            int move = int.Parse(Console.ReadLine());
            while (!valid(move))
            {
                Console.Write("Try again (0-6): ");
                move = int.Parse(Console.ReadLine());
            }
            return move;
        }
    }

    class RandomPlayer : IGamePlayer
    {
        static Random random = new Random();
        public int NextMove(int[] state, Func<int, bool> valid)
        {
            int move = random.Next(7);
            while (!valid(move))
                move = random.Next(7);
            return move;
        }
    }

    class GreedyPlayer : IGamePlayer
    {
        static Random random = new Random();
        public int NextMove(int[] state, Func<int, bool> valid)
        {
            for (int c = 0; c < 7; c++)
                if (valid(c))
                {
                    var result = GameEngine.MakeMove((int[])state.Clone(), c, 1);
                    if (GameEngine.GetWinner(result) == 1)
                        return c;

                    result = GameEngine.MakeMove((int[])state.Clone(), c, -1);
                    if (GameEngine.GetWinner(result) == -1)
                        return c;
                }
            int move = random.Next(7);
            while (!valid(move))
                move = random.Next(7);
            return move;
        }
    }
}
