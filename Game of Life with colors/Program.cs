using System;
using static GameOfLife;

class GameOfLife
{
    internal class Cell
    {
        public Cell() { }
        public Cell(int x, int y, ConsoleColor color)
        {
            X = x; Y = y; Color = color;
        }

        public int X;
        public int Y;
        public List<Cell> Neighbours = new List<Cell>();
        public bool ALive = false;
        public ConsoleColor Color;
    }

    internal class Map
    {
        public Map() { }
        public Map(int x, int y, Cell[] aLivingCells = null)
        {
            X = x; Y = y;
            Initialize(aLivingCells);
        }

        public void Draw()
        {
            ScanCells();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (_map[i, j].ALive)
                    {
                        Console.ForegroundColor = _map[i, j].Color;
                        Console.BackgroundColor = _map[i, j].Color;
                        Console.Write(_map[i, j].Neighbours.Count);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write("*");
                    }
                    //Thread.Sleep(1);
                }
                Console.WriteLine();
            }
            UpdateCells();
        }
        public void Clear()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    Console.Write("");
                }

            }
        }

        private void ScanCells()
        {
            foreach (Cell cell in _map)
                cell.Neighbours?.Clear();
            
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (_map[i, j].ALive)
                    {
                        if (i < X - 1 && j < Y - 1)
                            _map[i + 1, j + 1].Neighbours.Add(_map[i, j]);
                        if (i > 0 && j > 0)
                            _map[i - 1, j - 1].Neighbours.Add(_map[i, j]);

                        if (i > 0 && j < Y - 1)
                            _map[i - 1, j + 1].Neighbours.Add(_map[i, j]);
                        if (i < X - 1 && j > 0)
                            _map[i + 1, j - 1].Neighbours.Add(_map[i, j]);

                        if (i > 0)
                            _map[i - 1, j].Neighbours.Add(_map[i, j]);
                        if (i < X - 1)
                            _map[i + 1, j].Neighbours.Add(_map[i, j]);

                        if (j > 0)
                            _map[i, j - 1].Neighbours.Add(_map[i, j]);
                        if (j < Y - 1)
                            _map[i, j + 1].Neighbours.Add(_map[i, j]);
                    }
                }
            }
        }

        private void UpdateCells()
        {
            foreach (Cell cell in _map)
            {
                if (!cell.ALive && cell.Neighbours.Count == 3)
                {
                    if (cell.Neighbours.Where(cell => cell.Color == ConsoleColor.Red).ToList().Count == 3)
                        cell.Color = ConsoleColor.Red;
                    else if (cell.Neighbours.Where(cell => cell.Color == ConsoleColor.Green).ToList().Count == 3)
                        cell.Color = ConsoleColor.Green;
                    else if (cell.Neighbours.Where(cell => cell.Color == ConsoleColor.Blue).ToList().Count == 3)
                        cell.Color = ConsoleColor.Blue;
                    else
                        cell.Color = cell.Neighbours[new Random().Next(cell.Neighbours.Count)].Color;

                    cell.ALive = true;
                }
                else if (cell.ALive && cell.Neighbours.Count > 3 || cell.ALive && cell.Neighbours.Count < 2)
                    cell.ALive = false;
            }
        }

        private void Initialize(Cell[] aLivingCells)
        {
            _map = new Cell[X, Y];
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    _map[i, j] = new Cell(i, j, ConsoleColor.Gray);
                }
            }

            if (aLivingCells != null)
            {
                foreach (Cell cell in aLivingCells)
                {
                    if (cell.X < X && cell.Y < Y)
                    {
                        _map[cell.X, cell.Y].ALive = true;
                        _map[cell.X, cell.Y].Color = cell.Color;
                    }
                }
            }
        }
        public int X;
        public int Y;
        private Cell[,] _map;
    }

    public static void Main()
    {
        int sizeY = 29;
        int sizeX = 120;

        List<Cell> cells = new List<Cell>();
        Random rnd = new Random();

        /*for (int i = 0; i < 750; i++)
            cells.Add(new Cell(rnd.Next(0, sizeY), rnd.Next(0, sizeX), ConsoleColor.Green));
        for (int i = 0; i < 300; i++)
            cells.Add(new Cell(rnd.Next(0, sizeY), rnd.Next(0, sizeX), ConsoleColor.Red));
        for (int i = 0; i < 350; i++)
            cells.Add(new Cell(rnd.Next(0, sizeY), rnd.Next(0, sizeX), ConsoleColor.Blue));*/


        // Glider // 
        /*cells.Add(new Cell(2, 2, ConsoleColor.Green));
        cells.Add(new Cell(3, 3, ConsoleColor.Red));
        cells.Add(new Cell(4, 3, ConsoleColor.Red));
        cells.Add(new Cell(4, 2, ConsoleColor.Green));
        cells.Add(new Cell(4, 1, ConsoleColor.Red));*/

        // Pulsar // 
        /*cells.Add(new Cell(15, 30, ConsoleColor.Blue));
        cells.Add(new Cell(16, 29, ConsoleColor.Blue));
        cells.Add(new Cell(16, 30, ConsoleColor.Blue));
        cells.Add(new Cell(16, 31, ConsoleColor.Blue));

        cells.Add(new Cell(15, 36, ConsoleColor.Red));
        cells.Add(new Cell(16, 35, ConsoleColor.Red));
        cells.Add(new Cell(16, 36, ConsoleColor.Red));
        cells.Add(new Cell(16, 37, ConsoleColor.Red));*/

        Map map = new Map(sizeY, sizeX, cells.ToArray());

        Console.CursorVisible = false;
        for (int i = 0; i < 1000; i++)
        {
            map.Draw();
            Thread.Sleep(0);
            map.Clear();
        }
    }
}