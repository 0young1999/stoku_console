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
			// 설정 변수
				// 파일명
			string fileName = "test.txt";

			// 변수
				// 반복 횟수 확인
			long count = 0;
				// 스토쿠
			Stoku stoku = new Stoku(fileName);

			// 콘솔 설정
				// 콘솔 사이즈
			Console.SetWindowSize(100, 42);
				// 콘솔 색상
			Console.ForegroundColor = ConsoleColor.White;

			// 클래스 로드
				// 화면 출력
			ConsoleWrite print = new ConsoleWrite();

			// 공식으로 인한 탐색
			while (true)
			{
				// 힌트 초기화
				stoku.hintScan();

				// 동기화

				// 반복 횟수 추가
				count++;

				// 힌트 한개만 가지는 경우
				if (stoku.onlyOneHintScan(count)) continue;

				// 상자 안 숨겨진 하나 Hidden Single
				if (stoku.boxInHiddenSingle(count)) continue;

				// Intersetion(pointing) 교차로(포인팅)
				if (stoku.intersetingPointing(count)) continue;

				// Intersetion(Claiming) 교차로(클레이밍)

				// Naked Pair 드라난 둘
				if (stoku.nakedPair(count)) continue;

				// 탈출
				break;
			}

			// 종료
			Console.WriteLine("종료됨\n");
			if(stoku.endCheck())
			{
				print.writeDefult(stoku.GetGameBase());
			} else
			{
				print.bigHint(stoku.GetGameBase(), stoku.GetHint());
			}
			Console.ReadKey();
		}
	}
}
