using AssistClickY.Models;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using WindowsInput.Native;
using WindowsInput;


namespace AssistClickY.Helpers.Input
{
    public class HotkeySender
    {
        public static void SendCustomHotkey(Hotkey hotkey)
        {

            var shiftActive = new bool();
            var controlActive = new bool();
            var altActive = new bool();
            var winActive = new bool();

            //performs a bitwise AND operation to see if the shift flag is on
            if ((hotkey.ModifierKeys & System.Windows.Input.ModifierKeys.Shift) != 0)
            {
                // Shift flag is active
                Trace.WriteLine("Shift is pressed");
                shiftActive = true;
            }
            if ((hotkey.ModifierKeys & System.Windows.Input.ModifierKeys.Control) != 0)
            {
                // Shift flag is active
                Trace.WriteLine("Control is pressed");
                controlActive = true;
            }
            if ((hotkey.ModifierKeys & System.Windows.Input.ModifierKeys.Alt) != 0)
            {
                // Shift flag is active
                Trace.WriteLine("Alt is pressed");
                altActive = true;
            }
            if ((hotkey.ModifierKeys & System.Windows.Input.ModifierKeys.Windows) != 0)
            {
                // Shift flag is active
                Trace.WriteLine("Windows is pressed");
                winActive = true;
            }

            VirtualKeyCode keyCode = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(hotkey.Key);

            var inputSimulator = new InputSimulator();

            if (controlActive)
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            }
            if (shiftActive)
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LSHIFT);
            }
            if (winActive)
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LWIN);
            }
            if (altActive)
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MENU);
            }

            inputSimulator.Keyboard.KeyPress(keyCode);

            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LSHIFT);

            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LWIN);

            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.MENU);
        }
    }
}
