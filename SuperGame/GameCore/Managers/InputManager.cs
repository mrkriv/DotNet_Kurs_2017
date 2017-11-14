using System;
using System.Linq;
using System.Collections.Generic;

namespace GameCore.Managers
{
    public class InputManager
    {
        private readonly Dictionary<Key, bool> keyboard = new Dictionary<Key, bool>();

        public InputManager()
        {
            foreach (var keyCode in Enum.GetValues(typeof(Key)))
            {
                if (!keyboard.ContainsKey((Key) keyCode))
                    keyboard.Add((Key) keyCode, false);
            }
        }

        public void KeyDpwn(Key key)
        {
            keyboard[key] = true;
        }

        public void KeyUp(Key key)
        {
            keyboard[key] = false;
        }

        public bool IsKeyDown(Key key)
        {
            return keyboard[key];
        }
    }
}