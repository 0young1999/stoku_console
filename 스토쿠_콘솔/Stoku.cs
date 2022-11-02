using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스토쿠_콘솔
{
	class Stoku
	{
		// 변수
			// 확정 칸
		private int[,] game = new int[9, 9];
			// 힌트
		private bool[,,] hint = new bool[9, 9, 9];
			// 힌트 숫자
		private int[,] hintCount = new int[9, 9];

		public Stoku(int[,] gameSet)
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

			game = gameSet;
		}
		private Stoku() { }


		// 힌트
		public Result hintScan()
		{
			nullScan();
			totalBluckScan();
			totalLineScan();

			Result result = new Result(game, hint, "힌트 초기화");
			return result;
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
							if (hint[game[i, j] - 1, i, k])
							{
								hint[game[i, j] - 1, i, k] = false;
								hintCount[i, k]--;
							}
							if (hint[game[i, j] - 1, k, j])
							{
								hint[game[i, j] - 1, k, j] = false;
								hintCount[k, j]--;
							}
						}
		}

		// 힌트 한개만 가지고 있는 칸 탐색
		public Result onlyOneHintScan(long count)
		{
			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
					if (hintCount[i, j] == 1)
						for (int k = 0; k < 9; k++)
							if (hint[k, i, j])
							{
								int[] location = { i, j };
								Result result = new Result(game, hint, " " + count + "번 힌트 한개만 가지고 있는 칸 탐색");
								result.targetOne(k, location);
								game[i, j] = k + 1;
								return result;
							}
			return null;
		}

		// 상자 안 숨겨진 하나 Hidden Single
		public Result boxInHiddenSingle(long count)
		{
			for (int k = 0; k < 9; k++)	// 들어갈 숫자
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
							Result result = new Result(game, hint, " " + count + "번 상자안에 숨겨진 하나");
							result.targetOne(k, location);
							game[location[0], location[1]] = k + 1;
							return result;
						}
					}
				}
			}
			return null;
		}


		// Intersetion(pointing) 교차로(포인팅)
		public Result intersetingPointing(long count)
		{
			for (int k = 0; k < 9; k++)	// 들어갈 숫자
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
									Result result = new Result(game, hint, " " + count + "번째 교차로(포인팅)");
									result.intersetion(k, j, (i * 3) + x, true);
									for (int b = 0; b < 9; b++)
									{
										if (b / 3 != j && hint[k, (i * 3) + x, b])
										{
											hint[k, (i * 3) + x, b] = false;
											hintCount[(i * 3) + x, b]--;
										}
									}
									return result;
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
									Result result = new Result(game, hint, " " + count + "번째 교차로(포인팅)");
									result.intersetion(k, i, (j * 3) + y, false);
									for (int b = 0; b < 9; b++)
									{
										if (b / 3 != i && hint[k, b, (j * 3) + y])
										{
											hint[k, b, (j * 3) + y] = false;
											hintCount[b, (j * 3) + y]--;
										}
									}
									return result;
								}
							}
						}
					}
				}
			}

			return null;
		}

		// Intersetion(Claiming) 교차로(클레이밍)
		public Result intersetionClaiming(long count)
		{
			for (int num = 0; num < 9; num++)	// 숫자
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
										Result result = new Result(game, hint, " " + count + "번 교차로(클레이밍)X");
										result.intersetion(num, checkX, x1, true);
										for (int hx = (x1 / 3) * 3; hx < ((x1 / 3) * 3) + 3; hx++)
										{
											for (int hy1 = checkX * 3; hy1 < (checkX * 3) + 3; hy1++)
											{
												if (hx != x1 && hint[num, hx, hy1])
												{
													hint[num, hx, hy1] = false;
													hintCount[hx, hy1]--;
												}
											}
										}
										return result;
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
										Result result = new Result(game, hint, " " + count + "번 교차로(클레이밍)Y");
										result.intersetion(num, checkY, x1, false);
										for (int hx = (x1 / 3) * 3; hx < ((x1 / 3) * 3) + 3; hx++)
										{
											for (int hy1 = checkY * 3; hy1 < (checkY * 3) + 3; hy1++)
											{
												if (hx != x1 && hint[num, hy1, hx])
												{
													hint[num, hy1, hx] = false;
													hintCount[hy1, hx]--;
												}
											}
										}
										return result;
									}
								}
							}
						}
					}
				}
			}

			return null;
		}

		// Naked Pair 드러난 둘
		public Result nakedPair(long count)
		{
			// line
			for (int i1 = 0; i1 < 9; i1++)
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
											int[] location = { j1, j2 };
											Result result = new Result(game, hint, " " + count + "번째 드러난 둘Y");
											result.nakedPair(temp1, i1, false, location);
											for (int a1 = 0; a1 < 9; a1++)
											{
												if (a1 != j1 && a1 != j2)
												{
													if (hint[temp1[0], a1, i1])
													{
														hint[temp1[0], a1, i1] = false;
														hintCount[a1, i1]--;
													}
													if (hint[temp1[1], a1, i1])
													{
														hint[temp1[1], a1, i1] = false;
														hintCount[a1, i1]--;
													}
												}
											}
											return result;
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
											int[] location = { j1, j2 };
											Result result = new Result(game, hint, " " + count + "번째 드러난 둘X");
											result.nakedPair(temp1, i1, true, location);
											for (int a1 = 0; a1 < 9; a1++)
											{
												if (a1 != j1 && a1 != j2)
												{
													if (hint[temp1[0], i1, a1])
													{
														hint[temp1[0], i1, a1] = false;
														hintCount[i1, a1]--;
													}
													if (hint[temp1[1], i1, a1])
													{
														hint[temp1[1], i1, a1] = false;
														hintCount[i1, a1]--;
													}
												}
											}
											return result;
										}
									}
								}
							}
						}
					}
				}
			}

			// box

			return null;
		}

		// Naked Triple 드러난 셋
		public Result nakedTriple(long count)
		{
			for(int x = 0; x < 9; x++)
			{
				int[] target = { 0, 0, 0 };
				for(target[0] = 0; target[0] < 7; target[0]++) // 예상 숫자 1
				{
					for(target[1] = target[0] + 1; target[1] < 8; target[1]++)  // 예상 숫자 2
					{
						for(target[2] = target[1] + 1; target[2] < 9; target[2]++)  // 예상 숫자 3
						{
							// X
							int[] check1 = new int[9];
							int num1Check = 0;
							int num2Check = 0;
							int num3Check = 0;
							int[] locationY = { -1, -1, -1 };
							for (int i = 0; i < 9; i++)
							{
								check1[i] = 0;
							}
							for(int y = 0; y < 9; y++)
							{
								int check2 = 0;	// 0:걸리는거 없음 1~3:보유수 -1:제외항목 -2:실행 검증때 사용
								if(hintCount[x, y] == 2 || hintCount[x,y] == 3)
								{
									for (int num = 0; num < 9; num++)
									{
										if (hint[num, x, y])
										{
											if (num == target[0] || num == target[1] || num == target[2])
											{
												if (check2 >= 0) check2++;
												else check2 = -2;
											}
											else
											{
												if (check2 > 0) check2 = -2;
												else check2 = -1;
											}
											if (check2 > 0)
											{
												if (num == target[0]) num1Check++;
												else if (num == target[1]) num2Check++;
												else num3Check++;
											}
										}
									}
									if (check2 > 1)
									{
										if (locationY[0] == -1) locationY[0] = y;
										else if (locationY[1] == -1) locationY[1] = y;
										else locationY[2] = y;
									}
								} else
								{
									for(int num = 0; num < 9; num++)
									{
										if ((num == target[0] || num == target[1] || num == target[2]) && hint[num, x, y])
										{
											check2 = -2;
										}
									}
								}
								check1[y] = check2;
							}
							if (locationY[0] != -1 && locationY[1] != -1 && locationY[2] != -1)
							{
								int three = 0;
								int two = 0;
								bool dump = false;
								for(int y = 0; y < 9; y++)
								{
									if (check1[y] == 3) three++;
									else if (check1[y] == 2) two++;
									else if (check1[y] == -2) dump = true;
								}
								if (dump)
								{
									Result result = new Result(game, hint, " " + count + "번째 드러난 셋 X");
									result.nakedTriple(target, x, true, locationY);
									for(int y = 0; y < 9; y++)
									{
										if(y != locationY[0] && y != locationY[1] && y != locationY[2])
										{
											if (hint[target[0], x, y])
											{
												hint[target[0], x, y] = false;
												hintCount[x, y]--;
											} else if (hint[target[1], x, y])
											{
												hint[target[1], x, y] = false;
												hintCount[x, y]--;
											}
											else if (hint[target[2], x, y])
											{
												hint[target[2], x, y] = false;
												hintCount[x, y]--;
											}
										}
									}
									return result;
								}
							}
							// Y
							num1Check = 0;
							num2Check = 0;
							num3Check = 0;
							for (int i = 0; i < 9; i++)
							{
								check1[i] = 0;
							}
							for(int i = 0; i < 3; i++)
							{
								locationY[i] = -1;
							}
							for (int y = 0; y < 9; y++)
							{
								int check2 = 0; // 0:걸리는거 없음 1~3:보유수 -1:제외항목 -2:실행 검증때 사용
								if (hintCount[x, y] == 2 || hintCount[x, y] == 3)
								{
									for (int num = 0; num < 9; num++)
									{
										if (check2 < 0) break;
										if (hint[num, y, x])
										{
											if (num == target[0] || num == target[1] || num == target[2])
											{
												if (check2 >= 0) check2++;
												else check2 = -2;
											}
											else
											{
												if (check2 > 0) check2 = -2;
												else check2 = -1;
											}
											if (check2 > 1)
											{
												if (num == target[0]) num1Check++;
												else if (num == target[1]) num2Check++;
												else num3Check++;
											}
										}
									}
									if (check2 > 1)
									{
										if (locationY[0] == -1) locationY[0] = y;
										else if (locationY[1] == -1) locationY[1] = y;
										else locationY[2] = y;
									}
								}
								check1[y] = check2;
							}
							if (locationY[0] != -1 && locationY[1] != -1 && locationY[2] != -1)
							{
								int three = 0;
								int two = 0;
								bool dump = false;
								for (int y = 0; y < 9; y++)
								{
									if (check1[y] == 3) three++;
									else if (check1[y] == 2) two++;
									else if (check1[y] == -2) dump = true;
								}
								if (dump)
								{
									Result result = new Result(game, hint, " " + count + "번째 드러난 셋 Y");
									result.nakedTriple(target, x, false, locationY);
									for (int y = 0; y < 9; y++)
									{
										if (y != locationY[0] && y != locationY[1] && y != locationY[2])
										{
											if (hint[target[0], y, x])
											{
												hint[target[0], y, x] = false;
												hintCount[y, x]--;
											}
											else if (hint[target[1], y, x])
											{
												hint[target[1], y, x] = false;
												hintCount[y, x]--;
											}
											else if (hint[target[2], y, x])
											{
												hint[target[2], y, x] = false;
												hintCount[y, x]--;
											}
										}
									}
									return result;
								}
							}
						}
					}
				}
			}
			return null;
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

		// brute force
		public List<Result> bruteForce()
		{
			List<Result> list = new List<Result>();	// 결과
			int bFState = 0;    // 진행 상태
			int[,] bFGame = GetGame();
			bool[,,] bFHint = new bool[9, 9, 9];
			long bFCount = 0;
			for (int i = 0; i < 9; i++) for (int j = 0; j < 9; j++) for (int k = 0; k < 9; k++) bFHint[i, j, k] = false;

			for (int x = 0; x < 9; x++)	// x
			{
				// 뒤로 탐색
				if(bFState < 0)
				{
					if (x == -1)
					{
						break;
					} else if(game[x, 0] != 0)
					{
						x = x - 2;
						continue;
					} else
					{
						bFState = 0;
					}
				}
				for (int y = 0; y < 9; y++) // y
				{
					if (bFState == -1) // 뒤로 탐색
					{
						if (y == -1)
						{
							if(x == 0)
							{
								return list;
							}
							if (game[x, 0] == 0) bFGame[x, 0] = 0;
							x = x - 1;
							y = 7;
							continue;
						} else if(game[x, y] != 0)
						{
							y = y - 2;
							continue;
						} else
						{
							bool temp1 = false;
							do
							{
								if(bFGame[x, y] != 9 && hint[bFGame[x, y], x, y])
								{
									break;
								}
								bFGame[x, y]++;
								if (bFGame[x, y] >= 9)
								{
									temp1 = true;
									break;
								}
							} while (!hint[bFGame[x, y], x, y]);
							if (temp1)
							{
								bFGame[x, y] = 0;
								y = y - 2;
								continue;
							}
							bFState = 0;
						}
					}
					if (game[x, y] == 0)
					{
						for(int num = bFGame[x, y]; num < 9; num++)
						{
							if (hint[num, x, y])
							{
								// 무결성 확인
								bool temp1State = false;
								// 줄단위
								for (int temp1 = 0; temp1 < 9; temp1++)
								{
									if(bFGame[x, temp1] == num + 1 || bFGame[temp1, y] == num + 1)
									{
										temp1State = true;
										bFGame[x, y] = 0;
										break;
									}
								}
								// 블럭 단위
								for(int tempX = (x / 3) * 3; tempX < (x / 3 + 1) * 3; tempX++)
								{
									for(int tempY = (y / 3) * 3; tempY < (y / 3 + 1) * 3; tempY++)
									{
										if(bFGame[tempX, tempY] == num + 1)
										{
											temp1State = true;
											bFGame[x, y] = 0;
											break;
										}
									}
								}
								if(!temp1State)
								{
									bFGame[x, y] = num + 1;
									break;
								}
							} 
							if(num == 8)
							{
								bFGame[x, y] = 0;
								bFState = -1;
								y = y - 2;
								break;
							}
						}
					}
					if (x == 8 && y == 8)
					{
						bFCount++;
						Result result = new Result(bFGame, bFHint, bFCount + "번째 결과");
						list.Add(result);
						bFState = -1;
						y = y - 2;
						continue;
					}
				}
			}

			return list;
		}

		// get
		public int[,] GetGame() { return (int[,])game.Clone(); }
		public bool[,,] GetHint() { return (bool[,,])hint.Clone(); }
	}
}
