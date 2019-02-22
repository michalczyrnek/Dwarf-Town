using Dwarf_Town.Interfaces;
using System;

namespace Dwarf_Town
{
    public class WindowsConsole : IOutputWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}