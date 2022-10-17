using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스토쿠_콘솔
{
	class Stoku
	{
		// 클래스
		FileIo io = null;
		ConsoleWrite print = null;

		// 변수
			// 확정 칸
		private int[,] game = new int[9, 9];
			// 힌트
		private bool[,,] hint = new bool[9, 9, 9];
			// 힌트 숫자
		private int[,] hintCount = new int[9, 9];
		// 힌트 표시 여부
		private bool hintStart = true;


		public Stoku(String fileLocation)
		{
			// 변수 초기화
			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
				{
					hintCount[i, j] = 9;
					game[i, j] = 0;
				}
			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
					for (int k = 0; k < 9; k++)
						hint[k, i, j] = true;


			// 클래스 로드
				// 파일 입출력
			io = new FileIo();
				// 화면 출력
			print = new ConsoleWrite();

			// 파일 불러오기
			Console.WriteLine("파일 내용 확인\n");
			Console.WriteLine(io.FileFullLoad(fileLocation));
			Console.ReadKey();
			Console.WriteLine();

			// 파일 분석
			Console.WriteLine("파일 내용 분석\n");
			game = io.FileAnalysis(fileLocation);
			print.writeDefult(game);
		}
		private Stoku() { }


		// 힌트
		public void hintScan()
		{
			nullScan();
			totalBluckScan();
			totalLineScan();
		}
			// 빈공간 탐색
		private void nullScan()
		{
			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
					if (game[i, j] != 0)
						for (int k = 0; k < 9; k++)
							if (hint[k, i, j])
							{
								hint[k, i, j] = false;
								hintCount[i, j]--;
							}
		}
			// 블럭 단위 전체 탐색
		private void totalBluckScan()
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if (game[i, j] != 0)
					{
						for (int a = (i / 3) * 3; a < ((i / 3) * 3) + 3; a++)
						{
							for (int b = (j / 3) * 3; b < ((j / 3) * 3) + 3; b++)
							{
								if (hint[game[i, j] - 1, a, b])
								{
									hint[game[i, j] - 1, a, b] = false;
									hintCount[a, b]--;
								}
							}
						}
					}
				}
			}
		}
			// 줄 단위 전체 탐색
		private void totalLineScan()
		{
			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
					if (game[i, j] != 0)
						for (int k = 0; k < 9; k++)
						{
							if(hint[game[i, j] - 1, i, k])
							{
								hint[game[i, j] - 1, i, k] = false;
								hintCount[i, k]--;
							}
							if(hint[game[i, j] - 1, k, j])
							{
								hint[game[i, j] - 1, k, j] = false;
								hintCount[k, j]--;
							}
						}

			if(hintStart)
			{
				Console.WriteLine("힌트 초기화 됨\n");
				print.bigHintDefult(game, hint);
			}
			hintStart = false;
		}


		// 힌트 한개만 가지고 있는 칸 탐색
		public bool onlyOneHintScan(long count)
		{
			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
					if (hintCount[i, j] == 1)
						for (int k = 0; k < 9; k++)
							if (hint[k, i, j])
							{
								int[] location = { i, j };
								print.bigHintTargeting(count, "힌트 한개만 가지고 있는 칸 탐색", game, hint, k, location);
								game[i, j] = k + 1;
								return true;
							}
			return false;
		}

		// 상자 안 숨겨진 하나 Hidden Single
		public bool boxInHiddenSingle(long count)
		{
			for(int k = 0; k < 9; k++)	// 들어갈 숫자
			{
				for(int i = 0; i < 3; i++)	// 상자
				{
					for(int j = 0; j < 3; j++)
					{
						int[] location = { -1, -1 };
						for(int a = i * 3; a < (i * 3) + 3; a++)	// 셀
						{
							for(int b = j * 3; b < (j * 3) + 3; b++)
							{
								if (hint[k, a, b])
								{
									if(location[0] == -1)
									{
										location[0] = a;
										location[1] = b;
									} else
									{
										location[0] = -2;
									}
								} 
							}
						}
						if(location[0] > -1)
						{
							print.bigHintTargeting(count, "상자 안 숨겨진 하나", game, hint, k, location);
							game[location[0], location[1]] = k + 1;
							return true;
						}
					}
				}
			}
			return false;
		}


		// Intersetion(pointing) 교차로(포인팅)
		public bool intersetingPointing(long count)
		{
			for(int k = 0; k < 9; k++)	// 들어갈 숫자
			{
				for (int i = 0; i < 3; i++) // 상자
				{
					for (int j = 0; j < 3; j++)
					{
						int x = -1;	// -1: 걸리는거 없음 0~2:해당 줄에만 있음 -2:2줄이상 가짐
						int y = -1;
						for (int a = 0; a < 3; a++) // 셀
						{
							for (int b = 0; b < 3; b++)
							{
								if (hint[k, (i * 3) + a, (j * 3) + b])
								{
									if (x == -1) x = a;
									else x = -2;
								}
								if (hint[k, (i * 3) + b, (j * 3) + a])
								{
									if (y == -1) y = a;
									else y = -2;
								}
							}
						}
						// x
						if(x > -1)
						{
							for(int a = 0; a < 9; a++)
							{
								if (a / 3 != j && hint[k, (i * 3) + x, a])
								{
									print.bigHintIntersetingPointing(count, "교차로(포인팅)", game, hint, k, j, (i * 3) + x, true);
									for (int b = 0; b < 9; b++)
									{
										if (b / 3 != j && hint[k, (i * 3) + x, b])
										{
											hint[k, (i * 3) + x, b] = false;
											hintCount[(i * 3) + x, b]--;
										}
									}
									return true;
								}
							}
						}
						// y
						if(y > -1)
						{
							for (int a = 0; a < 9; a++)
							{
								if (a / 3 != i && hint[k, a, (j * 3) + y])
								{
									print.bigHintIntersetingPointing(count, "교차로(포인팅)", game, hint, k, i, (j * 3) + y, false);
									for (int b = 0; b < 9; b++)
									{
										if (b / 3 != i && hint[k, b, (j * 3) + y])
										{
											hint[k, b, (j * 3) + y] = false;
											hintCount[b, (j * 3) + y]--;
										}
									}
									return true;
								}
							}
						}
					}
				}
			}

			return false;
		}

		// Intersetion(Claiming) 교차로(클레이밍)
		public bool intersetionClaiming(long count)
		{
			for(int num = 0; num < 9; num++)	// 숫자
			{
				for(int x1 = 0; x1 < 9; x1++)
				{
					int checkX = -1;
					int checkY = -1;
					for(int y1 = 0; y1 < 3; y1++)
					{
						for(int y2 = 0; y2 < 3; y2++)
						{
							if(hint[num, x1, (y1 * 3) + y2])
							{
								if(checkX == -1)
								{
									checkX = y1;
								} else if(checkX != y1)
								{
									checkX = -2;
								}
							}
							if(hint[num, (y1 * 3) + y2, x1])
							{
								if(checkY == -1)
								{
									checkY = y1;
								} else if(checkY != y1)
								{
									checkY = -2;
								}
							}
						}
					}
					if(checkX > -1)
					{
						for(int i = (x1 / 3) * 3; i < ((x1 / 3) * 3) + 3; i++)
						{
							if(i != x1)
							{
								for (int hy = checkX * 3; hy < (checkX * 3) + 3; hy++)
								{
									if (hint[num, i, hy])
									{
										print.bigHintIntersetionClaiming(count, "교차로(클레이밍)X", game, hint, num, checkX, x1, true);
										for (int hx = (x1 / 3) * 3; hx < ((x1 / 3) * 3) + 3; hx++)
										{
											for (int hy1 = checkX * 3; hy1 < (checkX * 3) + 3; hy1++)
											{
												if (hx != x1)
												{
													hint[num, hx, hy1] = false;
												}
											}
										}
										return true;
									}
								}
							}
						}
					}
					if (checkY > -1)
					{
						for (int i = (x1 / 3) * 3; i < ((x1 / 3) * 3) + 3; i++)
						{
							if (i != x1)
							{
								for (int hy = checkY * 3; hy < (checkY * 3) + 3; hy++)
								{
									if (hint[num, hy, i])
									{
										print.bigHintIntersetionClaiming(count, "교차로(클레이밍)Y", game, hint, num, checkY, x1, false);
										for (int hx = (x1 / 3) * 3; hx < ((x1 / 3) * 3) + 3; hx++)
										{
											for (int hy1 = checkY * 3; hy1 < (checkY * 3) + 3; hy1++)
											{
												if (hx != x1)
												{
													hint[num, hy1, hx] = false;
												}
											}
										}
										return true;
									}
								}
							}
						}
					}
				}
			}

			return false;
		}

		// Naked Pair 드러난 둘
		public bool nakedPair(long count)
		{
			// line
			for(int i1 = 0; i1 < 9; i1++)
			{
				int x = 0;
				int y = 0;
				for(int i2 = 0; i2 < 9; i2++)
				{
					if (hintCount[i1, i2] == 2) x++;
					if (hintCount[i2, i1] == 2) y++;
				}
				if (y > 1)   // y
				{
					for (int j1 = 0; j1 < 8; j1++)
					{
						if (hintCount[j1, i1] == 2)
						{
							int[] temp1 = new int[2];
							int temp1Count = 0;
							for (int a1 = 0; a1 < 9; a1++)
							{
								if (hint[a1, j1, i1])
								{
									temp1[temp1Count] = a1;
									temp1Count++;
								}
							}
							for(int j2 = j1 + 1; j2 < 9; j2++)
							{
								if (hintCount[j2, i1] == 2)
								{
									int[] temp2 = new int[2];
									int temp2Count = 0;
									for (int a1 = 0; a1 < 9; a1++)
									{
										if (hint[a1, j2, i1])
										{
											temp2[temp2Count] = a1;
											temp2Count++;
										}
									}
									if(temp1[0] == temp2[0] && temp1[1] == temp2[1])
									{
										// 실행 여부 확인
										int stanby = 0;

										for(int b1 = 0; b1 < 9; b1++)
										{
											if (hint[temp1[0], b1, i1]) stanby++;
											if (hint[temp1[1], b1, i1]) stanby++;
										}

										if(stanby > 4)
										{
											print.bigHintNakedPair(count, "드러난 둘Y", game, hint, temp1, i1, false, j1, j2);
											for (int a1 = 0; a1 < 9; a1++)
											{
												if (a1 != j1 && a1 != j2)
												{
													if (hint[temp1[0], a1, i1])
													{
														hint[temp1[0], a1, i1] = false;
													}
													if (hint[temp1[1], a1, i1])
													{
														hint[temp1[1], a1, i1] = false;
													}
												}
											}
											return true;
										}
									}
								}
							}
						}
					}
				}
				if (x > 1)   // x
				{
					for (int j1 = 0; j1 < 8; j1++)
					{
						if (hintCount[i1, j1] == 2)
						{
							int[] temp1 = new int[2];
							int temp1Count = 0;
							for (int a1 = 0; a1 < 9; a1++)
							{
								if (hint[a1, i1, j1])
								{
									temp1[temp1Count] = a1;
									temp1Count++;
								}
							}
							for (int j2 = j1 + 1; j2 < 9; j2++)
							{
								if (hintCount[i1, j2] == 2)
								{
									int[] temp2 = new int[2];
									int temp2Count = 0;
									for (int a1 = 0; a1 < 9; a1++)
									{
										if (hint[a1, i1, j2])
										{
											temp2[temp2Count] = a1;
											temp2Count++;
										}
									}
									if (temp1[0] == temp2[0] && temp1[1] == temp2[1])
									{
										// 실행 여부 확인
										int stanby = 0;

										for (int b1 = 0; b1 < 9; b1++)
										{
											if (hint[temp1[0], i1, b1]) stanby++;
											if (hint[temp1[1], i1, b1]) stanby++;
										}

										if (stanby > 4)
										{
											print.bigHintNakedPair(count, "드러난 둘X", game, hint, temp1, i1, true, j1, j2);
											for (int a1 = 0; a1 < 9; a1++)
											{
												if (a1 != j1 && a1 != j2)
												{
													if (hint[temp1[0], i1, a1])
													{
														hint[temp1[0], i1, a1] = false;
													}
													if (hint[temp1[1], i1, a1])
													{
														hint[temp1[1], i1, a1] = false;
													}
												}
											}
											return true;
										}
									}
								}
							}
						}
					}
				}
			}

			// box

			return false;
		}


		// end check
		public bool endCheck()
		{
			for(int i = 0; i < 9; i++)
			{
				for(int j = 0; j < 9; j++)
				{
					if(game[i,j] == 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// get
		public int[,] GetGameBase() { return game; }
		public bool[,,] GetHint() { return hint; }
	}
}
