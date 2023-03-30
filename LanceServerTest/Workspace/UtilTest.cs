using LanceServer.Core.Workspace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest
{
    [TestClass]
    public class UtilTest
    {
    
        [TestMethod]
        public void EmptyData()
        {
            var expectedResult = new Document(new Uri("c:/Users/YBigler/workspace/lance/LanceServerTest/InCycleFpController.spf"));
        
            var actualResult = FileIoHelper.ReadDocument(new Uri(Uri.UnescapeDataString("file:///c%3A/Users/YBigler/workspace/lance/LanceServerTest/InCycleFpController.spf")));

            Assert.AreEqual(expectedResult.Uri, actualResult.Uri);
        }
    }
}
