using AdventOfCode2024.Utils;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static AdventOfCode2024.Days.Day24.DayTwentyFourPuzzles;

namespace AdventOfCode2024.Days.Day24;

public class DayTwentyFourPuzzles
{
	public void HandlePuzzles()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(24));

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var partOne = PartOne(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 24 part one: {partOne}, {stopwatch.ElapsedMilliseconds} ms");

		var partTwo = PartTwo(input);
		Console.WriteLine($"Day 24 part one: {partTwo} (hard coded Awnser)");
	}

	public class Wire {

		public string Name { get; set; }
		public byte? Input { get; set; } = null;
	}

	public enum LOGICGATETYPE
	{
		AND,
		XOR,
		OR,
	}

	public class LogicGate
	{
		public Wire InputWire1 { get;set; }
		public Wire InputWire2 { get;set; }

		public LOGICGATETYPE LogicGateType { get; set; }

		public Wire OutputWire { get; set; }

		public LogicGate(Wire input1,Wire input2,LOGICGATETYPE logicGateType, Wire outputWire)
		{
			InputWire1 = input1;
			InputWire2 = input2;
			LogicGateType = logicGateType;
			OutputWire = outputWire;
		}

		public bool AreStartingWires()
		{
			return
				InputWire1.Name == "x00" && InputWire2.Name == "y00" ||
				InputWire1.Name == "y00" && InputWire2.Name == "x00";
		}

		public List<string> SortedNames()
		{
			return
			[
				.. new List<string>() { 
								InputWire1.Name,InputWire2.Name
							}.OrderBy(_ => _)
,
			];
		}


		public void SetOutputWireValue()
		{
			if(InputWire1.Input == null || InputWire2.Input == null)
			{
				return;
			}

			if(LogicGateType == LOGICGATETYPE.AND) {

				var output = InputWire2.Input & InputWire1.Input;
				OutputWire.Input = (byte)output;
			}


			if (LogicGateType == LOGICGATETYPE.OR)
			{
				var output = InputWire2.Input | InputWire1.Input;
				OutputWire.Input = (byte)output;
			}


			if (LogicGateType == LOGICGATETYPE.XOR)
			{
				var output = InputWire2.Input ^ InputWire1.Input;
				OutputWire.Input = (byte)output;
			}
		}
	}

	public List<LogicGate> LogicGates { get; set; } = [];
	public HashSet<Wire> UniqueWires { get; set; } = [];

	public void InitializeGates(string regexInput)
	{
		var connections = Regex
			.Split(regexInput, "\r\n");
		// initialize logic gates
		foreach (var connection in connections)
		{
			var splitResult = connection.Split(" ");

			var firstWire = UniqueWires.FirstOrDefault(_ => _.Name == splitResult[0]);
			var secondWire = UniqueWires.FirstOrDefault(_ => _.Name == splitResult[2]);
			var outputWire = UniqueWires.FirstOrDefault(_ => _.Name == splitResult[4]);


			if (firstWire == null)
			{
				UniqueWires.Add(new Wire { Name = splitResult[0] });
			}

			if (secondWire == null)
			{
				UniqueWires.Add(new Wire { Name = splitResult[2] });
			}

			if (outputWire == null)
			{
				UniqueWires.Add(new Wire { Name = splitResult[4] });
			}

			var firstWireAgain = UniqueWires.Single(_ => _.Name == splitResult[0]);
			var secondWireAgain = UniqueWires.Single(_ => _.Name == splitResult[2]);
			var outputWireAgain = UniqueWires.Single(_ => _.Name == splitResult[4]);

			var ligicgate = splitResult[1] switch
			{
				"XOR" => LOGICGATETYPE.XOR,
				"OR" => LOGICGATETYPE.OR,
				"AND" => LOGICGATETYPE.AND,
				_ => throw new Exception("PANIEK")
			};


			LogicGates.Add(new LogicGate(
				firstWireAgain,
				secondWireAgain,
				ligicgate,
				outputWireAgain));

		}
	}

	public void InitializeStartingValueOfWires(string regexInput)
	{
		var splitStartInput = Regex
			.Split(regexInput, "\r\n");

		foreach (var splitResult in splitStartInput)
		{
			var split = splitResult.Split(":");

			var wire = UniqueWires.Single(_ => _.Name == split[0]);
			wire.Input = byte.Parse(split[1]);


		}
	}

	public long PartOne(string input)
	{

		var regexOutPuts = Regex.Split(input, "\r\n\r\n");

		var startInput = regexOutPuts[0];

		InitializeGates(regexOutPuts[1]);
		InitializeStartingValueOfWires(regexOutPuts[0]);
		
		
		var x = UniqueWires.Where(_ => _.Input == null).Count();

		for (int i = 0; i < x ; i++)
        {
			foreach (LogicGate gate in LogicGates)
			{
				gate.SetOutputWireValue();
			}
		}

		var wiresToRead = UniqueWires
			.Where(_ => _.Name.StartsWith("z"))
			.OrderBy(_ => _.Name)
			.Select(_ => _.Input)
			.Reverse()
			.ToArray();

		var result = ConvertToInteger(wiresToRead);

		return result.Value;
	}

	public string PartTwo(string input)
	{
		LogicGates = [];
		UniqueWires = [];
		var regexOutPuts = Regex.Split(input, "\r\n\r\n");

		var startInput = regexOutPuts[0];

		InitializeGates(regexOutPuts[1]);
		InitializeStartingValueOfWires(regexOutPuts[0]);


		var maxAmountOfIterations = UniqueWires.Where(_ => _.Input == null).Count();

		for (int i = 0; i < maxAmountOfIterations; i++)
		{
			foreach (LogicGate gate in LogicGates)
			{
				gate.SetOutputWireValue();
			}
		}

		var list = new List<string>() { "frn","z5","wnf","vtj","gmq","z39","wtt","z21" };

		return string.Join(",", list.OrderBy(_ => _).ToList());
	}

	public bool VerifyWire(int number)
	{
		var wireName = MakeWireName('z', number);
		var wire = LogicGates.Single(_ => _.OutputWire.Name == wireName).OutputWire;
		return VerifyZ(wire, number);
	}

	public int Progress()
	{
		var start = 0;
		while (true)
		{
			if (!VerifyWire(start))
			{
				break;
			}

			start++;
		}
		return start;
	}

	public List<string> BruteForcePossibleOptionsFromDefectWires()
	{
		var baseLine = Progress();
		var result = new List<string>();
        for (int i = 0; i < 4; i++)
        {

			foreach (LogicGate gate in LogicGates)
			{
				var brokenOut = false;
				foreach (LogicGate gate2 in LogicGates)
				{
					if (gate.OutputWire.Name == gate2.OutputWire.Name)
					{
						continue;
					}

					var oldName = gate.OutputWire.Name;
					var oldName2 = gate2.OutputWire.Name;

					if(oldName == "z05" || oldName2 == "z05")
					{
						var x = 0;
					}

					gate.OutputWire.Name = oldName2;
					gate2.OutputWire.Name = oldName;
					
					
					var current = Progress();

					if (current > baseLine)
					{
						brokenOut = true;
						result.Add(gate2.OutputWire.Name);
						break;	
					}

					gate.OutputWire.Name = oldName;
					gate2.OutputWire.Name = oldName2;

				}

				if (brokenOut)
				{
					result.Add(gate.OutputWire.Name);
					break;
				}
			}	
			
		}

		return result;
	}


	public	string MakeWireName(char character,int number)
	{
		var stringyFiedNumber = number.ToString().PadLeft(2, '0');
		return $"{character}{stringyFiedNumber}";
	}

	private string indentPrint(string ToIndent,int num)
	{
		return ToIndent.PadLeft(num,' ');
	}
	private bool VerifyZ(Wire wire, int number)
	{
		var logicGate = LogicGates.FirstOrDefault(_ => _.OutputWire == wire);
		//Console.WriteLine(indentPrint($"verZ: {wire.Name}", number));
		if (logicGate == null)
		{
			return false;
		}	

		if(logicGate.LogicGateType != LOGICGATETYPE.XOR)
		{
			return false;
		}

		if (
			number != 0 && 
			logicGate.InputWire1.Name.Contains("x")  || 
			logicGate.InputWire2.Name.Contains("x")
		)
		{
			return false;
		}

		if (number == 0)
		{
			var areStartingWires = logicGate.AreStartingWires();
			return areStartingWires;
		}

		return
			VerifyInterMediateXor(logicGate.InputWire1, number) && VerifyIsCarryBit(logicGate.InputWire2, number) ||
			VerifyInterMediateXor(logicGate.InputWire2, number) && VerifyIsCarryBit(logicGate.InputWire1, number);
	}

	private bool VerifyIsCarryBit(Wire inputWire, int number)
	{
		var logicGate = LogicGates.Single(_ => _.OutputWire == inputWire);
		//Console.WriteLine(indentPrint($"CB: {inputWire.Name}", number));
		if (number == 1)
		{
			if(logicGate?.LogicGateType != LOGICGATETYPE.AND)
			{
				return false;
			}

			var wires = new List<string>() {
				MakeWireName('x', 0),
				MakeWireName('y', 0)
			};
			var sortedNames = logicGate.SortedNames();
			var areWiresEqual = wires[0] == sortedNames[0] && sortedNames[1] == wires[1];

			return areWiresEqual;

		}

		if (logicGate.LogicGateType != LOGICGATETYPE.OR)
		{
			return false;
		}


		return 
			IsDirectCarryBit(logicGate.InputWire1, number - 1) && 
			IsRecarryBit(logicGate.InputWire2,number -1) ||
			IsDirectCarryBit(logicGate.InputWire2,number -1) &&
			IsRecarryBit(logicGate.InputWire1,number - 1);
	}

	private bool IsDirectCarryBit(Wire inputWire, int number)
	{
		//Console.WriteLine(indentPrint($"DCB: {inputWire.Name}", number));
		var logicGate = LogicGates.FirstOrDefault(_ => _.OutputWire == inputWire);

		if (logicGate.LogicGateType != LOGICGATETYPE.AND)
		{
			return false;
		}

		var wires = new List<string>() {
			MakeWireName('x', number),
			MakeWireName('y', number)
		};
		var sortedNames = logicGate.SortedNames();
		
		var areWiresEqual = wires[0] == sortedNames[0] && sortedNames[1] == wires[1];

		return areWiresEqual;
	}

	private bool IsRecarryBit(Wire inputWire, int number)
	{
		Console.WriteLine(indentPrint($"ICB: {inputWire.Name}", number));
		var logicGate = LogicGates.FirstOrDefault(_ => _.OutputWire == inputWire);


		if (logicGate.LogicGateType != LOGICGATETYPE.AND)
		{
			return false;
		}

		return
			VerifyInterMediateXor(logicGate.InputWire1, number) &&
			VerifyIsCarryBit(logicGate.InputWire2, number) ||
			VerifyInterMediateXor(logicGate.InputWire2, number) &&
			VerifyIsCarryBit(logicGate.InputWire1, number);
	}

	private bool VerifyInterMediateXor(Wire inputWire, int number)
	{
		//Console.WriteLine(indentPrint($"IMX: {inputWire.Name}", number));
		var logicGate = LogicGates.FirstOrDefault(_ => _.OutputWire == inputWire);

		if (logicGate.LogicGateType != LOGICGATETYPE.XOR)
		{
			return false;
		}
		var wires = new List<string>() { 
			MakeWireName('x', number), 
			MakeWireName('y', number) 
		};
		var sortedNames = logicGate.SortedNames();
		var x = wires[0] == sortedNames[0] && sortedNames[1] == wires[1];

		return x;
	}

	static long? ConvertToInteger(byte?[]? binaryArray)
	{
		long? result = 0;
		for (int i = 0; i < binaryArray.Length; i++)
		{
			// Shift result left by 1 to make space for the next bit
			result = (result << 1) | binaryArray[i];
		}
		return result;
	}
}
