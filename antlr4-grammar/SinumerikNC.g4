grammar SinumerikNC;

/*
 * Parser Rules
 */

// temporary simple rules
file: part* EOF;
part
    : type
    | procedure
    | INT
    | REAL
    | BLOCK_NUMBER
    | PROGRAM_NAME_SIMPLE
    | PROGRAM_NAME_EXTENDED
    | OPEN_PAREN
    | CLOSE_PAREN
    | OPEN_BRACKET
    | CLOSE_BRACKET
    | LESS
    | LESS_EQUAL
    | GREATER
    | GREATER_EQUAL
    | EQUAL
    | NOT_EQUAL
    | LEFT_SHIFT
    | RIGHT_SHIFT
    | ADD
    | INC
    | SUB
    | DEC
    | MUL
    | DIV
    | MOD
    | ASSIGNMENT
    | PREPARATORY_FUNCTION
    | COMMENT
    | FEED_GROUP
    | ABSOLUTE_COORDINATE
    | INCREMENTAL_COORDINATE
    | FEEDRATE;

/*
//// Functions
// feedrate override
feedrate_override_path: FEEDRATE_OVERRIDE_PATH '=' INT;
feedrate_override_rapid_traverse_velocity: FEEDRATE_OVERRIDE_RAPID_TRAVERSE_VELOCITY '=' INT;
feedrate_override_position_or_spindle: FEEDRATE_OVERRIDE_POSITION_OR_SPINDLE OPEN_BRACKET axis_spindle_identifier CLOSE_BRACKET '=' INT;

// acceleration compensation
acceleration_compensation: ACCELERATION_COMPENSATION OPEN_BRACKET axis_spindle_identifier CLOSE_BRACKET '=' INT;

// feedrate override handwheel
feedrate_override_path_handwheel: FEEDRATE_OVERRIDE_PATH_HANDWHEEL '=' INT;
feedrate_override_axial_handwheel: FEEDRATE_OVERRIDE_AXIAL_HANDWHEEL OPEN_BRACKET axis_identifier CLOSE_BRACKET '=' INT;

//// Types
// axis identifier
axis_spindle_identifier: axis_identifier | spindle_identifier;
axis_identifier: axis INT_POSITIVE;
spindle_identifier: SPINDLE_IDENTIFIER OPEN_PAREN INT_POSITIVE CLOSE_PAREN | SPINDLE INT_POSITIVE;
axis: A_AXIS | B_AXIS | C_AXIS | X_AXIS | Y_AXIS | Z_AXIS;
*/

//// Core
// type
type
    : BOOL_TYPE
    | CHAR_TYPE
    | INT_TYPE
    | REAL_TYPE
    | STRING_TYPE
    | AXIS_TYPE
    | FRAME_TYPE;

// procedure
procedure : PROCEDURE PROGRAM_NAME_SIMPLE;

/*
 * Lexer Rules
 */

// fragments
fragment A: [a|A];
fragment B: [b|B];
fragment C: [c|C];
fragment D: [d|D];
fragment E: [e|E];
fragment F: [f|F];
fragment G: [g|G];
fragment H: [h|H];
fragment I: [i|I];
fragment J: [j|J];
fragment K: [k|K];
fragment L: [l|L];
fragment M: [m|M];
fragment N: [n|N];
fragment O: [o|O];
fragment P: [p|P];
fragment Q: [q|Q];
fragment R: [r|R];
fragment S: [s|S];
fragment T: [t|T];
fragment U: [u|U];
fragment V: [v|V];
fragment W: [w|W];
fragment X: [x|X];
fragment Y: [y|Y];
fragment Z: [z|Z];

// function names
// 5 or more characters
DEFINE: D E F I N E;
TOOL_OFFSET_LENGTH_RADIUS: T O F F L R;
TOOL_OFFSET_LENGTH: T O F F L;
TOOL_OFFSET_RADIUS: T O F F R;
SET_MASTER_SPINDLE: S E T M S;
GRINDING_WHEEL_PERIPHERAL_SPEED_ON: G W P S O N;
GRINDING_WHEEL_PERIPHERAL_SPEED_OFF: G W P S O F;
FEED_GROUP: F G R O U P;
FEED_GROUP_EFFECTIVE_RADIUS: F G R E F;
WAIT_FOR_POSITION: W A I T P;
WAIT_FOR_MARKER: W A I T M C;
WAIT_FOR_SPINDLE: W A I T S;
SPINDLE_POSITION_CONTROL_MODE_ON: S P C O N;
SPINDLE_POSITION_CONTROL_MODE_OFF: S P C O F;
SPINDLE_POSITIONING_IMMEDIATE: S P O S A;
MOTION_END_FINE: F I N E A;
MOTION_END_COARSE: C O A R S E A;
MOTION_END_INTERPOLATION: I P O E N D A;
MOTION_END_INTERPOLATION_BREAK: I P O B R K A;
FEEDRATE_PATH_ROTARY_AXIS_ON: F P R A O N;
FEEDRATE_PATH_ROTARY_AXIS_OFF: F P R A O F;
FEEDRATE_OVERRIDE_RAPID_TRAVERSE_VELOCITY: O V R R A P;

// 4 characters
TOOL_CORRECTION_SUPPRESSION: S U P D;
TOOL_OFFSET: T O F F;
MASTER_SPINDLE_SPEED_LIMIT: L I M S;
POSITIONING_IN_SECTIONS: P O S P;
POSITIONING_IMMEDIATE: P O S A;
SPINDLE_POSITIONING_DELAYED: S P O S;
FEEDRATE_OVERRIDE_POSITION_OR_SPINDLE: O V R A;
PROCEDURE: P R O C;

// 3 characters
TOOL_CUTTING_SPEED: S V C;
DEF: D E F;
ABSOLUTE_COORDINATE_NEGATIVE: A C N;
ABSOLUTE_COORDINATE_POSITIVE: A C P;
POSITIONING_DELAYED: P O S;
CONSTANT_CUTTING_RATE_REFERENCE_AXIS: S C C;
FEEDRATE_PATH_ROTARY_AXIS: F P R;
SPINDLE_IDENTIFIER: S P I;
FEEDRATE_OVERRIDE_PATH: O V R;
ACCELERATION_COMPENSATION: A C C;
FEEDRATE_OVERRIDE_AXIAL_HANDWHEEL: F D A;

// 2 characters
AS: A S;
FEEDRATE_LIMIT: F L;
FEEDRATE_POSITION_AXIS: F A;
ABSOLUTE_COORDINATE: A C;
INCREMENTAL_COORDINATE: I C;
DIRECT_APPROACH_COORDINATE: D C;
FEEDRATE_OVERRIDE_PATH_HANDWHEEL: F D;
BLOCK_NUMBER: N ID;
ADDITIONAL_FUNCTION: M ID;
AUXILIARY_FUNCTION: H ID;
PREPARATORY_FUNCTION: G ID;

// single character
X_AXIS: X;
Y_AXIS: Y;
Z_AXIS: Z;
A_AXIS: A;
B_AXIS: B;
C_AXIS: C;
SPINDLE: S;
FEEDRATE: F;
TOOL: T;
TOOL_CORRECTION: D;

// types
BOOL_TYPE: B O O L;
CHAR_TYPE: C H A R;
INT_TYPE: I N T;
REAL_TYPE: R E A L;
STRING_TYPE: S T R I N G;
AXIS_TYPE: A X I S;
FRAME_TYPE: F R A M E;

// symbols
OPEN_PAREN : '(';
CLOSE_PAREN : ')';
OPEN_BRACKET : '[';
CLOSE_BRACKET : ']';

LESS : '<';
LESS_EQUAL : '<=';
GREATER : '>';
GREATER_EQUAL : '>=';
EQUAL : '==';
NOT_EQUAL : '<>';
LEFT_SHIFT : '<<';
RIGHT_SHIFT : '>>';

ADD : '+';
INC : '++';
SUB : '-';
DEC : '--';
MUL : '*';
DIV : '/';
MOD : '%';

ASSIGNMENT: '=';

DOLLAR : '$';
POINT: '.';

// reserved words
RESERVED: C O N | P R N | A U X | N U L | C O M [1-9] | L P T [1-9];

// names
PROGRAM_NAME_SIMPLE: [_a-zA-Z][a-zA-Z]NAME;
PROGRAM_NAME_EXTENDED: NAME;

// general
INT_POSITIVE: [0-9]+;
INT: SUB? INT_POSITIVE;
REAL: [0-9]* POINT [0-9]+;
ID: [0]* INT_POSITIVE;
WHITESPACE: [ \t]+ -> skip;
NAME: [a-zA-Z0-9_]+;
SKIP_BLOCK: ('/');
NEWLINE: ('\r' '\n'? | '\n') -> skip;
COMMENT: ';' ~[\r\n]* -> skip;