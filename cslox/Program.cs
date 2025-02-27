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
List<String> tokens = new List<String>();

if (!string.IsNullOrEmpty(fileContents))
{
    foreach (char character in fileContents)
    {
        switch(character)
        {
            case '(':
                tokens.Add("LEFT_PAREN ( null");
                break;
            case ')':
                tokens.Add("RIGHT_PAREN ) null");
                break;
            case '{':
                tokens.Add("LEFT_BRACE { null");
                break;
            case '}':
                tokens.Add("RIGHT_BRACE } null");
                break;
            case '*':
                tokens.Add("STAR * null");
                break;
            case '.':
                tokens.Add("DOT . null");
                break;
            case ',':
                tokens.Add("COMMA , null");
                break;
            case '+':
                tokens.Add("PLUS + null");
                break;
            case '-':
                tokens.Add("MINUS - null");
                break;
            case ';':
                tokens.Add("SEMICOLON ; null");
                break;
            case '/':
                tokens.Add("SLASH / null");
                break;
        }
     }
     tokens.Add("EOF null");
}
else
{
    tokens.Add("EOF  null"); // Placeholder, remove this line when implementing the scanner
}

foreach(String token in tokens)
{
    Console.WriteLine(token);
}