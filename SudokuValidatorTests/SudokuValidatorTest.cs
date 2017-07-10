using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
namespace SudokuTests
{
    public class FakeValidator : Sudoku.SudokuValidator
    {
        public int ValidateLine(int[] line)
        {
            return base.ValidateNoDuplicateNumberInLine(line);
        }
        public int ValidateBlock(int[,] block)
        {
            return base.ValidateNoDuplicateNumberInBlock(block);
        }
    }
    [TestClass]
    public class SudokuValidatorTest
    {
        protected static int[,] puzzle = new int[,]
        {
            { 5,3,4,6,7,8,9,1,2 },            { 6,7,2,1,9,5,3,4,8 },            { 1,9,8,3,4,2,5,6,7 },            { 8,5,9,7,6,1,4,2,3 },            { 4,2,6,8,5,3,7,9,1 },            { 7,1,3,9,2,4,8,5,6 },            { 9,6,1,5,3,7,2,8,4 },
            { 2,8,7,4,1,9,6,3,5 },            { 3,4,5,2,8,6,1,7,9 }
        };
        [TestMethod]
        public void should_have_no_duplicate_number_in_one_line()
        {
            FakeValidator val = new FakeValidator();
            int dup = val.ValidateLine(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.AreEqual(0, dup);
        }

        [TestMethod]
        public void should_return_duplicate_number_in_one_line()
        {
            FakeValidator val = new FakeValidator();
            int dup = val.ValidateLine(new int[] { 1, 2, 3, 4, 5, 6, 5, 8, 9 });
            Assert.AreEqual(5, dup);
        }
        [TestMethod]
        public void should_have_no_duplicate_number_in_block()
        {
            FakeValidator val = new FakeValidator();
            int dup = val.ValidateBlock(new int[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }, 
                { 7, 8, 9 }
            });
            Assert.AreEqual(0, dup);
        }

        [TestMethod]
        public void should_return_duplicate_number_in_block()
        {
            FakeValidator val = new FakeValidator();
            int dup = val.ValidateBlock(new int[,] {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 5, 9 }
            });
            Assert.AreEqual(5, dup);
        }
        [TestMethod]
        public void should_throw_exception_with_different_size_demension ()
        {
            try { 
            FakeValidator val = new FakeValidator();
            int dup = val.ValidateBlock(new int[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });
            }
            catch (SudokuValidatorException ex)
            {
                Assert.AreEqual(ex.Message, SudokuValidatorException.ERR_MSG_BLOCK_SIZE);
            }
        }

        [TestMethod]
        public void should_have_no_duplicate_number_in_puzzle()
        {
            FakeValidator val = new FakeValidator();
            val.Validate(puzzle);
        }
        [TestMethod]
        public void should_throw_exception_when_there_is_invalid_number_in_puzzle()
        {
            try
            {
                FakeValidator val = new FakeValidator();
                int[,] input = (int[,])puzzle.Clone();
                input[1, 4] = 0;
                val.Validate(input);
            }
            catch (SudokuValidatorException ex)
            {
                Assert.AreEqual(ex.Message, String.Format(SudokuValidatorException.ERR_MSG_INVALID_NUMBER, 1, 4));
            }
        }
        [TestMethod]
        public void should_throw_exception_with_non_9_x_9_puzzle()
        {
            try
            { 
                FakeValidator val = new FakeValidator();
                val.Validate(new int[,] {
                    { 5,3 },                    { 6,7 }
                });
            }
            catch (SudokuValidatorException ex)
            {
                Assert.AreEqual(ex.Message, SudokuValidatorException.ERR_MSG_PUZZLE_SIZE);
            }
        }

        [TestMethod]
        public void should_throw_exception_due_to_duplicate_number_in_row()
        {
            try
            {
                FakeValidator val = new FakeValidator();
                int[,] input = (int[,])puzzle.Clone();
                input[1, 4] = 4;
                val.Validate(input);
            }
            catch (SudokuValidatorException ex)
            {
                Assert.AreEqual(ex.Message, String.Format(SudokuValidatorException.ERR_MSG_DUP_IN_ROW, 2, 4));
            }
        }
        [TestMethod]
        public void should_throw_exception_due_to_duplicate_number_in_column()
        {
            try
            {
                FakeValidator val = new FakeValidator();
                int[,] input = (int[,])puzzle.Clone();
                input[3, 2] = 8;
                input[3, 0] = 9;
                val.Validate(input);
            }
            catch (SudokuValidatorException ex)
            {
                Assert.AreEqual(ex.Message, String.Format(SudokuValidatorException.ERR_MSG_DUP_IN_COLUMN, 1, 9));
            }
        }
        [TestMethod]
        public void should_throw_exception_due_to_duplicate_number_in_block()
        {
            try
            {
                FakeValidator val = new FakeValidator();
                int[,] input = (int[,])puzzle.Clone();
                for (int i = 0; i < 9; i++)
                {
                    input[5, i] = puzzle[8, i];
                    input[8, i] = puzzle[5, i];
                }
                val.Validate(input);
                Assert.Fail("SudokuValidator should throw exception.");
            }
            catch (SudokuValidatorException ex)
            {
                Assert.AreEqual(ex.Message, String.Format(SudokuValidatorException.ERR_MSG_DUP_IN_BLOCK, 2, 1, 4));
            }
        }
    }
}
