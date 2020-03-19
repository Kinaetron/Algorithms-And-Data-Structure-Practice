using Library.DataStructures;
using System;

namespace Tests
{
    public class DynamicArrayTests
    {

        private class Arrangement
        {
            public DynamicArray<string> SUT { get; set; }

            public Arrangement(DynamicArray<string> dynamicArray)
            {
                SUT = dynamicArray;
            }
        }

        private class ArrangementBuilder
        {
            private DynamicArray<string> dynamicArray;

            public Arrangement Build()
            {
                return new Arrangement(dynamicArray);
            }
        }


    }
}
