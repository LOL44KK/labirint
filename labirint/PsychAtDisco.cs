using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class PsychAtDisco : Enemy 
    {
        private Direction _direction;

        public PsychAtDisco(Labyrinth labyrinth, Point coordinates, Direction directory) : base(labyrinth, coordinates)
        {
            _direction = directory;
        }

        public override void Move()
        {
            _oldCoordinates = new Point(_coordinates.X, _coordinates.Y);

            if (_direction == Direction.UP && _labyrinth.Map[_coordinates.Y - 1, _coordinates.X] != '#') 
            {
                _coordinates.Y--;
            }
            else if (_direction == Direction.DOWN && _labyrinth.Map[_coordinates.Y + 1, _coordinates.X] != '#') 
            {
                _coordinates.Y++;
            }
            else if(_direction == Direction.LEFT && _labyrinth.Map[_coordinates.Y, _coordinates.X - 1] != '#')
            {
                _coordinates.X--;
            }
            else if(_direction == Direction.RIGHT && _labyrinth.Map[_coordinates.Y, _coordinates.X + 1] != '#')
            {
                _coordinates.X++;
            }

            else if(_direction == Direction.UP && _labyrinth.Map[_coordinates.Y - 1, _coordinates.X] == '#')
            {
                _direction = Direction.DOWN;
            }
            else if (_direction == Direction.DOWN && _labyrinth.Map[_coordinates.Y + 1, _coordinates.X] == '#')
            {
                _direction = Direction.UP;
            }
            else if (_direction == Direction.LEFT && _labyrinth.Map[_coordinates.Y, _coordinates.X - 1] == '#')
            {
                _direction = Direction.RIGHT;
            }
            else if (_direction == Direction.RIGHT && _labyrinth.Map[_coordinates.Y, _coordinates.X + 1] == '#')
            {
                _direction = Direction.LEFT;
            }
        }
    }
}
