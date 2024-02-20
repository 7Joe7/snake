using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
    	static readonly int ScreenWidth = 32;
        static readonly int ScreenHeight = 16;
        static readonly Random RandomNumber = new Random();
        static int score = 5;
        static bool isGameOver = false;
        static Pixel head;
        static List<int> bodyXPositions = new List<int>();
        static List<int> bodyYPositions = new List<int>();
        static int berryX;
        static int berryY;
        static string movement = "RIGHT";
        static string buttonpressed = "no";

        static void Main(string[] args)
        {
        	ConsoleSetup();
        	InitializeGame();
           
            while (!isGameOver)
            {
                RenderFrame();
                DateTime moveTimestamp = DateTime.Now;
                buttonpressed = "no";
                while (true)
                {
                    if (DateTime.Now.Subtract(moveTimestamp).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable) ProcessInput();
                }
                UpdateGameState();
                DetectCollisions();
            }
            
            EndGame();
        }

        static void ConsoleSetup()
        {
            Console.WindowHeight = ScreenHeight;
            Console.WindowWidth = ScreenWidth;
        }

        static void InitializeGame()
        {
            head = new Pixel
            {
                XPos = ScreenWidth / 2,
                YPos = ScreenHeight / 2,
                Color = ConsoleColor.Red
            };

            berryX = RandomNumber.Next(0, ScreenWidth);
            berryY = RandomNumber.Next(0, ScreenHeight);
        }

        static void RenderFrame()
        {
            Console.Clear();
            DrawBorders();
            DrawBerry();
            DrawSnake();
        }

        static void DrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < ScreenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, ScreenHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < ScreenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(ScreenWidth - 1, i);
                Console.Write("■");
            }
        }

        static void DrawBerry()
        {
        	if (berryX == head.XPos && berryY == head.YPos)
            {
                score++;
                berryX = RandomNumber.Next(1, ScreenWidth-2);
                berryY = RandomNumber.Next(1, ScreenHeight-2);
            }
            Console.SetCursorPosition(berryX, berryY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■");
        }

        static void DrawSnake()
        {
            foreach (var pos in bodyXPositions.Zip(bodyYPositions, (x, y) => new { x, y }))
            {
                Console.SetCursorPosition(pos.x, pos.y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("■");
            }

            Console.SetCursorPosition(head.XPos, head.YPos);
            Console.ForegroundColor = head.Color;
            Console.Write("■");
        }

        static void ProcessInput()
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow when movement != "DOWN" && buttonpressed == "no":
                    movement = "UP";
                    buttonpressed = "yes";
                    break;
                case ConsoleKey.DownArrow when movement != "UP" && buttonpressed == "no":
                    movement = "DOWN";
                    buttonpressed = "yes";
                    break;
                case ConsoleKey.LeftArrow when movement != "RIGHT" && buttonpressed == "no":
                    movement = "LEFT";
                    buttonpressed = "yes";
                    break;
                case ConsoleKey.RightArrow when movement != "LEFT" && buttonpressed == "no":
                    movement = "RIGHT";
                    buttonpressed = "yes";
                    break;
            }
        }

        static void UpdateGameState()
        {
            bodyXPositions.Add(head.XPos);
            bodyYPositions.Add(head.YPos);

            switch (movement)
            {
                case "UP": head.YPos--; break;
                case "DOWN": head.YPos++; break;
                case "LEFT": head.XPos--; break;
                case "RIGHT": head.XPos++; break;
            }

            if (bodyXPositions.Count > score)
            {
                bodyXPositions.RemoveAt(0);
                bodyYPositions.RemoveAt(0);
            }
        }

        static void DetectCollisions()
        {
            if (head.XPos == ScreenWidth - 1 || head.XPos == 0 || head.YPos == ScreenHeight - 1 || head.YPos == 0)
                isGameOver = true;

            if (head.XPos == berryX && head.YPos == berryY)
            {
                score++;
                berryX = RandomNumber.Next(0, ScreenWidth);
                berryY = RandomNumber.Next(0, ScreenHeight);
            }

            if (bodyXPositions.Zip(bodyYPositions, (x, y) => new { x, y }).Any(p => p.x == head.XPos && p.y == head.YPos))
                isGameOver = true;
        }

        static void EndGame()
        {
            Console.SetCursorPosition(ScreenWidth / 5, ScreenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(ScreenWidth / 5, ScreenHeight / 2 +1);
        }

        class Pixel
        {
            public int XPos { get; set; }
            public int YPos { get; set; }
            public ConsoleColor Color { get; set; }
        }
    }
}
//¦