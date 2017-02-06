using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PlatinumTravel.Models
{
    public class logReport
    {

        private string logText;

        public logReport(string text)
        {
            logText = text;
        }

        public string[] ReadFileByStrings(string logPath)
        {
            return File.ReadAllLines(logPath);
        }

        public string[] ReadTextByStrings(string Text)
        {
            return Text.Split(new string[] { "[START]" }, StringSplitOptions.None);
        }

        public string[] ReadTextByStrings()
        {
            return this.logText.Split(new string[] { "[START]" }, StringSplitOptions.None);
        }

        public string[] ByLevelReports(string minLevel)
        {
            List<string> result = new List<string>();

            foreach(string str in this.ReadTextByStrings())
            {
                if (str.Contains(minLevel))
                {
                    result.Add(str);
                }
            }

            return result.ToArray();
        }
    }
 }
