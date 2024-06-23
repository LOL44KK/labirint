using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class PlayerAttacks
    {
        private Labyrinth _labyrinth;
        private Player _player;
        private EnemyManager _enemyManager;
        private Point _shiftDraw;
        private Point _boombCoord;


        public PlayerAttacks(Player player, EnemyManager enemyManager, Labyrinth labyrinth, Point shiftDraw)
        {
            _player = player;
            _enemyManager = enemyManager;
            _labyrinth = labyrinth;
            _shiftDraw = shiftDraw;
            _boombCoord = new Point(-1, -1);
        }

        public void Attacks(ConsoleKey key)
        {
            if (key == ConsoleKey.Q)
            {
                _player.Energy -= 10;
                _enemyManager.Kill(new Point(_player.Coordinates.X + 1, _player.Coordinates.Y), _shiftDraw);
                _enemyManager.Kill(new Point(_player.Coordinates.X - 1, _player.Coordinates.Y), _shiftDraw);
                _enemyManager.Kill(new Point(_player.Coordinates.X, _player.Coordinates.Y + 1), _shiftDraw);
                _enemyManager.Kill(new Point(_player.Coordinates.X, _player.Coordinates.Y - 1), _shiftDraw);
                _enemyManager.Kill(new Point(_player.Coordinates.X + 1, _player.Coordinates.Y + 1), _shiftDraw);
                _enemyManager.Kill(new Point(_player.Coordinates.X - 1, _player.Coordinates.Y - 1), _shiftDraw);
                _enemyManager.Kill(new Point(_player.Coordinates.X + 1, _player.Coordinates.Y + 1), _shiftDraw);
                _enemyManager.Kill(new Point(_player.Coordinates.X - 1, _player.Coordinates.Y - 1), _shiftDraw);
            }
            else if (key == ConsoleKey.G)
            {
                _player.Energy -= 25;
                if (_player.Direction == Direction.UP)
                {
                    for (int i = _player.Coordinates.Y; _labyrinth.Map[i, _player.Coordinates.X] != '#'; i--)
                    {
                        _enemyManager.Kill(new Point(_player.Coordinates.X, i), _shiftDraw);
                    }
                }
                if (_player.Direction == Direction.DOWN)
                {
                    for (int i = _player.Coordinates.Y; _labyrinth.Map[i, _player.Coordinates.X] != '#'; i++)
                    {
                        _enemyManager.Kill(new Point(_player.Coordinates.X, i), _shiftDraw);
                    }
                }
                if (_player.Direction == Direction.LEFT)
                {
                    for (int i = _player.Coordinates.X; _labyrinth.Map[_player.Coordinates.Y, i] != '#'; i--)
                    {
                        _enemyManager.Kill(new Point(i, _player.Coordinates.Y), _shiftDraw);
                    }
                }
                if (_player.Direction == Direction.RIGHT)
                {
                    for (int i = _player.Coordinates.X; _labyrinth.Map[_player.Coordinates.Y, i] != '#'; i++)
                    {
                        _enemyManager.Kill(new Point(i, _player.Coordinates.Y), _shiftDraw);
                    }
                }
            }
            else if (key == ConsoleKey.Enter)
            {
                _player.Energy -= 49;
                if (_boombCoord.X != -1 && _boombCoord.Y != -1)
                {
                    _labyrinth.Map[_boombCoord.Y, _boombCoord.X] = ' ';
                    Console.SetCursorPosition(_boombCoord.X + _shiftDraw.X, _boombCoord.Y + _shiftDraw.Y);
                    Console.Write(' ');
                }
                _boombCoord.X = _player.Coordinates.X;
                _boombCoord.Y = _player.Coordinates.Y;
                _labyrinth.Map[_player.Coordinates.Y, _player.Coordinates.X] = '%';
            }
            else if (key == ConsoleKey.Spacebar) 
            {
                _player.Energy -= 1;
                if (_boombCoord.X != -1 && _boombCoord.Y != -1)
                {
                    for (int dx = -3; dx <= 3; dx++)
                    {
                        for (int dy = -3; dy <= 3; dy++)
                        {
                            _enemyManager.Kill(new Point(_boombCoord.X + dx, _boombCoord.Y + dy), _shiftDraw);
                            _labyrinth.BreakBlock(new Point(_boombCoord.X + dx, _boombCoord.Y + dy));
                            _labyrinth.Draw(_shiftDraw.X, _shiftDraw.Y);
                            if (_player.Coordinates.X == _boombCoord.X + dx && _player.Coordinates.Y == _boombCoord.Y + dy)
                            {
                                _player.Hit(100);
                            }
                        }
                    }
                    _labyrinth.Map[_boombCoord.Y, _boombCoord.X] = ' ';
                    Console.SetCursorPosition(_boombCoord.X + _shiftDraw.X, _boombCoord.Y + _shiftDraw.Y);
                    Console.Write(' ');
                    _boombCoord = new Point(-1, -1);
                }
            }
        }
    }
}
