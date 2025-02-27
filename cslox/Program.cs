using System;
using System.IO;

if (args.Length < 2)
{
    Console.Error.WriteLine("Usage: ./your_program.sh tokenize <filename>");
    Environment.Exit(1);
}

string command = args[0];
string filename = args[1];

if (command != "tokenize")
{
    Console.Error.WriteLine($"Unknown command: {command}");
    Environment.Exit(1);
}

string fileContents = File.ReadAllText(filename);

if (!string.IsNullOrEmpty(fileContents))
{
     foreach (char character in fileContents)
     {
        if (character == '(')
        {
            Console.WriteLine("LEFT_PAREN ( null");
        }
        else if (character == ')')
        {
            Console.WriteLine("RIGHT_PAREN ) null");
        }
        else if (character == '{')
        {
            Console.WriteLine("LEFT_BRACE { null");
        }
        else if (character == '}')
        {
            Console.WriteLine("RIGHT_BRACE } null");
        }
     }
     Console.WriteLine("EOF null");
}
else
{
     Console.WriteLine("EOF  null"); // Placeholder, remove this line when implementing the scanner
}
