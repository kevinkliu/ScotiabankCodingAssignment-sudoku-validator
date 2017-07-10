using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class SudokuReaderException : Exception
    {
        public static string ERR_MSG_INVALID_GRID_SIZE = "Line {0} needs to have {1} numbers";
        public static string ERR_MSG_INVALID_NUMBER = "Line {0} has invalid number";

        public SudokuReaderException() : base() { }
        public SudokuReaderException(String message) : base(message) { }
    }
}
