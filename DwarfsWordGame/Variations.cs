//-----------------------------------------------------------------------
// <copyright file="Variations.cs" company="GH">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Gladys Huang</author>
//-----------------------------------------------------------------------
namespace DwarfsWordGame
{
    /// <summary>
    /// Variations does variation algorithm and provides all possible valid combination with no repeatition.
    /// An example of valid combinations with no repeatition for {2, 1, 0} are: {2}, {1}, {0}, {2,1}, {2,0},
    /// {1,2}, {1,0}, {0,2}, {0,1}, {2,1,0}, {2,0,1}, {1,2,0}, {1,0,2},{0,2,1},{0,1,2}
    /// <para>
    /// The algorithm is approached by using a N-based integer in form of integeray array, where each
    /// number maps to an index of the string array between 0 and N-1. 
    /// </para>
    /// Although all possible combination varies in length(can vary from 1 to N), to minimize runtime memory allocation,
    /// the class uses an integer array with a fixed size of N. -1 is used as padding. 
    /// For example, when N = 5, a valid combination [2, 3, 1] is
    /// represented as [-1, -1, 2, 3, 1]. -1 is simply used here as padding.
    /// </summary>
    /// <remarks>
    /// Since it is no repeatition combination, CheckUnique() method is implemented to make sure no repeated indexes
    /// in the combination. It is observe this function can be expensive if getting call everytime from GetNext() method, 
    /// and is performance culprit. Optimization is done by using _dupIndex property to mark duplication index in last combination.
    /// This trick is to reduce unecessary loop if detecting if there is duplicate in previous combination in any position
    /// other than last index. If so, some loops can be skipped. Please refer to detail in GetNext() method. After optimization,
    /// the performance improved dramatically.
    /// </remarks>
    public class Variations
    {
        #region Private Properties
        /// <summary>
        /// int array that represents a valid combination
        /// </summary>
        private int[] indices;

        /// <summary>
        /// actual number of indexes contains in a combination
        /// </summary>
        private int indiceItemLength = 0;

        /// <summary>
        /// Fixed length for _indices array. 
        /// </summary>
        private int size = 0;

        /// <summary>
        /// To check if indexes are unique in the combination
        /// </summary>
        private bool isUnique = true;

        /// <summary>
        /// N base for variation algorithm. It equals to the string array length.
        /// </summary>
        private int baseN = 0;

        /// <summary>
        /// Mark the position of duplicate index in a combination. For performance optimization purpose
        /// </summary>
        private int dupIndex = -1;
        #endregion

        /// <summary>
        /// Initializes a new instance of the Variations class.
        /// </summary>
        /// <param name="sizeAsBase">interger base for algorithm</param>
        public Variations(int sizeAsBase)
        {
            this.indices = new int[sizeAsBase];
            this.baseN = sizeAsBase;
            this.size = sizeAsBase;

            this.InitializeIndicies();
        }

        #region Public Properties
        /// <summary>
        /// Gets a combination of the given array of numbers
        /// </summary>
        public int[] Indices
        {
            get { return this.indices; }
        }

        /// <summary>
        /// Gets a combination's numbers' count
        /// </summary>
        public int IndiceItemLength
        {
            get { return this.indiceItemLength; }
        }

        /// <summary>
        /// Gets the fixed length of Indices array.
        /// </summary>
        public int Size
        {
            get { return this.size; }
        }

        #endregion

        /// <summary>
        ///  Mutate the combination(int array with array indexes) to the next valid combination.
        /// </summary>
        /// <returns>
        ///  True if the next valid combination is created. False means no more valid combination.
        /// </returns>
        public bool Next()
        {
            // If it has reached reached to the last possible combination, which is [N-1, N-2, ..., 0],
            // then return false.
            bool hasNext = false;
            for (int i = 0; i < this.size; i++)
            {
                if (this.indices[this.size - i - 1] != i)
                {
                    hasNext = true;
                    break;
                }
            }

            if (!hasNext)
            {
                return false;
            }

            // Keep trying until find next valid combination.
            while (true)
            {
                this.GetNext();
                if (this.isUnique)
                {
                    break;
                }
            }

            return true;
        }

        /// <summary>
        ///  Check if all numbers in the current combination are unique.
        ///  "-1" is the padding so is ignored.
        /// </summary>
        private void CheckUnique()
        {
            this.dupIndex = -1;
            this.isUnique = true;
            if (this.indiceItemLength < 2)
            {
                // If only one index in combination, sure it is unique and a valid combination
                return;
            }

            for (int i = this.size - this.indiceItemLength + 1; i < this.size; i++)
            {
                int num = this.indices[i];
                for (int j = this.size - this.indiceItemLength; j < i; j++)
                {
                    if (num == this.indices[j])
                    {
                        this.dupIndex = i;
                        this.isUnique = false;
                        return;
                    }
                }
            }

            return;
        }

        /// <summary>
        ///  Mutate the current combination(int array) with the next N based integer. 
        ///  For example, when base is 5, if the current combination array is [-1, -1, 2, 3, 1],
        ///  then after calling this method, the combination array will be changed to [-1, -1, 2, 3, 2].
        ///  Similarly, if the current combination is [-1, -1, 2, 3, 4], then the nexe one will be [-1, -1, 2, 4, 0].
        ///  Calling this method after InitializeIndicies() will return the first
        ///  5 based integer array, which is [-1, -1, -1, -1, 0].
        /// </summary>
        private void GetNext()
        {
            if (this.indiceItemLength == 0)
            {
                // First iteration after _indicies initialization
                // Will set _indicies as first value of n based integer
                // eg. [-1, -1, -1, -1, 0] for 5 based. -1 is just padding
                this.indices[this.size - 1] = 0;
                this.indiceItemLength++;
                this.isUnique = true;
                return;
            }

            // If current combination has duplicate index at the position other than last position,
            // Then the base calculation can skip from the duplicate postion to last position.
            // This way can avoid unnecessary loops dramatically.
            int start = this.size - 1;
            if (this.dupIndex >= 0 && this.dupIndex < this.size - 1)
            {
                start = this.dupIndex;
                for (int i = this.dupIndex + 1; i < this.size; i++)
                {
                    this.indices[i] = i - this.dupIndex - 1;
                }
            }

            for (int i = start; i >= 0; i--)
            {
                if (this.indices[i] < 0)
                {
                    // A new index becomes valid.
                    this.indiceItemLength++;
                    this.indices[i] = 0;
                }

                this.indices[i] = this.indices[i] + 1;
                
                // Check if incrementing the current index causes overflow.
                if (this.indices[i] < this.baseN)
                {
                    // No overflow.
                    this.CheckUnique();
                    return;
                }

                // Overflow. Reset the current index to 0 and move on to the previous
                // index in the array.
                this.indices[i] = 0;
            }
        }

        /// <summary>
        /// This is to initialize _indicies. 
        /// eg. for base 5, the initial value of _indicies is [-1, -1, -1, -1, -1].
        /// </summary>
        private void InitializeIndicies()
        {
            this.indiceItemLength = 0;
            for (int i = 0; i < this.size; i++)
            {
                this.indices[i] = -1;
            }
        }
    }
}
