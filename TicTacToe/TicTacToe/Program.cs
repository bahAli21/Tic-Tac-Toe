using System;
using System.Collections.Generic;

namespace ConsoleAppTutoTicTacToe
{
    class Program
    {
        // Variables
        public static bool quitGame = false;
        public static bool playerTurn = true;
        public static char[,] board; // Plateau de jeu

        // Fonction main
        static void Main(string[] args)
        {
            // Boucle de jeu / Game loop
            while (!quitGame) // Tant que le jeu tourne
            {
                // Plateau de 3 lignes et 3 col
                board = new char[3, 3]
                {
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                };
                while (!quitGame)
                {
                    // Tour du joueur
                    if (playerTurn)
                    {
                        PlayerTurn();
                        if (CheckLines('X'))
                        {
                            EndGame("You win!");
                            break;
                        }
                    }
                    // Tour de l'ordinateur
                    else
                    {
                        ComputerTurn();
                        if (CheckLines('O'))
                        {
                            EndGame("You loose!");
                            break;
                        }
                    }
                    // Changement de joueur
                    playerTurn = !playerTurn;
                    // Vérifier si match nul
                    if (CheckDraw())
                    {
                        EndGame("Draw!");
                        break;
                    }
                }
                if (!quitGame)
                {
                    // Instructions
                    Console.WriteLine("Appuyer sur [Escape] pour quitter, [Enter] pour rejouer.");
                // Récupération touche du clavier
                GetKey:
                    switch (Console.ReadKey(true).Key)
                    {
                        // Rejouer
                        case ConsoleKey.Enter:
                            break;
                        // Quitter le jeu
                        case ConsoleKey.Escape:
                            quitGame = true;
                            Console.Clear();
                            break;
                        // Tester une autre touche de clavier
                        default:
                            goto GetKey;
                    }
                }
            }
        } // Fin donction main

        // Fonctions

        // Au tour du joueur
        public static void PlayerTurn()
        {
            // Où se trouve le joueur sur la grille ?
            // Le curseur sera sur une ligne et une col
            var (row, column) = (0, 0);
            // Le curseur a t-il été bougé ?
            bool moved = false;
            // Boucle pour déplacer le curseur à l'écran
            while (!quitGame && !moved)
            {
                Console.Clear();
                // Afficher la grille
                RenderBoard();
                Console.WriteLine();
                // Afficher les instructions
                Console.WriteLine("Choisir une case valide puis appuyer sur [Enter].");
                // Afficher le curseur
                Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);
                // Attendre que l'utilisateur réalise une action (Key)
                switch (Console.ReadKey(true).Key)
                {
                    // Quitter le jeu
                    case ConsoleKey.Escape:
                        quitGame = true;
                        Console.Clear();
                        break;
                    // Gérer les flèches du clavier
                    // Pour déplacer le curseur à l'écran
                    case ConsoleKey.RightArrow:
                        // Si on est sur la col 2...
                        if (column >= 2)
                        {
                            // On retourne dans la col 0
                            column = 0;
                        }
                        else
                        {
                            // Sinon on va à droite
                            column = column + 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (column <= 0)
                        {
                            column = 2;
                        }
                        else
                        {
                            column = column - 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (row <= 0)
                        {
                            row = 2;
                        }
                        else
                        {
                            row = row - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row >= 2)
                        {
                            row = 0;
                        }
                        else
                        {
                            row = row + 1;
                        }
                        break;
                    // Jouer dans la case actuelle
                    case ConsoleKey.Enter:
                        if (board[row, column] is ' ')
                        {
                            board[row, column] = 'X';
                            moved = true;
                        }
                        break;
                }
            }
        }

        // Au tour de l'ordinateur
        public static void ComputerTurn()
        {
            // Liste des cases vides
            var emptyBox = new List<(int X, int Y)>();
            // Double boucle pour parcourir les cases
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Vérif si case vide
                    if (board[i, j] == ' ')
                    {
                        emptyBox.Add((i, j));
                    }
                }
            }
            // Où est-ce que l'ordinateur va jouer ?
            var (X, Y) = emptyBox[new Random().Next(0, emptyBox.Count)];
            board[X, Y] = 'O';
        }

        // Afficher le plateau de jeu
        public static void RenderBoard()
        {
            Console.WriteLine();
            Console.WriteLine($" {board[0, 0]}  |  {board[0, 1]}  |  {board[0, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[1, 0]}  |  {board[1, 1]}  |  {board[1, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[2, 0]}  |  {board[2, 1]}  |  {board[2, 2]}");
        }

        // Vérifier si un joueur a gagné
        public static bool CheckLines(char c) =>
            board[0, 0] == c && board[1, 0] == c && board[2, 0] == c ||
            board[0, 1] == c && board[1, 1] == c && board[2, 1] == c ||
            board[0, 2] == c && board[1, 2] == c && board[2, 2] == c ||
            board[0, 0] == c && board[0, 1] == c && board[0, 2] == c ||
            board[1, 0] == c && board[1, 1] == c && board[1, 2] == c ||
            board[2, 0] == c && board[2, 1] == c && board[2, 2] == c ||
            board[0, 0] == c && board[1, 1] == c && board[2, 2] == c ||
            board[2, 0] == c && board[1, 1] == c && board[0, 2] == c;

        // Vérifier si match nul
        public static bool CheckDraw() =>
            board[0, 0] != ' ' && board[1, 0] != ' ' && board[2, 0] != ' ' &&
            board[0, 1] != ' ' && board[1, 1] != ' ' && board[2, 1] != ' ' &&
            board[0, 2] != ' ' && board[1, 2] != ' ' && board[2, 2] != ' ';

        // Fin de partie
        public static void EndGame(string msg)
        {
            Console.Clear();
            RenderBoard();
            Console.WriteLine(msg);
        }
    }
}