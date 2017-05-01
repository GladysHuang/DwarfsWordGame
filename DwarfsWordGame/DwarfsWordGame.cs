//-----------------------------------------------------------------------
// <copyright file="DwarfsWordGame.cs" company="GH">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Gladys Huang</author>
//-----------------------------------------------------------------------
namespace DwarfsWordGame
{
    using System;
    using System.Text;

    /// <summary>
    /// Main program for Five Dwarves Name Game.
    /// </summary>
    public class DwarfsWordGame
    {
        /// <summary>
        /// Five Dwarves ( Gimli Fili Ilif Ilmig and Mark) met at the Prancing Pony
        /// and played a word game to determine which combinations of their names resulted in a palindrome. 
        /// <para>
        /// Assumption1: Each name can only be used once in each combination
        /// Assumption2: They are playing case insenstive game
        /// </para>
        /// </summary>
        /// <output>Valid combination of names</output>
        public static void Main()
        {
            string[] names = new string[] { "Gimli", "Fili", "Ilif", "Ilmig", "Mark" };
            
            // sizeAsBase is to used as number base, to be used in Variations' algorithm funtion.
            int sizeAsBase = names.Length;
            
            // Instantiate Variations class, which handles the algorithm to get variations.
            Variations varians = new Variations(sizeAsBase);
            StringBuilder combinedStrBuff = new StringBuilder();

            // Each loop, varians object gets a valid combination using names' array index
            while (true)
            {
                combinedStrBuff.Clear();

                // Get next combinaton
                if (!varians.Next())
                {
                    // No more valid combination.
                    break;
                }

                // Valid combination, using array indexes in the combination to map to actual names.
                // varians.Indices is the ombination in format like [-1,-1, 3, 2, 1], 
                // which -1 is just padding that needs to be ignored.
                // varians.IndiceItemLength is the actual number of name indexes in combination
                // Eg. for [-1, -1, 3, 2, 1] combination, indiceItemLength is 2. 
                // varians.Size is the fixed combination array length, which is 5 in this case.
                int idx = 0;
                for (int j = 0; j < varians.IndiceItemLength; j++)
                {
                    idx = varians.Size - varians.IndiceItemLength + j; 
                    combinedStrBuff.Append(names[varians.Indices[idx]]);
                }
                
                // Check to see if it is palindrome.
                if (HelperClass.IsPalindrome(combinedStrBuff.ToString().ToLower()))
                {
                    // Yes it is palindrome, print out
                    for (int j = 0; j < varians.IndiceItemLength; j++)
                    {
                        idx = varians.Size - varians.IndiceItemLength + j;
                        Console.Write(names[varians.Indices[idx]] + " ");
                    }

                    Console.WriteLine();
                }

                // If this combination end with 0, need to consider reversed as an valid combination too.
                // This logic is needed because the algorithm used in Variations class doesn't represent
                // a N-based integer starts with "0". For example, [3, 1, 0] is a valid combination returned by
                // varians.Next(), but [0, 1, 3] won't be returned even it is also a valid combination.
                if (varians.Indices[varians.Size - 1] == 0)
                {
                    combinedStrBuff.Clear();
                    for (int j = 0; j < varians.IndiceItemLength; j++)
                    {
                        idx = varians.Indices[varians.Size - j - 1];
                        combinedStrBuff.Append(names[idx]);
                    }

                    if (HelperClass.IsPalindrome(combinedStrBuff.ToString().ToLower()))
                    {
                        for (int j = 0; j < varians.IndiceItemLength; j++)
                        {
                            idx = varians.Indices[varians.Size - j - 1];
                            Console.Write(names[idx] + " ");
                        }

                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Please hit enter to end...");
            Console.ReadLine();
        }
    }
}