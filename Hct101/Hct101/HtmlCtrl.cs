using Microsoft.Maui.Controls;
using System;
using System.Net;

namespace Hct101
{
    public class HtmlCtrl
    {
        public string getHtml(WebClient web_client, string str_html)
        {

            string resultStr = "";
            //for (int i = 0; i < 5; i++)
            //{
                try
                {
                    resultStr = web_client.DownloadString(str_html);
                    //break;
                }
                catch (WebException exc)
                {
                    //continue;
                }
            //}

            
            return resultStr;
        }
    }
}
