using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스토쿠_콘솔
{
	class ConsoleWrite
	{
		// 콘솔 색깔
		private void consoleColor(int num)
		{
			switch(num)
			{
				case 0:
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case 1:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				case 2:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case 3:
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case 4:
				default:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
			}
		}
		// 숫자 하나
		public void print(Result result)
		{
			Console.WriteLine(result.GetContent() + "\n");
			for (int i1 = 0; i1 < 9; i1++)  // X 셀
			{
				for (int i2 = 0; i2 < 3; i2++)  // X 힌트 셀
				{
					for (int j1 = 0; j1 < 9; j1++)  // Y 셀
					{
						for (int j2 = 0; j2 < 3; j2++)  // Y 힌트 셀
						{
							Console.Write(" ");
							if(result.GetGameDetail(i1, j1) != 0 && i2 == 1 && j2 == 1)	// 셀 확정됨
							{
								consoleColor(4);
								Console.Write(result.GetGameDetail(i1, j1));
								consoleColor(0);
							}
							else if(result.GetHintDetail(i2 * 3 + j2, i1, j1))
							{
								consoleColor(result.GetHintColorDetail(i2 * 3 + j2, i1, j1));
								Console.Write(i2 * 3 + j2 + 1);
								consoleColor(0);
							}
							else
							{
								Console.Write(" ");
							}
						}
						if (j1 != 8)
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
					if (i1 != 8)
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
			Console.ReadKey();
			Console.WriteLine();
		}
		// big hint
		/*
		 White	: 기본
		 yellow	: 완성된 숫자
		 red	: 삭제될 힌트
		 blue	: 주목할 힌트
		 green	: 선택된 숫자의 다른 셀의 힌트
		 */
		// 기본형
		public void bigHintDefult(int [,] state, bool[,,] hint)
		{
			for(int i1 = 0; i1 < 9; i1++)	// X 셀
			{
				for(int i2 = 0; i2 < 3; i2++)	// X 힌트 셀
				{
					for(int j1 = 0; j1 < 9; j1++)	// Y 셀
					{
						for(int j2 = 0; j2 < 3; j2++)	// Y 힌트 셀
						{
							Console.Write(" ");
							if(state[i1, j1] != 0 && i2 == 1 && j2 == 1)
							{
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.Write(state[i1, j1]);
								Console.ForegroundColor = ConsoleColor.White;
							} else if (hint[i2 * 3 + j2, i1, j1])
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
			Console.ReadKey();
			Console.WriteLine();
		}
		// 비정상 종료형
		public void bigHintEndCheck(int[,] state, bool[,,] hint)
		{
			for(int num = 0; num < 10; num++)	// 숫자
			{
				Console.WriteLine("hint target : " + (num + 1));
				for (int i1 = 0; i1 < 9; i1++)  // X 셀
				{
					for (int i2 = 0; i2 < 3; i2++)  // X 힌트 셀
					{
						for (int j1 = 0; j1 < 9; j1++)  // Y 셀
						{
							for (int j2 = 0; j2 < 3; j2++)  // Y 힌트 셀
							{
								Console.Write(" ");
								if (state[i1, j1] != 0 && i2 == 1 && j2 == 1)
								{
									Console.ForegroundColor = ConsoleColor.Yellow;
									Console.Write(state[i1, j1]);
									Console.ForegroundColor = ConsoleColor.White;
								}
								else if (hint[i2 * 3 + j2, i1, j1])
								{
									if(num == i2 * 3 + j2)
									{
										Console.ForegroundColor = ConsoleColor.Blue;
									}
									Console.Write(i2 * 3 + j2 + 1);
									Console.ForegroundColor = ConsoleColor.White;
								}
								else
								{
									Console.Write(" ");
								}
							}
							if (j1 != 8)
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
						if (i1 != 8)
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
				Console.ReadKey();
				Console.WriteLine();
			}
		}

	}
}
