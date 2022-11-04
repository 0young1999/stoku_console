using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 스토쿠_콘솔
{
	class TxtControll
	{
		// 파일 체크
		public bool FileCheck(string fileName)
		{
			return File.Exists(fileName);
		}

		// 파일 전체 불러오기
		public string FileFullLoad(string fileName)
		{
			StreamReader sr = null;
			try
			{
				sr = new StreamReader(fileName);
			} catch (Exception e)
			{
				throw e;
			}

			string result = "";
			result = sr.ReadToEnd();

			return result;
		}

		// 파일 분석
		public int[,] FileAnalysis(string fileName)
		{
			try
			{
				int[,] result = new int[9, 9];

				string temp = temp = FileFullLoad(fileName);

				if (temp == null) return null;

				string[] tempS = temp.Split('\n');

				if (tempS.Length != 9) return null;

				for (int i = 0; i < 9; i++)
				{
					if (tempS[i].Trim().Split(' ').Length != 9) return null;
				}

				for (int i = 0; i < 9; i++)
				{
					string[] tempSS = tempS[i].Trim().Split(' ');
					for (int j = 0; j < 9; j++)
					{
						result[i, j] = int.Parse(tempSS[j]);
					}
				}

				return result;
			} catch(Exception e)
			{
				throw e;
			}
		}

		// 파일 쓰기
		public bool FileWrite(string FileName, string text, bool check)
		{
			if(check)
			{
				if (FileCheck(FileName)) return false;
			}

			try
			{
				File.WriteAllText(FileName, text);
			} 
			catch(Exception e)
			{
				throw e;
			}

			return true;
		}

		// 파일 리스트 불러오기
		public List<string> DirectoryScan()
		{
			List<string> fileList = new List<string>();
			DirectoryInfo dir = new DirectoryInfo(".\\");
			foreach (FileInfo File in dir.GetFiles())
			{
				if (File.Extension.ToLower().CompareTo(".txt") == 0)
				{
					fileList.Add(File.Name);
				}
			}
			return fileList;
		}
	}
}
