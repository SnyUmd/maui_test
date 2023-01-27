
using System.Text.RegularExpressions;

namespace Hct101
{
    public class TextCtrl
    {
        //**********************************************************************************
        /// <summary>
        /// テキストから数字を抽出
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// 

        public int extractNum_int(string val)
        {
            return int.Parse(Regex.Replace(val, @"[^0-9]", ""));
        }

        public string extractNum_string(string val)
        {
            return Regex.Replace(val, @"[^!-~]", "");
        }
    }
}
