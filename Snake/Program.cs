using System;

namespace Snake
{
    class Program
    {
        static int cursorLeftCentre;
        static int cursorTopCentre;

        static void Main()
        {
            Console.Title = "Змейка";
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight = Snake.fieldHeight + 4;
            Console.BufferWidth = Console.WindowWidth = Snake.fieldWidth + 2;

            cursorLeftCentre = (Console.WindowWidth / 2);
            cursorTopCentre = (Console.WindowHeight / 2);

            Stand(cursorLeftCentre - 5, cursorTopCentre - 4, ConsoleColor.Green, "Новая игра");
            Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Уровень");
            Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Правила");
            Stand(cursorLeftCentre - 3, cursorTopCentre + 2, ConsoleColor.DarkGreen, "Выход");

            ConsoleKeyInfo cki;
            int cur = 1; // какой пункт выбран сейчас

            while (true)
            {
                cki = Console.ReadKey(true);

                switch (cki.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (cur < 4)
                        {
                            cur++;

                            switch (cur)
                            {
                                case 2:
                                    Stand(cursorLeftCentre - 5, cursorTopCentre - 4, ConsoleColor.DarkGreen, "Новая игра");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.Green, "Уровень");
                                    break;
                                case 3:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Уровень");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.Green, "Правила");
                                    break;
                                case 4:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Правила");
                                    Stand(cursorLeftCentre - 3, cursorTopCentre + 2, ConsoleColor.Green, "Выход");
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (cur > 1)
                        {
                            cur--;

                            switch (cur)
                            {
                                case 1:
                                    Stand(cursorLeftCentre - 5, cursorTopCentre - 4, ConsoleColor.Green, "Новая игра");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Уровень");
                                    break;
                                case 2:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.Green, "Уровень");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Правила");
                                    break;
                                case 3:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.Green, "Правила");
                                    Stand(cursorLeftCentre - 3, cursorTopCentre + 2, ConsoleColor.DarkGreen, "Выход");
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.ResetColor();

                        switch (cur)
                        {
                            case 1:
                                Console.Clear();
                                Snake.StartGame();

                                Console.Clear();
                                Stand(cursorLeftCentre - 5, cursorTopCentre - 4, ConsoleColor.Green, "Новая игра");
                                Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Уровень");
                                Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Правила");
                                Stand(cursorLeftCentre - 3, cursorTopCentre + 2, ConsoleColor.DarkGreen, "Выход");
                                break;
                            case 2:
                                MenuLevel();

                                Console.Clear();
                                Stand(cursorLeftCentre - 5, cursorTopCentre - 4, ConsoleColor.DarkGreen, "Новая игра");
                                Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.Green, "Уровень");
                                Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Правила");
                                Stand(cursorLeftCentre - 3, cursorTopCentre + 2, ConsoleColor.DarkGreen, "Выход");
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Snake.Rules();
                                Console.ResetColor();

                                do
                                {
                                    cki = Console.ReadKey(true);
                                } while (!(cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.Enter));

                                Console.Clear();
                                Stand(cursorLeftCentre - 5, cursorTopCentre - 4, ConsoleColor.DarkGreen, "Новая игра");
                                Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Уровень");
                                Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.Green, "Правила");
                                Stand(cursorLeftCentre - 3, cursorTopCentre + 2, ConsoleColor.DarkGreen, "Выход");
                                break;
                            case 4:
                                return;
                        }
                        break;
                }
            }
        }

        static void Stand(int cursorLeft, int cursorTop, ConsoleColor foregroundColor, string str)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.ForegroundColor = foregroundColor;
            Console.Write(str);
        }

        static void MenuLevel()
        {
            ConsoleKeyInfo cki;
            int cur = 0; // какой пункт выбран сейчас

            switch (Snake.MSecsPerFrame)
            {
                case Snake.Level.EASY:
                    Stand(cursorLeftCentre - 4, cursorTopCentre - 4, ConsoleColor.Green, "Лёгкий");
                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Средний");
                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Сложный");
                    cur = 1;
                    break;
                case Snake.Level.MEDIUM:
                    Stand(cursorLeftCentre - 4, cursorTopCentre - 4, ConsoleColor.DarkGreen, "Лёгкий");
                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.Green, "Средний");
                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Сложный");
                    cur = 2;
                    break;
                case Snake.Level.HARD:
                    Stand(cursorLeftCentre - 4, cursorTopCentre - 4, ConsoleColor.DarkGreen, "Лёгкий");
                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Средний");
                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.Green, "Сложный");
                    cur = 3;
                    break;
            }

            while (true)
            {
                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (cur < 3)
                        {
                            cur++;

                            switch (cur)
                            {
                                case 2:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 4, ConsoleColor.DarkGreen, "Лёгкий");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.Green, "Средний");
                                    break;
                                case 3:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Средний");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.Green, "Сложный");
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (cur > 1)
                        {
                            cur--;

                            switch (cur)
                            {
                                case 1:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 4, ConsoleColor.Green, "Лёгкий");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.DarkGreen, "Средний");
                                    break;
                                case 2:
                                    Stand(cursorLeftCentre - 4, cursorTopCentre - 2, ConsoleColor.Green, "Средний");
                                    Stand(cursorLeftCentre - 4, cursorTopCentre, ConsoleColor.DarkGreen, "Сложный");
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (cur)
                        {
                            case 1:
                                Snake.MSecsPerFrame = Snake.Level.EASY;
                                break;
                            case 2:
                                Snake.MSecsPerFrame = Snake.Level.MEDIUM;
                                break;
                            case 3:
                                Snake.MSecsPerFrame = Snake.Level.HARD;
                                break;
                        }
                        return;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}