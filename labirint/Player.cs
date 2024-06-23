using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class Player
    {
        Labyrinth _labyrinth;
        private Point _coordinates;
        private Point _oldCoordinates;
        private Direction _direction;
        public int _health;
        public int _energy;

        public Point Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        public Direction Direction 
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; } 
        }

        public int Energy
        {
            get { return _energy; }
            set { _energy = value; }
        }

        public Player(Labyrinth labyrinth, Point coordinates, int health=25, int energy=500) {
            _labyrinth = labyrinth;
            _coordinates = coordinates;
            _health = health;
            _energy = energy;
        }

        public void Move(int x = 0, int y = 0)
        {
            _oldCoordinates = new Point(_coordinates.X, _coordinates.Y);
            _energy--;
            _coordinates.X = _coordinates.X + x;
            _coordinates.Y = _coordinates.Y + y;
        }

        public void Hit(int health)
        {
            _health -= health;
        }

        public void Heal(int health) {
            if (_health < 100)
            {
                _health += health;
            }
        }

        public void Draw(Point shiftDraw)
        {
            if (_oldCoordinates.X != _coordinates.X || _oldCoordinates.Y != _coordinates.Y)
            {
                Console.SetCursorPosition(_oldCoordinates.X + shiftDraw.X, _oldCoordinates.Y + shiftDraw.Y);
                Console.Write(_labyrinth.Map[_oldCoordinates.Y, _oldCoordinates.X]);
            }
            Console.SetCursorPosition(_coordinates.X + shiftDraw.X, _coordinates.Y + shiftDraw.Y);
            Console.Write("@");
        }
    }
}
