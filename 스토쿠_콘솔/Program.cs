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
				// 출력 여부
			bool printState = false;

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
				// 힌트 표시 여부
			bool hintStart = true;

			// 공식으로 인한 탐색
			while (true)
			{
				// 반복 횟수 추가
				count++;

				// result
				Result result = null;

				// 힌트 초기화
				if(hintStart)
				{
					result = stoku.hintScan();
					print.print(result);
					hintStart = false;
					continue;
				}
				else
				{
					stoku.hintScan();
				}

				// 힌트 한개만 가지는 경우

				result = stoku.onlyOneHintScan(count);
				if(result != null)
				{
					if(printState) print.print(result);
					continue;
				}

				// 상자 안 숨겨진 하나 Hidden Single
				result = stoku.boxInHiddenSingle(count);
				if (result != null)
				{
					if (printState) print.print(result);
					continue;
				}

				// Intersetion(pointing) 교차로(포인팅)
				result = stoku.intersetingPointing(count);
				if (result != null)
				{
					if (printState) print.print(result);
					continue;
				}

				// Intersetion(Claiming) 교차로(클레이밍)
				result = stoku.intersetionClaiming(count);
				if (result != null)
				{
					if (printState) print.print(result);
					continue;
				}

				// Naked Pair 드라난 둘
				result = stoku.nakedPair(count);
				if (result != null)
				{
					if (printState) print.print(result);
					continue;
				}

				// Naked Triple 드러난 셋
				result = stoku.nakedTriple(count);
				if (result != null)
				{
					if (printState) print.print(result);
					continue;
				}

				// Hidden Pair 숨겨진 둘

				// 탈출
				break;
			}

			// 종료
			if(stoku.endCheck())
			{
				Console.WriteLine("정상 종료됨\n");
				print.bigHintDefult(stoku.GetGame(), stoku.GetHint());
			} else
			{
				Console.WriteLine("답을 다 찾지 못함!");
				print.bigHintEndCheck(stoku.GetGame(), stoku.GetHint());
			}
			Console.ReadKey();
		}
	}
}
