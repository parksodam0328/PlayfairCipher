using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 정보보호
{
	class Encryption
	{
		public string  MultipleCipher(string str1, string str2, bool check)
		{
			str1 = (str1.Replace(" ", "")).ToLower();
			string str = "";
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
			str2 = (str2.Replace(" ", "")).ToLower(); //공백 제거
			char[] deduplication2 = str1.ToCharArray(0, str1.Length);
			List<string> plainText = new List<string>();
			char[] chr = str2.ToCharArray(0, str2.Length);
			List<char> addX = new List<char>(chr);
			foreach (char iter in cipherKey)
			{
				alpTable += iter;
			}
			str += DivideLetter(str2, plainText, addX, alpTable, check);
			Console.Write(str);
			
			foreach (char iter in cipherKey)
				alpTable += iter;

			return str;
		}

		public static char[] RemoveDuplicates(char[] str)
		{ // 암호키 중복 제거 함수
			HashSet<char> set = new HashSet<char>(str);
			char[] result = new char[set.Count];
			set.CopyTo(result);
			return result;
		}

		public static string MultipleEncryption(string str1, string str2, bool check)
		{
			List<char> element = new List<char>(str2.ToCharArray(0, str2.Length)); //알파벳
			List<string> text = new List<string>();
			int row = 5, col = 5;
			char[] str = str1.ToCharArray(0, str1.Length); //두글자씩 자른 평문
			char[,] table = new char[row, col];
			string result = "";
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					table[i, j] = element[i * col + j];
					if (table[i, j] == 'z') // 암호키에 z가 포함되어있으면 q 값 삭제
					{
						element.Remove('q');
					}
					else // 암호키에 q가 포함되어있으면 z 값 삭제
					{
						element.Remove('z');
					}
				}
			}
			int frow = 0, fcol = 0, srow = 0, scol = 0;
			for (int i = 0; i < row; i++) //평문 암호화
			{
				for (int j = 0; j < col; j++)
				{
					if (str[0] == table[i, j]) //첫번째 글자
					{
						frow = i;
						fcol = j;
					}
					else if (str[1] == table[i, j]) //두번째 글자
					{
						srow = i;
						scol = j;
					}
				}
			}
			if (frow == srow) //같은 행에 배치되어있으면
			{
				if (check == false)
				{
					fcol = (fcol + 1) % 5;
					scol = (scol + 1) % 5;
					result += table[frow, fcol].ToString() + table[srow, scol].ToString();
				}
				else // 인덱스 값이 음수가 되면
				{
					fcol = ((fcol - 1) % 5)== -1 ? 4 : (fcol - 1);
					scol = ((scol - 1) % 5)== -1 ? 4 : (scol - 1);
					result += table[frow, fcol].ToString() + table[srow, scol].ToString();
				}
			}
			else if (fcol == scol) //같은 열에 배치되어있으면
			{
				if (check == false)
				{
					frow = (frow +1)% 5;
					srow = (srow +1)% 5;
					
					result += table[frow, fcol].ToString() + table[srow, scol].ToString(); 
				}
				else
				{
					frow = ((frow - 1) % 5) == -1 ? 4 : (frow - 1); // 인덱스 값이 음수가 되면
					srow = ((srow - 1) % 5) == -1 ? 4 : (srow - 1);
					result += table[frow, fcol].ToString() + table[srow, scol].ToString();
				}
			}
			else //같은 열, 행에 있지 않을 경우
			{
				result += table[srow, fcol].ToString() + table[frow, scol].ToString();
			}
			return result;
		}
		public static string DivideLetter(string str2, List<string> list1, List<char> list2, string alp, bool check)
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
				
				if (list2[i] == list2[i + 1])
				{
					list2.Insert(i + 1, 'x');
				}
			}
			if (list2.Count % 2 != 0) // 평문의 길이가 홀수일 경우 마지막에 x를 추가
			{
				list2.Add('x');
			}
			int cnt = 1;
			string res = "";
			foreach (char iter in list2)
			{
				str += iter.ToString();
				if (cnt % 2 == 0)
				{
					res += MultipleEncryption(str, alp, check);
					str = "";
				}
				cnt++;
			}
			return res;
		}
		public string Decryption(string str1, string str2, bool check)
		{
			string decryption = MultipleCipher(str1, str2, check);
			decryption = decryption.Replace("x", "");
			return decryption;
		}
	}
}

