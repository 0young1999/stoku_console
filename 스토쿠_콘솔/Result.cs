using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스토쿠_콘솔
{
	class Result
	{
		// 게임 상태
		private int[,] game = null;
		// 힌트 상태
		private bool[,,] hint = null;
		// 힌트 색깔
		// 0:White	기본
		// 1:Green	선택된 숫자와 같은 숫자
		// 2:Red	사라질 힌트
		// 3:Blue	주목할 힌트
		private int[,,] hintColor = null;
		// 공식 이름
		private string content = "";

		// get
		public int GetGameDetail(int x, int y) { return game[x, y]; }
		public bool GetHintDetail(int num, int x, int y) { return hint[num, x, y]; }
		public int GetHintColorDetail(int num, int x, int y) { return hintColor[num, x, y]; }
		public string GetContent() { return content; }

		// 생성자
		private Result() { }
		public Result(int [,] game, string content)
		{
			this.game = (int[,])game.Clone();
			hint = new bool[9, 9, 9];
			for (int i = 0; i < 9; i++) for (int j = 0; j < 9; j++) for (int k = 0; k < 9; k++) hint[i, j, k] = false;
			this.content = content;
		}
		public Result(int[,] game, bool[,,] hint, string content)
		{
			this.game = (int[,])game.Clone();
			this.hint = (bool[,,])hint.Clone();
			this.content = content;

			hintColor = new int[9, 9, 9];
			for (int i = 0; i < 9; i++) for (int j = 0; j < 9; j++) for (int k = 0; k < 9; k++) hintColor[i, j, k] = 0;
		}

		/**색입히기**/
		// 숫자 한개만
		public void targetOne(int num, int[] location)
		{
			for(int x = 0; x < 9; x++)	// x 좌표
			{
				for (int y = 0; y < 9; y++) // y 좌표
				{
					if (game[x, y] == 0)    // 셀에 숫자 확정 됬는지 확인
					{
						if (x == location[0] && y == location[1])
						{
							hintColor[num, x, y] = 3;
						}
						else if (x == location[0] || y == location[1])
						{
							hintColor[num, x, y] = 2;
						}
						else
						{
							hintColor[num, x, y] = 1;
						}
					}
				}
			}
		}
		// Intersetion
		public void intersetion(int num, int locationBox, int line, bool lineDir)
		{
			for(int x = 0; x < 9; x++)	// x 좌표
			{
				for(int y = 0; y < 9; y++)	// y 좌표
				{
					if(game[x,y] == 0)  // 셀에 숫자 확정 됬는지 확인
					{
						if((lineDir && locationBox == (y / 3) && x / 3 == line / 3) || (!lineDir && locationBox == (x / 3) && y / 3 == line / 3))
						{
							if((lineDir && line == x) || (!lineDir && line == y))
							{
								hintColor[num, x, y] = 3;
							}
							else
							{
								hintColor[num, x, y] = 2;
							}
						}
						else
						{
							hintColor[num, x, y] = 1;
						}
					}
				}
			}
		}
		// Naked Pair 드러난 둘
		public void nakedPair(int[] num, int line, bool lineDir, int[] location)
		{
			for (int x = 0; x < 9; x++) // x 좌표
			{
				for (int y = 0; y < 9; y++) // y 좌표
				{
					if (game[x, y] == 0)  // 셀에 숫자 확정 됬는지 확인
					{
						if ((lineDir && line == x) || (!lineDir && line == y))
						{
							if ((lineDir && (location[0] == y || location[1] == y)) || (!lineDir && (location[0] == x || location[1] == x)))
							{
								hintColor[num[0], x, y] = 3;
								hintColor[num[1], x, y] = 3;
							}
							else
							{
								hintColor[num[0], x, y] = 2;
								hintColor[num[1], x, y] = 2;
							}
						}
						else
						{
							hintColor[num[0], x, y] = 1;
							hintColor[num[1], x, y] = 1;
						}
					}
				}
			}
		}
		// Naked Triple 드러난 셋
		public void nakedTriple(int[] num, int line, bool lineDir, int[] location)
		{
			for (int x = 0; x < 9; x++) // x 좌표
			{
				for (int y = 0; y < 9; y++) // y 좌표
				{
					if (game[x, y] == 0)  // 셀에 숫자 확정 됬는지 확인
					{
						if((lineDir && line == x) || (!lineDir && line == y))
						{
							if((lineDir && (location[0] == y || location[1] == y || location[2] == y)) || (!lineDir && (location[0] == x || location[1] == x || location[2] == x)))
							{
								hintColor[num[0], x, y] = 3;
								hintColor[num[1], x, y] = 3;
								hintColor[num[2], x, y] = 3;
							}
							else
							{
								hintColor[num[0], x, y] = 2;
								hintColor[num[1], x, y] = 2;
								hintColor[num[2], x, y] = 2;
							}
						}
						else
						{
							hintColor[num[0], x, y] = 1;
							hintColor[num[1], x, y] = 1;
							hintColor[num[2], x, y] = 1;
						}
					}
				}
			}
		}
	}
}
