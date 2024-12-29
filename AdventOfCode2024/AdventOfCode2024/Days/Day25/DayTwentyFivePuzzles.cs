using AdventOfCode2024.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.Day25;

public class DayTwentyFivePuzzles
{
	public void HandlePuzzels()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(25));
		var resultOne = PartOne(input);
		Console.WriteLine($"Day 25 part one {resultOne}");
	}


	private class LockKeySchemetic
	{
		public List<List<char>> Pins { get; set; }
		public bool IsLock { get; set; }

		public List<int> PinHeights { get; set; } = [];

		public LockKeySchemetic SetPinHeights()
		{
			var result = new List<int>();

            for (int column = 0; column < Pins.First().Count; column++)
            {
				var total = 0;

				for (int row = 0; row < Pins.Count; row++)
				{
					var x = Pins[row][column] == '#' ? 1 : 0;
					total += x;
				}

				result.Add(total -1);
			}
           
			PinHeights = result;

			return this;

		}
	}

	public int PartOne(string input)
	{
		var schemetics = Regex.Split(input, "\r\n\r\n")
			.Select(_ =>
			{
				var schemetic = _.Split("\r\n")
					.Select(_ => _.Select(_ => _).ToList()).ToList();

				var lockKey = new LockKeySchemetic() {
					Pins = schemetic,
					IsLock = schemetic[0].Any(o => o == '#')
				};
			
				return lockKey;
			});

		var diffrentPins = schemetics.Select(_ => _.SetPinHeights());

		var keys = diffrentPins.Where(_ => !_.IsLock).Select(_ => _.PinHeights);
		var keyLocks = diffrentPins.Where(_ => _.IsLock).Select(_ => _.PinHeights);

		var total = 0;
		foreach(var key in keys)
		{
			foreach(var keyLock in keyLocks)
			{
				var ziped = keyLock.Zip(key);

				var validCombo = ziped.Select(_ => _.First + _.Second).Any(_ => _ >= 6);;

				if (!validCombo)
				{
					total++;
				}

			}
		}

        return total;
	}
}
