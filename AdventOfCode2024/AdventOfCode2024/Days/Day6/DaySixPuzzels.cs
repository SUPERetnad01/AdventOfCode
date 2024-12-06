using AdventOfCode2024.Days.Day5;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Days.Day6;

public static class DaySixPuzzels
{
	public static void HandleQuestions()
	{
		var grid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(6));

		var awnserOne = PartOne(grid);
		Console.WriteLine($"Day 6 part one: {awnserOne}");

		//var awnserTwo = PartTwo(orderingRules, manuals);
		//Console.WriteLine($"Day 5 part two: {awnserTwo}");
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


	//public static int PartTwo(List<List<char>> labBluePrint)
	//{
	//	var labGrid = new Grid<char>(labBluePrint);

	//	var startingPoint = labGrid.Cells.FirstOrDefault(_ => _.Value == '^');

	//	var (grid, currentPos,amountOfloops) = WalkWithObstruction(labGrid, startingPoint);

	//	return amountOfloops;
	//}

	//public static (Grid<char> grid, Cell<char> currentPos,int amountOfloops) WalkWithObstruction(Grid<char> grid, Cell<char> currentPos,int amountOfLoops = 0)
	//{
	//	var currentDirection = CurrentDirection(currentPos.Value);

	//	var cellInCurrentDirection = grid.GetCellBasedOnDirection(currentPos, currentDirection);

	//	var nightyDegrees = Turn90Degrees(currentPos.Value);
	//	var nightyDegreeDirection = CurrentDirection(nightyDegrees);
	//	var possibleLoop = grid.GetCellBasedOnDirection(currentPos, nightyDegreeDirection);
	
	//	if (cellInCurrentDirection == null)
	//	{
	//		currentPos.Value = 'X';
	//		return (grid, currentPos,amountOfLoops);
	//	}

	//	if (possibleLoop?.Value == 'X')
	//	{
	//		amountOfLoops = +1;
	//	}

	//	if (cellInCurrentDirection.Value == '#')
	//	{
	//		currentPos.Value = Turn90Degrees(currentPos.Value);
	//		return WalkWithObstruction(grid, currentPos,amountOfLoops);
	//	}
	//	cellInCurrentDirection.Value = currentPos.Value;
	//	currentPos.Value = 'X';
	//	return WalkWithObstruction(grid, cellInCurrentDirection,amountOfLoops);
	//}


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
