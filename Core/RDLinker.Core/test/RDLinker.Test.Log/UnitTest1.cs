using RDLinker.Log;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace RDLinker.Test.Log
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int len = 20000;
            try
            {
                //var ass1 = Assembly.GetCallingAssembly();
                //var ass2 = Assembly.GetExecutingAssembly();
                //var ass3 = Assembly.GetEntryAssembly();
                Parallel.For(0, len, (i) =>
                {
                    LogHelper.Info($"i from rdlinker: {i}");
                });

                throw new Exception("i from rdlinker test log");
            }
            catch (Exception ex)
            {
                Parallel.For(0, len, (i) =>
                {
                    LogHelper.Error(ex);
                });
                throw;
            }
        }
    }
}
