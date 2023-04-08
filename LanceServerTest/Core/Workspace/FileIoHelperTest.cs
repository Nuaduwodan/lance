using LanceServer.Core.Workspace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.Core.Workspace
{
    [TestClass]
    public class FileIoHelperTest
    {
    
        [TestMethod]
        public void ReadDocument()
        {
            var path = "c:/Users/YBigler/workspace/lance/LanceServerTest/testfile##test##test##.spf";
            var expectedContent = "testfile";
            Uri uri = new Uri(path);
        
            var actualResult = FileIoHelper.ReadContent(uri);

            Assert.AreEqual(expectedContent, actualResult);
        }
        
        [TestMethod]
        public void UriTest()
        {
            var path1 = "c:/Users/YBigler/workspace/lance/LanceServerTest/testfile##test##test##1.spf";
            var path2 = "c:/Users/YBigler/workspace/lance/LanceServerTest/testfile##test##test##2.spf";
            var escapedPath1 = path1.Replace("#", "%23");
            var escapedPath2 = path2.Replace("#", "%23");
            
            Uri uri1 = new Uri("file:///" + escapedPath1);
            Uri uri2 = new Uri("file:///" + escapedPath2);

            Assert.AreNotEqual(uri1, uri2);
            Assert.AreNotEqual(uri1.GetHashCode(), uri2.GetHashCode());
            Assert.AreNotEqual(uri1.LocalPath, uri2.LocalPath);
        }
    }
}
