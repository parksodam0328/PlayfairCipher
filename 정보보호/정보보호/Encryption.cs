using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 정보보호
{
	class Encryption
	{
		public string MultipleCipher(string str1, string str2)
		{
			string real = "";
			char[] alp = new char[26];
			string alpTable = "";
			char[] deduplication = str1.ToCharArray(0, str1.Length);
			List<char> cipherKey = new List<char>(RemoveDuplicates(deduplication));//암호키 입력 후 중복제거

			for (int i = 0; i < 26; i++) //암호키와 알파벳 합친 후 또 다시 중복제거
			{
				alp[i] = Char.ToLower((char)(97 + i));

			}
			cipherKey.AddRange(alp);
			cipherKey = cipherKey.Distinct().ToList();

			str2 = str2.Replace(" ", ""); // 공백 제거
			List<string> plainText = new List<string>();
			char[] chr = str2.ToCharArray(0, str2.Length);
			List<char> addX = new List<char>(chr);
			for (int i = 0; i < str2.Length; i += 2) // 평문을 두 글자씩 분리
			{
				if (str2.Length % 2 == 1)
				{
					plainText.Add(str2.Substring(str2.Length-1, 1));
				}
				plainText.Add(str2.Substring(i, 2));
			}
			for (int i = 0; i < str2.Length; i += 2)
			{
				if (addX[i] == addX[i + 1])
				{
					addX.Insert(i + 1, 'x'); i++;
				}
			}
			if (addX.Count % 2 == 1) // 평문의 길이가 홀수일 경우 마지막에 x를 넣음
			{
				addX.Add('x');
			}
			foreach (char iter in addX)
			{
				real += iter.ToString();
			}
			foreach (char iter in cipherKey)
				alpTable += iter;
			//String res = MultipleEncryption(alpTable, real);
			return real;
		}

		public static char[] RemoveDuplicates(char[] str)
		{ // 암호키 중복 제거 함수
			HashSet<char> set = new HashSet<char>(str);
			char[] result = new char[set.Count];
			set.CopyTo(result);
			return result;
		}

		/*public string MultipleEncryption(string str1, string str2)
		{
			char[] str = str1.ToCharArray(0, str1.Length);
			int row = 5, col = 5;
			char[,] table = new char[row, col];
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					table[i, j] = str[i * col + j];
				}
			}

			//암호키에 i가 포함되어있으면 q와 z를 묶어서, q가 포함되어있으면 i와 j를 묶어5*5 암호판 배열

			//암호문 2글자씩 끊어서 해독하기

			//같은 열에 배치되어있으면

			//같은 행에 배치되어있으면
			string result = "hello";
			return result;
		}*/
	}
}
