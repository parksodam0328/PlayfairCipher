using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

		static void Main(string[] args)
		{
			string str1 = Console.ReadLine();
			string str = "";
			char[] alp = new char[26];
			string alpTable = "";
			char[] deduplication = str1.ToCharArray(0, str1.Length);
			List<char> cipherKey = new List<char>(RemoveDuplicates(deduplication));//암호키 입력 후 중복제거
			string key = new string(RemoveDuplicates(deduplication));
			for (int i = 0; i < 26; i++) //암호키와 알파벳 합친 후 또 다시 중복제거
			{
				alp[i] = Char.ToLower((char)(97 + i));
			}
			cipherKey.AddRange(alp);
			cipherKey = cipherKey.Distinct().ToList();
			string str2 = Console.ReadLine(); //평문 입력
			str2 = str2.Replace(" ", ""); //공백 제거
			List<string> plainText = new List<string>();
			char[] chr = str2.ToCharArray(0, str2.Length);
			List<char> addX = new List<char>(chr);

			str = DivideLetter(str2, plainText, addX);
			
			foreach (char iter in cipherKey)
				alpTable += iter;
			string res = MultipleEncryption(alpTable, str);
			Console.WriteLine(res);
		}

        public static char[] RemoveDuplicates(char[] str) { // 암호키 중복 제거 함수
            HashSet<char> set = new HashSet<char>(str);
            char[] result = new char[set.Count];
            set.CopyTo(result);
            return result;
        }

		public static string MultipleEncryption(string str1, string str2)
		{
			List<char> element = new List<char>(str1.ToCharArray(0, str1.Length));
			List<string> text = new List<string>();
			int row = 5, col = 5;
			char[] str = str2.ToCharArray(0, str2.Length);
			char[,] table = new char[row, col];
			string result = "";
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{	
					table[i, j] = element[i * col + j];
					if (table[i, j] == 'z') // 암호키에 z가 포함되어있으면 q 값 삭제
					{
						//Console.Write(table[i, j]);
						element.Remove('q');
					}
					if (table[i, j] == 'q') // 암호키에 q가 포함되어있으면 z 값 삭제
					{
						element.Remove('z');
					}
				}
			}
			for (int i = 0; i < str2.Length; i += 2) //평문을 두 글자씩 분리
			{
				text.Add(str2.Substring(i, 2));
			}
			int frow=0, fcol=0, srow=0, scol=0;
			//평문 암호화(같은 열, 행에 있지 않을 경우)
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					if (str[i * col + j] == table[i,j]) //첫번째 글자
					{
						frow = i;
						fcol = j;
						result += table[frow, fcol].ToString();
						
					}
					else if (str[i * col + j ] == table[i,j]) //두번째 글자
					{
						srow = i;
						scol = j;
						result += table[srow, scol].ToString();
					}
				}
			}
			if (frow == srow) //같은 행에 배치되어있으면
			{
				fcol = (fcol + 1) % 5;
				scol = (scol + 1) % 5;
				result += table[fcol, scol].ToString();
			}
			else if (fcol == scol) //같은 열에 배치되어있으면
			{
				frow = (frow + 1) % 5;
				srow = (srow + 1) % 5;
				result += table[frow, srow].ToString();
			}
			return result;
		}

		public static string DivideLetter(string str2, List<string> list1, List<char> list2)
		{
			string str = "";
			for (int i = 0; i < str2.Length; i += 2) //평문을 두 글자씩 분리
			{
				if (str2.Length % 2 != 0)
				{
					if (str2.Length - 1 == i)
					{
						list1.Add(str2.Substring(i, 1));
					}
					else
						list1.Add(str2.Substring(i, 2));
				}
				else
				{
					list1.Add(str2.Substring(i, 2));
				}
			}

			for (int i = 0; i < str2.Length; i += 2)
			{
				if (str2.Length % 2 != 0) // 평문의 길이가 홀수일 경우 마지막에 x를 추가
				{
					if (str2.Length - 1 == i)
					{
						list2.Add('x');
					}
				}
				if (list2[i] == list2[i + 1])
				{
					list2.Insert(i + 1, 'x');
				}
			}
			foreach (char iter in list2)
			{
				str += iter.ToString();
			}
			return str;

		}
	}
}
