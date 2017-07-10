using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Sudoku;
using System.Text;

namespace SudokuTest
{
    [TestClass]
    public class SudokuReaderTest
    {
        protected static int[,] puzzle = new int[,]
        {
            { 5,3,4,6,7,8,9,1,2 },            { 6,7,2,1,9,5,3,4,8 },            { 1,9,8,3,4,2,5,6,7 },            { 8,5,9,7,6,1,4,2,3 },            { 4,2,6,8,5,3,7,9,1 },            { 7,1,3,9,2,4,8,5,6 },            { 9,6,1,5,3,7,2,8,4 },
            { 2,8,7,4,1,9,6,3,5 },            { 3,4,5,2,8,6,1,7,9 }
        };
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void should_throw_exception_with_invalid_constructor_parameters_null_text_reader()
        {
            new SudokuReader(null, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void should_throw_exception_with_invalid_constructor_parameters_zero_grid_size()
        {
            new SudokuReader(new StringReader(""), 0);
        }

        [TestMethod]
        public void should_load_result()
        {
            load_result(MakeStringWithoutEmptyLine());
        }
        
        [TestMethod]
        public void should_load_result_from_file_without_empty_line()
        {
            load_result(MakeString());
        }
        private void load_result(StringBuilder sb)
        {
            SudokuReader reader = new SudokuReader(new StringReader(sb.ToString()), 9);
            int[,] result = reader.Load();
            Assert.IsTrue(result.GetLength(0) == 9, "Grid size is invalid");
            Assert.IsTrue(result.GetLength(1) == 9, "Grid size is invalid");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Assert.IsTrue(result[i, j] == puzzle[i, j], String.Format("Value at ({0}, {1}) is not matched", i, j));
                }
            }
        }
        private StringBuilder MakeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"534678912
672195348
198342567
859761423
426853791
713924856
961537284
287419635
345286179");
            return sb;
        }

        private StringBuilder MakeStringWithoutEmptyLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"534678912
672195348
198342567
859761423
426853791
713924856
961537284
287419635
345286179");
            return sb;
        }
    }
}
