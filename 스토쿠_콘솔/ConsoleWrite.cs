using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스토쿠_콘솔
{
	class ConsoleWrite
	{
		// 한개 출력
		public void writeDefult(int[,] state)
		{
			for (int i = 0; i < 9; i++)
			{
				Console.Write(state[i, 0]);
				for (int j = 1; j < 9; j++)
					Console.Write(" " + state[i, j]);
				Console.WriteLine();
			}
			Console.ReadKey();
			Console.WriteLine();
		}
		public void writeDefult(int[,] state, int[] target)
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if (target[0] == i && target[1] == j) Console.ForegroundColor = ConsoleColor.Red;
					Console.Write(" " + state[i, j]);
					if (target[0] == i && target[1] == j) Console.ForegroundColor = ConsoleColor.White;
				}
				Console.WriteLine();
			}
			Console.ReadKey();
			Console.WriteLine();
		}

		// 힌트 포함 모두 출력
		public void writeTotalDefult(int[,] state, bool[,,] hint)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					for (int a = 0; a < 3; a++)
					{
						for (int b = 0; b < 9; b++)
						{
							Console.Write(" ");
							if (hint[(i * 3) + a, j, b]) Console.Write("1");
							else Console.Write("0");
						}
						Console.Write(" |");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			writeDefult(state);
			Console.WriteLine();
		}
		public void writeTotalDefult(int[,] state, bool[,,] hint, int[] target)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					for (int a = 0; a < 3; a++)
					{
						for (int b = 0; b < 9; b++)
						{
							Console.Write(" ");
							if(target[0] == j && target[1] == b) Console.ForegroundColor = ConsoleColor.Red;
							if (hint[(i * 3) + a, j, b]) Console.Write("1");
							else Console.Write("0");
							if (target[0] == j && target[1] == b) Console.ForegroundColor = ConsoleColor.White;
						}
						Console.Write(" |");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			writeDefult(state, target);
			Console.WriteLine();
		}

		// 힌트 포함 카운트 내용 출력
		public void writeTotalDefult(long count, string content, int[,] state, bool[,,] hint)
		{
			Console.WriteLine(count + content + "\n");
			writeTotalDefult(state, hint);
		}

		// 힌트 포함 타겟팅 내용 출력
		public void writeTargetTotal(long count, string content, int[,] state, bool[,,] hint, int[] location)
		{
			Console.WriteLine(count + "번째 " + content + " target : " + location[0] + "," + location[1] + "\n");
			writeTotalDefult(state, hint, location);
		}

		// Intersetion(pointing) 교차로(포인팅)
		public void intersetionPointing(long count, int[,] state, bool[,,] hint, int targetNum, bool targetDir, int targetLineNum)
		{
			Console.WriteLine(count + "번째 교차로(포인팅)\n");
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					for (int a = 0; a < 3; a++)
					{
						for (int b = 0; b < 9; b++)
						{
							//if (target) Console.ForegroundColor = ConsoleColor.Red;
							if(targetNum == (i * 3) + a && ((targetDir && targetLineNum == j) || (!targetDir && targetLineNum == b)))
							{
								Console.ForegroundColor = ConsoleColor.Red;
							}
							Console.Write(" ");
							if (hint[(i * 3) + a, j, b]) Console.Write("1");
							else Console.Write("0");
							if (targetNum == (i * 3) + a && ((targetDir && targetLineNum == j) || (!targetDir && targetLineNum == b)))
							{
								Console.ForegroundColor = ConsoleColor.White;
							}
						}
						Console.Write(" |");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			writeDefult(state);
			Console.WriteLine();
		}

		// nakedPair
		public void nakedPair(long count, int[,] state, bool[,,] hint, bool targetDir, int lineNum, int[] targetNum)
		{
			Console.Write(count + "번째 드러난 둘 ");
			if (targetDir) Console.Write("좌우 " + targetNum[0] + ", " + targetNum[1]);
			else Console.Write("상하 " + targetNum[0] + ", " + targetNum[1]);
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					for (int a = 0; a < 3; a++)
					{
						for (int b = 0; b < 9; b++)
						{
							Console.Write(" ");
							if ((targetNum[0] == (i * 3) + a && targetNum[1] == (i * 3) + a) && ((targetDir && (i * 3) + j == lineNum) || (!targetDir && (a * 3) + b == lineNum)))
							{
								Console.ForegroundColor = ConsoleColor.Red;
							}
							if (hint[(i * 3) + a, j, b]) Console.Write("1");
							else Console.Write("0");
							if ((targetNum[0] == (i * 3) + a && targetNum[1] == (i * 3) + a) && ((targetDir && (i * 3) + j == lineNum) || (!targetDir && (a * 3) + b == lineNum)))
							{
								Console.ForegroundColor = ConsoleColor.White;
							}
						}
						Console.Write(" |");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			writeDefult(state);
			Console.WriteLine();
		}

		// 힌트 크게 보기
		public void bigHint(int [,] state, bool[,,] hint)
		{
			for(int i1 = 0; i1 < 9; i1++)
			{
				for(int i2 = 0; i2 < 3; i2++)
				{
					for(int j1 = 0; j1 < 9; j1++)
					{
						for(int j2 = 0; j2 < 3; j2++)
						{
							Console.Write(" ");
							if (hint[i2 * 3 + j2, i1, j1])
							{
								Console.Write(i2 * 3 + j2 + 1);
							} else
							{
								Console.Write(" ");
							}
						}
						if(j1 != 8)
						{
							if (j1 % 3 == 2)
							{
								Console.ForegroundColor = ConsoleColor.Red;
							}
							Console.Write(" |");
							if (j1 % 3 == 2)
							{
								Console.ForegroundColor = ConsoleColor.White;
							}
						}
					}
					Console.WriteLine();
				}
				for (int a = 0; a < 71; a++)
				{
					if(i1 != 8)
					{
						if (a % 8 == 7)
						{
							if (i1 % 3 == 2 || a % 24 == 23)
							{
								Console.ForegroundColor = ConsoleColor.Red;
							}
							Console.Write("+");
							if (i1 % 3 == 2 || a % 24 == 23)
							{
								Console.ForegroundColor = ConsoleColor.White;
							}
						}
						else
						{
							if (i1 % 3 == 2)
							{
								Console.ForegroundColor = ConsoleColor.Red;
							}
							Console.Write("-");
							if (i1 % 3 == 2)
							{
								Console.ForegroundColor = ConsoleColor.White;
							}
						}
					}
				}
				Console.WriteLine();
			}
		}
	}
}
