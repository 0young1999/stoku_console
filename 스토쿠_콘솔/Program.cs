using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스토쿠_콘솔
{
	class Program
	{
		static void Main(string[] args)
		{
			// 콘솔 설정
				// 콘솔 사이즈
			Console.SetWindowSize(100, 39);
				// 콘솔 색상
			Console.ForegroundColor = ConsoleColor.White;

			// 설정 변수
			// 파일명
			string fileName = "Naked Triple.txt";
			//string fileName = "Naked Pair.txt";
			//string fileName = "test.txt";
			//string fileName = "Intersetion(Pointing).txt";
			//string fileName = "test1.txt";

			// 클래스 로드
			// 화면 출력
			ConsoleWrite print = new ConsoleWrite();
				// 파일
			FileIo io = new FileIo();
				// 스토쿠
			Stoku stoku = null;

			// 파일 불러오기
			Console.WriteLine("파일 내용 확인\n");
			Console.WriteLine(io.FileFullLoad(fileName));
			Console.ReadKey();
			Console.WriteLine();

			// 변수
				// 반복 횟수 확인
			long count = 0;
				// 스토쿠
			int[,] game = io.FileAnalysis(fileName);
			if (game == null) return;
			stoku = new Stoku(game);

			// 공식으로 인한 탐색
			while (true)
			{
				// 힌트 초기화
				stoku.hintScan();

				// 반복 횟수 추가
				count++;

				// 힌트 한개만 가지는 경우
				if (stoku.onlyOneHintScan(count)) continue;

				// 상자 안 숨겨진 하나 Hidden Single
				if (stoku.boxInHiddenSingle(count)) continue;

				// Intersetion(pointing) 교차로(포인팅)
				if (stoku.intersetingPointing(count)) continue;

				// Intersetion(Claiming) 교차로(클레이밍)
				if (stoku.intersetionClaiming(count)) continue;

				// Naked Pair 드라난 둘
				if (stoku.nakedPair(count)) continue;

				// Naked Triple 드러난 셋
				if (stoku.nakedTriple(count)) continue;

				// Hidden Pair 숨겨진 둘

				// 탈출
				break;
			}

			// 종료
			if(stoku.endCheck())
			{
				Console.WriteLine("정상 종료됨\n");
				print.bigHintDefult(stoku.GetGameBase(), stoku.GetHint());
			} else
			{
				Console.WriteLine("답을 다 찾지 못함!");
				print.bigHintEndCheck(stoku.GetGameBase(), stoku.GetHint());

				while (true) {
					Console.WriteLine("num x y");
					string controll1 = Console.ReadLine();
					int num = 0, x = 0, y = 0;
					try
					{
						string[] controll2 = controll1.Trim().Split(' ');
						if (controll2.Length != 3)
						{
							Console.WriteLine("입력 오류 " + controll1);
							continue;
						}
						num = int.Parse(controll2[0].Trim());
						x = int.Parse(controll2[1].Trim());
						y = int.Parse(controll2[2].Trim());
					} catch (Exception e)
					{
						Console.WriteLine(controll1);
						Console.WriteLine(e.Message);
					}

					if (stoku.customMode(num - 1, x - 1, y - 1)) continue;

					break;
				}
			}
			Console.ReadKey();
		}
	}
}
