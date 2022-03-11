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
                    outputFile.Write(string.Format("{0} - numeric,", RandomInteger()));

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
                    outputFile.Write(string.Format("{0} - float,", RandomFloat()));

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
                    outputFile.Write(string.Format("{0} - alphaNumeric,", RandomAlphaNumeric()));

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
        public ReportRandomNumberDto ViewRandomNumberReport()
        {
            ReportRandomNumberDto randomNumberDto = new ReportRandomNumberDto();


            var listVal = System.IO.File.ReadAllText("MyTextFile.txt");
            List<string> allVal = listVal.Split(',').ToList<string>();
            var numericVal = allVal.Where(stringToCheck => stringToCheck.Contains(" - numeric")).ToList();

            randomNumberDto.NumericPerct = ((float)numericVal.Count() / (float)allVal.Count()) * 100;

            var floatVal = allVal.Where(stringToCheck => stringToCheck.Contains(" - float")).ToList();
            randomNumberDto.FloatPerct = ((float)floatVal.Count() / (float)allVal.Count()) * 100;

            var alphaVal = allVal.Where(stringToCheck => stringToCheck.Contains(" - alphaNumeric")).ToList();
            randomNumberDto.AlphaNumericPerct = ((float)alphaVal.Count() / (float)allVal.Count()) * 100;

            randomNumberDto.FirstTwentyVal = allVal.Take(20).ToList();
            return randomNumberDto;



        }
    }
}
