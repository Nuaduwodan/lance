using LanceServer.Core.Workspace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest;

[TestClass]
public class UtilTest
{
    [TestMethod]
    public void EmptyData()
    {
        var expectedResult = new Document("c:/Users/YBigler/workspace/cameleon/Nc/WKS.DIR/Cmp.wpd/Auto.wpd/AutoPreCutQ.tpl");
        
        var actualResult = Util.CheckDoc(new Uri(Uri.UnescapeDataString("file:///c%3A/Users/YBigler/workspace/cameleon/Nc/WKS.DIR/Cmp.wpd/Auto.wpd/AutoPreCutQ.tpl")));

        Assert.AreEqual(expectedResult.FullPath, actualResult.FullPath);
    }
}