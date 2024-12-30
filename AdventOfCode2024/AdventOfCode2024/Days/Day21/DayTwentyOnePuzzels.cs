using AdventOfCode2024.Utils.Grid;
using AdventOfCode2024.Utils;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day21;

using Cache = System.Collections.Concurrent.ConcurrentDictionary<(char currentKey, char nextKey, int depth), long>;
using Keypad = Dictionary<Vec2, char>;
record struct Vec2(int x, int y);

public class DayTwentyOnePuzzels
{
	public void HandlePuzzeles()
	{
		var res1 = File.ReadAllText(ReadInputFile.GetPathToInput(21));
		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var result = Solve(res1, 2);
		Console.WriteLine($"Day 21 part one: {result}, {stopwatch.ElapsedMilliseconds} ms");

		var res2 = File.ReadAllText(ReadInputFile.GetPathToInput(21));
		var result2 = Solve(res2, 25);
		Console.WriteLine($"Day 21 part one: {result2}, {stopwatch.ElapsedMilliseconds} ms");
	}

	long Solve(string input, int depth)
	{
		var numpad = ParseKeypad("789\n456\n123\n 0A");
		var keypad2 = ParseKeypad(" ^A\n<v>");
		var keypads = Enumerable.Repeat(keypad2, depth).Prepend(numpad).ToArray();

		var cache = new Cache();
		var res = 0L;

		foreach (var line in input.Split("\r\n"))
		{
			var num = int.Parse(line[..^1]);
			res += num * EncodeKeys(line, keypads, cache);
		}
		return res;
	}

	long EncodeKeys(string keys, Keypad[] keypads, Cache cache)
	{
		if (keypads.Length == 0)
		{
			return keys.Length;
		}
		else
		{
			var currentKey = 'A';
			var length = 0L;

			foreach (var nextKey in keys)
			{
				length += EncodeKey(currentKey, nextKey, keypads, cache);
				currentKey = nextKey;
			}

			return length;
		}
	}
	long EncodeKey(char currentKey, char nextKey, Keypad[] keypads, Cache cache) =>
	   cache.GetOrAdd((currentKey, nextKey, keypads.Length), _ => {
		   var keypad = keypads[0];

		   var currentPos = keypad.Single(kvp => kvp.Value == currentKey).Key;
		   var nextPos = keypad.Single(kvp => kvp.Value == nextKey).Key;

		   var dy = nextPos.y - currentPos.y;
		   var vert = new string(dy < 0 ? 'v' : '^', Math.Abs(dy));

		   var dx = nextPos.x - currentPos.x;
		   var horiz = new string(dx < 0 ? '<' : '>', Math.Abs(dx));

		   var cost = long.MaxValue;

		   if (keypad[new Vec2(currentPos.x, nextPos.y)] != ' ')
		   {
			   cost = Math.Min(cost, EncodeKeys($"{vert}{horiz}A", keypads[1..], cache));
		   }

		   if (keypad[new Vec2(nextPos.x, currentPos.y)] != ' ')
		   {
			   cost = Math.Min(cost, EncodeKeys($"{horiz}{vert}A", keypads[1..], cache));
		   }
		   return cost;
	   });

	Keypad ParseKeypad(string keypad)
	{
		var lines = keypad.Split("\n");
		return (
			from y in Enumerable.Range(0, lines.Length)
			from x in Enumerable.Range(0, lines[0].Length)
			select new KeyValuePair<Vec2, char>(new Vec2(x, -y), lines[y][x])
		).ToDictionary();
	}
}