using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day6;

public static class DaySixPuzzels
{
	public static void HandleQuestions()
	{
		var grid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(6));

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var awnserOne = PartOne(grid);
		stopwatch.Restart();
		Console.WriteLine($"Day 6 part one: {awnserOne}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var awnserTwo = PartTwo(grid);
		stopwatch.Stop();
		Console.WriteLine($"Day 6 part two: {awnserTwo}, {stopwatch.ElapsedMilliseconds} ms");
	}

	public static int PartOne(List<List<char>> labBluePrint)
	{
		var labGrid = new Grid<char>(labBluePrint);

		var startingPoint = labGrid.Cells.FirstOrDefault(_ => _.Value == '^');

		WalkNotRecursively(labGrid, startingPoint);

		var result = labGrid.Cells.Where(_ => _.Value == 'X').Count();

		return result;
	}

	public static void WalkNotRecursively(Grid<char> grid, Cell<char> currentPos)
	{
		while (true)
		{
			var currentDirection = CurrentDirection(currentPos.Value);

			var cellInCurrentDirection = grid.GetCellBasedOnDirection(currentPos, currentDirection);

			if(cellInCurrentDirection == null)
			{
				currentPos.Value = 'X';
				break;
			}

			if(cellInCurrentDirection.Value == '#')
			{
				currentPos.Value = Turn90Degrees(currentPos.Value);
				continue;
			}

			cellInCurrentDirection.Value = currentPos.Value;
			currentPos.Value = 'X';
			currentPos = cellInCurrentDirection;
		}
	}

	public static int PartTwo(List<List<char>> labBluePrint)
	{
		var firstWalkOfGrid = new Grid<char>(labBluePrint);

		var startingPoint = firstWalkOfGrid.Cells.FirstOrDefault(_ => _.Value == '^');

		WalkNotRecursively(firstWalkOfGrid, startingPoint);

		var allCelsWithX = firstWalkOfGrid.Cells
			.Where(_ => _.Value == 'X')
			.ToList();

		var cleanGrid = new Grid<char>(labBluePrint);
		startingPoint.Value = '^';

		var total = WalkWithObstruction(cleanGrid, allCelsWithX, startingPoint);

		return total;
	}

	public static int WalkWithObstruction(Grid<char> grid, List<Cell<char>> allWalkedCells, Cell<char> startingPos) 
	{
		var cleanGrid = grid.Cells.Select(_ => 
			new Cell<char>() { 
				Value = _.Value,
				Coordinate = _.Coordinate,
			}
		).ToList();


		var total = 0;
		foreach(var cell in allWalkedCells)
		{
			var cleanGridCells = grid.CellGrid.Select(_ =>
				_.Select(_ => 
					new Cell<char>()
					{
						Value = _.Value,
						Coordinate = _.Coordinate,
					}
				).ToList()
			).ToList();

			var newGrid = new Grid<char>(cleanGridCells);

			var startingPosition = new Cell<char> { Value = startingPos.Value, Coordinate = startingPos.Coordinate };

			var theCell = newGrid.CellGrid[cell.Coordinate.Y][cell.Coordinate.X];
				
			if (theCell == null || (theCell.Coordinate == startingPosition.Coordinate))
			{
				continue;
			}

			theCell.Value = 'O';

			var isInfinite = IsInfiniteNotRecursivly(newGrid, startingPosition);

			if (isInfinite)
			{
				total++;	
			}
		}
		return total;
	
	}

	public static bool IsInfiniteNotRecursivly(Grid<char> grid, Cell<char> currentPos)
	{
		var listMap = new HashSet<(Coordinate, DIRECTION)>();

		while (true)
		{
			var currentDirection = CurrentDirection(currentPos.Value);
			var cellInCurrentDirection = grid.GetCellBasedOnDirection(currentPos, currentDirection);

			if(cellInCurrentDirection == null)
			{
				currentPos.Value = 'X';
				return false;
			}

			listMap.Add((currentPos.Coordinate, currentDirection));

			if (listMap.Contains((cellInCurrentDirection.Coordinate,currentDirection)))
			{
				return true;
			}

			if (cellInCurrentDirection.Value == '#' || cellInCurrentDirection.Value == 'O')
			{
				currentPos.Value = Turn90Degrees(currentPos.Value);
				continue;
			}

			cellInCurrentDirection.Value = currentPos.Value;
			currentPos.Value = 'X';
			currentPos = cellInCurrentDirection;

		}
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
