using AdventOfCode2024.Days.Day5;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System;
using System.Collections.Immutable;

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
		var firstWalkOfGrid = new Grid<char>(labBluePrint);

		var startingPoint = firstWalkOfGrid.Cells.FirstOrDefault(_ => _.Value == '^');

		var (grid, currentPos) = Walk(firstWalkOfGrid, startingPoint);

		var allCelsWithX = grid.Cells
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
			var cleanGridCells = grid.Cells.Select(_ =>
				new Cell<char>()
				{
					Value = _.Value,
					Coordinate = _.Coordinate,
				}
			).ToList();

			var newGrid = new Grid<char>(cleanGridCells);

			var startingPosition = new Cell<char> { Value = startingPos.Value, Coordinate = startingPos.Coordinate };

			var theCell = newGrid.Cells.FirstOrDefault(_ => _.Coordinate.X == cell.Coordinate.X && _.Coordinate.Y == cell.Coordinate.Y);

			if (theCell == null || 
				(theCell.Coordinate.X == startingPosition.Coordinate.X && theCell.Coordinate.Y == startingPosition.Coordinate.X)
				)
			{
				continue;
			}

			theCell.Value = 'O';

			var (isInfinite, gridd, _) = IsInfinitePath(newGrid, startingPosition, []);



			if (isInfinite)
			{
				total++;
				Console.Clear();
				Console.WriteLine(total.ToString());
				
			}
		}
		return total;
	
	}


	public static (bool isInfinite, Grid<char> grid, Cell<char> currentPos) IsInfinitePath(Grid<char> grid, Cell<char> currentPos, List<(Coordinate, DIRECTION)> listMap)
	{
		var currentDirection = CurrentDirection(currentPos.Value);

		var cellInCurrentDirection = grid.GetCellBasedOnDirection(currentPos, currentDirection);

        if (cellInCurrentDirection == null)
		{
			currentPos.Value = 'X';
			return (false, grid, currentPos);
		}

		listMap.Add((currentPos.Coordinate,currentDirection));

		var isAlreadyInList = listMap.Where(_ => 
			_.Item1.Y == cellInCurrentDirection.Coordinate.Y &&
			_.Item1.X == cellInCurrentDirection.Coordinate.X && 
			_.Item2 == currentDirection).Count();

		if(isAlreadyInList > 1)
		{
			return (true, grid, currentPos);
		}

		if (cellInCurrentDirection.Value == '#' || cellInCurrentDirection.Value == 'O')
		{
			currentPos.Value = Turn90Degrees(currentPos.Value);
			return IsInfinitePath(grid, currentPos,listMap);
		}
		cellInCurrentDirection.Value = currentPos.Value;
		currentPos.Value = 'X';
		return IsInfinitePath(grid, cellInCurrentDirection, listMap);
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
