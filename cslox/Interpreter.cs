using System;
using System.IO;
using System.Security.Principal;
using System.Text;

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

// Essentials needed for the interpeter to function
string fileContents = File.ReadAllText(filename);
Scanner scanner = new Scanner();
(List<string> tokens, bool lexicalError) = scanner.Scan(fileContents);

foreach(string token in tokens)
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