grammar SinumerikNC;

// it is not possible to use regex flags but the options can be overwritten per rule
// see: https://github.com/antlr/antlr4/blob/master/doc/lexer-rules.md#lexer-rule-options
options { caseInsensitive=true; }

/*
 * Lexer Rules
 */

////
//// keywords
////
// control
WHILE: 'while';
WHILE_END: 'endwhile';
FOR: 'for';
FOR_TO: 'to';
FOR_END: 'endfor';
LOOP: 'loop';
LOOP_END: 'endloop';
REPEAT: 'repeat';
REPEAT_END: 'until';
IF: 'if';
ELSE: 'else';
IF_END: 'endif';
GOTO: 'goto';
GOTO_B: 'gotob';
GOTO_C: 'gotoc';
GOTO_F: 'gotof';
GOTO_S: 'gotos';
LABEL_END: 'endlabel';
SYNC_WHEN: 'when';
SYNC_WHENEVER: 'whenever';
SYNC_DO: 'do';
SYNC_CANCEL: 'cancel';
RETURN: 'ret';
CALL: 'call';
CALL_P: 'pcall';
CALL_EXT: 'extcall';
CALL_PATH: 'callpath';

// operators
ASSIGNMENT: '=';

ADD: '+';
SUB: '-';
MUL: '*';
DIV: '/' | 'div';
MOD: 'mod';

EQUAL: '==';
NOT_EQUAL: '<>';
LESS: '<';
GREATER: '>';
LESS_EQUAL: '<=';
GREATER_EQUAL: '>=';

NOT: 'not';
AND: 'and';
OR: 'or';
XOR: 'xor';

NOT_B: 'b_not';
AND_B: 'b_and';
OR_B: 'b_or';
XOR_B: 'b_xor';

CONCAT: '<<';

// modifier
RANGE_NCK: 'nck';
RANGE_NC: 'nc';
RANGE_CHAN: 'chan';
PRE_PROC_STOP_R: 'syncr';
PRE_PROC_STOP_W: 'syncw';
PRE_PROC_STOP_RW: 'syncrw';
ACCESS_READ_PROGRAM: 'aprp';
ACCESS_WRITE_PROGRAM: 'apwp';
ACCESS_READ_OPI: 'aprb';
ACCESS_WRITE_OPI: 'apwb';
PHYS_UNIT: 'phy';
LIMIT_UPPER: 'uli';
LIMIT_LOWER: 'lli';

// other
PROC: 'proc';
PROC_END: 'endproc';
EXTERN: 'extern';
DEFINE: 'def';
VAR: 'var';
MACRO_DEFINE: 'define';
MACRO_AS: 'as';
REDEFINE: 'redef';
SET: 'set';


////
//// support
////
// types
BOOL_TYPE: 'bool';
CHAR_TYPE: 'char';
INT_TYPE: 'int';
REAL_TYPE: 'real';
STRING_TYPE: 'string';
AXIS_TYPE: 'axis';
FRAME_TYPE: 'frame';


////
//// functions
////
// math
SIN: 'sin';
COS: 'cos';
TAN: 'tan';
ASIN: 'asin';
ACOS: 'acos';
ATAN2: 'atan2';
SQRT: 'sqrt';
ABS: 'abs';
POT: 'pot';
TRUNC: 'trunc';
ROUND: 'round';
LN: 'ln';
EXP: 'exp';

// math
STR_LEN: 'strlen';

// tool offset
TOOL_OFFSET_LENGTH_RADIUS: 'tofflr';
TOOL_OFFSET_LENGTH: 'toffl';
TOOL_OFFSET_RADIUS: 'toffr';
TOOL_OFFSET: 'toff';


SET_MASTER_SPINDLE: 'setms';
GRINDING_WHEEL_PERIPHERAL_SPEED_ON: 'gwpson';
GRINDING_WHEEL_PERIPHERAL_SPEED_OFF: 'gwpsof';
FEED_GROUP: 'fgroup';
FEED_GROUP_EFFECTIVE_RADIUS: 'fgref';
WAIT_FOR_POSITION: 'waitp';
WAIT_FOR_MARKER: 'waitmc';
WAIT_FOR_SPINDLE: 'waits';
SPINDLE_POSITION_CONTROL_MODE_ON: 'spcon';
SPINDLE_POSITION_CONTROL_MODE_OFF: 'spcof';
SPINDLE_POSITIONING_IMMEDIATE: 'sposa';
MOTION_END_FINE: 'finea';
MOTION_END_COARSE: 'coarsea';
MOTION_END_INTERPOLATION: 'ipoenda';
MOTION_END_INTERPOLATION_BREAK: 'ipobrka';
FEEDRATE_PATH_ROTARY_AXIS_ON: 'fpraon';
FEEDRATE_PATH_ROTARY_AXIS_OFF: 'fpraof';
FEEDRATE_OVERRIDE_RAPID_TRAVERSE_VELOCITY: 'ovrrap';

// 4 characters
TOOL_CORRECTION_SUPPRESSION: 'supd';
MASTER_SPINDLE_SPEED_LIMIT: 'lims';
POSITIONING_IN_SECTIONS: 'posp';
POSITIONING_IMMEDIATE: 'posa';
SPINDLE_POSITIONING_DELAYED: 'spos';
FEEDRATE_OVERRIDE_POSITION_OR_SPINDLE: 'ovra';

// 3 characters
TOOL_CUTTING_SPEED: 'svc';
ABSOLUTE_COORDINATE_NEGATIVE: 'acn';
ABSOLUTE_COORDINATE_POSITIVE: 'acp';
POSITIONING_DELAYED: 'pos';
CONSTANT_CUTTING_RATE_REFERENCE_AXIS: 'scc';
FEEDRATE_PATH_ROTARY_AXIS: 'fpr';
SPINDLE_IDENTIFIER: 'spi';
FEEDRATE_OVERRIDE_PATH: 'ovr';
ACCELERATION_COMPENSATION: 'acc';
FEEDRATE_OVERRIDE_AXIAL_HANDWHEEL: 'fda';

// 2 characters
FEEDRATE_LIMIT: 'fl';
FEEDRATE_POSITION_AXIS: 'fa';
ABSOLUTE_COORDINATE: 'ac';
INCREMENTAL_COORDINATE: 'ic';
DIRECT_APPROACH_COORDINATE: 'dc';
FEEDRATE_OVERRIDE_PATH_HANDWHEEL: 'fd';
ADDITIONAL_FUNCTION: 'm';
AUXILIARY_FUNCTION: 'h';
PREPARATORY_FUNCTION: 'g';

// single character
X_AXIS: 'x';
Y_AXIS: 'y';
Z_AXIS: 'z';
A_AXIS: 'a';
B_AXIS: 'b';
C_AXIS: 'c';
SPINDLE: 's';
FEEDRATE: 'f';
TOOL: 't';
TOOL_CORRECTION: 'd';

// symbols
OPEN_PAREN: '(';
CLOSE_PAREN: ')';
OPEN_BRACKET: '[';
CLOSE_BRACKET: ']';


DOLLAR: '$';
POINT: '.';
COMMA: ',';

// reserved words
RESERVED: 'con' | 'prn' | 'aux' | 'nul' | 'com'[1-9] | 'lpt'[1-9];

// system variables
SYS_VAR: '$'[$acmnopstv]*[a-z_]*; // could be improved

// axis variables
AXIS: [abcxyz][0-9]*;

// user variables
R_PARAM: '$'?'r'[0-9]+;

// general
WHITESPACE: [ \t]+ -> skip;
NAME: [a-z0-9_]+;
NEWLINE: ('\r' '\n'? | '\n') -> skip;
COMMENT: ';' ~[\r\n]* -> skip;
HIDE: [ \t]*'/'[0-7]?;
BLOCK_NUMBER: 'n';

////
//// constant
////
// numeric
INT_POSITIVE: [0-9]+;
INT: SUB? INT_POSITIVE;
REAL_POSITIVE: [0-9]* POINT [0-9]+;
REAL: SUB? REAL_POSITIVE;
BIN: '\'B'[01]+'\'';
HEX: '\'H'[0-9A-F]+'\'';

// language
BOOL: 'true'|'false';
PI: '$PI';
STRING: '"'~[\r\n]*'"';

// names
PROGRAM_NAME_SIMPLE: [_a-z][a-z]NAME; // program name as defined
PROGRAM_NAME_EXTENDED: NAME;
LABEL_NAME: [a-z_][a-z_][a-z0-9_]*;
LABEL: LABEL_NAME':';


/*
 * Parser Rules
 */

file: (declarationBlock* block* | procedureDefinition) EOF;

declarationBlock: blocknumber? declaration NEWLINE;
block: blocknumber? (statement | command*) NEWLINE;

blocknumber: BLOCK_NUMBER INT_POSITIVE;

// command
command
    : preparatoryFunction 
    | additionalFunction 
    | auxiliaryFunction 
    | codeWord;

additionalFunction: ADDITIONAL_FUNCTION INT_POSITIVE;
auxiliaryFunction: AUXILIARY_FUNCTION INT_POSITIVE;
preparatoryFunction: PREPARATORY_FUNCTION INT_POSITIVE;

codeWord: axis constant | axis_identifier ASSIGNMENT expression;

// declaration
declaration: macroDeclaration | variableDeclaration | procedureDeclaration;

macroDeclaration: MACRO_DEFINE NAME MACRO_AS statement*;
variableDeclaration: DEFINE type variableDefinition (COMMA variableDefinition)*;
procedureDeclaration: EXTERN PROGRAM_NAME_SIMPLE parameterDeclarations?;
parameterDeclarations: OPEN_PAREN parameterDeclaration (COMMA parameterDeclaration)* CLOSE_PAREN;
parameterDeclaration: VAR? type arrayDefinition?;
arrayDeclaration: OPEN_BRACKET expression? (COMMA expression?)? (COMMA expression?)? CLOSE_BRACKET;

// definition
variableDefinition: NAME arrayDefinition? (ASSIGNMENT expression)?;
arrayDefinition: OPEN_BRACKET expression (COMMA expression)? (COMMA expression)? CLOSE_BRACKET;

procedureDefinition: PROC PROGRAM_NAME_SIMPLE parameterDefinitions? declarationBlock* block* PROC_END;
parameterDefinitions: OPEN_PAREN parameterDefinition (COMMA parameterDefinition)* CLOSE_PAREN;
parameterDefinition: VAR? type variableDefinition;

// statement
statement
    : ifStatement
    | iterativeStatement
    | jumpStatement
    | declaration
    | procedureDefinition
    | expression;

ifStatement: IF OPEN_PAREN expression CLOSE_PAREN block* (ELSE block*)? IF_END;

iterativeStatement: WHILE OPEN_PAREN expression CLOSE_PAREN block* WHILE_END;

jumpStatement
    : GOTO LABEL_NAME
    | GOTO_B LABEL_NAME
    | GOTO_C LABEL_NAME
    | GOTO_F LABEL_NAME
    | GOTO_S LABEL_NAME
    | RETURN expression?;

// expression
primaryExpression
    : NAME
    | function
    | constant
    | OPEN_PAREN expression CLOSE_PAREN;

unaryExpression
    : NOT primaryExpression
    | NOT_B primaryExpression;

multiplicativeExpression : unaryExpression ((MUL|DIV|MOD) unaryExpression)*;
additiveExpression : multiplicativeExpression ((ADD|SUB) multiplicativeExpression)*;
binaryAndExpression : additiveExpression (AND_B additiveExpression)*;
binaryExclusiveOrExpression : binaryAndExpression (XOR_B binaryAndExpression)*;
binaryInclusiveOrExpression : binaryExclusiveOrExpression (OR_B binaryExclusiveOrExpression)*;
andExpression : binaryInclusiveOrExpression (AND binaryInclusiveOrExpression)*;
exclusiveOrExpression : andExpression (XOR andExpression)*;
inclusiveOrExpression : exclusiveOrExpression (OR exclusiveOrExpression)*;
stringExpression : inclusiveOrExpression (CONCAT inclusiveOrExpression)*;
relationalExpression : stringExpression ((EQUAL|NOT_EQUAL|LESS_EQUAL|GREATER_EQUAL|LESS|GREATER) stringExpression)*;

expression
    : relationalExpression;

// basic
type
    : BOOL_TYPE
    | CHAR_TYPE
    | INT_TYPE
    | REAL_TYPE
    | STRING_TYPE OPEN_BRACKET expression CLOSE_BRACKET
    | AXIS_TYPE
    | FRAME_TYPE;

constant
    : INT
    | REAL
    | HEX
    | BIN
    | STRING;

//// predefined procedure
// feedrate override
feedrate_override_path: FEEDRATE_OVERRIDE_PATH '= 'INT;
feedrate_override_rapid_traverse_velocity: FEEDRATE_OVERRIDE_RAPID_TRAVERSE_VELOCITY '= 'INT;
feedrate_override_position_or_spindle: FEEDRATE_OVERRIDE_POSITION_OR_SPINDLE OPEN_BRACKET axis_spindle_identifier CLOSE_BRACKET '= 'INT;

// acceleration compensation
acceleration_compensation: ACCELERATION_COMPENSATION OPEN_BRACKET axis_spindle_identifier CLOSE_BRACKET '= 'INT;

// feedrate override handwheel
feedrate_override_path_handwheel: FEEDRATE_OVERRIDE_PATH_HANDWHEEL '= 'INT;
feedrate_override_axial_handwheel: FEEDRATE_OVERRIDE_AXIAL_HANDWHEEL OPEN_BRACKET axis_identifier CLOSE_BRACKET '= 'INT;

//// Types
// axis identifier
axis_spindle_identifier: axis_identifier | spindle_identifier;
axis_identifier: axis INT_POSITIVE | NAME;
spindle_identifier: SPINDLE_IDENTIFIER OPEN_PAREN INT_POSITIVE CLOSE_PAREN | SPINDLE INT_POSITIVE;
axis: A_AXIS | B_AXIS | C_AXIS | X_AXIS | Y_AXIS | Z_AXIS;

