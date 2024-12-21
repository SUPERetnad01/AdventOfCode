using AdventOfCode2024.Days.Day20;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Tests.Days;

public class DayTwentyPuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(20));
		var grid = new Grid<char>(rawGrid);

		var awnserOne = DayTwentyPuzzels.PartOne(grid,2);
		Assert.Equal(44, awnserOne); 
	}

	[Fact]
	public void PartOneSecondTestCase()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(20));
		var grid = new Grid<char>(rawGrid);

		var awnserOne = DayTwentyPuzzels.PartOne(grid, 19);
		Assert.Equal(5, awnserOne);
	}

	[Fact]
	public void PartOneThirdTestCase()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(20));
		var grid = new Grid<char>(rawGrid);

		var awnserOne = DayTwentyPuzzels.PartOne(grid, 40);
		Assert.Equal(2, awnserOne);
	}

	[Fact]
	public void PartTwo()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(20));
		var grid = new Grid<char>(rawGrid);

		var awnserOne = DayTwentyPuzzels.PartTwo(grid, 50);
		Assert.Equal(285, awnserOne);
	}

	[Fact]
	public void PartTwoSecondTestCase()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(20));
		var grid = new Grid<char>(rawGrid);

		var awnserOne = DayTwentyPuzzels.PartTwo(grid, 60);
		Assert.Equal(129, awnserOne);
	}

	[Fact]
	public void PartTwoThirdTestCase()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(20));
		var grid = new Grid<char>(rawGrid);

		var awnserOne = DayTwentyPuzzels.PartTwo(grid, 70);
		Assert.Equal(41, awnserOne);
	}
}
