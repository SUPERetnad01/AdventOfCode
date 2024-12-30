using AdventOfCode2024.Utils;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day7;

public static class DaySevenPuzzels
{
	public static void HandlePuzzels()
	{
		var inputFile = ReadInputFile.GetPathToInput(7);
		var inputString = File.ReadAllLines(inputFile);

		var input = inputString.Select(_ =>
		{
			var splitString = _.Split(':');

			var testResults = long.Parse(splitString.First());
			var testInput = splitString[1]
				.Trim()
				.Split(" ")
				.Select(_ => long.Parse(_))
				.ToList();
			return (testResults, testInput);
		}).ToList();

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var awnser = PartOne(input);
		stopwatch.Stop();

		Console.WriteLine($"Day 7 part one: {awnser}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var awnser2 = PartTwo(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 7 part two: {awnser2}, {stopwatch.ElapsedMilliseconds} ms");

	}

	public static long PartOne(List<(long testResults, List<long> numbersToTest)> input)
	{
		long total = 0;

		foreach (var (testResult, testNumbers) in input) 
		{
			var result = Solve(testResult, testNumbers);
			if (result == testResult)
			{
				total += testResult;
			}
		}
		
		return total;
	}

	public static long PartTwo(List<(long testResults, List<long> numbersToTest)> input)
	{
		long total = 0;
		foreach (var (testResult, testNumbers) in input)
		{
			var result = Solve2(testResult, testNumbers);
			if (result == testResult)
			{
				total += testResult;
			}
		}
		return total;
	}

	public static long Solve(long TestResult, List<long> numbersToTest) 
	{
		if (numbersToTest.Count == 1 ) { 
			return 0;
		}

		var additionSolve = numbersToTest.First() + numbersToTest[1];
		var multipleSolve = numbersToTest.First() * numbersToTest[1];

		if (additionSolve == TestResult || multipleSolve == TestResult)
		{
			return TestResult;
		}
		

		var additionElements = new List<long>(numbersToTest.GetRange(2, numbersToTest.Count - 2));
		additionElements.Insert(0, additionSolve);

		var multipleElements = new List<long>(numbersToTest.GetRange(2, numbersToTest.Count - 2));
		multipleElements.Insert(0, multipleSolve);

		var addtionTotal = Solve(TestResult, additionElements);

		if (addtionTotal == TestResult)
		{
			return addtionTotal;
		}

		var multipleTotal = Solve(TestResult, multipleElements);

		if (multipleTotal == TestResult)
		{
			return multipleTotal;
		}


		return 0;

	}

	public static long Solve2(long TestResult, List<long> numbersToTest)
	{
		if (numbersToTest.Count == 1)
		{
			return 0;
		}

		var additionSolve = numbersToTest.First() + numbersToTest[1];
		var multipleSolve = numbersToTest.First() * numbersToTest[1];
		var concatSolve = long.Parse(numbersToTest.First().ToString() + numbersToTest[1].ToString());

		if (
			(concatSolve == TestResult ||
			additionSolve == TestResult ||
			multipleSolve == TestResult) &&
			numbersToTest.Count == 2
			)
		{
			return TestResult;
		}

		var additionElements = new List<long>(numbersToTest.GetRange(2, numbersToTest.Count - 2));
		additionElements.Insert(0, additionSolve);

		var multipleElements = new List<long>(numbersToTest.GetRange(2, numbersToTest.Count - 2));
		multipleElements.Insert(0, multipleSolve);

		var concatElements = new List<long>(numbersToTest.GetRange(2, numbersToTest.Count - 2));
		concatElements.Insert(0, concatSolve);

		var addtionTotal = Solve2(TestResult, additionElements);

		if (addtionTotal == TestResult)
		{
			return addtionTotal;
		}

		var multipleTotal = Solve2(TestResult, multipleElements);

		if (multipleTotal == TestResult)
		{
			return multipleTotal;
		}


		var concactTotal = Solve2(TestResult, concatElements);

		if(concactTotal == TestResult)
		{
			return concactTotal;
		}

		return 0;
	}


}
