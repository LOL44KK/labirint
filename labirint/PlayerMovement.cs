using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class PlayerMovement
    {
        private Player _player;
        private Labyrinth _labyrinth;

        public PlayerMovement(Player player, Labyrinth labyrinth)
        {
            _player = player;
            _labyrinth = labyrinth;
        }

        public void Movement(ConsoleKey key)
        {
            if (key == ConsoleKey.W && _player.Coordinates.Y - 1 >= 0 && _labyrinth.Map[_player.Coordinates.Y - 1, _player.Coordinates.X] != '#' )
            {
                _player.Move(0, -1);
                _player.Direction = Direction.UP;
            }
            if (key == ConsoleKey.S && _player.Coordinates.Y + 1 < _labyrinth.Map.GetLength(0) && _labyrinth.Map[_player.Coordinates.Y + 1, _player.Coordinates.X] != '#')
            {
                _player.Move(0, 1);
                _player.Direction = Direction.DOWN;
            }
            if (key == ConsoleKey.A && _player.Coordinates.X - 1 >= 0  && _labyrinth.Map[_player.Coordinates.Y, _player.Coordinates.X - 1] != '#')
            {
                _player.Move(-1, 0);
                _player.Direction = Direction.LEFT;
            }
            if (key == ConsoleKey.D && _player.Coordinates.X + 1 < _labyrinth.Map.GetLength(1) && _labyrinth.Map[_player.Coordinates.Y, _player.Coordinates.X + 1] != '#')
            {
                _player.Move(1, 0);
                _player.Direction = Direction.RIGHT;
            }
        }
    }
}
