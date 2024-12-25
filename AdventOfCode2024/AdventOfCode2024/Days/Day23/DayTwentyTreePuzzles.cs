using AdventOfCode2024.Utils;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day23;

public class DayTwentyTreePuzzles
{
	public void HandlePuzzles()
	{
		var input = File.ReadAllLines(ReadInputFile.GetPathToInput(23))
			.Select(_ =>
			{
				var split = _.Split('-');
				return new ConnectedPcs() { PC1 = split[0], PC2 = split[1] };
			});


		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var partOne = PartOne(input);
		stopwatch.Stop();

		Console.WriteLine($"Day 22 part one: {partOne} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Restart();
		var partTwo = PartTwo(input);
		stopwatch.Stop();

		Console.WriteLine($"Day 22 part two: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
	}

	public struct ConnectedPcs
	{
		public string PC1 { get; set; }
		public string PC2 { get; set; }

		public string GetOtherPc(string PcAlreadyInList)
		{
			if (PC1 == PcAlreadyInList)
			{
				return PC2;
			}

			return PC1;
		}
	}



	public int PartOne(IEnumerable<ConnectedPcs> input)
	{
		var uniquePcs = input
			.Select(_ => new List<string>() {
				_.PC1, _.PC2
			})
			.SelectMany(_ => _)
			.Distinct();

		var dictOfConnections = new Dictionary<string, IEnumerable<string>>();


		foreach (var uniquePc in uniquePcs)
		{
			var allconnectedPcs = input
				.Where(_ => _.PC2 == uniquePc || _.PC1 == uniquePc)
				.Select(_ => _.GetOtherPc(uniquePc))
				.Distinct();

			dictOfConnections.Add(uniquePc, allconnectedPcs);
		}

		var result = PairsOfThree(dictOfConnections);

		var amountOfTs = result.Where(_ => _.Item1.StartsWith('t') || _.Item2.StartsWith('t') || _.Item3.StartsWith('t'));


		return amountOfTs.Count();

	}

	public string PartTwo(IEnumerable<ConnectedPcs> input)
	{
		var uniquePcs = input
		.Select(_ => new List<string>() {
				_.PC1, _.PC2
		})
		.SelectMany(_ => _)
		.Distinct()
		.ToHashSet();

		var dictOfConnections = new Dictionary<string, HashSet<string>>();

		foreach (var uniquePc in uniquePcs)
		{
			var allConnectedPcs = input
				.Where(_ => _.PC2 == uniquePc || _.PC1 == uniquePc)
				.Select(_ => _.GetOtherPc(uniquePc))
				.Distinct();

			dictOfConnections.Add(uniquePc, allConnectedPcs.ToHashSet());
		}

		var orderedByAmountOfNodes = dictOfConnections
			.OrderBy(_ => _.Value.Count())
			.ToDictionary();

		var result = FindAllCliques([], uniquePcs, [], orderedByAmountOfNodes).Distinct();

		var max = result.MaxBy(_ => _.Count).OrderBy(_ => _);
		var resultString = string.Join(",", max);

		return resultString;
	}

	private HashSet<HashSet<string>> FindAllCliques(HashSet<string> currentCollectionOfPcs, HashSet<string> potentialPcs, HashSet<string> pcVisited, Dictionary<string, HashSet<string>> graph)
	{

		var cliques = new HashSet<HashSet<string>>(HashSet<string>.CreateSetComparer());

		if (potentialPcs.Count == 0 && pcVisited.Count == 0)
		{

			cliques.Add(new HashSet<string>(currentCollectionOfPcs));
			return cliques;

		}

		string pivot = potentialPcs.Union(pcVisited)
			.OrderByDescending(_ => graph[_].Count)
			.First();

		var nonNeighbors = new HashSet<string>(potentialPcs);
		nonNeighbors.ExceptWith(graph[pivot]);


		foreach (var pc in nonNeighbors)
		{
			currentCollectionOfPcs.Add(pc);

			var connectedToPc = graph[pc];

			var newPotentialPcs = new HashSet<string>(potentialPcs);
			newPotentialPcs.IntersectWith(connectedToPc);

			var newPcVisited = new HashSet<string>(pcVisited);
			newPcVisited.IntersectWith(connectedToPc);

			var result = FindAllCliques(currentCollectionOfPcs, newPotentialPcs, newPcVisited, graph);

			cliques.UnionWith(result);

			currentCollectionOfPcs.Remove(pc);
			potentialPcs.Remove(pc);
			pcVisited.Add(pc);

		}

		return cliques;
	}

	private HashSet<(string, string, string)> PairsOfThree(Dictionary<string, IEnumerable<string>> dictOfConnections)
	{
		var totalConnections = new HashSet<(string, string, string)>();

		foreach (var (pc, connections) in dictOfConnections)
		{

			foreach (var rr in connections)
			{
				foreach (var thridPc in dictOfConnections[rr])
				{
					var thirdConnections = dictOfConnections[thridPc];
					if (thirdConnections.Contains(pc))
					{


						var r = new List<string>() {
							pc, rr, thridPc
						}.OrderBy(_ => _)
						.ToList();

						var newTupple = (r[0], r[1], r[2]);

						if (!totalConnections.Contains(newTupple))
						{
							totalConnections.Add(newTupple);
						}

						continue;
					}
				}
			}
		}

		return totalConnections;
	}
}