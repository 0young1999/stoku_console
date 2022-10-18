using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 스토쿠_콘솔
{
	class FileIo
	{
		// 파일 전체 불러오기
		public string FileFullLoad(string fileName)
		{
			StreamReader sr = new StreamReader(fileName);

			string result = "";
			result = sr.ReadToEnd();

			return result;
		}

		// 파일 분석
		public int[,] FileAnalysis(string fileName)
		{
			int[,] result = new int[9,9];

			string temp = FileFullLoad(fileName);

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
