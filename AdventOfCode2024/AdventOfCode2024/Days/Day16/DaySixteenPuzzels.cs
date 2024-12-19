using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day16;

public class DaySixteenPuzzels
{
	public void HandlePuzzels()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(16));
		var grid = new Grid<char>(rawGrid);

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var result = PartOne(grid);
		stopwatch.Stop();
		Console.WriteLine($"Day 16 part one: {result} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Start();
		var partTwo = PartTwo(grid);
		stopwatch.Stop();
		Console.WriteLine($"Day 16 part two: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
	}

	public int PartOne(Grid<char> grid)
	{

		var startingPos = grid.Cells.FirstOrDefault(_ => _.Value == 'S');
		var target = grid.Cells.FirstOrDefault(_ => _.Value == 'E');
		var shortestPath = FindShortestPath(grid, startingPos, target);

		return shortestPath;
	}

	public int PartTwo(Grid<char> grid)
	{

		var startingPos = grid.Cells.FirstOrDefault(_ => _.Value == 'S');
		var target = grid.Cells.FirstOrDefault(_ => _.Value == 'E');
		var shortestPath = FindAllSeatingSpots(grid, startingPos, target);

		return shortestPath;
	}

	public int FindShortestPath(Grid<char> grid, Cell<char> startingPosition, Cell<char>? target)
	{
		var priorityQueue = new PriorityQueue<(Cell<char> cell, DIRECTION direction), int>();
		priorityQueue.Enqueue((startingPosition, DIRECTION.EAST), 0);

		var seen = new HashSet<(Cell<char>, DIRECTION)>();

		while (priorityQueue.Count > 0)
		{
			var canDeque = priorityQueue.TryDequeue(out var element, out int score);
			if (!canDeque) continue;

			var (cell, direction) = element;

			if (cell.Coordinate == target.Coordinate)
			{
				return score;
			}

			if (!seen.Add((cell, direction))) continue;

			foreach (var neighbor in grid.GetCellsForEachDirection(cell))
			{
				var (neighborCell, neighborDirection) = neighbor;

				if (neighborCell.Value == '#' ||
					DirectionHelper.IsOppisite(direction, neighborDirection) ||
					!DirectionHelper.UpDownLeftRight.Contains(neighborDirection) ||
					seen.Contains((neighborCell, neighborDirection)))
				{
					continue;
				}

				int newScore = score + CalculateScoreForPath(cell, direction, neighborCell, neighborDirection);
				priorityQueue.Enqueue((neighborCell, neighborDirection), newScore);
			}
		}

		return -1;
	}

	public int CalculateScoreForPath(Cell<char> currentCell, DIRECTION? currentDirection, Cell<char> cellToConsider, DIRECTION directionBasedOnCurrentPosition)
	{
		return currentDirection == directionBasedOnCurrentPosition || currentDirection == null ? 1 : 1001;
	}

	public int FindAllSeatingSpots(Grid<char> grid, Cell<char> startingPosition, Cell<char>? target)
	{
		var priorityQueue = new PriorityQueue<(Cell<char> cell, DIRECTION direction , Cell<char>? previousCell, DIRECTION? previousDirection),int>();
		priorityQueue.Enqueue((startingPosition, DIRECTION.EAST, null,null), 0);

		var lowestCost = new Dictionary<(Cell<char>, DIRECTION),int>();
		var backtrack = new Dictionary<(Cell<char>? cell, DIRECTION? direction),HashSet<(Cell<char>? cell, DIRECTION? direction)>>();
		var bestCost = int.MaxValue;
		var endStates = new HashSet<(Cell<char>? cell, DIRECTION?)>();

		while (priorityQueue.Count > 0)
		{
			var canDeque = priorityQueue.TryDequeue(out var element, out int score);
			if (!canDeque) continue;

			var (cell, direction, previousCell, previousCellDirection) = element;

			if (cell.Coordinate == target.Coordinate)
			{
				if (score > bestCost) {
					break;
				}
				endStates.Add((cell, direction));
				bestCost = score;
			}
			
			var isAvaliable = lowestCost.TryGetValue((cell, direction), out int lowestCostValue);
			if (!isAvaliable)
			{
				lowestCostValue = int.MaxValue;
			}

			if (score > lowestCostValue)
			{
				continue;
			}

			var isInBacktrack = backtrack.TryGetValue((cell, direction), out var value);
			if (!isInBacktrack)
			{
				backtrack.Add((cell, direction), [(previousCell, previousCellDirection)]);
			}else
			{
				value.Add((previousCell, previousCellDirection));
			}

			lowestCost[(cell, direction)] = score;

			foreach (var neighbor in grid.GetCellsForEachDirection(cell))
			{
				var (neighborCell, neighborDirection) = neighbor;

				var isAvaliableneighbour = lowestCost.TryGetValue((cell, direction), out int lowestCostNeighbor);
				if (!isAvaliableneighbour)
				{
					lowestCostNeighbor = int.MaxValue;
				}

				if (neighborCell.Value == '#' ||
					DirectionHelper.IsOppisite(direction, neighborDirection) ||
					!DirectionHelper.UpDownLeftRight.Contains(neighborDirection) ||
					score > lowestCostNeighbor)
				{
					continue;
				}

				int newScore = score + CalculateScoreForPath(cell, direction, neighborCell, neighborDirection);
				priorityQueue.Enqueue((neighborCell, neighborDirection,cell,direction), newScore);
			}
		}


		var states = new Queue<(Cell<char>? cell, DIRECTION? direction)>(endStates);
		var seen = new List<(Cell<char>? cell, DIRECTION? direction)>(endStates);

		while(states.Count > 0)
		{
			var key = states.Dequeue();

			var g = backtrack.TryGetValue(key,out var state);
			if (!g)
			{
				seen.Add((null,null));
				states.Append((null, null));
				break;
			}


			foreach(var lastState in state)
			{
				if (!seen.Contains(lastState)) 
				{
					seen.Add(lastState);
					states.Enqueue(lastState);
				}
			}
		}

		return seen.Where(_ => _.cell != null).Select(_ => _.cell.Coordinate)
			.Distinct()
			.Count();
	}

}
