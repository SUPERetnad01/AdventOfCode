using AdventOfCode2024.Utils.Grid;
using AdventOfCode2024.Utils;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day21;

public class DayTwentyOnePuzzels
{
	public void HandlePuzzles()
	{
		var codes = File.ReadLines(ReadInputFile.GetPathToInput(21)).ToList();

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		//var result = PartOne(codes);
		//stopwatch.Stop();
		//Console.WriteLine($"Day 20 part one: {result} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Restart();
		var partTwo = PartTwo(codes);
		stopwatch.Stop();
		Console.WriteLine($"Day 21 part two: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
	}

	public int PartOne(List<string> codes)
	{
		var numberDialGridCells = new List<Cell<char>>()
		{
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 0 }, Value = '7'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 0 }, Value = '8'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 0 }, Value = '9'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 1 }, Value = '4'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 1 }, Value = '5'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 1 }, Value = '6'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 2 }, Value = '1'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 2 }, Value = '2'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 2 }, Value = '3'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 3 }, Value = '0'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 3 }, Value = 'A'  },
		};

		var controller = new List<Cell<char>>() {
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 0 }, Value = '^'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 0 }, Value = 'A'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 1 }, Value = '<'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 1 }, Value = 'v'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 1 }, Value = '>'  }
		};
		var controledRobotGrid = new Grid<char>(controller);
		var numberDailGrid = new Grid<char>(numberDialGridCells);


		var totalComplexitity = 0;
		foreach (var code in codes)
		{
			var keyDailSolve = SolveArm(code, numberDailGrid);
			var next = keyDailSolve;

			foreach (var _ in Enumerable.Range(0, 2))
			{
				var possilbeSolves = new List<List<string>>();

				foreach (var option in next)
				{
					var result = SolveArm(option, controledRobotGrid);
					possilbeSolves.Add(result);
				}

				var r = possilbeSolves.SelectMany(_ => _).Min(_ => _.Count());
				var allMinValues = possilbeSolves.SelectMany(_ => _).Where(_ => _.Count() == r).ToList();
				next = allMinValues;
			}

			var length = next.First().Count();
			var codeComplexitiy = int.Parse(code.Split('A').First());
			totalComplexitity += length * codeComplexitiy;

		}

		return totalComplexitity;
	}

	public long PartTwo(List<string> codes)
	{
		var numberDialGridCells = new List<Cell<char>>()
		{
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 0 }, Value = '7'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 0 }, Value = '8'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 0 }, Value = '9'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 1 }, Value = '4'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 1 }, Value = '5'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 1 }, Value = '6'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 2 }, Value = '1'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 2 }, Value = '2'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 2 }, Value = '3'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 3 }, Value = '0'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 3 }, Value = 'A'  },
		};

		var controller = new List<Cell<char>>() {
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 0 }, Value = '^'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 0 }, Value = 'A'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 0, Y = 1 }, Value = '<'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 1, Y = 1 }, Value = 'v'  },
			new Cell<char>() { Coordinate = new Coordinate() { X = 2, Y = 1 }, Value = '>'  }
		};
		var controledRobotGrid = new Grid<char>(controller);
		var numberDailGrid = new Grid<char>(numberDialGridCells);


		long totalComplexitity = 0;

		var allPossibleNumPadMoves = GetAllPossibleMovesForGrid(numberDailGrid);

		AllControllerSequences = GetAllPossibleMovesForGrid(numberDailGrid);
		DirectionLenghts = AllControllerSequences.Select(_ => (_.Key , _.Value.FirstOrDefault().Count()))
			.ToDictionary(_ =>  _.Key, _ => _.Item2 );

		foreach (var code in codes)
		{
			var keyDailSolve = SolveArm(code, numberDailGrid);
			var optimal = long.MaxValue;
			var possibleInputs = new List<long>();
			foreach (var sequence in keyDailSolve)
			{
				possibleInputs.Add(ComputeLenght2(sequence, controledRobotGrid));
			}

			var shortestDistance = possibleInputs.Min();
			var parsedCode = long.Parse(code.Split('A').First());
			totalComplexitity += shortestDistance * parsedCode;
		}

		return totalComplexitity;
	}

	private Dictionary<(Coordinate s, Coordinate e), int> DirectionLenghts { get; set; }

	private Dictionary<(Coordinate start, Coordinate end), List<string>> AllControllerSequences { get; set;	 }

	private Dictionary<(Coordinate start,Coordinate end,Grid<char> grid,int depth), long> Memoization { get; set; } = [];
	private Dictionary<(string seqence ,int depth), long> Memoization2 { get; set; } = [];

	public long ComputeLenght2(string sequence, Grid<char> grid, int depth = 25)
	{
		if (depth == 1)
		{
			var allLengths = 0;
			foreach (var combination in ("A" + sequence).Zip(sequence))
			{
				var startPos = grid.Cells.FirstOrDefault(_ => _.Value == combination.First);
				var endpost = grid.Cells.FirstOrDefault(_ => _.Value == combination.Second);

				allLengths += DirectionLenghts[(startPos.Coordinate, endpost.Coordinate)];
			}
			return allLengths;
		}

		if (Memoization2.TryGetValue((sequence, depth), out var val))
		{
			return val;
		}

		long lenght = 0;

		foreach (var pairOfSequences in ("A" + sequence).Zip(sequence))
		{
			var startPos = grid.Cells.FirstOrDefault(_ => _.Value == pairOfSequences.First).Coordinate;
			var endpost = grid.Cells.FirstOrDefault(_ => _.Value == pairOfSequences.Second).Coordinate;
			var minvalueOfSequence = AllControllerSequences[(startPos, endpost)]
				.Select(_ => {
					//Memoization2.TryAdd((pairOfSequences, depth), lenght);
					var computedValue = ComputeLenght2(_, grid, depth - 1);
					return computedValue;
				}).Min();

			lenght += minvalueOfSequence;
		}

		Memoization2.TryAdd((sequence, depth), lenght);
		return lenght;
	}

	//public long ComputeLenght(
	//	Coordinate start,
	//	Coordinate end,
	//	Grid<char> grid,
	//	int depth = 25)
	//{

	//	if(depth == 1)
	//	{
	//		return DirectionLenghts[(start, end)];
	//	}

	//	if (Memoization.TryGetValue((start, end,grid,depth), out var val))
	//	{
	//		return val;
	//	}


	//	var optimal = long.MaxValue;

	//	foreach(var seqences in AllControllerSequences[(start,end)])
	//	{
	//		var alloptions = new List<List<string>>();
	//		long allLenghts = 0;
	//		foreach (var combination in ("A" + seqences).Zip(seqences))
	//		{
	//			var startPos = grid.Cells.FirstOrDefault(_ => _.Value == combination.First);
	//			var endpost = grid.Cells.FirstOrDefault(_ => _.Value == combination.Second);

	//			allLenghts += ComputeLenght(startPos.Coordinate, endpost.Coordinate,grid,depth -1);
	//		}

	//		optimal = Math.Min(optimal, allLenghts);
	//	}

	//	Memoization.TryAdd((start,end,grid,depth),optimal);
	//	return optimal;
	//}

	public Dictionary<(Coordinate start, Coordinate end), List<string>> GetAllPossibleMovesForGrid(Grid<char> grid)
	{
		var sequenceMap = new Dictionary<(Coordinate start, Coordinate end), List<string>>();

		foreach (var startPoint in grid.Cells)
		{
			foreach (var endPoint in grid.Cells)
			{
				sequenceMap[(startPoint.Coordinate, endPoint.Coordinate)] = GetAllShortestPaths(startPoint.Coordinate, endPoint.Coordinate, grid);
			}
		}

		return sequenceMap;
	}

	public List<string> SolveArm(string code, Grid<char> grid)
	{
		var sequenceMap = new Dictionary<(Coordinate start, Coordinate end), List<string>>();

		foreach (var startPoint in grid.Cells)
		{
			foreach (var endPoint in grid.Cells)
			{
				sequenceMap[(startPoint.Coordinate, endPoint.Coordinate)] = GetAllShortestPaths(startPoint.Coordinate, endPoint.Coordinate, grid);
			}
		}

		var alloptions = new List<List<string>>();

		foreach (var combination in ("A" + code).Zip(code))
		{
			var startCord = grid.Cells.FirstOrDefault(_ => _.Value == combination.First);
			var endCord = grid.Cells.FirstOrDefault(_ => _.Value == combination.Second);
			var combi = sequenceMap[(startCord.Coordinate, endCord.Coordinate)];
			alloptions.Add(combi);

		}

		var carmesianProduct = CartesianProduct(alloptions).
			Select(_ => new string(_.SelectMany(_ => _).ToArray()));

		return carmesianProduct.ToList();

	}

	public IEnumerable<IEnumerable<string>> CartesianProduct(IEnumerable<IEnumerable<string>> sequences)
	{
		IEnumerable<IEnumerable<string>> emptyProduct = new[] { Enumerable.Empty<string>() };
		return sequences.Aggregate(
			emptyProduct,
			(accumulator, sequence) =>
				from accseq in accumulator
				from item in sequence
				select accseq.Concat(new[] { item })
			);
	}


	public List<string> GetAllShortestPaths(Coordinate startingPoint, Coordinate endPoint, Grid<char> grid)
	{
		if (startingPoint == endPoint)
		{
			return ["A"];
		}


		var que = new Queue<(Coordinate currentCord, string movementString)>();
		que.Enqueue((startingPoint, ""));

		var optimalScore = int.MaxValue;
		var options = new List<string>();
		while (que.Count > 0)
		{
			var current = que.Dequeue();

			foreach (var direction in DirectionHelper.UpDownLeftRight)
			{

				var neighbouringCell = grid.GetCellBasedOnDirection(current.currentCord, direction);

				if (
					neighbouringCell == null ||
					neighbouringCell.Value == 'X'
				)
				{
					continue;
				}

				var directionSymbole = direction switch
				{
					DIRECTION.NORTH => '^',
					DIRECTION.SOUTH => 'v',
					DIRECTION.EAST => '>',
					DIRECTION.WEST => '<',
					_ => throw new NotImplementedException(),
				};

				if (neighbouringCell.Coordinate == endPoint)
				{
					if (optimalScore < current.movementString.Count())
					{
						return options;
					}
					optimalScore = current.movementString.Count();
					options.Add(current.movementString + directionSymbole + "A");
				}
				else
				{
					que.Enqueue((neighbouringCell.Coordinate, current.movementString + directionSymbole));
				}
			}
		}

		return [];
	}
}