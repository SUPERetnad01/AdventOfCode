using AdventOfCode2024.Days.Day5;
using AdventOfCode2024.Days.Day6;

namespace AdventOfCode2024.Tests.Days;

public class DaySixPuzelsTests
{
	[Fact]
	public void PartOne()
	{
		var input = @"
....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";

		var labBluePrint = input
			.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			.Select(_ => _.ToList()).ToList();

		var question1 = DaySixPuzzels.PartOne(labBluePrint);

		Assert.Equal(41, question1);
	}

	[Fact]
	public void PartTwo()
	{

		var normalInput = @"
....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";

		var labBluePrint = normalInput
			.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			.Select(_ => _.ToList()).ToList();


		var question2 = DaySixPuzzels.PartTwo(labBluePrint);

		Assert.Equal(6, question2);

	}

	[Fact]
	public void PartTwoCustomInput() {

		var input = @"
....#....................
.........#...............
.................#.......
..#...................#..
.......#.................
.........................
.#..^...........#........
........#............#...
#........................
......#..................";

		var labBluePrint = input
			.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			.Select(_ => _.ToList()).ToList();


		var question2 = DaySixPuzzels.PartTwo(labBluePrint);

		Assert.Equal(7, question2);

	}

	[Fact]
	public void PartTwoCustomInputPartTwo()
	{

		var input = @"
........................#....................
.............................#...............
.....................................#.......
......................#...................#..
...........................#.................
.....+#......................................
.....................#..^...........#........
............................#............#...
....................#........................
..........................#..................";

		var labBluePrint = input
			.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			.Select(_ => _.ToList()).ToList();


		var question2 = DaySixPuzzels.PartTwo(labBluePrint);

		Assert.Equal(7, question2);

	}
}
