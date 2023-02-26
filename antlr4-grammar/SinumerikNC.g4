grammar SinumerikNC;

// it is not possible to use regex flags but the options can be overwritten per rule
// see: https://github.com/antlr/antlr4/blob/master/doc/lexer-rules.md#lexer-rule-options
options { caseInsensitive=true; }

/*
 * Lexer Rules
 */

// general
ID: [0]* INT_POSITIVE;
WHITESPACE: [ \t]+ -> skip;
NAME: [a-z0-9_]+;
NEWLINE: ('\r' '\n'? | '\n') -> skip;
COMMENT: ';' ~[\r\n]* -> skip;
HIDE: [ \t]*'/'[0-7]?;
BLOCK_NUMBER: 'n'ID;


////
//// constant
////
// numeric
INT_POSITIVE: [0-9]+;
INT: SUB? INT_POSITIVE;
REAL_POSITIVE: [0-9]* POINT [0-9]+;
REAL: SUB? REAL_POSITIVE;
BIN: 'B'([01]{8})+;
HEX: 'H'([0-9A-F]{2})+;
STRING: '"'~[\r\n]*'"';

// language
BOOL: 'true'|'false';
PI: '$PI';


////
//// keywords
////
// control
WHILE: 'while';
WHILE_END: 'while';
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
LABEL_NAME: [a-z_]{2}[a-z0-9_]{0,30};
LABEL: LABEL_NAME':';
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

// system variables
SYS_VAR: '$'[$acmnopstv]{0,3}[a-z_]*; // could be improved

// axis variables
AXIS: [abcxyz][0-9]{0,5};

// user variables
R_PARAM: '$'?'r'[0-9]{1,4};


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
ADDITIONAL_FUNCTION: 'm'ID;
AUXILIARY_FUNCTION: 'h'ID;
PREPARATORY_FUNCTION: 'g'ID;

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

// names
PROGRAM_NAME_SIMPLE: [_a-z][a-z]NAME;
PROGRAM_NAME_EXTENDED: NAME;


/*
 * Parser Rules
 */

file: block* EOF;

block
    : procedure
    | statement;

// procedure
procedure: PROC PROGRAM_NAME_SIMPLE OPEN_PAREN params CLOSE_PAREN statement* PROC_END;

params: param | param_out | param COMMA params | param_out COMMA params;

param_out: VAR param;

param: type NAME;

// statement
statement
    : if_statement
    | while_statement;




type
    : BOOL_TYPE
    | CHAR_TYPE
    | INT_TYPE
    | REAL_TYPE
    | STRING_TYPE
    | AXIS_TYPE
    | FRAME_TYPE;

//// Functions
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
axis_identifier: axis INT_POSITIVE;
spindle_identifier: SPINDLE_IDENTIFIER OPEN_PAREN INT_POSITIVE CLOSE_PAREN | SPINDLE INT_POSITIVE;
axis: A_AXIS | B_AXIS | C_AXIS | X_AXIS | Y_AXIS | Z_AXIS;

