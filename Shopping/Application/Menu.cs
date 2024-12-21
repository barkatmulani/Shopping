using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    public class Menu
    {
        private string _heading;
        private List<MenuOption> _options;
        private int _level;
        private bool _exitAfterSelection;

        public Menu(string heading, List<MenuOption> options, int level = 1, bool exitAfterSelection = false)
        {
            _heading = heading;
            _options = options;
            _level = level;
            _exitAfterSelection = exitAfterSelection;
        }

        public void Display()
        {
            DisplayMenu();

            bool loop = true;

            while (loop)
            {
                var input = Console.ReadKey();

                int.TryParse((input.KeyChar).ToString(), out int num);
                if (num < 1 || num > _options.Count)
                {
                    Console.WriteLine("\nInvalid selection");
                }
                else
                {
                    var option = _options[num - 1];
                    option.Fn();
                    if (_exitAfterSelection || option.IsExist)
                    {
                        loop = false;
                    }
                    else
                    {
                        DisplayMenu();
                    }
                }
            }
        }

        private void DisplayMenu() {
            Console.WriteLine($"\n{_heading}");
            Console.WriteLine(new string((_level == 1 ? '=' : '-'), _heading.Length));

            for (int i = 0; i < _options.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_options[i].Text}");
            }
        }
    }

    public class MenuOption
    {
        private string _text;
        private Action _fn;
        private bool _isExist;

        public MenuOption(string text, Action fn, bool isExist = false)
        {
            _text = text;
            _fn = fn;
            _isExist = isExist;
        }

        public string Text { get { return _text; } }
        public Action Fn { get { return _fn; } }
        public bool IsExist { get { return _isExist; } }
    }
}
