using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Workflow.Platform.Common.Helper
{
    public static class HttpHelper
    {
        /// <summary>
        /// POST数据
        /// </summary>
        /// <param name="posturl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string PostData(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            //HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "post";
                //request.a
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    //直到request.GetResponse()程序才开始向目标网页发送Post请求
                    instream = response.GetResponseStream();
                    sr = new StreamReader(instream, encoding);
                    //返回结果网页（html）代码
                    string content = sr.ReadToEnd();
                    string err = string.Empty;
                    return content;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Console.Write(err);
                return string.Empty;
            }
        }

        public static string PostRequest(string url, string[] paramName, string[] paramValue)
        {
            // 编辑并Encoding提交的数据
            StringBuilder sbuilder = new StringBuilder(paramName[0] + "=" + paramValue[0]);
            for (int i = 1; i < paramName.Length; i++)
                sbuilder.Append("&" + paramName[i] + "=" + paramValue[i]);
            byte[] data = new ASCIIEncoding().GetBytes(sbuilder.ToString());

            // 发送请求
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            // 获得回复
            using (HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                reader.Close();

                return result;
            }
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="getUrl"></param>
        /// <returns></returns>
        public static string GetRequestData(string getUrl)
        {
            //return string.Empty;
            try
            {
                HttpWebRequest request = WebRequest.Create(getUrl) as HttpWebRequest;
                if (request == null)
                {
                    return string.Empty;
                }

                request.Method = "GET";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Stream instream = null;
                    StreamReader sr = null;
                    //直到request.GetResponse()程序才开始向目标网页发送Post请求
                    instream = response.GetResponseStream();
                    sr = new StreamReader(instream, Encoding.UTF8);
                    //返回结果网页（html）代码
                    string content = sr.ReadToEnd();
                    string err = string.Empty;
                    return content;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Console.Write(err);
                return string.Empty;
            }
        }
    }
}
