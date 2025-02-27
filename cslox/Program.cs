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
int line = 1;
bool lexicalError = false;

if (!string.IsNullOrEmpty(fileContents))
{
    for (int i = 0; i < fileContents.Length; i++)
    {
        char character = fileContents[i];
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
            case '=':
                if (i != fileContents.Length - 1 && fileContents[i + 1] == '=')
                {
                    tokens.Add("EQUAL_EQUAL == null");
                    i += 1;
                }
                else
                {
                    tokens.Add("EQUAL = null");
                }
                break;
            default:
                tokens.Add($"[line {line}] Error: Unexpected Character: {character}");
                lexicalError = true;
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
    if (token.Contains("Error"))
    {
        Console.Error.WriteLine(token);
    }
    else
    {
        Console.WriteLine(token);
    }
}
if (lexicalError)
{
    Environment.Exit(65);
}
Environment.Exit(0);