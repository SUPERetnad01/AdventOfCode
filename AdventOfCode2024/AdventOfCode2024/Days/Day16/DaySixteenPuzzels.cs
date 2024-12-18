using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day16;

public class DaySixteenPuzzels
{
	public void HandlePuzzels()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(16));
		var grid =  new Grid<char>(rawGrid);

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var result = PartOne(grid);
		stopwatch.Stop();
		Console.WriteLine($"Day 10 part one: {result} time ms: {stopwatch.ElapsedMilliseconds}");
	}

	public int PartOne(Grid<char> grid) 
	{

		var startingPos = grid.Cells.FirstOrDefault(_ => _.Value == 'S');
		var target = grid.Cells.FirstOrDefault(_ => _.Value == 'E');
		var shortestPath = FindShortestPath(grid,startingPos,target);

		return shortestPath;
	}

	public int FindShortestPath(Grid<char> grid, Cell<char> startingPosition, Cell<char>? target)
	{
		var priortyQueue = new PriorityQueue<(Cell<char> cell, DIRECTION direction), int>();
		priortyQueue.Enqueue((startingPosition, DIRECTION.EAST), 0);

		var seen = new HashSet<(Cell<char>, DIRECTION)>();


		while(priortyQueue.Count > 0)
		{
			var canDeque = priortyQueue.TryDequeue(out var element, out int score);
			var (cell,direction) = element;

			if(cell.Coordinate == target.Coordinate)
			{
				return score;
			}

			seen.Add((cell,direction));

			var getValidNodes = grid.GetCellsForEachDirection(cell)
				.Where(_ => 
					cell != null && 
					!DirectionHelper.IsOppisite(direction, _.direction) && 
					_.cell.Value != '#' && 
					DirectionHelper.UpDownLeftRight.Contains(_.direction) && 
					!seen.Contains(_))
				.ToList();


			var distances = getValidNodes
				.Select(_ => (score: CalculateScoreForPath(cell, direction, _.cell, _.direction) + score, cellAndDirection: _))
				.ToList();
			
			foreach( var distance in distances ) {
				priortyQueue.Enqueue(distance.cellAndDirection, distance.score);
			}

		}


		//var unvisidedNodes = grid.Cells.Where(_ => _.Coordinate != startingPosition.Coordinate).ToList();

		//var distanceTable = new Dictionary<Cell<char>, int>() {
		//	{startingPosition,0 }
		//};

		//unvisidedNodes.ForEach(_ =>
		//{
		//	distanceTable[_] = int.MaxValue;

		//});

		//var NodesToVisit = new List<Cell<char>>();


		//var currentNode = startingPosition;
		//var currentDirection = DIRECTION.EAST;
		//while (unvisidedNodes.Any())
		//{
		//	var neighboringCells = grid
		//		.GetCellsForEachDirection(currentNode)
		//		.OfType<(Cell<char>,DIRECTION)>()	
		//		.Where(_ => 
		//			DirectionHelper.UpDownLeftRight.Contains(_.Item2) &&
		//			_.Item1.Value != '#' &&
		//			!DirectionHelper.IsOppisite(currentDirection,_.Item2)
		//	);

		//	var distances = neighboringCells
		//		.Where(_ => !DirectionHelper.IsOppisite(currentDirection, _.Item2))
		//		.Select(_ => (score: CalculateScoreForPath(currentNode, currentDirection, _.Item1, _.Item2), cellAndDirection: _));

		//	foreach(var distance in distances) {
		//		distanceTable[distance.cellAndDirection.Item1] += distance.score;
		//	}

		//	var smallestDistance = distances.OrderBy(_ => _.score).FirstOrDefault();

		//	unvisidedNodes.Remove(smallestDistance.cellAndDirection.Item1);

		//	currentNode = smallestDistance.cellAndDirection.Item1;

		//}



		return 1;
	}

	public int CalculateScoreForPath(Cell<char> currentCell,DIRECTION? currentDirection,Cell<char> cellToConsider, DIRECTION directionbasedOnCurrentPosition)
	{
		if (currentDirection == directionbasedOnCurrentPosition || currentDirection == null) 
		{
			return 1;
		}

		return 1001;
	}

	// compute score based on turns
}
