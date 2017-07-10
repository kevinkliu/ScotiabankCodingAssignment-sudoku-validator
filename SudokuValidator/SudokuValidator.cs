using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuValidator
    {
        private const int DEFAULT_GRID_SIZE = 9;
        private const int DEFAULT_BLOCK_SIZE = 3;
        private int _gridSize;
        private int _blockSize;

        public SudokuValidator()
        {
            this._gridSize = DEFAULT_GRID_SIZE;
            this._blockSize = DEFAULT_BLOCK_SIZE;
           
        }
        public void Validate(int[,] grid)
        {
            if (grid.GetLength(0) != _gridSize || grid.GetLength(1) != _gridSize)
            {
                throw new SudokuValidatorException("Only 9 x 9 puzzle is valid");
            }
            
            //validate row
            for (int row = 0; row < _gridSize; row++)
            {
                int[] line = new int[_gridSize];
                for (int col = 0; col < _gridSize; col++)
                {
                    if (InvalidNumber(grid[row,col]))
                    {
                        throw new SudokuValidatorException(String.Format(SudokuValidatorException.ERR_MSG_INVALID_NUMBER, row, col));
                    }
                    line[col] = grid[row, col];
                }
                int dup = ValidateNoDuplicateNumberInLine(line);
                if (dup > 0)
                {
                    throw new SudokuValidatorException(String.Format(SudokuValidatorException.ERR_MSG_DUP_IN_ROW, row + 1, dup));
                }
            }
            //validate column
            for (int col = 0; col < _gridSize; col++)
            {
                int[] line = new int[_gridSize];
                for (int row = 0; row < _gridSize; row++)
                {
                    line[row] = grid[row, col];
                }
                int dup = ValidateNoDuplicateNumberInLine(line);
                if (dup > 0)
                {
                    throw new SudokuValidatorException(String.Format(SudokuValidatorException.ERR_MSG_DUP_IN_COLUMN, col + 1, dup));
                }
            }
            //validate block
            int blocks = _gridSize / _blockSize;
            for (int i = 0; i < blocks; i++)
            {
                for (int j = 0; j < blocks; j++)
                {
                    int[,] block = new int[_blockSize, _blockSize];
                    for (int row = 0; row < _blockSize; row++)
                    {
                        for (int col = 0; col < _blockSize; col++)
                        {
                            block[row, col] = grid[i * _blockSize + row, j * _blockSize + col];
                        }
                    }
                    int dup = ValidateNoDuplicateNumberInBlock(block);
                    if (dup > 0)
                    {
                        throw new SudokuValidatorException(String.Format(SudokuValidatorException.ERR_MSG_DUP_IN_BLOCK, i + 1, j + 1, dup));
                    }
                }
            }

        }
        protected int ValidateNoDuplicateNumberInLine(int[] line)
        {
            List<int> unqiueSeq = new List<int>();
            foreach(int num in line)
            {
                if (unqiueSeq.IndexOf(num) >= 0)
                {
                    return num;
                }
                unqiueSeq.Add(num);
            }
            return 0;
        }

        protected int ValidateNoDuplicateNumberInBlock(int[,] block)
        {
            if (block.GetLength(0) != block.GetLength(1))
            {
                throw new SudokuValidatorException(SudokuValidatorException.ERR_MSG_BLOCK_SIZE);
            }
            List<int> unqiueSeq = new List<int>();
            for (int i = 0; i < _blockSize; i++)
            {
                for (int j = 0; j < _blockSize; j++)
                {
                    if (unqiueSeq.IndexOf(block[i,j]) >= 0)
                    {
                        return block[i,j];
                    }
                    unqiueSeq.Add(block[i, j]);
                }
            }
            
            return 0;
        }

        protected bool InvalidNumber(int number)
        {
            return number <= 0 || number > _gridSize;
        }
    }
}
