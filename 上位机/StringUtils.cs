using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace competition
{
    static class StringUtils
    {
        /// <summary>
        /// 字节数组转16进制字符串：空格分隔
        /// </summary>
        /// <param name="byteDatas"></param>
        /// <returns></returns>
        public static string ToHexStrFromByte(this byte[] byteDatas,int count)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                //Encoding.ASCII.GetString(byteDatas[i]);
                builder.Append(string.Format("{0:X2} ", byteDatas[i])); //dataout
            }

            return builder.ToString().Trim();
        }
        public static string HexStringToBytes(this byte[] byteDatas, int count)
        {
            //if (string.IsNullOrEmpty(hex)) return new byte[0];
            byte data = 0;
            byte dataL = 0;

            byte dataout = 0;
            
            StringBuilder builder = new StringBuilder();
            int len = count;
            byte[] result = new byte[len / 2];

            for (int i = 0; i < len/2; i++)
            {

                if (byteDatas[i*2 + 0] > 0x39) data = (byte)(byteDatas[i * 2 + 0] - 0x37);
                else data = (byte)(byteDatas[i] - 0x30);//if (byteDatas[i] > 0x39) data = byteDatas[i] = 0x37;

                if (byteDatas[i * 2 + 0] > 0x39) data = (byte)(byteDatas[i * 2 + 0] - 0x37);
                else data = (byte)(byteDatas[i * 2 + 0] - 0x30);

                if (byteDatas[i * 2 + 1] > 0x39) dataL = (byte)(byteDatas[i * 2 + 1] - 0x37);
                else dataL = (byte)(byteDatas[i] - 0x30);//if (byteDatas[i] > 0x39) data = byteDatas[i] = 0x37;

                if (byteDatas[i * 2 + 1] > 0x39) dataL = (byte)(byteDatas[i * 2 + 1] - 0x37);
                else dataL = (byte)(byteDatas[i * 2 + 1] - 0x30);

                dataout = (byte)((data << 4) | dataL);
                // result[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                //builder.Append(Convert.ToByte(byteDatas[i].Substring(i, 2), 16));//    string.Format("{0:X2} ", byteDatas[i]));
                builder.AppendFormat("{0:X2}", dataout);
               // builder.Append(string.Format("{0:X2} ", byteDatas[i])); //dataout
            }
            return builder.ToString().Trim();
            //return result;
        }

        public static Bitmap GetJpegImage(string data)
        {
            String[] img = Regex.Split(data, "(?<=\\G.{2})");
            if (img.Length < 100)
            {
                return null;
            }
            Byte[] by = new byte[img.Length];
            for (int i = 0; i < img.Length-1; i++)
            {
                by[i] = Convert.ToByte(img[i].Trim(), 16);
            }
            Bitmap bitmap = new Bitmap(new MemoryStream(by));

            return bitmap;
        }
    }
}
