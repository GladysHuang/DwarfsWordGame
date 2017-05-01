//-----------------------------------------------------------------------
// <copyright file="VariationsTest.cs" company="GH">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Gladys Huang</author>
//-----------------------------------------------------------------------
namespace DwarfsWordGame.Test
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]

    /// <summary>
    /// Test class for Variations
    /// </summary>
    public class VariationsTest
    {
        [TestMethod]

        /// <summary>
        /// Test Next() method
        /// </summary>
        public void TestNext()
        {
            // Case 1: check all variations are returned
            // {2,1,0}'s variations are: {2}, {1}, {0}, {2,1}, {2,0}, {1,2}, {1,0}, {0,2}, {0,1},
            // {2,1,0}, {2,0,1}, {1,2,0}, {1,0,2}, {0,2,1}, {0,1,2}
            // With "-1" as padding, and consider ends with 0 situation, then expected result should be {-1,-1,2}, {-1,-1,1}, {-1,-1,0}, {-1,2,1}, {-1,2,0},
            // {-1,1,2}, {-1,1,0}, {2,1,0}, {2,0,1}, {1,2,0}, {1,0,2}
            List<int[]> expected = new List<int[]>();
            expected.Add(new int[] { -1, -1, 0 });
            expected.Add(new int[] { -1, -1, 1 });
            expected.Add(new int[] { -1, -1, 2 });
            expected.Add(new int[] { -1, 1, 0 });
            expected.Add(new int[] { -1, 1, 2 });
            expected.Add(new int[] { -1, 2, 0 });
            expected.Add(new int[] { -1, 2, 1 });
            expected.Add(new int[] { 1, 0, 2 });
            expected.Add(new int[] { 1, 2, 0 });
            expected.Add(new int[] { 2, 0, 1 });
            expected.Add(new int[] { 2, 1, 0 });

            Variations varians = new Variations(3);
            List<int[]> result = new List<int[]>();
            while (true)
            {
                if (!varians.Next())
                {
                    break;
                }

                result.Add((int[])varians.Indices.Clone());
             }

            Assert.AreEqual(result.Count, expected.Count);

            for(int i = 0; i < result.Count; i++)
            {
                CollectionAssert.AreEqual(result[i], expected[i]);
            }
        }
    }
}
