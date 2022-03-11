using Microsoft.AspNetCore.Mvc;
using PTC1002.Dtos;
using PTC1002.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC1002.Services.Repositories
{
    public class NumberGeneratorRepository : INumberGeneratorRepository
    {
        public RandomNumberDto GenerateRandomNumber(string fileSize)
        {
            RandomNumberDto randomNumberDto = new RandomNumberDto();

            if (fileSize != null && fileSize != "undefined" && fileSize != "0")
            {
                FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt"));
                if (Convert.ToInt64(fileSize) * 1024 <= fi.Length)
                {
                    randomNumberDto.NumCounter = 0;
                    randomNumberDto.AlphaCounter = 0;
                    randomNumberDto.FloatCounter = 0;
                    randomNumberDto.FileSize = fi.Length;
                    return randomNumberDto;
                }
            }
            Random random = new Random();
            int select;
            select = random.Next(1, 4);
            if (select == 1)
            {
                using (StreamWriter outputFile = File.AppendText(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt")))
                {
                    //outputFile.Write(string.Format("{0} - numeric,", RandomInteger()));
                    outputFile.Write(string.Format("{0},", RandomInteger()));

                }
                FileInfo fiNew = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt"));
                randomNumberDto.NumCounter = 1;
                randomNumberDto.AlphaCounter = 0;
                randomNumberDto.FloatCounter = 0;
                randomNumberDto.FileSize = fiNew.Length;
                return randomNumberDto;
            }
            else if (select == 2)
            {
                using (StreamWriter outputFile = File.AppendText(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt")))
                {
                    //outputFile.Write(string.Format("{0} - float,", RandomFloat()));
                    outputFile.Write(string.Format("{0},", RandomFloat()));

                }
                FileInfo fiNew = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt"));
                randomNumberDto.NumCounter = 0;
                randomNumberDto.AlphaCounter = 0;
                randomNumberDto.FloatCounter = 1;
                randomNumberDto.FileSize = fiNew.Length;
                return randomNumberDto;
            }
            else
            {
                using (StreamWriter outputFile = File.AppendText(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt")))
                {
                    //outputFile.Write(string.Format("{0} - alphaNumeric,", RandomAlphaNumeric()));
                    outputFile.Write(string.Format("{0},", RandomAlphaNumeric()));

                }
                FileInfo fiNew = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt"));
                randomNumberDto.NumCounter = 0;
                randomNumberDto.AlphaCounter = 1;
                randomNumberDto.FloatCounter = 0;
                randomNumberDto.FileSize = fiNew.Length;
                return randomNumberDto;
            }

        }
        private string RandomInteger()
        {
            Random random = new Random();
            return random.Next().ToString().ToLower();
        }  
       
        private string RandomFloat()
        {
            Random random = new Random();
            return (float.MaxValue * 2.0 * (random.NextDouble() - 0.5)).ToString();
        }
        private string RandomSpace()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char c;
            for (int i = 0; i <= random.Next(10); i++)
            {
                c = ' ';
                builder.Append(c);
            }
            return builder.ToString();
        }
        private string RandomAlphaNumeric()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string output = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            return RandomSpace() + output.ToString().ToLower() + RandomSpace();
        }
        private long GetFileSizeString()
        {
            FileInfo info = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt"));
            long size = info.Length;
            return size;
        }
        public ReportRandomNumberDto ViewRandomNumberReport()
        {
            ReportRandomNumberDto randomNumberDto = new ReportRandomNumberDto();


            List<string> allVal = new List<string>();
            var input = System.IO.File.ReadAllText("MyTextFile.txt");
            char[] inputString = input.ToCharArray();
            string getSt;
            int start = 0, i = 0;
            for (i = 0; i < input.Length; i++)
            {
                if (input[i] == ',')
                {
                    getSt = input.Substring(start, i - start);
                    start = i + 1;
                    allVal.Add(getSt + CatagoryOf(getSt));
                }
            }
            var numericVal = allVal.Where(stringToCheck => stringToCheck.Contains(" - numeric")).ToList();

            randomNumberDto.NumericPerct = ((float)numericVal.Count() / (float)allVal.Count()) * 100;

            var floatVal = allVal.Where(stringToCheck => stringToCheck.Contains(" - float")).ToList();
            randomNumberDto.FloatPerct = ((float)floatVal.Count() / (float)allVal.Count()) * 100;

            var alphaVal = allVal.Where(stringToCheck => stringToCheck.Contains(" - alphaNumeric")).ToList();
            randomNumberDto.AlphaNumericPerct = ((float)alphaVal.Count() / (float)allVal.Count()) * 100;

            randomNumberDto.FirstTwentyVal = allVal.Take(20).ToList();
            return randomNumberDto;
        }
        public string CatagoryOf(string input)
        {
            if (input.IndexOf(' ') == 0)
            {
                return " - alphaNumeric";
            }
            //else if (input.IndexOf('.') >= 0)
            //{
            //    return " - real numbers";
            //}
            else
            {
                if (IsInteger(input))
                {
                    return " - numeric";
                }
                else
                {
                    return " - float";
                }
            }
        }
        private bool IsInteger(string input)
        {
            int counter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '0' || input[i] == '1' || input[i] == '2' || input[i] == '3' || input[i] == '4' || input[i] == '5' || input[i] == '6' || input[i] == '7' || input[i] == '8' || input[i] == '9')
                {
                    counter++;
                }
            }
            if (counter < input.Length)
            {
                return false;
            }
            else
                return true;
        }
        public void EmptyTextFile()
        {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "MyTextFile.txt"), String.Empty);
        }

    }
}
