using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        int playfieldWidth = 20;
        int playfieldHeight = 20;

        Console.WindowWidth = playfieldWidth * 2;
        Console.WindowHeight = playfieldHeight;

        char[,] playfield = new char[playfieldWidth, playfieldHeight];

        // Заполняем внешний периметр символами "#"
        for (int x = 0; x < playfieldWidth; x++)
        {
            for (int y = 0; y < playfieldHeight; y++)
            {
                if (x == 0 || x == playfieldWidth - 1 || y == 0 || y == playfieldHeight - 1)
                {
                    playfield[x, y] = '#';
                }
                else
                {
                    playfield[x, y] = ' ';
                }
            }
        }

        // Создаем змейку и помещаем ее в центр
        List<Point> snake = new List<Point>() { new Point(playfieldWidth / 2, playfieldHeight / 2) };
        int snakeLength = 1;

        // Устанавливаем начальное направление движения змейки вправо
        int dx = 1;
        int dy = 0;

        // Создаем переменные для координат яблока
        int appleX = new Random().Next(1, playfieldWidth - 1);
        int appleY = new Random().Next(1, playfieldHeight - 1);

        // Основной игровой цикл
        while (true)
        {
            Console.Clear();

            // Отображаем каждый символ из игрового поля
            for (int y = 0; y < playfieldHeight; y++)
            {
                for (int x = 0; x < playfieldWidth; x++)
                {
                    if (snake.Any(p => p.X == x && p.Y == y))
                    {
                        Console.Write("O ");
                    }
                    else if (x == appleX && y == appleY)
                    {
                        Console.Write("A "); // Отображаем яблоко
                    }
                    else
                    {
                        Console.Write(playfield[x, y] + " ");
                    }
                }
                Console.WriteLine();
            }

            // Управление змейкой
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (dy != 1) // Проверка на противоположное направление
                        {
                            dx = 0;
                            dy = -1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (dy != -1)
                        {
                            dx = 0;
                            dy = 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (dx != 1)
                        {
                            dx = -1;
                            dy = 0;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (dx != -1)
                        {
                            dx = 1;
                            dy = 0;
                        }
                        break;
                }
            }

            // Двигаем змейку
            Point newHead = new Point(snake.Last().X + dx, snake.Last().Y + dy);

            // Проверка на столкновение с границами
            if (newHead.X < 1 || newHead.X >= playfieldWidth - 1 || newHead.Y < 1 || newHead.Y >= playfieldHeight - 1)
            {
                break; // Игра окончена
            }

            // Проверка на столкновение с собственным телом
            if (snake.Any(p => p.X == newHead.X && p.Y == newHead.Y))
            {
                break; // Игра окончена
            }

            snake.Add(newHead); // Добавляем новую голову змейке

            // Проверка на съедание яблока и увеличение длины змейки
            if (newHead.X == appleX && newHead.Y == appleY)
            {
                snakeLength++; // Увеличиваем длину змейки
                appleX = new Random().Next(1, playfieldWidth - 1); // Генерируем новое положение яблока
                appleY = new Random().Next(1, playfieldHeight - 1);
            }
            else
            {
                if (snake.Count > snakeLength)
                {
                    snake.RemoveAt(0); // Убираем хвост, если длина змейки превысила текущую длину
                }
            }

            // Ждем некоторое время перед следующим обновлением экрана
            Thread.Sleep(60);
        }

        Console.Clear();
        Console.WriteLine("Игра окончена. Нажмите любую клавишу для выхода.");
        Console.ReadKey();
    }
}

// Дополнительный класс для хранения координат точек
class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
