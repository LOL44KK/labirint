using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class LabyrinthGenerator
    {
        private char[,] _lab;
        private int _cycle;

        public Point EntranceDoor { get; private set; }
        public Point ExitDoor {  get; private set; }
        public int Coin {  get; private set; }

        public char[,] Generate(int height, int width, int difficulty)//не я писал частично
        {
            int h = MakeEven(height);
            int w = MakeEven(width);

            int length = difficulty;

            length = length == 0 ? 70 : length;
            ++h; ++w;

            _lab = new char[h, w];
            for (int i = 0; i < h; ++i)
                for (int j = 0; j < w; ++j)
                    _lab[i, j] = ' ';

            CreateBorder(w, h);
            CreateDoors(w, h);

            int x = 0;
            _cycle += h * w + (h * w) / 2;

            while (GetRandom(w, h, out x) > 0)
            {
                int row = x / w;
                int col = x % w;

                if (_lab[row, col] == '#') continue;
                int z = GetDirection(w);

                do
                {
                    if (_lab[row, col] == ' ') _lab[row, col] = '#';
                    x += z;
                    row = x / w;
                    col = x % w;
                    if (MyRand(100) > length) break;
                } while (_lab[row, col] == ' ');
            }
            CoinFill(w, h);
            CoffeFill(w, h);
            HeartFill(w, h);
            return _lab;
        }

        private int MakeEven(int num) => num & 0xFE;//не я писал
        private int MyRand(int num) => num != 0 ? new Random().Next(num) : 0;//не я писал
        private int MyEvenRand(int num) => MyRand(num) & 0xFE;//не я писал
        private int EvenMax(int num) => num & 0xFE;//не я писал
        private int EvenAverage(int num) => (EvenMax(num) / 2) & 0xFE;//не я писал
        private int MyOddRand(int num) => MyRand(num) | 0x01;//не я писал

        private void CreateBorder(int width, int height)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        _lab[i, j] = '#';
                    }
                }
            }
        }

        private void CreateDoors(int width, int height)//не я писал
        {
            int t;
            do { t = OddBetween(0, width); } while (t == 0 || t == width);
            _lab[0, t] = '/';
            ExitDoor = new Point(t, 0);

            do { t = OddBetween(0, width); } while (t == 0 || t == width);
            _lab[height - 1, t] = '/';
            EntranceDoor = new Point(t, height - 1);
        }

        private void CoinFill(int width, int height)
        {
            int coin = 0;
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (_lab[i, j] == ' ' && new Random().Next(1, 5) == 1)
                    {
                        _lab[i, j] = '$';
                        coin++;
                    }
                }
            }
            Coin = coin;
        }

        private void CoffeFill(int width, int height)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (_lab[i, j] == ' ' && new Random().Next(1, 20) == 1)
                    {
                        _lab[i, j] = 'U';
                    }
                }
            }
        }

        private void HeartFill(int width, int height)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (_lab[i, j] == ' ' && new Random().Next(1, 20) == 1)
                    {
                        _lab[i, j] = '♥';
                    }
                }
            }
        }

        private int GetDirection(int width)//не я писал
        {
            int z = MyRand(4);
            return z == 0 ? -1 : z == 1 ? 1 : z == 2 ? width : -width;
        }

        private int OddBetween(int n1, int n2)//не я писал
        {
            int t;
            return (t = MyOddRand(n2)) > n1 ? t : OddBetween(n1, n2);
        }

        private int EvenBetween(int n1, int n2)//не я писал
        {
            int t;
            return (t = MyEvenRand(n2)) > n1 ? t : EvenBetween(n1, n2);
        }

        private int GetRandom(int width, int height, out int x)//не я писал
        {
            int row, col;
            row = EvenBetween(0, height);
            col = EvenBetween(0, width);
            x = width * row + col;
            if ((x % width) % 2 != 0)
            {
                GetRandom(width, height, out x);
            }
            return _cycle--;
        }
    }
}
