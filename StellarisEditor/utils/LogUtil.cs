using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.utils
{
    public class LogUtil
    {
        public static string PATH = Environment.CurrentDirectory + "\\Log.txt";
        public static  string ERROR_PATH = Environment.CurrentDirectory + "\\ErrorLog.txt";
        static LogUtil()
        {
            File.Delete(PATH);
            File.Create(PATH).Close();
            File.Delete(ERROR_PATH);
            File.Create(ERROR_PATH).Close();
        }

        public static void Log(string text)
        {
            File.WriteAllText(PATH, File.ReadAllText(PATH) + text + "\n");
        }

        public static void ErrorLog(Exception ex)
        {
            

            StringBuilder msg = new StringBuilder();
            msg.Append("*************************************** \n");
            msg.AppendFormat(" 异常发生时间： {0} \n", DateTime.Now);
            msg.AppendFormat(" 异常类型： {0} \n", ex.HResult);
            msg.AppendFormat(" 导致当前异常的 Exception 实例： {0} \n", ex.InnerException);
            msg.AppendFormat(" 导致异常的应用程序或对象的名称： {0} \n", ex.Source);
            msg.AppendFormat(" 引发异常的方法： {0} \n", ex.TargetSite);
            msg.AppendFormat(" 异常堆栈信息： {0} \n", ex.StackTrace);
            msg.AppendFormat(" 异常消息： {0} \n", ex.Message);
            msg.Append("***************************************");

            File.WriteAllText(ERROR_PATH, msg.ToString());
        }
    }
}
