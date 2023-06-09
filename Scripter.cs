using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static bool isInEditorMode = false; // Track if we are in editor mode
        static Dictionary<string, ConsoleColor> highlightColors = new Dictionary<
            string,
            ConsoleColor
        >();

        static void Main(string[] args)
        {
            Console.Title = "SCRIPTER - Version 1.01";
            // Add your desired word-color mappings to the highlightColors dictionary
            highlightColors.Add("<html>", ConsoleColor.DarkBlue);
            highlightColors.Add("</html>", ConsoleColor.DarkBlue);
            highlightColors.Add("<head>", ConsoleColor.Blue);
            highlightColors.Add("</head>", ConsoleColor.Blue);
            highlightColors.Add("<title>", ConsoleColor.Red);
            highlightColors.Add("</title>", ConsoleColor.Red);
            highlightColors.Add("<body>", ConsoleColor.DarkGreen);
            highlightColors.Add("</body>", ConsoleColor.DarkGreen);
            highlightColors.Add("<table>", ConsoleColor.DarkRed);
            highlightColors.Add("</table>", ConsoleColor.DarkRed);
            highlightColors.Add("<marquee>", ConsoleColor.DarkYellow);
            highlightColors.Add("</marquee>", ConsoleColor.DarkYellow);

            Console.Clear();
            string input;

            while (true)
            {
                Console.WriteLine(
                    @"▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
██░▄▄▄░██░▄▄▀██░▄▄▀█▄░▄██░▄▄░█▄▄░▄▄██░▄▄▄██░▄▄▀███▀███▀█▀░█████░▄▄░█▀░██
██▄▄▄▀▀██░█████░▀▀▄██░███░▀▀░███░████░▄▄▄██░▀▀▄████░▀░███░██▀▀█░▀▄░██░██
██░▀▀▀░██░▀▀▄██░██░█▀░▀██░██████░████░▀▀▀██░██░█████▄███▀░▀█▄▄█░▀▀░█▀░▀█
▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀"
                );
                Console.WriteLine(
                    @"
                .---------.
                | samTime |
                '---------'
"
                );

                Console.WriteLine(
                    "Enter E to enter editor mode, I to open a file, or Q to quit the program."
                );
                input = Console.ReadLine();
                if (input.ToUpper() == "E")
                {
                    isInEditorMode = true;
                    EditorMode();
                    isInEditorMode = false;
                }
                else if (input.ToUpper() == "I")
                {
                    Console.WriteLine("Enter the path of the file to open: ");
                    string filePath = Console.ReadLine();
                    if (File.Exists(filePath))
                    {
                        string text = File.ReadAllText(filePath);
                        isInEditorMode = true;
                        EditorMode(text);
                        isInEditorMode = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(filePath + " doesnot exist");
                    }
                }
                else if (input.ToUpper() == "Q")
                {
                    break; // Exit the while loop and terminate the program
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void EditorMode(string text = "")
        {
            Console.Clear();
            Console.WriteLine("Editor mode. Press Ctrl+S to save, Ctrl+X to exit.");
            int topMargin = Console.CursorTop;
            int leftMargin = Console.CursorLeft;
            int currentLine = topMargin;
            int currentPosition = leftMargin;

            Console.Write(text);
            currentPosition += text.Length;

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                // Check if we are in editor mode before processing the key
                if (isInEditorMode)
                {
                    if (
                        keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.S
                    )
                    {
                        Console.Clear(); // Clear the console screen
                        SaveFile(text);
                        return; // Exit the EditorMode method
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace && currentPosition > leftMargin)
                    {
                        currentPosition--;
                        Console.SetCursorPosition(currentPosition, currentLine);
                        Console.Write(" ");
                        Console.SetCursorPosition(currentPosition, currentLine);
                        
if (text.Length > 0)
{
    //try to find the index within the text varialbe based on currentLine and currentPosition

    //split text into lines
   var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

    //get the starting index eg. currentLine is 5, we want to ignore first 4 lines
    var startIndex = lines.Take(currentLine - 1).Sum(x => x.Length + Environment.NewLine.Length);

    //shift startIndex by the currentPosition
    var currentIndex = startIndex + currentPosition;

    //add whitespace, just like Console.Write(" ") did
    text = text.Substring(0, currentIndex) + " " + text.Substring(currentIndex + 1);
}

                    }
                    else if (
                        keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.X
                    )
                    {
                        Console.Clear(); // Clear the console screen
                        return; // Exit the EditorMode method
                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow && currentPosition > leftMargin)
                    {
                        currentPosition--;
                        Console.SetCursorPosition(currentPosition, currentLine);
                    }
                    else if (
                        keyInfo.Key == ConsoleKey.RightArrow
                        && currentPosition < Console.WindowWidth - 1
                    )
                    {
                        currentPosition++;
                        Console.SetCursorPosition(currentPosition, currentLine);
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow && currentLine > topMargin)
                    {
                        currentLine--;
                        Console.SetCursorPosition(currentPosition, currentLine);
                    }
                    else if (
                        keyInfo.Key == ConsoleKey.DownArrow
                        && currentLine < Console.WindowHeight - 1
                    )
                    {
                        currentLine++;
                        Console.SetCursorPosition(currentPosition, currentLine);
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        text += Environment.NewLine;
                        currentLine++;
                        currentPosition = leftMargin;
                        Console.SetCursorPosition(currentPosition, currentLine);
                    }
                    else if (char.IsControl(keyInfo.KeyChar))
                    {
                        // Ignore control characters
                    }
 else
{
    //text += keyInfo.KeyChar;
    var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    var startIndex = lines.Take(currentLine - 1).Sum(x => x.Length + Environment.NewLine.Length);
    var currentIndex = startIndex + currentPosition;

    text = text.Substring(0, currentIndex) + keyInfo.KeyChar + text.Substring(currentIndex);

    Console.Write(keyInfo.KeyChar);
    currentPosition++;
}

                    // Check if any highlighted words are present in the text and apply
                    // the corresponding colors
                    foreach (var highlight in highlightColors)
                    {
                        string word = highlight.Key;
                        ConsoleColor color = highlight.Value;
                        int index = text.IndexOf(word);
                        while (index >= 0)
                        {
                            int line = topMargin + text.Substring(0, index).Split('\n').Length - 1;
                            if (line == currentLine)
                            {
                                int lineStartIndex = text.LastIndexOf('\n', index) + 1;
                                int column = index - lineStartIndex;
                                Console.SetCursorPosition(leftMargin + column, currentLine);
                                Console.ForegroundColor = color;
                                Console.Write(word);
                                Console.ResetColor();
                            }
                            index = text.IndexOf(word, index + word.Length);
                        }
                    }
                    Console.SetCursorPosition(currentPosition, currentLine);
                }
            } while (true);
        }

        static void SaveFile(string text)
        {
            Console.Clear();
            Console.WriteLine("Enter file name with extension: ");
            string fileName = Console.ReadLine();
            File.WriteAllText(fileName, text);
            Console.Clear();
            Console.WriteLine("File saved as " + fileName + ".");
            Console.WriteLine("Do you want to open the file? (Y/N)");
            string input = Console.ReadLine();
            if (input.ToUpper() == "Y")
            {
                System.Diagnostics.Process.Start(fileName);
                Console.Clear();
            }
            else if (input.ToUpper() == "N")
            {
                Console.Clear();
            }
        }
    }
}