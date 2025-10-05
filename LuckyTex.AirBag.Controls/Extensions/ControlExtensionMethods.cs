#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

#endregion

namespace LuckyTex
{
    /// <summary>
    /// Controls Extension Methods.
    /// </summary>
    public static class ControlExtensionMethods
    {
        /// <summary>
        /// Checks is numeric input. Used in PreviewKeyDown handler.
        /// </summary>
        /// <param name="ctrl">The controls.</param>
        /// <param name="e">The KeyEventArgs.</param>
        /// <returns>Returns true if the key is consider that can be used as numeric input text.</returns>
        public static bool IsNumericInput(this Control ctrl, KeyEventArgs e)
        {
            bool isNumPadNumeric = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal;
            bool isNumeric = (e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod;

            if ((isNumeric || isNumPadNumeric) && Keyboard.Modifiers != ModifierKeys.None)
            {
                return true;
            }

            bool isControl = ((Keyboard.Modifiers != ModifierKeys.None && Keyboard.Modifiers != ModifierKeys.Shift)
                || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Insert
                || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up
                || e.Key == Key.Tab
                || e.Key == Key.PageDown || e.Key == Key.PageUp
                || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape
                || e.Key == Key.Home || e.Key == Key.End);

            return (isControl || isNumeric || isNumPadNumeric);
        }

        public static bool IsNumeric(this Control ctrl, KeyEventArgs e)
        {
            bool isNumPadNumeric = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal;
            bool isNumeric = (e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod;

            if ((isNumeric || isNumPadNumeric) && Keyboard.Modifiers != ModifierKeys.None)
            {
                return true;
            }

            bool isControl = ((Keyboard.Modifiers != ModifierKeys.None && Keyboard.Modifiers != ModifierKeys.Shift)
                || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Insert
                || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up
                || e.Key == Key.Tab
                || e.Key == Key.PageDown || e.Key == Key.PageUp
                || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape
                || e.Key == Key.Home || e.Key == Key.End
                || e.Key == Key.Add || e.Key == Key.Subtract || e.Key == Key.Multiply || e.Key == Key.Divide || e.Key == Key.Oem2 || e.Key == Key.OemComma
                || e.Key == Key.RightShift || e.Key == Key.LeftShift);

            return (isControl || isNumeric || isNumPadNumeric);
        }

    }
}
