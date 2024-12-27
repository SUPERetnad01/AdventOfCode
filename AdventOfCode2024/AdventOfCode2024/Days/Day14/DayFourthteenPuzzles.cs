using AdventOfCode2024.Days.Day21;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode2024.Days.Day14.DayFourthteenPuzzles;

namespace AdventOfCode2024.Days.Day14;



public class DayFourthteenPuzzles
{
	public void HandlePuzzles()
	{
		var testinput = File.ReadAllLines(ReadInputFile.GetPathToInput(14)).ToList();
		var result = PartOne(testinput, 103, 101);
		Console.WriteLine($"Day 14 part one: {result}");

		var result2 = PartTwo(testinput, 103, 101);
		Console.WriteLine($"Day 14 part two: {result2}");
	}

	public class Bot
	{
		public int X { get; set; }
		public int Y { get; set; }

		public int VelocityX { get; set; }
		public int VelocityY { get; set; }

		public void Move(int gridHeight, int gridWith)
		{
			var newX = X + VelocityX;
			var newY = Y + VelocityY;

			Func<int, int> CalculateX = x => x switch
			{
				< 0 => gridWith - Math.Abs(x),
				_ when x >= gridWith => x - gridWith,
				_ => x,
			};


			Func<int, int> CalculateY = y => y switch
			{
				< 0 => gridHeight - Math.Abs(y),
				_ when y >= gridHeight => y - gridHeight,
				_ => y,
			};

			X = CalculateX(newX);
		
			Y = CalculateY(newY);

		}

		public int AmountOfSurroundingBots(List<Bot> bots)
		{
			var botXY = bots.Select(_ => (_.X , _.Y)).ToList();

			var le = new List<(int, int)>() {
			(Y - 1, X ),
			(Y + 1, X ),
			(Y, X + 1 ),
			(Y, X - 1 ),
			(Y - 1,X + 1 ),
			(Y - 1, X - 1 ),
			(Y + 1, X - 1 ),
			(Y + 1, X + 1 ),

			};

			var suroundingBots = botXY.Where(_ => le.Contains(_)).Count();

			return suroundingBots;

		}
	}


	public int PartTwo(List<string> testinput, int gridHeigth, int gridWidth)
	{
		var amountOfSeconds = int.MaxValue;
		var allBots = new List<Bot>();


		foreach (var bot in testinput)
		{
			var match = Regex.Match(bot, @"p=(-?\d+),(-?\d+)\s+v=(-?\d+),(-?\d+)");
			var newBot = new Bot()
			{
				X = int.Parse(match.Groups[1].Value),
				Y = int.Parse(match.Groups[2].Value),
				VelocityX = int.Parse(match.Groups[3].Value),
				VelocityY = int.Parse(match.Groups[4].Value)
			};
			allBots.Add(newBot);
		}

		var maxX = 0;
		var maxY = 0;
		for (int i = 0; i < amountOfSeconds; i++)
		{


			var maxSuroundingBots = 0;
			foreach (var bot in allBots)
			{
				bot.Move(gridHeigth, gridWidth);
				//maxSuroundingBots =+ bot.AmountOfSurroundingBots(allBots);
			}

			var x = allBots.GroupBy(_ => _.Y)
				.ToDictionary(_ => _.Key, _ => _.Select(coord => coord.X)).ToList()
				.MaxBy(_ => _.Value.Count())
				.Value
				.Count();

			//if (x == 33 || y == 33)
			//{
			//	//Console.WriteLine(" ");
			//	//Console.WriteLine($"Iteration Count {i}");
			//	//PrintGrid(gridHeigth, gridWidth, allBots);
			//}
		}

		return 1;
	}



	public int PartOne(List<string> testinput, int gridHeigth, int gridWidth)
	{
		var amountOfSeconds = 100;

		var allBots = new List<Bot>();

		foreach(var bot in testinput)
		{
			var match = Regex.Match(bot,@"p=(-?\d+),(-?\d+)\s+v=(-?\d+),(-?\d+)");
			var newBot = new Bot() { 
				X = int.Parse(match.Groups[1].Value),
				Y = int.Parse(match.Groups[2].Value),
				VelocityX = int.Parse(match.Groups[3].Value),
				VelocityY = int.Parse(match.Groups[4].Value)
			};
			allBots.Add(newBot);
		}

        for (int i = 0; i < amountOfSeconds; i++)
        {
			foreach (var bot in allBots)
			{
				bot.Move(gridHeigth, gridWidth);
			}
        }

		var halfGridHeight = gridHeigth / 2;
		var halfGridWidth = gridWidth / 2;

		var TLbots = allBots.Where(_ => _.X < halfGridWidth & _.Y < halfGridHeight).Count();
		var TRbots = allBots.Where(_ => _.X > halfGridWidth & _.Y < halfGridHeight).Count();
		var BLbots = allBots.Where(_ => _.X < halfGridWidth & _.Y > halfGridHeight).Count();
		var BRbots = allBots.Where(_ => _.X > halfGridWidth & _.Y > halfGridHeight).Count();

		var result = TLbots * TRbots * BLbots * BRbots;

		return result;
	}

	private void PrintGrid(int gridHeight,int gridWidth,List<Bot> bots)
	{
        for (int i = 0; i < gridHeight; i++)
        {
            for (var j = 0; j < gridWidth; j++)
            {
				var bot = bots.Where(_ => _.X == j && _.Y == i).ToList();
                if(bot.Count > 0)
				{
					Console.Write(bot.Count);
				}
				else
				{
					Console.Write(".");
				}

            }

			Console.WriteLine();
        }
    }

}
