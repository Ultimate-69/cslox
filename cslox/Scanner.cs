public class Scanner
{
    string[] keywords = {"and", "class", "else", "false", "for", "fun", "if", "nil", "or", "print", "return",
    "super", "this", "true", "var", "while"};
    char[] validTokens = {'(', ')', '{', '}', '*', '.', ',', ';', '+', '-', '/'};
    int line = 1;
    bool lexicalError = false;
    
    public (List<String>, bool) Scan(string fileContents)
    {
        List<string> tokens = new List<string>();


        if (!String.IsNullOrEmpty(fileContents))
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
                        if (GetNextCharEquality(fileContents, '/', i))
                        {
                            int newlineIndex = fileContents.IndexOf('\n', i);
                            if (newlineIndex == -1)
                            {
                                // Not found
                                fileContents = fileContents.Substring(0, i);
                            }
                            else
                            {
                                fileContents = fileContents.Substring(0, i) + fileContents.Substring(newlineIndex);
                            }
                        }
                        else
                        {
                            tokens.Add("SLASH / null");
                        }
                        break;
                    case '=':
                        if (GetNextCharEquality(fileContents, '=', i))
                        {
                            tokens.Add("EQUAL_EQUAL == null");
                            i += 1;
                        }
                        else
                        {
                            tokens.Add("EQUAL = null");
                        }
                        break;
                    case '!':
                        if (GetNextCharEquality(fileContents, '=', i))
                        {
                            tokens.Add("BANG_EQUAL != null");
                            i += 1;
                        }
                        else
                        {
                            tokens.Add("BANG ! null");
                        }
                        break;
                    case '>':
                        if (GetNextCharEquality(fileContents, '=', i))
                        {
                            tokens.Add("GREATER_EQUAL >= null");
                            i += 1;
                        }
                        else
                        {
                            tokens.Add("GREATER > null");
                        }
                        break;
                    case '<':
                        if (GetNextCharEquality(fileContents, '=', i))
                        {
                            tokens.Add("LESS_EQUAL <= null");
                            i += 1;
                        }
                        else
                        {
                            tokens.Add("LESS < null");
                        }
                        break;
                    case '\t':
                    case '\r':
                    case ' ':
                        // All of these are whitespaces
                        break;
                    case '\n':
                        line += 1;
                        break;
                    case '"':
                        int terminatorIndex = fileContents.IndexOf('"', i + 1);
                        if (terminatorIndex == -1)
                        {
                            // Not found
                            tokens.Add($"[line {line}] Error: Unterminated String");
                            int newlineIndex = fileContents.IndexOf('\n', i);
                            if (newlineIndex != -1)
                            {
                                i = newlineIndex;
                            }
                            else
                            {
                                i = fileContents.Length - 1;
                            }
                        }
                        else
                        {
                            string stringContent = fileContents.Substring(i + 1, terminatorIndex - (i + 1));
                            tokens.Add($"STRING \"{stringContent}\" {stringContent}");
                            i = terminatorIndex;
                        }
                        break;
                    default:
                        string convertedChar = "";
                        convertedChar += character;

                        // Keywords
                        string keywordString = GetKeyword(fileContents, character, i)[0];
                        string keyword = GetKeyword(fileContents, character, i)[1];
                        if (keywordString != "")
                        {
                            i += keywordString.Length - 1;
                            tokens.Add($"{keyword} {keywordString} null");
                            break;
                        }

                        // Identifier
                        string identifier = GetIdentifier(fileContents, character, i);
                        if (identifier != "")
                        {
                            tokens.Add($"IDENTIFIER {identifier} null");
                            i += identifier.Length - 1;
                            break;
                        }

                        // Number Literals
                        int convertedString;
                        if (int.TryParse(convertedChar, out convertedString))
                        {
                            for (int j = i + 1; j < fileContents.Length; j++)
                            {
                                if (char.IsNumber(fileContents[j]) || fileContents[j] == '.')
                                {
                                    convertedChar += fileContents[j];
                                }
                                i = j;
                            }
                            bool isInt = int.TryParse(convertedChar, out convertedString);
                            double convertedStringDouble = double.Parse(convertedChar);
                            string representedDouble = convertedChar;

                            if (!isInt)
                            {
                                tokens.Add($"NUMBER {convertedStringDouble} {representedDouble}");
                            }
                            else
                            {
                                representedDouble = String.Format("{0:0.0}", convertedStringDouble);
                                tokens.Add($"NUMBER {convertedString} {representedDouble}");
                            }
                            break;
                        }
                        
                        // Not an identified token
                        tokens.Add($"[line {line}] Error: Unexpected Character: {character}");
                        lexicalError = true;
                        break;
                }
            }
            // End of file
            tokens.Add("EOF  null");
        }
        else
        {
        tokens.Add("EOF  null"); // Placeholder, remove this line when implementing the scanner
        }

        return (tokens, lexicalError);
    }

    bool GetNextCharEquality(string text, char character, int iterator)
    {
        // Check if the next character in the string is equal to the character passed in
        return iterator != text.Length -1 && text[iterator + 1] == character;
    }

    string GetIdentifier(string text, char character, int iterator)
    {
        string finalString = "";
        if (character == '_' || char.IsLetter(character))
        {
            for (int i = iterator; i < text.Length; i++)
            {
                if (text[i] != ' ' && text[i] != ';')
                {
                    for (int j = 0; j < validTokens.Length; j++)
                    {
                        if (text[i] == validTokens[j])
                        {
                            return finalString;
                        }
                    }
                    finalString += text[i];
                }
                else
                {
                    break;
                }
            }
        }
        return finalString;
    }

    string[] GetKeyword(string text, char character, int iterator)
    {
        foreach (string keyword in keywords)
        {
            if (keyword[0] == character)
            {
                int keywordLength = keyword.Length;
                
                // Check bounds to prevent out of range exception
                if (iterator + keywordLength <= text.Length)
                {
                    bool isMatch = true;
                    string finalString = "";
                    
                    for (int j = 0; j < keywordLength; j++)
                    {
                        if (text[iterator + j] == keyword[j])
                        {
                            finalString += keyword[j];
                        }
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    
                    if (isMatch)
                    {
                        string keywordReturn = finalString.ToUpper();
                        return new string[] {finalString, keywordReturn};
                    }
                }
            }
        }
        
        return new string[] {"", ""};
    }
}