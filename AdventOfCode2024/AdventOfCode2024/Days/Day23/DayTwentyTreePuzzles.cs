namespace AdventOfCode2024.Days.Day23;

public class DayTwentyTreePuzzles
{
	public void HandlePuzzles()
	{

	}

	public struct ConnectedPcs
	{
		public string PC1 { get; set; }
		public string PC2 { get; set; }
	}

	public int PartOne(IEnumerable<ConnectedPcs> input)
	{
		var uniquePcs = input
			.Select(_ => new List<string>() {
				_.PC1, _.PC2
			})
			.SelectMany(_ => _)
			.Distinct();


		var x = input.GroupBy(_ => _.PC1);
		var gr = input.GroupBy(_ => _.PC2);

		return 1;

	}
}