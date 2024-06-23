using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class Labyrinth
    {
        private int _height;
        private int _width;
        private int _difficulty;
        LabyrinthGenerator _generator;
        private char[,] _map;
        private int _coin;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; }
        }
        public char[,] Map { get { return _map; } }
        public int Coin { get { return _coin; } }
        public Point EntranceDoor { get; private set; }
        public Point ExitDoor { get; private set; }

        public Labyrinth(int width, int height, int difficulty)
        {
            _generator = new LabyrinthGenerator();
            _map = _generator.Generate(height, width, difficulty);
            _height = height;
            _width = width;
            _difficulty = difficulty;

            EntranceDoor = _generator.EntranceDoor;
            ExitDoor = _generator.ExitDoor;
            _coin = _generator.Coin;
        }

        public bool ColletCoin(Point coinCoord)
        {
            if (_map[coinCoord.Y, coinCoord.X] == '$')
            {
                _map[coinCoord.Y, coinCoord.X] = ' ';
                _coin--;
                return true;
            }
            return false;
        }

        public bool ColletCoffe(Point coffeCoord)
        {
            if (_map[coffeCoord.Y, coffeCoord.X] == 'U')
            {
                _map[coffeCoord.Y, coffeCoord.X] = ' ';
                return true;
            }
            return false;
        }

        public bool ColletHeart(Point heartCoord)
        {
            if (_map[heartCoord.Y, heartCoord.X] == '♥')
            {
                _map[heartCoord.Y, heartCoord.X] = ' ';
                return true;
            }
            return false;
        }

        public bool BreakBlock(Point coord)
        {
            if (coord.Y >= 1 && coord.Y < _map.GetLength(0)-1 && coord.X >= 1 && coord.X < _map.GetLength(1)-1)
            {
                ColletCoin(coord);
                _map[coord.Y, coord.X] = ' ';
                return true;
            }
            return false;
        }

        public void ReGenerateMap()
        {
            _map = _generator.Generate(_height, _width, _difficulty);
            EntranceDoor = _generator.EntranceDoor;
            ExitDoor = _generator.ExitDoor;
        }

        public void Draw(int x=0, int y=0)
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (_map[i, j] == '#')
                    {
                        Console.Write('▓');
                    }
                    else
                    {
                        Console.Write(_map[i, j]);
                    }
                }
                y++;
            }
        }
    }
}
