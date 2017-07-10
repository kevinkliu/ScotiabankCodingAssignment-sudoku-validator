using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuValidatorException : Exception
    {
        public static string ERR_MSG_PUZZLE_SIZE = "Only 9 x 9 puzzle is valid";
        public static string ERR_MSG_DUP_IN_ROW = "Row {0} has duplicate number of {1}";
        public static string ERR_MSG_DUP_IN_COLUMN = "Column {0} has duplicate number of {1}";
        public static string ERR_MSG_DUP_IN_BLOCK = "Block ({0}, {1}) has duplicate number {2}";
        public static string ERR_MSG_BLOCK_SIZE = "Block's demensions must have same size";
        public static string ERR_MSG_INVALID_NUMBER = "Number at ({0},{1}) is invalid";

        public SudokuValidatorException() : base() { } 
        public SudokuValidatorException(String message) : base(message) { }         
    }
}
