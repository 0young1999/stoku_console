using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 스토쿠_콘솔
{
	class TxtReader
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
				return null;
			}

			string result = "";
			result = sr.ReadToEnd();

			return result;
		}

		// 파일 분석
		public int[,] FileAnalysis(string fileName)
		{
			int[,] result = new int[9,9];

			string temp = FileFullLoad(fileName);

			if (temp == null) return null;

			string[] tempS = temp.Split('\n');

			if (tempS.Length != 9) return null;

			for(int i = 0; i < 9; i++)
			{
				if (tempS[i].Trim().Split(' ').Length != 9) return null;
			}

			for(int i = 0; i < 9; i++)
			{
				string[] tempSS = tempS[i].Trim().Split(' ');
				for(int j = 0; j < 9; j++)
				{
					result[i, j] = int.Parse(tempSS[j]);
				}
			}

			return result;
		}
	}
}
