﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RDLinker.Utils
{
    public enum SequentialGuidType
    {
        SequentialAsString,
        SequentialAsBinary,
        SequentialAtEnd
    }

    public static class GuidUtil
    {
        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        /// <summary>
        /// 生成序列的Guid
        /// </summary>
        /// <param name="guidType">生成类型</param>
        /// <returns></returns>
        public static Guid NewSequentialGuid(SequentialGuidType guidType)
        {
            byte[] randomBytes = new byte[10];
            _rng.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to reverse the order
                    // of the Data1 and Data2 blocks on little-endian systems.
                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }

        /// <summary>
        /// 生成系统无序的Guid
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}