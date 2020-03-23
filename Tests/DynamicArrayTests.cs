using System;
using Library.DataStructures;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class DynamicArrayTests
    {

        private class Arrangement
        {
            public string TestString { get; } = "Test String";

            public string TestStringTwo { get; } = "Test String Two";

            public DynamicArray<string> SUT { get; set; }

            public Arrangement(DynamicArray<string> dynamicArray)
            {
                SUT = dynamicArray;
            }
        }

        private class ArrangementBuilder
        {
            private DynamicArray<string> dynamicArray;

            public ArrangementBuilder WithDefault()
            {
                dynamicArray  = new DynamicArray<string>();
                return this;
            }

            public ArrangementBuilder WithCapacity(int capacity)
            {
                dynamicArray = new DynamicArray<string>(capacity);
                return this;
            }


            public Arrangement Build()
            {
                return new Arrangement(dynamicArray);
            }
        }

        [Fact]
        public void DynamicArray_InitializedDefault_ShouldHaveCorrectCapacity()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithDefault()
                .Build();

            // Assert
            arrangement.SUT.Capacity.Should().Be(4);
        }

        [Fact]
        public void DynamicArray_InitializedWithMinusOne_ShouldThrowOutOfRangeException()
        {
            // Arrange
            var arrangement = Record.Exception(() => 
                        new ArrangementBuilder()
                        .WithCapacity(-1)
                        .Build());

            // Assert
            arrangement.Should().BeOfType<ArgumentOutOfRangeException>();
            arrangement.Message.Should().Be("Specified argument was out of the range of valid values. (Parameter 'capacity')");
        }

        [Fact]
        public void DynamicArray_InitializedWithFive_ShouldHaveCorrectCapacity()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithCapacity(5)
               .Build();

            // Assert
            arrangement.SUT.Capacity.Should().Be(5);
        }

        [Fact]
        public void DynamicArray_AddNullItem_ShouldReturnNullException()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            // Act
            var error = Record.Exception(() => arrangement.SUT.Add(null));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null. (Parameter 'item')");
        }

        [Fact]
        public void DynamicArray_AddValidItem_ShouldAddCorrectItem()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            // Act
            arrangement.SUT.Add(arrangement.TestString);

            // Assert
            arrangement.SUT.Count.Should().Be(1);
            arrangement.SUT[0].Should().Be(arrangement.TestString);
        }


        [Fact]
        public void DynamicArray_AddFourItems_ShouldAddDoubleCapacity()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();
            
            // Act
            arrangement.SUT.Add(arrangement.TestString);
            arrangement.SUT.Add(arrangement.TestString);
            arrangement.SUT.Add(arrangement.TestString);
            arrangement.SUT.Add(arrangement.TestString);

            // Assert
            arrangement.SUT.Count.Should().Be(4);
            arrangement.SUT.Capacity.Should().Be(8);
        }

        [Fact]
        public void DynamicArray_RemoveNullItem_ShouldReturnNullExceptionArguement()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            // Act
            var error = Record.Exception(() => arrangement.SUT.Remove(null));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null. (Parameter 'item')");
        }

        [Fact]
        public void DynamicArray_RemoveValidItem_ShouldRemoveItem()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            arrangement.SUT.Add(arrangement.TestString);

            // Act
            arrangement.SUT.Remove(arrangement.TestString);

            // Assert
            arrangement.SUT.Count.Should().Be(0);
        }

        [Fact]
        public void DynamicArray_IndexOfNullItem_ShouldReturnNullExceptionArguement()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            // Act
            var error = Record.Exception(() => arrangement.SUT.IndexOf(null));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null. (Parameter 'item')");
        }

        [Fact]
        public void DynamicArray_IndexOfValidItem_ShouldReturnCorrectIndex()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            arrangement.SUT.Add(arrangement.TestString);
            arrangement.SUT.Add(arrangement.TestStringTwo);

            // Act
            var result = arrangement.SUT.IndexOf(arrangement.TestStringTwo);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public void DynamicArray_FirstItem_ShouldReturnCorrectItem()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            arrangement.SUT.Add(arrangement.TestString);
            arrangement.SUT.Add(arrangement.TestStringTwo);

            // Act
            var result = arrangement.SUT.First();

            // Assert
            result.Should().Be(arrangement.TestString);
        }

        [Fact]
        public void DynamicArray_LastItem_ShouldReturnCorrectItem()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            arrangement.SUT.Add(arrangement.TestString);
            arrangement.SUT.Add(arrangement.TestStringTwo);

            // Act
            var result = arrangement.SUT.Last();

            // Assert
            result.Should().Be(arrangement.TestStringTwo);
        }


        [Fact]
        public void DynamicArray_DoesNotContainsItem_ShouldReturnFalse()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            // Act
            var result = arrangement.SUT.Contains(arrangement.TestString);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void DynamicArray_DoesContainsItem_ShouldReturnTrue()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            arrangement.SUT.Add(arrangement.TestString);

            // Act
            var result = arrangement.SUT.Contains(arrangement.TestString);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void DynamicArray_Enumerate_ShouldReturnAllItems()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
               .WithDefault()
               .Build();

            arrangement.SUT.Add(arrangement.TestString);
            arrangement.SUT.Add(arrangement.TestStringTwo);

            var stringList = new List<string>();

            // Act
            foreach (var item in arrangement.SUT) {
                stringList.Add(item);
            }

            // Assert
            stringList.Count.Should().Be(2);
            stringList.First().Should().Be(arrangement.TestString);
            stringList.Last().Should().Be(arrangement.TestStringTwo);
        }
    }
}
