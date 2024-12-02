// See https://aka.ms/new-console-template for more information
using System;
using System;
using System.Linq;
using System.Threading;

class CrossyRoadGame
{
    // Game constants
    const int GridWidth = 10;
    const int GridHeight = 10;
    const char PlayerChar = 'P';
    const char ObstacleChar = 'C';
    const char EmptyChar = '|';

    // Player and obstacles state
    static int playerX = GridWidth / 2;
    static int playerY = GridHeight - 2;
    static int[] obstaclePositions = new int[GridWidth]; // Represents obstacle positions per row
    static Random random = new Random();
    static bool gameOver = false;

    static char[,] grid = new char[GridWidth,GridHeight];

    static void Main()
    {
        // Set up console
        Console.CursorVisible = false;
        Console.SetWindowSize(GridWidth, GridHeight + 2);

        // Game loop
        while (!gameOver)
        {
            // Render the game
            Render();

            // Handle player input
            HandleInput();

            // Move obstacles
            MoveObstacles();

            // Check for collisions
            CheckCollisions();

            // Wait a bit before the next frame
            Thread.Sleep(200); // Adjust this for game speed
        }

        // Game over message
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Game Over!");
        Console.ReadKey();
    }

    static void Render()
    {
        Console.Clear();
        //fill the gride
        for(int x=0;x <GridWidth-1;x++){
            for(int y=0;y <GridHeight-1;y++){
                if (y == playerY && x == playerX)
                {
                    grid[x,y] = PlayerChar; // Draw player
                }
                else
                {
                     grid[x,y] = EmptyChar; // Empty space
                }
            }
        }
        for(int x=0;x <GridWidth;x++){
            for(int y=0;y <GridHeight;y++){
                Console.Write(grid[x,y]);
            }
            Console.WriteLine();
        }
        // Render grid
        // for (int x = 0; x < GridHeight ; x++)
        // {
        //     for (int y = 0; y <GridWidth; y++)
        //     {

                // if (y == playerY && x == playerX)
                // {
                //     Console.Write(PlayerChar); // Draw player
                // }
                // else if (obstaclePositions[y] == x)
                // {
                //     Console.Write(ObstacleChar); // Draw obstacle
                // }
                // else
                // {
                //     Console.Write(EmptyChar); // Empty space
                // }
        //     }
        //     Console.WriteLine();
        // }
    }

    static void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (playerX > 0) playerX--; // Move left
                    break;
                case ConsoleKey.RightArrow:
                    if (playerX < GridWidth - 1) playerX++; // Move right
                    break;
                case ConsoleKey.UpArrow:
                    if (playerY > 0) playerY--; // Move up
                    break;
                case ConsoleKey.DownArrow:
                    if (playerY < GridHeight - 1) playerY++; // Move down
                    break;
            }
        }
    }

    static void MoveObstacles()
    {
        // Move obstacles downwards
        for (int x = GridHeight - 1; x > 0; x--)
        {
            obstaclePositions[x] = obstaclePositions[x - 1]; // Move the obstacles down
        }

        // Generate new obstacles at the top
        obstaclePositions[0] = random.Next(0, GridHeight); // Random column for obstacle

        // You can adjust obstacle frequency here
        // if (random.Next(0, 10) < 3) // Random chance to generate an obstacle - edit so the 'randomness' increases with levels 
        // {
        //     obstaclePositions[0] = random.Next(0, GridHeight);
        // }
    }

    static void CheckCollisions()
    {
        // If player position matches obstacle position
        if (obstaclePositions[playerY] == playerX)
        {
            gameOver = true; // Collision detected, end game
        }
    }
}
