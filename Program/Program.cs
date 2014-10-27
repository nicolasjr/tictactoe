using System;
using System.Text.RegularExpressions;

namespace Program
{
    using TicTacToe;

    class Program
    {
        static void Main(string[] args)
        {
            var playerReal = new PlayerReal();
            var playerUnbeatable = new PlayerUnbeatable();

            var matchManager = new MatchManager(playerReal, playerUnbeatable);

            matchManager.StartNewMatch();  

            bool loop = true;
            while (loop)
            {
                matchManager.RequestPlay();

                loop = !matchManager.IsGameFinished();
            }
        }
    }
}
