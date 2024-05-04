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
        public List<ConsoleLine> Line { get; private set; }

        public ConsoleBuilder()
        {
            Line = new List<ConsoleLine>();
        }
        public ConsoleBuilder(string text, Func<bool> isColor = null, ConsoleColor color = ConsoleColor.White, bool isNewLine = true)
        {
            Line = new List<ConsoleLine>();
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
            Line.Add(new ConsoleLine($"{text}\n", isColor, color));
            return this;
        }

        public ConsoleBuilder Append(string text, Func<bool> isColor = null, ConsoleColor color = ConsoleColor.White)
        {
            Line.Add(new ConsoleLine(text, isColor, color));
            return this;
        }

        public ConsoleBuilder Combine(ConsoleBuilder cb)
        {
            Line.AddRange(cb.Line);
            return this;
        }


        public void Display()
        {
            Line.ForEach(line =>
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