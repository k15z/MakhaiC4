using System;

namespace MakhaiC4
{
    static class GameEngine
    {
        public static bool VERBOSE_MODE = false;
        public static bool PLAY_BY_PLAY = false;

        /// <param name="a">Player 1</param>
        /// <param name="b">Player -1</param>
        /// <returns>Winner of match.</returns>
        public static IGamePlayer RunMatch(IGamePlayer a, IGamePlayer b)
        {
            int move = 0;
            int[] state = new int[42];
            while (!GameOver(state))
            {
                if (VERBOSE_MODE) Print(state);
                move = a.NextMove(Copy(state), (c) => state[c * 6] == 0);
                MakeMove(state, move, 1);
                if (GameOver(state))
                {
                    if (VERBOSE_MODE) Print(state);
                    return GetWinner(state) == 1 ? a : null;
                }

                if (VERBOSE_MODE) Print(state);
                move = b.NextMove(Flip(state), (c) => state[c * 6] == 0);
                MakeMove(state, move, -1);
                if (GameOver(state))
                {
                    if (VERBOSE_MODE) Print(state);
                    return GetWinner(state) == -1 ? b : null;
                }
            }
            return null;
        }

        /// <param name="state">Game state.</param>
        /// <returns>Array with negated numbers.</returns>
        public static int[] Copy(int[] state)
        {
            return (int[])state.Clone();
        }

        /// <param name="state">Game state.</param>
        /// <returns>Array with negated numbers.</returns>
        public static int[] Flip(int[] state)
        {
            int[] flipped = (int[])state.Clone();
            for (int i = 0; i < flipped.Length; i++)
                flipped[i] = -flipped[i];
            return flipped;
        }

        /// <param name="state">Game state.</param>
        public static void Print(int[] state)
        {
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    if (state[c * 6 + r] == 0)
                        Console.Write(".");
                    if (state[c * 6 + r] == 1)
                        Console.Write("a");
                    if (state[c * 6 + r] == -1)
                        Console.Write("b");
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            if (PLAY_BY_PLAY) Console.ReadLine();
            else Console.WriteLine();
        }

        /// <param name="state">Game state.</param>
        /// <returns>Whether game is over</returns>
        public static bool GameOver(int[] state)
        {
            if (GetWinner(state) != 0)
                return true;
            for (int c = 0; c < 7; c++)
                if (state[c * 6] == 0)
                    return false;
            return true;
        }

        /// <param name="state">Game state.</param>
        /// <returns>Player number if winner, 0 if tie or ongoing.</returns>
        public static int GetWinner(int[] state)
        {
            for (int c = 0; c < 7; c++)
                for (int r = 0; r < 3; r++)
                    if (state[(c) * 6 + r] == state[(c) * 6 + r + 1] &&
                        state[(c) * 6 + r] == state[(c) * 6 + r + 2] &&
                        state[(c) * 6 + r] == state[(c) * 6 + r + 3])
                        if (state[(c * 6) + r] != 0)
                            return state[c * 6 + r];
            for (int r = 0; r < 6; r++)
                for (int c = 0; c < 4; c++)
                    if (state[(c) * 6 + r] == state[(c + 1) * 6 + r] &&
                        state[(c) * 6 + r] == state[(c + 2) * 6 + r] &&
                        state[(c) * 6 + r] == state[(c + 3) * 6 + r])
                        if (state[(c * 6) + r] != 0)
                            return state[c * 6 + r];
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 4; c++)
                    if (state[(c) * 6 + r] == state[(c + 1) * 6 + r + 1] &&
                        state[(c) * 6 + r] == state[(c + 2) * 6 + r + 2] &&
                        state[(c) * 6 + r] == state[(c + 3) * 6 + r + 3])
                        if (state[(c * 6) + r] != 0)
                            return state[c * 6 + r];
            for (int r = 0; r < 3; r++)
                for (int c = 3; c < 7; c++)
                    if (state[(c) * 6 + r] == state[(c - 1) * 6 + r + 1] &&
                        state[(c) * 6 + r] == state[(c - 2) * 6 + r + 2] &&
                        state[(c) * 6 + r] == state[(c - 3) * 6 + r + 3])
                        if (state[(c * 6) + r] != 0)
                            return state[c * 6 + r];
            return 0;
        }

        /// <param name="state">Game state to change.</param>
        /// <param name="c">Destination column.</param>
        /// <param name="p">Player number.</param>
        /// <returns>Whether move was successful.</returns>
        public static int[] MakeMove(int[] state, int c, int p)
        {
            for (int r = 6 - 1; r >= 0; r--)
                if (state[c * 6 + r] == 0)
                {
                    state[c * 6 + r] = p;
                    return state;
                }
            return null;
        }
    }
}
