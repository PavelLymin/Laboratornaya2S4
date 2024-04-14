using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Logic
{
    public class Logical
    {
        public string GenerationText { get; set; }
        private List<TextDictionary> text1 { get; set; } = new List<TextDictionary>();
        public string[] GetPhrase()
        {
            string path1 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\1.txt";
            var srcEncoding = Encoding.GetEncoding("windows-1251");
            using (StreamReader reader = new StreamReader(path1, encoding: srcEncoding))
            {
                text1.Add(new TextDictionary { Text = reader.ReadToEnd(), Number = 1 });
            }
            
            string path2 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\2.txt";
            using (StreamReader reader = new StreamReader(path2, encoding: srcEncoding))
            {
                text1.Add(new TextDictionary { Text = reader.ReadToEnd(), Number = 2 });
            }
            
            string path3 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\3.txt";
            using (StreamReader reader = new StreamReader(path3, encoding: srcEncoding))
            {
                text1.Add(new TextDictionary { Text = reader.ReadToEnd(), Number = 3 });
            }
            
            string path4 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\4.txt";
            using (StreamReader reader = new StreamReader(path4, encoding: srcEncoding))
            {
                text1.Add(new TextDictionary { Text = reader.ReadToEnd(), Number = 4 });
            }
            int i = 0;
            string[] words = new string[text1.Count];
            foreach (var word in text1)
                words[i++] = word.Text.Trim();
            return words;
        }

        public string GenerateText(int NumberOfOffers, bool CheckBox, string Register)
        {
            string[] getphrase = GetPhrase();
            if (!CheckBox)
            {
                for (int i = 0; i < NumberOfOffers; i++)
                {
                    for (int j = 0; j < getphrase.Length; j++)
                    {
                        if (j != 3)
                            GenerationText += getphrase[j].Split('\n')[i] + " ";
                        else if (j == 3)
                            GenerationText += getphrase[j].Split('\n')[i] + ". ";
                    }
                }
            }
            else
            {
                Random random = new Random();
                for (int i = 0; i < NumberOfOffers; i++)
                {
                    int item1 = random.Next(0, getphrase.Length);
                    for (int j = item1; j < getphrase.Length; j++)
                    {
                        int item = random.Next(0, getphrase[j].Split('\n').Length);
                        if (j != getphrase.Length - 1)
                            GenerationText += getphrase[j].Split('\n')[item].Trim() + " ";
                        else if (j == getphrase.Length - 1)
                            GenerationText += getphrase[j].Split('\n')[item].Trim() + ". ";
                    }
                }
            }
            if (Register == "Нижний регистр")
                GenerationText = GenerationText.ToLower();
            else if (Register == "Верхний регистр")
                GenerationText = GenerationText.ToUpper();
            else if (Register == "Верхний регистр первой буквы каждого предложения")
            {
                string GanerationText2 = "";
                GenerationText = GenerationText.ToLower();
                GanerationText2 += GenerationText[0].ToString().ToUpper();
                for (int i = 1; i < GenerationText.Length; i++)
                {
                    if (char.IsLetter(GenerationText[i]) && char.IsWhiteSpace(GenerationText[i - 1]) && ".!?".IndexOf(GenerationText[i - 2]) != -1)
                        GanerationText2 += GenerationText[i].ToString().ToUpper();
                    else
                        GanerationText2 += GenerationText[i].ToString();
                }
                GenerationText = GanerationText2;
            }
            return GenerationText;
        }

        public string NumberOfCharacters()
        {
            int Character = 0;
            foreach (char s in GenerationText)
                Character++;
            return $"Общее количесвто символов: {Character}";
        }

        public string NumberOfWords()
        {
            int Word = 0;
            char[] separator = { ' ', '.', '!', ',', '(', ')' };
            foreach (string s in GenerationText.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                Word++;
            return $"Общее количество слов: {Word}";
        }

        public string UniqueWords()
        {
            char[] separator = { ' ', '.', '!', ',', '(', ')' };
            string[] strings = GenerationText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int Word = 0;
            for (int i = 0; i < strings.Length; i++)
            {
                bool proverka = true;
                for (int j = 0; j < strings.Length; j++)
                {
                    if (i != j && (strings[i].ToLower() == strings[j].ToLower())) 
                    {
                        proverka = false;
                        break;
                    }
                }
                if (proverka)
                    Word++;
            }
            return $"Общее количество уникальных слов: {Word}";
        }

        public IOrderedEnumerable<TextDictionary> CommonWords() 
        {
            List<TextDictionary> words = new List<TextDictionary>();
            char[] separator = { ' ', '.', '!', ',', '(', ')' };
            string[] strings = GenerationText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int Word = 0;
            string[] bezpovtora = new string[0];
            int index = 0;
            for (int j = 0; j < strings.Length; j++)
            {
                if (!bezpovtora.Contains(strings[j]))
                {
                    Array.Resize(ref bezpovtora, bezpovtora.Length + 1);
                    bezpovtora[index++] = strings[j];
                }
            }
            for (int i = 0; i < bezpovtora.Length; i++)
            {
                for (int j = 0; j < strings.Length; j++)
                {
                    if (bezpovtora[i].ToLower() == strings[j].ToLower())
                        Word++;
                }
                words.Add(new TextDictionary { Number = Word, Text = strings[i] });
                Word = 0;
            }
            //Сортировка сложных объектов
            var sortedPeople1 = from p in words
                                orderby p.Number descending /*descending(сортировка по убыванию)*/
                                select p;
            return sortedPeople1;
        }

        public void UploadingToAFile(string Text, int Number)
        {
            if (Text != GetPhrase()[Number - 1])
            {
                if (Number == 1)
                {
                    string path1 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\1.txt";
                    System.IO.File.WriteAllText(path1, Text, Encoding.Default);
                }
                if (Number == 2)
                {
                    string path2 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\2.txt";
                    System.IO.File.WriteAllText(path2, Text, Encoding.Default);
                }
                if (Number == 3)
                {
                    string path3 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\3.txt";
                    System.IO.File.WriteAllText(path3, Text, Encoding.Default);
                }
                if (Number == 4)
                {
                    string path4 = @"C:\\Users\\User\\Desktop\\Программирование\\Laboratornaya2S4\\4.txt";
                    System.IO.File.WriteAllText(path4, Text, Encoding.Default);
                }
            }
        }
    }
}
