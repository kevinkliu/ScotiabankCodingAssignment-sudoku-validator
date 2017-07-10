using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sudoku
{
    public class SudokuReader : IDisposable
    {
        private TextReader _reader;
        private int _gridSize;

        public SudokuReader(TextReader reader, int gridSize)
        {
            if (reader == null || gridSize <= 0)
            {
                throw new ArgumentException();
            }
            this._reader = reader;
            this._gridSize = gridSize;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public int[,] Load()
        {
            int[,] result = new int[_gridSize, _gridSize];
            string line = _reader.ReadLine();
            int numOfLines = 0;
            while (line != null) 
            {
                if (line == string.Empty) {
                    line = _reader.ReadLine();
                    continue;
                }

                if (numOfLines == _gridSize)
                {
                    throw new SudokuReaderException();
                }
                if (line.Length != _gridSize)
                {
                    throw new SudokuReaderException(String.Format(SudokuReaderException.ERR_MSG_INVALID_GRID_SIZE, numOfLines + 1));
                }
                for (int j = 0; j < _gridSize; j++)
                {
                    if (!int.TryParse(line.Substring(j,1), out result[numOfLines, j]))
                    {
                        throw new SudokuReaderException(String.Format(SudokuReaderException.ERR_MSG_INVALID_NUMBER, numOfLines + 1));
                    }
                }
                line = _reader.ReadLine();
                numOfLines++;
            }
            
            return result;
        }
    }
}
