using System;
using System.Text;

namespace RDLinker.Utils
{
    public class RandomUtil
    {
        //private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        /// <summary>
        /// 英文字母
        /// </summary>
        public const string Letters = "abcdefghijklmnopqrstuvwxyz";
        /// <summary>
        /// 数字
        /// </summary>
        public const string Numbers = "0123456789";

        /// <summary>
        /// 英文字母 + 数字
        /// </summary>
        public const string LetterNumbers = "abcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// 随机生成数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomNumber(int length)
        {
            StringBuilder stringBuilder = new StringBuilder(length);
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.AppendFormat("{0}", random.Next(9));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 随机生成英文 + 数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            StringBuilder stringBuilder = new StringBuilder(length);
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.AppendFormat("{0}", LetterNumbers[random.Next(LetterNumbers.Length)]);
            }
            return stringBuilder.ToString();
        }

    }
}
