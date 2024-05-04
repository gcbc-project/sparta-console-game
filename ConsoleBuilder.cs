using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public struct ConsoleLine
    {
        public string Str { get; private set; }
        public Func<bool> IsColor { get; private set; }
        public ConsoleColor Color { get; private set; }
        public ConsoleLine(string str, Func<bool> isColor = null, ConsoleColor color = ConsoleColor.White)
        {
            Str = str;
            IsColor = isColor;
            Color = color;
        }
    }
    public class ConsoleBuilder
    {
        private List<ConsoleLine> _lines { get; set; }

        public ConsoleBuilder()
        {
            _lines = new List<ConsoleLine>();
        }
        public ConsoleBuilder(string text, Func<bool> isColor = null, ConsoleColor color = ConsoleColor.White, bool isNewLine = true)
        {
            _lines = new List<ConsoleLine>();
            if (isNewLine)
            {
                AppendLine(text, isColor, color);
            }
            else
            {
                Append(text, isColor, color);
            }
        }

        public ConsoleBuilder AppendLine(string text, Func<bool> isColor = null, ConsoleColor color = ConsoleColor.White)
        {
            _lines.Add(new ConsoleLine($"{text}\n", isColor, color));
            return this;
        }

        public ConsoleBuilder Append(string text, Func<bool> isColor = null, ConsoleColor color = ConsoleColor.White)
        {
            _lines.Add(new ConsoleLine(text, isColor, color));
            return this;
        }

        public void Display()
        {
            _lines.ForEach(line =>
            {
                if (line.IsColor != null && line.IsColor.Invoke())
                {
                    Console.ForegroundColor = line.Color;
                }
                Console.Write(line.Str);
                Console.ResetColor();
            });
        }
    }
}