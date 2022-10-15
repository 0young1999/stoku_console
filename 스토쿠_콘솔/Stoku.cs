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
		private int[,] gameBase = new int[9, 9];
			// 현재 진행
		private int[,] gameState = new int[9, 9];
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
					gameBase[i, j] = 0;
				}
			sync();
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
			gameBase = io.FileAnalysis(fileLocation);
			reset();
			print.writeDefult(gameBase);
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
					if (gameBase[i, j] != 0)
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
					if (gameBase[i, j] != 0)
					{
						// 문제?
						for (int a = (i / 3) * 3; a < ((i / 3) * 3) + 3; a++)
						{
							for (int b = (j / 3) * 3; b < ((j / 3) * 3) + 3; b++)
							{
								if (hint[gameBase[i, j] - 1, a, b])
								{
									hint[gameBase[i, j] - 1, a, b] = false;
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
					if (gameBase[i, j] != 0)
						for (int k = 0; k < 9; k++)
						{
							if(hint[gameBase[i, j] - 1, i, k])
							{
								hint[gameBase[i, j] - 1, i, k] = false;
								hintCount[i, k]--;
							}
							if(hint[gameBase[i, j] - 1, k, j])
							{
								hint[gameBase[i, j] - 1, k, j] = false;
								hintCount[k, j]--;
							}
						}

			if(hintStart)
			{
				Console.WriteLine("힌트 초기화 됨\n");
				print.writeTotalDefult(gameBase, hint);
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
								gameState[i, j] = k + 1;
								int[] target = { i, j };
								sync();
								print.writeTargetTotal(count, "힌트 한개만 가지고 있는 칸 탐색", gameBase, hint, target);
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
							print.writeTargetTotal(count, "상자 안 숨겨진 하나 Hidden Single : " + (k + 1), gameBase, hint, location);
							gameBase[location[0], location[1]] = k + 1;
							reset();
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
							bool foucusX = false;
							bool foucusY = false;
							for (int b = 0; b < 3; b++)
							{
								if (hint[k, (i * 3) + a, (j * 3) + b]) foucusX = true;
								if (hint[k, (i * 3) + b, (j * 3) + a]) foucusY = true;
							}
							if(foucusX)
							{
								if (x == -1) x = a;
								else x = -2;
							}
							if(foucusY)
							{
								if (y == -1) y = a;
								else y = -2;
							}
						}
						// x
						if(x > -1)
						{
							bool available = false;
							for(int a = 0; a < 9; a++)
							{
								if (a / 3 != j && hint[k, (i * 3) + x, a])
								{
									available = true;
								}
							}
							if(available)
							{
								print.intersetionPointing(count, gameBase, hint, k, true, (i * 3) + x);
								for (int a = 0; a < 9; a++)
								{
									if (a / 3 != j && hint[k, (i * 3) + x, a])
									{
										hint[k, (i * 3) + x, a] = false;
										hintCount[(i * 3) + x, a]--;
									}
								}
								return true;
							}
						}
						// y
						if(y > -1)
						{
							bool available = false;
							for (int a = 0; a < 9; a++)
							{
								if (a / 3 != i && hint[k, a, (j * 3) + y])
								{
									available = true;
								}
							}
							if(available)
							{
								print.intersetionPointing(count, gameBase, hint, k, false, (j * 3) + y);
								for (int a = 0; a < 9; a++)
								{
									if (a / 3 != i && hint[k, a, (j * 3) + y])
									{
										hint[k, a, (j * 3) + y] = false;
										hintCount[a, (j * 3) + y]--;
									}
								}
								return true;
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
				if(x > 2)   // x
				{
					for (int j1 = 0; j1 < 8; j1++)
					{
						if (hintCount[i1,j1] == 2)
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
							for(int j2 = j1 + 1; j2 < 9; j2++)
							{
								if (hintCount[i1, j2] == 2)
								{
									int[] temp2 = new int[2];
									int temp2Count = 0;
									for (int a1 = 0; a1 < 9; a1++)
									{
										if(hint[a1, i1, j2])
										{
											temp2[temp2Count] = a1;
											temp2Count++;
										}
									}
									if(temp1[0] == temp2[0] && temp1[1] == temp2[1])    // x 특정
									{
										print.nakedPair(count, gameBase, hint, true, i1, temp1);
										for(int a1 = 0; a1 < 9; a1++)
										{
											if(a1 != j1 && a1 != j2)
											{
												if(hint[temp1[0], i1, a1])
												{
													hint[temp1[0], i1, a1] = false;
												}
												if(hint[temp1[1], i1, a1])
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
				// y
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
					if(gameBase[i,j] == 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// auto
		public void sync() { this.gameBase = this.gameState; }
		public void reset() { this.gameState = this.gameBase; }

		// get
		public int[,] GetGameBase() { return gameBase; }
		public bool[,,] GetHint() { return hint; }
	}
}
