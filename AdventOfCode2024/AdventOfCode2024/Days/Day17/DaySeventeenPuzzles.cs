using AdventOfCode2024.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2024.Days.Day17;

public class DaySeventeenPuzzles
{
	public void HandlePuzzles()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(17));

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var partOne = PartOne(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 17 part one: {partOne}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var partTwo = PartTwo(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 17 part one: {partTwo}, {stopwatch.ElapsedMilliseconds} ms");
	}



	public long PartTwo(string input)
	{
		var regexPattern = @"Register A: (\d+)\s*Register B: (\d+)\s*Register C: (\d+)\s*Program: ([\d,]+)";


		var regexOut = Regex.Match(input, regexPattern);

		var registerA = int.Parse(regexOut.Groups[1].Value);
		var registerB = int.Parse(regexOut.Groups[2].Value);
		var registerC = int.Parse(regexOut.Groups[3].Value);

		var program = regexOut.Groups[4].Value
			.Split(',')
			.Select(int.Parse)
			.ToList();

		var comp = new BitComputer()
		{
			RegisterA = registerA,
			RegisterB = registerB,
			RegisterC = registerC,
			Program = program
		};

		//comp.RunProgram();

		var result = comp.ReverseProgram(program, 0);
		return result.Value;
	}

	public string PartOne(string input)
	{
		var regexPattern = @"Register A: (\d+)\s*Register B: (\d+)\s*Register C: (\d+)\s*Program: ([\d,]+)";


		var regexOut = Regex.Match(input, regexPattern);

		var registerA = int.Parse(regexOut.Groups[1].Value);
		var registerB = int.Parse(regexOut.Groups[2].Value);
		var registerC = int.Parse(regexOut.Groups[3].Value);

		var program = regexOut.Groups[4].Value
			.Split(',')
			.Select(int.Parse)
			.ToList();

		var comp = new BitComputer()
		{
			RegisterA = registerA,
			RegisterB = registerB,
			RegisterC = registerC,
			Program = program
		};

		comp.RunProgram();

		var result = string.Join(",",comp.Output.Select(_ => _.ToString()));
		return result;
	}


	private class BitComputer
	{
		public int RegisterA { get; set; }
		public int RegisterB { get; set; }
		public int RegisterC { get; set; }

		public List<int> Program { get; set; } = [];

		public int InstructionPointer { get; set; } = 0;

		public List<int> Output { get; set; } = [];


		public long? ReverseProgram(List<int> program,long awnser = 0)
		{
			if(program.Count == 0)
			{
				return awnser;
			}

			
            for (int i = 0; i < 9; i++)
            {
				long B = i;
				var A = (awnser << 3) + B;
		
				B = A % 8;
				B = B ^ 5;
				var C = (long)Math.Floor(A / Math.Pow(2, B));
				B = B ^ C;
				B = B ^ 6;

				if (B % 8 == program[program.Count - 1])
				{

					var truncedList = program.GetRange(0, program.Count - 1);
					var subAwnser = ReverseProgram(truncedList, A);
					if(subAwnser == null)
					{
						continue;
					}

					return subAwnser;
				}
			}

			return null;
        }


		public void RunProgram()
		{
			while (InstructionPointer < Program.Count())
			{
				var opCode = Program[InstructionPointer];
				var operand = Program[InstructionPointer + 1];

				switch (opCode)
				{
					case 0:
						Adv(operand);
						InstructionPointer += 2;
						break;
					case 1:
						Bxl(operand);
						InstructionPointer += 2;
						break;
					case 2:
						Bst(operand);
						InstructionPointer += 2;
						break;
					case 3:
						Jnz(operand);
						break;
					case 4:
						Bxc(operand);
						InstructionPointer += 2;
						break;
					case 5:
						Out(operand);
						InstructionPointer += 2;
						break;
					case 6:
						Bdv(operand);
						InstructionPointer += 2;
						break;
					case 7:
						Cdv(operand);
						InstructionPointer += 2;
						break;
					default:
						Console.WriteLine("Default case: Number is out of range.");
						break;
				}
			}
		}

		public int ComboOperator(int operand) => operand switch
		{
			0 => 0,
			1 => 1,
			2 => 2,
			3 => 3,
			4 => RegisterA,
			5 => RegisterB,
			6 => RegisterC,
			_ => throw new NotImplementedException(),
		};

		public void Adv(int operand)
		{
			var actualNumber = ComboOperator(operand);
			RegisterA >>= actualNumber;
		}

		public void Bxl(int operand)
		{
			RegisterB ^= operand;
		}

		public void Bst(int operand)
		{
			var resultCombo = ComboOperator(operand);
			RegisterB = resultCombo % 8;
		}

		public void Jnz(int operand)
		{
			if (RegisterA == 0) {
				InstructionPointer += 2;
				return;
			}
			InstructionPointer = operand;		
		}

		public void Bxc(int operand)
		{
			RegisterB ^= RegisterC;
		}

		public void Out(int operand)
		{
			var resultCombo = ComboOperator(operand);
			var res = resultCombo % 8;
			var digits = GetDigits(res);
			Output.AddRange(digits);
		}

		public void Bdv(int operand)
		{
			var actualNumber = ComboOperator(operand);

			RegisterB = RegisterA >> actualNumber;
		}

		public void Cdv(int operand)
		{
			var actualNumber = ComboOperator(operand);

			RegisterC = RegisterA >> actualNumber;
		}
	}

	public static IEnumerable<int> GetDigits(int source)
	{
		int individualFactor = 0;
		int tennerFactor = Convert.ToInt32(Math.Pow(10, source.ToString().Length));
		do
		{
			source -= tennerFactor * individualFactor;
			tennerFactor /= 10;
			individualFactor = source / tennerFactor;

			yield return individualFactor;
		} while (tennerFactor > 1);
	}
}
