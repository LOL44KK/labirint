using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class SafariHunter : Enemy
    {
        private Player _player;

        public SafariHunter(Labyrinth labyrinth, Point coordinates, Player player) : base(labyrinth, coordinates)
        {
            _player = player;
        }

        public override void Move()
        {
            Point start = _coordinates;
            Point end = _player.Coordinates;

            List<Point> path = FindPath(_labyrinth.Map, start, end);
            if (path != null && path.Count > 1)
            {
                _oldCoordinates = start;
                _coordinates = path[1];
            }
        }

        private List<Point> FindPath(char[,] map, Point start, Point end)//не я писал
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            bool[,] visited = new bool[rows, cols];
            Queue<Point> queue = new Queue<Point>();
            Dictionary<Point, Point> parent = new Dictionary<Point, Point>();

            queue.Enqueue(start);
            visited[start.Y, start.X] = true;

            int[] dRow = { -1, 1, 0, 0 };
            int[] dCol = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.Equals(end))
                {
                    return ReconstructPath(parent, start, end);
                }

                for (int i = 0; i < 4; i++)
                {
                    int newRow = current.Y + dRow[i];
                    int newCol = current.X + dCol[i];

                    if (IsValidMove(newRow, newCol, rows, cols, map, visited))
                    {
                        var neighbor = new Point(newCol, newRow);
                        queue.Enqueue(neighbor);
                        visited[newRow, newCol] = true;
                        parent[neighbor] = current;
                    }
                }
            }

            return null;
        }

        private bool IsValidMove(int row, int col, int rows, int cols, char[,] map, bool[,] visited)//не я писал
        {
            return row >= 0 && row < rows && col >= 0 && col < cols && map[row, col] != '#' && !visited[row, col];
        }

        private List<Point> ReconstructPath(Dictionary<Point, Point> parent, Point start, Point end)//не я писал
        {
            List<Point> path = new List<Point>();
            var current = end;
            while (!current.Equals(start))
            {
                path.Add(current);
                current = parent[current];
            }
            path.Add(start);
            path.Reverse();
            return path;
        }
    }
}
