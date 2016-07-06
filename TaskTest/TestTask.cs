using System;
using System.IO;
using System.Text;
using TaskCore;

namespace TaskTest
{
    [Serializable]
    public class TestTask : BaseTask
    {
        public override bool Run(out string resultMessage)
        {
            resultMessage = null;

            using (var fs = new FileStream(string.Format("D:\\{0}.txt",DateTime.Now.ToString("yyyyMMdd")), FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                }
            }

            return true;
        }
    }
}
