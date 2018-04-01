using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GetWebDataByHttpWebRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest httpReq;
            HttpWebResponse httpResp;

            string strBuff = "";
            char[] cbuffer = new char[256];
            int byteRead = 0;

            ///定义写入流操作

            Uri httpURL = new Uri("http://www.baidu.com");
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换
            httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换

            httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容

            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理
            Stream respStream = httpResp.GetResponseStream();

            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);

            byteRead = respStreamReader.Read(cbuffer, 0, 256);
            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                strBuff = strBuff + strResp;
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }
            respStream.Close();
            System.Console.WriteLine(strBuff);
            System.Console.ReadKey();
        }
    }
}
