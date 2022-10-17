using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스토쿠_콘솔
{
	class ConsoleWrite
	{
		// 기본 출력
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
			// 타겟팅
		public void bigHintTargeting(long count, string content, int[,] state, bool[,,] hint, int target, int[] location)
		{
			Console.WriteLine(count + "번째 " + content + " target : " + (target + 1) + "\n");
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
								if((i2 * 3 + j2 + 1) == (target + 1))
								{
									if(i1 == location[0] && j1 == location[1])
									{
										Console.ForegroundColor = ConsoleColor.Blue;
									} else
									{
										Console.ForegroundColor = ConsoleColor.Green;
									}
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
		// Intersetion(pointing) 교차로(포인팅)
		public void bigHintIntersetingPointing(long count, string content, int[,] state, bool[,,] hint, int target, int locationBox, int line, bool lineDir)
		{
			Console.WriteLine(count + "번째 " + content + " target : " + (target + 1) + "\n");
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
								if (target + 1 == (i2 * 3 + j2 + 1))
								{
									if ((lineDir && line == i1) || (!lineDir && line == j1))
									{
										if ((lineDir && locationBox == (j1 / 3)) || (!lineDir && locationBox == (i1 / 3)))
										{
											Console.ForegroundColor = ConsoleColor.Blue;
										}
										else
										{
											Console.ForegroundColor = ConsoleColor.Red;
										}
									}
									else
									{
										Console.ForegroundColor = ConsoleColor.Green;
									}
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
		// Intersetion(Claiming) 교차로(클레이밍)
		public void bigHintIntersetionClaiming(long count, string content, int[,] state, bool[,,] hint, int target, int locationBox, int line, bool lineDir)
		{
			Console.WriteLine(count + "번째 " + content + " target : " + (target + 1) + "\n");
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
								if (target + 1 == (i2 * 3 + j2 + 1))
								{
									if ((lineDir && locationBox == (j1 / 3) && i1 / 3 == line / 3) || (!lineDir && locationBox == (i1 / 3) && j1 / 3 == line / 3))
									{
										if ((lineDir && line == i1) || (!lineDir && line == j1))
										{
											Console.ForegroundColor = ConsoleColor.Blue;
										}
										else
										{
											Console.ForegroundColor = ConsoleColor.Red;
										}
									}
									else
									{
										Console.ForegroundColor = ConsoleColor.Green;
									}
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
		// Naked Pair 드러난 둘
		public void bigHintNakedPair(long count, string content, int[,] state, bool[,,] hint, int[] target, int lineNum, bool targetDir, int targetLocation1, int targetLocation2)
		{
			Console.WriteLine(count + "번째 " + content + " target : " + (target[0] + 1) + "," + (target[1] + 1) + "\n");
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
								if(target[0] == i2 * 3 + j2 || target[1] == i2 * 3 + j2)
								{
									if((targetDir && lineNum == i1) || (!targetDir && lineNum == j1))
									{
										if((targetDir && (targetLocation1 == j1 || targetLocation2 == j1)) || (!targetDir && (targetLocation1 == i1 || targetLocation2 == i1)))
										{
											Console.ForegroundColor = ConsoleColor.Blue;
										} else
										{
											Console.ForegroundColor = ConsoleColor.Red;
										}
									} else
									{
										Console.ForegroundColor = ConsoleColor.Green;
									}
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
