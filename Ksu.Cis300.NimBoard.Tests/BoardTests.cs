/* BoardTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Ksu.Cis300.NimBoard.Tests
{
    /// <summary>
    /// Unit tests for Ksu.Cis300.NimBoard.Board.
    /// </summary>
    [TestFixture]
    public class BoardTests
    {
        /// <summary>
        /// Tests the hash code computation when all parts of the key are the same.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAHashCodeAllPartsSame()
        {
            Board b = new Board(new int[] { 2, 2 }, new int[] { 2, 2 });
            Assert.That(b.GetHashCode, Is.EqualTo(3852442));
        }

        /// <summary>
        /// Tests that each part of the key is used.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBHashCodeEachPart()
        {
            Board[] boards = new Board[]
            {
                new Board(new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }),
                new Board(new int[] { 3, 0, 0 }, new int[] { 0, 0, 0 }),
                new Board(new int[] { 0, 3, 0 }, new int[] { 0, 0, 0 }),
                new Board(new int[] { 0, 0, 3 }, new int[] { 0, 0, 0 }),
                new Board(new int[] { 0, 0, 0 }, new int[] { 3, 0, 0 }),
                new Board(new int[] { 0, 0, 0 }, new int[] { 0, 3, 0 }),
                new Board(new int[] { 0, 0, 0 }, new int[] { 0, 0, 3 })
            };
            List<int> results = new List<int>();
            List<int> expected = new List<int>();
            results.Add(boards[0].GetHashCode()); // Only the number of piles is used.
            expected.Add(3);
            for (int i = 1; i < boards.Length; i++)
            {
                unchecked
                {
                    results.Add(boards[i].GetHashCode() - results[0]); // Contribution of the number of piles is removed.
                    expected.Add(expected[i - 1] * 37);
                }
            }
            Assert.That(results, Is.EquivalentTo(expected)); // Must contain the same values, not necessarily in the same order.
        }

        /// <summary>
        /// Tests that the hash code is only computed once. If it isn't, this method will time out.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCSaveHashCodes()
        {
            Board b = new Board(new int[1000000], new int[1000000]);
            int code = b.GetHashCode();
            for (int i = 0; i < 1000000; i++)
            {
                b.GetHashCode();
            }
            Assert.That(b.GetHashCode(), Is.EqualTo(code));
        }
    }
}