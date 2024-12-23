using AdventOfCode2024.Days.Day23;
using AdventOfCode2024.Utils;
using static AdventOfCode2024.Days.Day23.DayTwentyTreePuzzles;

namespace AdventOfCode2024.Tests.Days;

public class DayTwentyThreePuzzlesTests
{
	[Fact]
	public void PartOne()
	{
		var input = File.ReadAllLines(ReadInputFile.GetPathToTestInput(23))
			.Select(_ =>
			{
				var split = _.Split('-');
				return new ConnectedPcs() { PC1 = split[0], PC2 = split[1] };
			});

		var partOne = new DayTwentyTreePuzzles().PartOne(input);
		Assert.Equal(126384, partOne);

	}
}
