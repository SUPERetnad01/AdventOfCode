using AdventOfCode2024.Days.Day5;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System;

namespace AdventOfCode2024.Days.Day6;

public static class DaySixPuzzels
{
	public static void HandleQuestions()
	{
		var grid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(6));

		var awnserOne = PartOne(grid);
		Console.WriteLine($"Day 6 part one: {awnserOne}");

		var awnserTwo = PartTwo(grid);
		Console.WriteLine($"Day 6 part two: {awnserTwo}");
	}

	public static int PartOne(List<List<char>> labBluePrint)
	{
		var labGrid = new Grid<char>(labBluePrint);

		var startingPoint = labGrid.Cells.FirstOrDefault(_ => _.Value == '^');

		var (grid,currentPos) = Walk(labGrid, startingPoint);

		var result = grid.Cells.Where(_ => _.Value == 'X').Count();

		return result;
	}

	public static (Grid<char> grid, Cell<char> currentPos) Walk(Grid<char> grid, Cell<char> currentPos)
	{	
		var currentDirection = CurrentDirection(currentPos.Value);

		var cellInCurrentDirection = grid.GetCellBasedOnDirection(currentPos, currentDirection);

		if(cellInCurrentDirection == null)
		{
			currentPos.Value = 'X';
			return (grid, currentPos);
		}

		if (cellInCurrentDirection.Value == '#')
		{
			currentPos.Value = Turn90Degrees(currentPos.Value);
			return Walk(grid, currentPos);
		}
		cellInCurrentDirection.Value = currentPos.Value;
		currentPos.Value = 'X';
		return Walk(grid, cellInCurrentDirection);
	}


	public static int PartTwo(List<List<char>> labBluePrint)
	{
		var labGrid = new Grid<char>(labBluePrint);

		var startingPoint = labGrid.Cells.FirstOrDefault(_ => _.Value == '^');

		var (grid, currentPos, amountOfloops) = WalkWithObstruction(labGrid, startingPoint);


		return amountOfloops;
	}

	public static (Grid<char> grid, Cell<char> currentPos, int amountOfloops) WalkWithObstruction(Grid<char> grid, Cell<char> currentPos, int amountOfLoops = 0)
	{
		//Console.WriteLine("=============================================");
		//grid.PrintGrid();
		//Console.WriteLine("=============================================");

		var currentDirection = CurrentDirection(currentPos.Value);

		var cellInCurrentDirection = grid.GetCellBasedOnDirection(currentPos, currentDirection);

	
		if (cellInCurrentDirection == null)
		{
			currentPos.Value = 'X';
			return (grid, currentPos, amountOfLoops);
		}


		if (cellInCurrentDirection.Value == '#')
		{
			var rotatedPos = Turn90Degrees(currentPos.Value);
			var newDirection = CurrentDirection(rotatedPos);
			var newCell = grid.GetCellBasedOnDirection(currentPos, newDirection);
			currentPos.Value = '+';
			newCell.Value = rotatedPos;
			//currentPos.Value = Turn90Degrees(currentPos.Value);
			return WalkWithObstruction(grid, newCell, amountOfLoops);
		}

		if (CanCreateInfiniteLoop(grid, currentPos))
		{
			amountOfLoops++;
		}

		cellInCurrentDirection.Value = currentPos.Value;
		currentPos.Value = 'X';
		return WalkWithObstruction(grid, cellInCurrentDirection, amountOfLoops);
	}

	public static bool CanCreateInfiniteLoop(Grid<char> grid, Cell<char> currentPos) {


		var nightyDegrees = Turn90Degrees(currentPos.Value);
		var nightyDegreeDirection = CurrentDirection(nightyDegrees);

		var cellAccourdingToDirection = grid.GetAllCellsInSpecificDirection(currentPos,nightyDegreeDirection);

		var cellContainingPlusSign = cellAccourdingToDirection.Where(_ => _.Value == '+').ToList();



		if(cellContainingPlusSign != null && cellContainingPlusSign.Count != 0 ) 
		{
			return true;
		}

		return false;
	}



	private static DIRECTION CurrentDirection(char character) 
	{
		var direction = character switch
		{
			'^' => DIRECTION.NORTH,
			'>' => DIRECTION.EAST,
			'<' => DIRECTION.WEST,
			'v' => DIRECTION.SOUTH,
			_ => throw new NotImplementedException(),
		};

		return direction;
	}

	private static char Turn90Degrees(char character)
	{
		var direction = character switch
		{
			'^' => '>',
			'>' => 'v',
			'<' => '^',
			'v' => '<',
			_ => throw new NotImplementedException(),
		};

		return direction;
	}

}
