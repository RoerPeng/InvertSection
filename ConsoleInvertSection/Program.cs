using System;
using System.Collections.Generic;  

namespace ConsoleInvertSection
{
    class Program
    {
        static void Main(string[] args)
        {
            //对于输入的字符串 ，请以句号和逗号为界 ，将字符串中各段中所有单词的顺序倒过来。
            Console.WriteLine("初始文本 ：");

            string section = @"A Dance with Dragons is a longer book than A Feast for Crows, and covers a longer time period. In the latter half of this volume, you will notice certain of the viewpoint characters from A Feast for Crows popping up again. And that means just what you think it means: the narrative has moved past the time frame of Feast, and the two streams have once again rejoined each other.";

            Console.WriteLine(section);

            string  result = InvertSection(section);

            Console.WriteLine("反转结果 ：");

            Console.WriteLine(result);

        }

        //1. 数据信息分析 ，2. 数据分解 ，3. 数据重组 

        //1.数据信息分析 
        //数据的总长度
        //找到所有',''.'所在索引

        //2. 数据分解
        //逐步循环',''.'所在索引池 ，得到单个单句
        //循环单句，使用栈容器 转换 单词

        //3. 数据重组 
        //等待索引池循环结束 ， 完成数据重组

        static string InvertSection(string section)
        {
            int sectionLength = section.Length;

            // ',' 与 '.' 所在位置索引池
            List<int> punctuationIndexList = new List<int>();
            for (int i = 0; i < sectionLength; i++)
            {
                char iChar = section[i];
                if (iChar == ',' || iChar == '.')
                {
                    punctuationIndexList.Add(i);
                }
            }
                
            //用于结果存储的数组
            Char[] resultList = new char[sectionLength];
            //resultList当前准备存入的地方
            int curCharIndex = 0;

            //用于单个单词的转换容器
            Stack<Char> charStack = new Stack<char>();
            
            // 逐步循环',''.'所在索引池 ，得到单个单句 ( 暂时默认段落最后以'.'结束 ，未做过多检测 )
            for (int pIndex = 0; pIndex < punctuationIndexList.Count; pIndex++)
            {
                //当前 准备转存的 ','  '.'所在位置 
                int punctuationIndex = punctuationIndexList[pIndex];

                //前一个 ','  '.'所在位置 
                int lastIndex = pIndex == 0 ? -1 : punctuationIndexList[pIndex - 1];

                //倒序循环当前单句转存入resultList
                for (int cIndex = punctuationIndex-1; cIndex > lastIndex; cIndex--)
                {
                    char tempChar = section[cIndex];

                    if (tempChar == ' ')
                    {
                        while (charStack.Count != 0)
                        {
                            resultList[curCharIndex++] = charStack.Pop();
                        }

                        resultList[curCharIndex++] = tempChar;
                    }
                    else
                    {
                        charStack.Push(tempChar);  
                    }
                }

                //清空charStack缓存
                while (charStack.Count != 0)
                {
                    resultList[curCharIndex++] = charStack.Pop();
                }

                //存入当前 ','  '.'
                resultList[curCharIndex++] = section[punctuationIndex];
            }

            //转换出最终结果
            string result = new string(resultList);

            return result;
        }


    }
}
