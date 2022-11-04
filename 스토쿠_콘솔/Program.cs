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
			string fileName = "Naked Triple.txt";
			//string fileName = "Naked Pair.txt";
			//string fileName = "test.txt";
			//string fileName = "Intersetion(Pointing).txt";
			//string fileName = "test1.txt";
				// 출력 여부
			bool printState = true;

			// file io
			TxtControll io = new TxtControll();


			while(true)
			{
				ConsoleClear();

				// 메인 페이지
				Console.WriteLine();
				Console.WriteLine(" 스도쿠 풀이기\n\n 현재 설정된 파일 이름 : " + fileName);
				Console.WriteLine(" 파일 상태 : " + io.FileCheck(fileName));
				Console.WriteLine();
				if(!io.FileCheck(fileName)) Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(" 0. 계속");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine(" 1. 파일명 변경");
				Console.WriteLine(" 2. 파일 리스트");
				Console.WriteLine(" 3. 파일 추가");
				Console.WriteLine(" 4. 설정");
				Console.WriteLine();
				Console.Write(" 메뉴 선택 : ");
				string menuSelect = Console.ReadLine().Trim();

				if (menuSelect.Equals("0"))	// 문제 해결
				{
					if(io.FileCheck(fileName))
					{
						Resolution(printState, fileName);
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(" 사용불가 파일 존재 하지 않음!");
						Console.ReadKey();
						Console.ForegroundColor = ConsoleColor.White;
					}
				} 
				else if (menuSelect.Equals("1"))	// 파일명 변경
				{
					fileName = FileNameChage(fileName);
				}
				else if (menuSelect.Equals("2"))	// 파일 리스트
				{
					fileName = FileList(fileName);
				}
				else if (menuSelect.Equals("3"))	// 파일 추가
				{
					// 추가필요
				}
				else if(menuSelect.Equals("4"))		// 설정
				{
					// 추가 필요
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(" 입력 오류 : " + menuSelect);
					Console.ReadKey();
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}

		// 파일 리스트
		private static string FileList(string fileName)
		{
			while(true)
			{
				ConsoleClear();

				TxtControll io = new TxtControll();
				List<string> list = io.DirectoryScan();
				int listNum = 0;

				Console.WriteLine();
				Console.WriteLine(" 파일 리스트");
				Console.WriteLine();
				Console.WriteLine(" 0 : 뒤로가기");
				foreach(string scanFileName in list)
				{
					Console.WriteLine(" " + ++listNum + " : " + scanFileName);
				}
				Console.WriteLine();
				Console.Write(" 번호 입력 : ");
				string menuSelect = Console.ReadLine().Trim();
				int menuSelectInt = 0;

				if (menuSelect.Equals(""))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(" 입력이 비었습니다.\n");
					Console.ReadKey();
					Console.ForegroundColor = ConsoleColor.White;
					continue;
				}
				else if (menuSelect.Equals("0"))	// 뒤로 가기
				{
					break;
				}
				else if(int.TryParse(menuSelect, out menuSelectInt))	// 파일 선택
				{
					if(menuSelectInt <= list.Count)
					{
						return list[menuSelectInt - 1];
					}
				}
				else    // 입력 오류
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(" 입력 오류 : " + menuSelect);
					Console.ReadKey();
					Console.ForegroundColor = ConsoleColor.White;
				}
			}

			return fileName;
		}

		// 파일명 변경
		private static string FileNameChage(string nowFileName)
		{
			string fileName = null;

			while(true)
			{
				fileName = "";

				ConsoleClear();

				Console.Write("\n 확장자는 .txt로 고정되어 붙이실 필요 없습니다.\n\n 파일명 : ");
				fileName = Console.ReadLine().Trim();

				// 오류 처리
				if(fileName.Equals(""))
				{
					return nowFileName;
				}
				else if(fileName.Split('.').Length != 1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(" 특수문자 '.'는 허용되지 않습니다.\n");
					Console.ReadKey();
					Console.ForegroundColor = ConsoleColor.White;
					continue;
				}

				// txt 붙이기
				fileName = fileName + ".txt";

				break;
			}

			return fileName;
		}

		// 문제 해결
		private static void Resolution(bool printState, string fileName)
		{
			ConsoleClear();

			// 파일
			TxtControll io = new TxtControll();
			// 스토쿠
			Stoku stoku = null;
			// 화면 출력
			ConsoleWrite print = new ConsoleWrite();
			// 반복 횟수 확인
			long count = 0;

			// 파일 불러오기
			Console.WriteLine(" 파일 내용 확인\n");
			try
			{
				Console.WriteLine(io.FileFullLoad(fileName));
			} catch (Exception e)
			{
				ConsoleClear();
				Console.WriteLine(" 에러 발생 : " + e.Message);
				Console.ReadKey();
				return;
			}
			Console.ReadKey();
			Console.WriteLine();

			// 스토쿠
			int[,] game = null;
			try
			{
				game = io.FileAnalysis(fileName);
			}
			catch (Exception e)
			{
				ConsoleClear();
				Console.WriteLine(" 에러 발생 : " + e.Message);
				Console.ReadKey();
				return;
			}
			if (game == null)
			{
				Console.WriteLine(" 파일오류!\n");
				Console.ReadKey();
				return;
			}
			stoku = new Stoku(game);

			// 공식으로 인한 탐색
			while (true)
			{
				// 반복 횟수 추가
				count++;

				// result
				Result result = null;

				// 힌트 초기화
				if (count == 1)
				{
					result = stoku.hintScan();
					print.print(result);
				}
				else
				{
					stoku.hintScan();
				}

				// 힌트 한개만 가지는 경우

				result = stoku.onlyOneHintScan(count);
				if (result != null)
				{
					if (printState) print.print(result);
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
			if (stoku.endCheck())
			{
				Console.WriteLine("정상 종료됨\n");
				print.bigHintDefult(stoku.GetGame(), stoku.GetHint());
			}
			else
			{
				Console.WriteLine("답을 다 찾지 못함!");
				print.bigHintEndCheck(stoku.GetGame(), stoku.GetHint());
				List<Result> list = stoku.bruteForce();
				if (list.Count() == 0)
				{
					Console.WriteLine("답이 없음");
				}
				else
				{
					foreach (Result item in list)
					{
						print.print(item);
					}
					Console.WriteLine("종료!");
				}
			}
			Console.ReadKey();
		}

		private static void ConsoleClear()
		{
			// 콘솔 설정
				// 콘솔 사이즈
			Console.SetWindowSize(100, 39);
				// 콘솔 색상
			Console.ForegroundColor = ConsoleColor.White;
				// 콘솔 내용 초기화
			Console.Clear();
		}
	}
}
