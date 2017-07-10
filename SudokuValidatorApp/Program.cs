using System;
using System.IO;
using Sudoku;

namespace SudokuValidatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            String filename = AppContext.BaseDirectory + "\\input_sudoku.txt";
            if (args.Length > 0)
            {
                if (args[0] == "/?" || args[0]=="-help")
                {
                    ShowHelp();
                    return;
                }
                else { 
                    filename = args[0];
                }
            }
            try
            {
                if (!File.Exists(filename)) {
                    throw new System.IO.FileNotFoundException();
                }
                int[,] input;
                using (SudokuReader reader = new SudokuReader(File.OpenText(filename), 9))
                {
                    input = reader.Load();
                }

                new SudokuValidator().Validate(input);
                Console.WriteLine("The puzzle is correct!");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Error: " + filename + " not found");
            }
            catch (SudokuReaderException ex)
            {
                Console.WriteLine("Error in load file: " + ex.Message);
            }
            catch (SudokuValidatorException ex)
            {
                Console.WriteLine("Error in puzzle: " + ex.Message);
            }

            Console.WriteLine("\n\nPress enter key to exit...");
            Console.Read();
        }
        
        static void ShowHelp()
        {
            Console.WriteLine("Validate Sudoku puzzle result");
            Console.WriteLine("\ndotnet SudokuValidatorApp.dll [drive:][path][filename]");
        }
    }
}