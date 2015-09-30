using System;
using OpenTK.Input;
using System.Collections.Generic;
using OpenTK;

namespace Labirint_Kiir
{
    class Input
    {
        private static List<Key> keysDown;
        private static List<Key> keysDownLast;
        private static List<MouseButton> buttonDown;
        private static List<MouseButton> buttonDownLast;

        public static void Initialize(GameWindow game)
        {
            keysDown = new List<Key>();
            keysDownLast = new List<Key>();
            buttonDown = new List<MouseButton>();
            buttonDownLast = new List<MouseButton>();

            game.KeyDown += game_KeyDown;
            game.KeyUp += game_KeyUp;
            game.MouseDown += game_MouseDown;
            game.MouseUp += game_MouseUp;
        }

        static void game_MouseUp(object sender, MouseButtonEventArgs e)
        {
            while (buttonDown.Contains(e.Button))
            {
                buttonDown.Remove(e.Button);
            }
        }

        static void game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonDown.Add(e.Button);
        }

        static void game_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            while (keysDown.Contains(e.Key))
            {
                keysDown.Remove(e.Key);
            }
        }

        static void game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
        }
        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            buttonDownLast = new List<MouseButton>(buttonDown);
        }
        public static bool KeyPress(Key key)
        {
            return (keysDown.Contains(key) && !keysDownLast.Contains(key));
        }
        public static bool KeyRelease(Key key)
        {
            return (!keysDown.Contains(key) && keysDownLast.Contains(key));
        }
        public static bool KeyDown(Key key)
        {
            return (keysDown.Contains(key));
        }

        public static bool MousePress(MouseButton button)
        {
            return (buttonDown.Contains(button) && !buttonDownLast.Contains(button));
        }
        public static bool MouseRelease(MouseButton button)
        {
            return (!buttonDown.Contains(button) && buttonDownLast.Contains(button));
        }
        public static bool MouseDown(MouseButton button)
        {
            return (buttonDown.Contains(button));
        }
    }
}
