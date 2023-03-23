using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.Parser;

[TestClass]
public class SinumerikNCLexerTest
{
    [TestMethod]
    public void EmptyString()
    {
        // Arrange
        int expectedTokenCount = 1;

        String code = "";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        Assert.AreEqual(expectedTokenCount, actualTokenList.Count);
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[0].Type);
    }
    
    [TestMethod]
    public void Whitespace()
    {
        // Arrange
        int expectedTokenCount = 1;

        String code = "     ";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        Assert.AreEqual(expectedTokenCount, actualTokenList.Count);
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[0].Type);
    }
    
    [TestMethod]
    public void Procedure()
    {
        // Arrange
        int expectedTokenCount = 6;

        String code = "proc procedure()\n\nendproc";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        Assert.AreEqual(expectedTokenCount, actualTokenList.Count);
        Assert.AreEqual(SinumerikNCLexer.PROC, actualTokenList[0].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[1].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[2].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[3].Type);
        Assert.AreEqual(SinumerikNCLexer.PROC_END, actualTokenList[4].Type);
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[5].Type);
    }
    
    [TestMethod]
    public void SimpleProcedure()
    {
        // Arrange
        int expectedTokenCount = 6;

        var code = 
            @"proc testProcedure(int testparam)

            define definedMacro as 42
            def int declaredVariable
            def real definedVariable = 2.3

            if (definedMacro > definedVariable) or (testparam < 0)
                declaredVariable = 7
            endif

            ret
            endproc";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        var tokenPosition = 0;
        Assert.AreEqual(SinumerikNCLexer.PROC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.INT_TYPE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.MACRO_DEFINE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.MACRO_AS, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.INT, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.DEFINE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.INT_TYPE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.DEFINE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL_TYPE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.IF, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.GREATER, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OR, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.LESS, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.INT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.INT, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.IF_END, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.RETURN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.PROC_END, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[tokenPosition++].Type);
    }
    
    [TestMethod]
    public void GCodeProcedure()
    {
        // Arrange
        int expectedTokenCount = 6;

        var code = 
            @"proc GEA01_HOB01_Modification

            G64 fnorm
            fgroup()

            G01 ;start point
            N00001 AxHobAxial=ac(59.432092) AxHobRadial=ac(59.353961) AxHobTangential=ac(0.000000) AxHobRotation=ic(0.00000000) F=224.841675 fnorm ;AxHobRotation=ac(0.04739117)

            G01
            N01003 AxHobAxial=ac(35.500000) AxHobRadial=ac(59.353961) AxHobTangential=ac(0.000000) AxHobRotation=ic(0.00000000) F=224.841675 fnorm ;AxHobRotation=ac(0.04739117)

            aspline bauto eauto
            N02043 AxHobAxial=ac(0.000000) AxHobRadial=ac(59.362450) AxHobTangential=ac(0.000000) AxHobRotation=ic(-0.00236956) F=224.842483 fnorm ;AxHobRotation=ac(-0.04739117)

            G01
            N03044 AxHobAxial=ac(-3.880770) AxHobRadial=ac(59.362450) AxHobTangential=ac(0.000000) AxHobRotation=ic(0.00000000) F=224.841675 fnorm ;AxHobRotation=ac(-0.04739117)

            G01 fnorm

            ret

            endproc
            ";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        var tokenPosition = 0;
        Assert.AreEqual(SinumerikNCLexer.PROC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.GCODE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.FNORM, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.FGROUP, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.GCODE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.BLOCK_NUMBER, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.IC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.F, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.FNORM, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.GCODE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.BLOCK_NUMBER, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.IC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.F, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.FNORM, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.ASPLINE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.BAUTO, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.EAUTO, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.BLOCK_NUMBER, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.IC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.SUB, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.F, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.FNORM, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.GCODE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.BLOCK_NUMBER, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.SUB, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.AC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.NAME, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.IC, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.OPEN_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.CLOSE_PAREN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.F, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.ASSIGNMENT, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.REAL, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.FNORM, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.GCODE, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.FNORM, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.RETURN, actualTokenList[tokenPosition++].Type);
        Assert.AreEqual(SinumerikNCLexer.PROC_END, actualTokenList[tokenPosition++].Type);
        
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[tokenPosition++].Type);
    }
}