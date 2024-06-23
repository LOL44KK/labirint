using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class EnemyManager
    {
        private Labyrinth _labyrinth;
        private Player _player;
        private List<Enemy> _enemyList;
        private int _coinCollected;

        public int CountEnemy
        {
            get { return _enemyList.Count; }
        }

        public int CoinCollected
        { 
            get { return _coinCollected; }
        }

        public EnemyManager(Labyrinth labyrinth, Player player, int countPsychAtDisco = 0,
            int countGuardInWarehouse = 0, int countSafariHunter = 0) {
            _labyrinth = labyrinth;
            _player = player;
            _enemyList = new List<Enemy>();

            Init(countPsychAtDisco, countGuardInWarehouse, countSafariHunter);
        }

        public void Init(int countPsychAtDisco = 0, int countGuardInWarehouse = 0, int countSafariHunter = 0)
        {
            _enemyList.Clear();
            for (int i = 0; i < countPsychAtDisco; i++)
            {
                AddPsychAtDisco();
            }

            for (int i = 0; i < countGuardInWarehouse; i++)
            {
                AddGuardInWarehouse();
            }

            for (int i = 0; i < countSafariHunter; i++)
            {
                AddSafariHunter();
            }
        }

        public void AddPsychAtDisco()
        {
            Random rand = new Random();
            while (true)
            {
                Point coord = new Point(rand.Next(0, _labyrinth.Map.GetLength(1)), rand.Next(0, _labyrinth.Map.GetLength(0)));
                if (_labyrinth.Map[coord.Y, coord.X] != '#')
                {
                    PsychAtDisco enemy = null;
                    if (_labyrinth.Map[coord.Y - 1, coord.X] != '#')
                    {
                        enemy = new PsychAtDisco(_labyrinth, coord, Direction.UP);
                    }
                    else if (_labyrinth.Map[coord.Y + 1, coord.X] != '#')
                    {
                        enemy = new PsychAtDisco(_labyrinth, coord, Direction.DOWN);
                    }
                    else if(_labyrinth.Map[coord.Y, coord.X - 1] != '#')
                    {
                        enemy = new PsychAtDisco(_labyrinth, coord, Direction.LEFT);
                    }
                    else if(_labyrinth.Map[coord.Y - 1, coord.X + 1] != '#')
                    {
                        enemy = new PsychAtDisco(_labyrinth, coord, Direction.RIGHT);
                    }

                    if (enemy != null) {
                        _enemyList.Add(enemy);
                        break;
                    }
                }
            }
        }

        public void AddGuardInWarehouse()
        {
            Random rand = new Random();
            while (true)
            {
                Point coord = new Point(rand.Next(0, _labyrinth.Map.GetLength(1)), rand.Next(0, _labyrinth.Map.GetLength(0)));
                if (_labyrinth.Map[coord.Y, coord.X] != '#')
                {
                    GuardInWarehouse enemy = null;
                    if (_labyrinth.Map[coord.Y - 1, coord.X] != '#')
                    {
                        enemy = new GuardInWarehouse(_labyrinth, coord, Direction.UP);
                    }
                    else if (_labyrinth.Map[coord.Y + 1, coord.X] != '#')
                    {
                        enemy = new GuardInWarehouse(_labyrinth, coord, Direction.DOWN);
                    }
                    else if (_labyrinth.Map[coord.Y, coord.X - 1] != '#')
                    {
                        enemy = new GuardInWarehouse(_labyrinth, coord, Direction.LEFT);
                    }
                    else if (_labyrinth.Map[coord.Y - 1, coord.X + 1] != '#')
                    {
                        enemy = new GuardInWarehouse(_labyrinth, coord, Direction.RIGHT);
                    }

                    if (enemy != null)
                    {
                        _enemyList.Add(enemy);
                        break;
                    }
                }
            }
        }

        public void AddSafariHunter()
        {
            Random rand = new Random();
            while (true)
            {
                Point coord = new Point(rand.Next(0, _labyrinth.Map.GetLength(1)), rand.Next(0, _labyrinth.Map.GetLength(0)));
                if (_labyrinth.Map[coord.Y, coord.X] != '#')
                {
                    SafariHunter enemy = new SafariHunter(_labyrinth, coord, _player);
                    _enemyList.Add(enemy);
                    break;
                }
            }
        }

        public void Move()
        {
            foreach (var enemy in _enemyList)
            {
                enemy.Move();
                if (enemy.GetType() == typeof(GuardInWarehouse))
                {
                    _labyrinth.ColletCoin(enemy.Coordinates);
                    _labyrinth.ColletCoffe(enemy.Coordinates);
                    _labyrinth.ColletHeart(enemy.Coordinates);
                }
            }
        }

        public void Draw(Point shiftDraw)
        {
            foreach (var enemy in _enemyList)
            {
                enemy.Draw(shiftDraw);
            }
        }

        public Enemy CheckCollisionEnemies(Point shiftDraw)
        {
            for (int i = 0; i < _enemyList.Count; i++)
            {
                if (_enemyList[i].Coordinates.X == _player.Coordinates.X && _enemyList[i].Coordinates.Y == _player.Coordinates.Y)
                {
                    Enemy enemy = _enemyList[i];
                    _enemyList.Remove(_enemyList[i]);
                    return enemy;
                }
            }
            return null;
        }

        public Enemy Kill(Point coord, Point shiftDraw)
        {
            for (int i = 0; i < _enemyList.Count; i++)
            {
                if (_enemyList[i].Coordinates == coord)
                {
                    Enemy enemy = _enemyList[i];
                    _enemyList.Remove(_enemyList[i]);
                    Console.SetCursorPosition(enemy.Coordinates.X + shiftDraw.X, enemy.Coordinates.Y + shiftDraw.Y);
                    Console.Write(_labyrinth.Map[enemy.Coordinates.Y, enemy.Coordinates.X]);
                    return enemy;
                }
            }
            return null;
        }
    }
}
