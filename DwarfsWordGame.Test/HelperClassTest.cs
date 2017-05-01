//-----------------------------------------------------------------------
// <copyright file="HelperClassTest.cs" company="GH">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Gladys Huang</author>
//-----------------------------------------------------------------------
namespace DwarfsWordGame.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]

   /// <summary>
   /// Test class for HelperClass
   /// </summary>
    public class HelperClassTest
    {
        [TestMethod]

        /// <summary>
        /// Test IsPalindrome() method
        /// </summary>
        public void TestIsPalindrome()
        {
            string testStr = "Racecar";
            bool result = HelperClass.IsPalindrome(testStr.ToLower());
            Assert.IsTrue(result);

            testStr = "ThisIsNotPalindrome";
            result = HelperClass.IsPalindrome(testStr.ToLower());
            Assert.IsFalse(result);

            testStr = string.Empty;
            result = HelperClass.IsPalindrome(testStr);
            Assert.IsFalse(result);
        }
    }
}
