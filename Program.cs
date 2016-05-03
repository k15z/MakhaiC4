using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace MakhaiC4
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new RandomPlayer();
            var player2 = new GreedyPlayer();
            for (int i = 0; i < 1000; i++)
            {
                var winner = GameEngine.RunMatch(player1, player2);
                if (winner == player1)
                    Console.WriteLine("Player 1 Wins!");
                if (winner == player2)
                    Console.WriteLine("Player 2 Wins!");
                Console.ReadLine();
            }
        }
    }
}
