﻿using System;
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

			// 변수
				// 반복 횟수 확인
			long count = 0;
				// 스토쿠
			Stoku stoku = new Stoku(fileName);

			// 클래스 로드
				// 화면 출력
			ConsoleWrite print = new ConsoleWrite();

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
			}
			Console.ReadKey();
		}
	}
}
