class GameOfLife
{
    internal class Cell
    {
        public Cell() { }
        public Cell(int x, int y, byte neigbours = 0)
        { X = x; Y = y; Neigbours = neigbours; }

        public int X;
        public int Y;
        public byte Neigbours;
        public bool ALive = false;
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("#");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write("*");
                    }
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
                cell.Neigbours = 0;
            foreach (Cell cell in _map)
            {
                if (cell.ALive)
                {
                    if (cell.X < _map.GetLength(0) - 1 && cell.Y < _map.GetLength(1) - 1)
                        _map[cell.X + 1, cell.Y + 1].Neigbours++;
                    if (cell.X > 0 && cell.Y > 0)
                        _map[cell.X - 1, cell.Y - 1].Neigbours++;

                    if (cell.X > 0 && cell.Y < _map.GetLength(1) - 1)
                        _map[cell.X - 1, cell.Y + 1].Neigbours++;
                    if (cell.Y > 0 && cell.X < _map.GetLength(0) - 1)
                        _map[cell.X + 1, cell.Y - 1].Neigbours++;

                    if (cell.Y < _map.GetLength(1) - 1)
                        _map[cell.X, cell.Y + 1].Neigbours++;
                    if (cell.Y > 0)
                        _map[cell.X, cell.Y - 1].Neigbours++;

                    if (cell.X < _map.GetLength(0) - 1)
                        _map[cell.X + 1, cell.Y].Neigbours++;
                    if (cell.X > 0)
                        _map[cell.X - 1, cell.Y].Neigbours++;
                }
            }
        }

        private void UpdateCells()
        {
            foreach (Cell cell in _map)
            {
                if (!cell.ALive && cell.Neigbours == 3)
                    cell.ALive = true;
                else if (cell.ALive && cell.Neigbours > 3 || cell.ALive && cell.Neigbours < 2)
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
                    _map[i, j] = new Cell(i, j);
                }
            }

            if (aLivingCells != null)
            {
                foreach (Cell cell in aLivingCells)
                {
                    if (cell.X < X && cell.Y < Y)
                        _map[cell.X, cell.Y].ALive = true;
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

        for (int i = 0; i < 500; i++)
            cells.Add(new Cell(rnd.Next(0, sizeY), rnd.Next(0, sizeX)));


        // Glider // 
        /*cells.Add(new Cell(10, 10));
        cells.Add(new Cell(11, 11));
        cells.Add(new Cell(12, 11));
        cells.Add(new Cell(12, 10));
        cells.Add(new Cell(12, 9));*/

        // Pulsar // 
        /*cells.Add(new Cell(15, 30));
        cells.Add(new Cell(16, 29));
        cells.Add(new Cell(16, 30));
        cells.Add(new Cell(16, 31));

        cells.Add(new Cell(15, 36));
        cells.Add(new Cell(16, 35));
        cells.Add(new Cell(16, 36));
        cells.Add(new Cell(16, 37));*/

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