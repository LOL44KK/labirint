using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public abstract class Enemy
    {
        protected Labyrinth _labyrinth;
        protected Point _coordinates;
        protected Point _oldCoordinates;
        protected int _onHitHealth = 25;

        public virtual Point Coordinates { get { return _coordinates; } }

        public int OnHitHealth { get { return _onHitHealth; } }

        public Enemy(Labyrinth labyrinth, Point coordinates)
        {
            _coordinates = coordinates;
            _labyrinth = labyrinth;
        }

        public virtual void Draw(Point shiftDraw)
        {
            if (_oldCoordinates.X != _coordinates.X || _oldCoordinates.Y != _coordinates.Y)
            {
                Console.SetCursorPosition(_oldCoordinates.X + shiftDraw.X, _oldCoordinates.Y + shiftDraw.Y);
                Console.Write(_labyrinth.Map[_oldCoordinates.Y, _oldCoordinates.X]);
            }

            Console.SetCursorPosition(_coordinates.X + shiftDraw.X, _coordinates.Y + shiftDraw.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("☺");
            Console.ResetColor();
        }

        public abstract void Move();
    }
}
