using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace Snake
{
    class Snake
    {
        struct Coords
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public const int fieldHeight = 17;
        public const int fieldWidth = 35;

        static List<Coords> snake = new List<Coords>();

        enum Direction { UP, RIGHT, DOWN, LEFT };
        static Direction d;
        static Coords headSnake; // координаты головы
        static Coords food = new Coords(); // координаты еды
        static Random rnd = new Random();
        public enum Level { EASY = 300, MEDIUM = 200, HARD = 100 };
        static Level mSecsPerFrame = Level.EASY;
        static System.Timers.Timer t = new System.Timers.Timer();
        static bool gameOver = false;

        static int score; // счёт в игре

        public static Level MSecsPerFrame
        {
            get { return mSecsPerFrame; }
            set { mSecsPerFrame = value; }
        }

        static Snake()
        {
            t.Elapsed += new ElapsedEventHandler(OnTimer);
        }

        public static void StartGame()
        {
            gameOver = false;
            score = 0;
            if (snake.Count > 0)
            {
                snake.Clear();
                snake.TrimExcess();
            }

            HandlerKeys();
        }

        static void HandlerKeys()
        {
            ShowScore();
            ShowGameField();
            CreateSnake();
            GenerateFood();

            d = Direction.RIGHT;
            t.Interval = (double)mSecsPerFrame;
            t.Start();

            ConsoleKeyInfo info;
            ConsoleKey key;
            do
            {
                info = Console.ReadKey(true);
                key = info.Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (d != Direction.RIGHT && t.Enabled)
                        {
                            d = Direction.LEFT;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (d != Direction.LEFT && t.Enabled)
                        {
                            d = Direction.RIGHT;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (d != Direction.DOWN && t.Enabled)
                        {
                            d = Direction.UP;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (d != Direction.UP && t.Enabled)
                        {
                            d = Direction.DOWN;
                        }
                        break;
                    case ConsoleKey.P:
                        if (t.Enabled)
                        {
                            t.Stop();
                            Console.SetCursorPosition(25, 0);
                            Console.Write("Pause");
                        }
                        else
                        {
                            if (gameOver)
                                break;

                            Console.SetCursorPosition(25, 0);
                            Console.Write("     ");
                            t.Start();
                        }
                        break;
                }

                Thread.Sleep((int)mSecsPerFrame);
            } while (key != ConsoleKey.Escape);

            t.Stop();
        }

        static void OnTimer(object sender, ElapsedEventArgs arg)
        {
            switch (d)
            {
                case Direction.UP:
                    if (headSnake.Y == 2 || IsSnake())
                    {
                        GameOver();
                    }
                    else
                    {
                        if (headSnake.X == food.X && headSnake.Y - 1 == food.Y)
                        {
                            AddScore();

                            headSnake.Y--;
                            snake.Add(headSnake);

                            Console.SetCursorPosition(headSnake.X, headSnake.Y);
                            Console.Write('O');

                            GenerateFood();
                        }
                        else
                        {
                            Console.SetCursorPosition(snake[0].X, snake[0].Y);
                            Console.Write(' ');

                            for (int i = 0; i < snake.Count - 1; i++)
                            {
                                snake[i] = snake[i + 1];
                            }
                            headSnake.Y--;
                            snake.RemoveAt(snake.Count - 1);
                            snake.Add(headSnake);

                            MoveSnake();
                        }
                    }
                    break;
                case Direction.RIGHT:
                    if (headSnake.X == Console.WindowWidth - 2 || IsSnake())
                    {
                        GameOver();
                    }
                    else
                    {
                        if (headSnake.X + 1 == food.X && headSnake.Y == food.Y)
                        {
                            AddScore();

                            headSnake.X++;
                            snake.Add(headSnake);

                            Console.SetCursorPosition(headSnake.X, headSnake.Y);
                            Console.Write('O');

                            GenerateFood();
                        }
                        else
                        {
                            Console.SetCursorPosition(snake[0].X, snake[0].Y);
                            Console.Write(' ');

                            for (int i = 0; i < snake.Count - 1; i++)
                            {
                                snake[i] = snake[i + 1];
                            }
                            headSnake.X++;
                            snake.RemoveAt(snake.Count - 1);
                            snake.Add(headSnake);

                            MoveSnake();
                        }
                    }
                    break;
                case Direction.DOWN:
                    if (headSnake.Y == Console.WindowHeight - 3 || IsSnake())
                    {
                        GameOver();
                    }
                    else
                    {
                        if (headSnake.X == food.X && headSnake.Y + 1 == food.Y)
                        {
                            AddScore();

                            headSnake.Y++;
                            snake.Add(headSnake);

                            Console.SetCursorPosition(headSnake.X, headSnake.Y);
                            Console.Write('O');

                            GenerateFood();
                        }
                        else
                        {
                            Console.SetCursorPosition(snake[0].X, snake[0].Y);
                            Console.Write(' ');

                            for (int i = 0; i < snake.Count - 1; i++)
                            {
                                snake[i] = snake[i + 1];
                            }
                            headSnake.Y++;
                            snake.RemoveAt(snake.Count - 1);
                            snake.Add(headSnake);

                            MoveSnake();
                        }
                    }
                    break;
                case Direction.LEFT:
                    if (headSnake.X == 1 || IsSnake())
                    {
                        GameOver();
                    }
                    else
                    {
                        if (headSnake.X - 1 == food.X && headSnake.Y == food.Y)
                        {
                            AddScore();

                            headSnake.X--;
                            snake.Add(headSnake);

                            Console.SetCursorPosition(headSnake.X, headSnake.Y);
                            Console.Write('O');

                            GenerateFood();
                        }
                        else
                        {
                            Console.SetCursorPosition(snake[0].X, snake[0].Y);
                            Console.Write(' ');

                            for (int i = 0; i < snake.Count - 1; i++)
                            {
                                snake[i] = snake[i + 1];
                            }
                            headSnake.X--;
                            snake.RemoveAt(snake.Count - 1);
                            snake.Add(headSnake);

                            MoveSnake();
                        }
                    }
                    break;
            }
        }

        static void ShowScore()
        {
            Console.SetCursorPosition(1, 0);
            Console.WriteLine(score.ToString("0000"));
        }

        static void ShowGameField()
        {
            // левый верхний угол
            Console.SetCursorPosition(0, 1);
            Console.Write((char)0x2554);

            // правый верхний угол
            Console.SetCursorPosition(Console.WindowWidth - 1, 1);
            Console.Write((char)0x2557);

            // левый нижний угол
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write((char)0x255A);

            // правый нижний угол
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 2);
            Console.Write((char)0x255D);

            // горизонтальные стенки
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write((char)0x2550);

                Console.SetCursorPosition(i, Console.WindowHeight - 2);
                Console.Write((char)0x2550);
            }

            // вертикальные стенки
            for (int i = 2; i < Console.WindowHeight - 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write((char)0x2551);

                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.Write((char)0x2551);
            }
        }

        static void CreateSnake()
        {
            Coords temp = new Coords();
            int countElement = 5;

            for (int i = 0; i < countElement; i++)
            {
                temp.X = i + (Console.WindowWidth - countElement) / 2;
                temp.Y = Console.WindowHeight / 2;
                snake.Add(temp);

                Console.SetCursorPosition(temp.X, temp.Y);
                Console.Write('O');
            }

            headSnake = snake[snake.Count - 1];
        }

        static void GameOver()
        {
            t.Stop();
            gameOver = true;

            for (int i = 0; i < 6; i++)
            {
                foreach (Coords p in snake)
                {
                    Console.SetCursorPosition(p.X, p.Y);

                    if (i % 2 == 0)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write('O');
                    }
                }

                Thread.Sleep(150);
            }
        }

        static void MoveSnake()
        {
            Console.SetCursorPosition(headSnake.X, headSnake.Y);
            Console.Write('O');
        }

        static bool IsSnake()
        {
            for (int i = 0; i < snake.Count - 3; i++)
            {
                switch (d)
                {
                    case Direction.UP:
                        if (headSnake.X == snake[i].X && headSnake.Y - 1 == snake[i].Y)
                        {
                            return true;
                        }
                        break;
                    case Direction.RIGHT:
                        if (headSnake.X + 1 == snake[i].X && headSnake.Y == snake[i].Y)
                        {
                            return true;
                        }
                        break;
                    case Direction.DOWN:
                        if (headSnake.X == snake[i].X && headSnake.Y + 1 == snake[i].Y)
                        {
                            return true;
                        }
                        break;
                    case Direction.LEFT:
                        if (headSnake.X - 1 == snake[i].X && headSnake.Y == snake[i].Y)
                        {
                            return true;
                        }
                        break;
                }
            }

            return false;
        }

        static void GenerateFood()
        {
            bool hitTheSnake;

            do
            {
                hitTheSnake = false;

                food.X = rnd.Next(1, Console.WindowWidth - 1);
                food.Y = rnd.Next(2, Console.WindowHeight - 2);

                foreach (Coords p in snake)
                {
                    if (food.X == p.X && food.Y == p.Y)
                    {
                        hitTheSnake = true;
                        break;
                    }
                }
            } while (hitTheSnake);

            Console.SetCursorPosition(food.X, food.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('O');
            Console.ResetColor();
        }

        static void AddScore()
        {
            score += 9;
            ShowScore();
        }

        public static void Rules()
        {
            Console.WriteLine("Выращивайте змею, направляя её к еде стрелками. Остановить или развернуть змею нельзя. " +
                "Постарайтесь избежать\nстолкновения змеи со стеной или со\nсвоим хвостом.");
            Console.Write("\nP - пауза.");
        }
    }
}