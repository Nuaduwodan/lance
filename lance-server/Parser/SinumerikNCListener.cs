//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ..\..\antlr4-grammar\SinumerikNC.g4 by ANTLR 4.11.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="SinumerikNCParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.CLSCompliant(false)]
public interface ISinumerikNCListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFile([NotNull] SinumerikNCParser.FileContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFile([NotNull] SinumerikNCParser.FileContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] SinumerikNCParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] SinumerikNCParser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.procedure"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProcedure([NotNull] SinumerikNCParser.ProcedureContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.procedure"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProcedure([NotNull] SinumerikNCParser.ProcedureContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParams([NotNull] SinumerikNCParser.ParamsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParams([NotNull] SinumerikNCParser.ParamsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.paramOut"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParamOut([NotNull] SinumerikNCParser.ParamOutContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.paramOut"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParamOut([NotNull] SinumerikNCParser.ParamOutContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParam([NotNull] SinumerikNCParser.ParamContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParam([NotNull] SinumerikNCParser.ParamContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] SinumerikNCParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] SinumerikNCParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatement([NotNull] SinumerikNCParser.IfStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatement([NotNull] SinumerikNCParser.IfStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.iterativeStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIterativeStatement([NotNull] SinumerikNCParser.IterativeStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.iterativeStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIterativeStatement([NotNull] SinumerikNCParser.IterativeStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.jumpStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterJumpStatement([NotNull] SinumerikNCParser.JumpStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.jumpStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitJumpStatement([NotNull] SinumerikNCParser.JumpStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.primaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrimaryExpression([NotNull] SinumerikNCParser.PrimaryExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.primaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrimaryExpression([NotNull] SinumerikNCParser.PrimaryExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.unaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnaryExpression([NotNull] SinumerikNCParser.UnaryExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.unaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnaryExpression([NotNull] SinumerikNCParser.UnaryExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.multiplicativeExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplicativeExpression([NotNull] SinumerikNCParser.MultiplicativeExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.multiplicativeExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplicativeExpression([NotNull] SinumerikNCParser.MultiplicativeExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.additiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdditiveExpression([NotNull] SinumerikNCParser.AdditiveExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.additiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdditiveExpression([NotNull] SinumerikNCParser.AdditiveExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.binaryAndExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryAndExpression([NotNull] SinumerikNCParser.BinaryAndExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.binaryAndExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryAndExpression([NotNull] SinumerikNCParser.BinaryAndExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.binaryExclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryExclusiveOrExpression([NotNull] SinumerikNCParser.BinaryExclusiveOrExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.binaryExclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryExclusiveOrExpression([NotNull] SinumerikNCParser.BinaryExclusiveOrExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.binaryInclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryInclusiveOrExpression([NotNull] SinumerikNCParser.BinaryInclusiveOrExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.binaryInclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryInclusiveOrExpression([NotNull] SinumerikNCParser.BinaryInclusiveOrExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.andExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAndExpression([NotNull] SinumerikNCParser.AndExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.andExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAndExpression([NotNull] SinumerikNCParser.AndExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.exclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExclusiveOrExpression([NotNull] SinumerikNCParser.ExclusiveOrExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.exclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExclusiveOrExpression([NotNull] SinumerikNCParser.ExclusiveOrExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.inclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInclusiveOrExpression([NotNull] SinumerikNCParser.InclusiveOrExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.inclusiveOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInclusiveOrExpression([NotNull] SinumerikNCParser.InclusiveOrExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.stringExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringExpression([NotNull] SinumerikNCParser.StringExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.stringExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringExpression([NotNull] SinumerikNCParser.StringExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.relationalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelationalExpression([NotNull] SinumerikNCParser.RelationalExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.relationalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelationalExpression([NotNull] SinumerikNCParser.RelationalExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] SinumerikNCParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] SinumerikNCParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] SinumerikNCParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] SinumerikNCParser.TypeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstant([NotNull] SinumerikNCParser.ConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstant([NotNull] SinumerikNCParser.ConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_path"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFeedrate_override_path([NotNull] SinumerikNCParser.Feedrate_override_pathContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_path"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFeedrate_override_path([NotNull] SinumerikNCParser.Feedrate_override_pathContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_rapid_traverse_velocity"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFeedrate_override_rapid_traverse_velocity([NotNull] SinumerikNCParser.Feedrate_override_rapid_traverse_velocityContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_rapid_traverse_velocity"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFeedrate_override_rapid_traverse_velocity([NotNull] SinumerikNCParser.Feedrate_override_rapid_traverse_velocityContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_position_or_spindle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFeedrate_override_position_or_spindle([NotNull] SinumerikNCParser.Feedrate_override_position_or_spindleContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_position_or_spindle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFeedrate_override_position_or_spindle([NotNull] SinumerikNCParser.Feedrate_override_position_or_spindleContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.acceleration_compensation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAcceleration_compensation([NotNull] SinumerikNCParser.Acceleration_compensationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.acceleration_compensation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAcceleration_compensation([NotNull] SinumerikNCParser.Acceleration_compensationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_path_handwheel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFeedrate_override_path_handwheel([NotNull] SinumerikNCParser.Feedrate_override_path_handwheelContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_path_handwheel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFeedrate_override_path_handwheel([NotNull] SinumerikNCParser.Feedrate_override_path_handwheelContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_axial_handwheel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFeedrate_override_axial_handwheel([NotNull] SinumerikNCParser.Feedrate_override_axial_handwheelContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.feedrate_override_axial_handwheel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFeedrate_override_axial_handwheel([NotNull] SinumerikNCParser.Feedrate_override_axial_handwheelContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.axis_spindle_identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAxis_spindle_identifier([NotNull] SinumerikNCParser.Axis_spindle_identifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.axis_spindle_identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAxis_spindle_identifier([NotNull] SinumerikNCParser.Axis_spindle_identifierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.axis_identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAxis_identifier([NotNull] SinumerikNCParser.Axis_identifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.axis_identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAxis_identifier([NotNull] SinumerikNCParser.Axis_identifierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.spindle_identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSpindle_identifier([NotNull] SinumerikNCParser.Spindle_identifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.spindle_identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSpindle_identifier([NotNull] SinumerikNCParser.Spindle_identifierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SinumerikNCParser.axis"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAxis([NotNull] SinumerikNCParser.AxisContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SinumerikNCParser.axis"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAxis([NotNull] SinumerikNCParser.AxisContext context);
}
