using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirint
{
    public class Input
    {
        public delegate void InputDelegate(ConsoleKey key);
        private InputDelegate _inputs;

        public void AddInput(InputDelegate input)
        {
            _inputs += input;
        }

        public void DelInput(InputDelegate input)
        {
            _inputs -= input;
        }

        public void GetInput()
        {
            if (Console.KeyAvailable && _inputs != null)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                _inputs.Invoke(key.Key);
            }
        }
    }
}
