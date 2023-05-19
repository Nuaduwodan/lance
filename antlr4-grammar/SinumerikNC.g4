grammar SinumerikNC;

// it is not possible to use regex flags but the options can be overwritten per rule
// see: https://github.com/antlr/antlr4/blob/master/doc/lexer-rules.md#lexer-rule-options
options { caseInsensitive=true; }

/*
 * Lexer Rules
 */

// general
WHITESPACE: [ \t]+ -> skip;
NEWLINE: ('\r' '\n'? | '\n');
COMMENT: ';' ~[\r\n]* -> skip;

////
//// constant
////
// numeric
INT_UNSIGNED: [0-9]+;
REAL_UNSIGNED: REAL_DECIMAL | (INT_UNSIGNED | REAL_DECIMAL) EX SUB? (INT_UNSIGNED | REAL_DECIMAL);
fragment REAL_DECIMAL: [0-9]* POINT [0-9]+;
fragment EX: 'ex';
BIN: '\'B'[01]+'\'';
HEX: '\'H'[0-9A-F]+'\'';

// language
BOOL: 'true'|'false';
STRING: '"'~[\r\n]*?'"';

////
//// keywords
////
// control
WHILE: 'while';
WHILE_END: 'endwhile';
FOR: 'for';
FOR_END: 'endfor';
TO: 'to';
LOOP: 'loop';
LOOP_END: 'endloop';
REPEAT: 'repeat';
REPEAT_BLOCK: 'repeatb';
REPEAT_END: 'until';
IF: 'if';
ELSE: 'else';
IF_END: 'endif';
CASE: 'case';
CASE_OF: 'of';
CASE_DEFAULT: 'default';
GOTO: 'goto';
GOTO_B: 'gotob';
GOTO_C: 'gotoc';
GOTO_F: 'gotof';
GOTO_S: 'gotos';
LABEL_END: 'endlabel';
SYNC_WHEN: 'when';
SYNC_WHENEVER: 'whenever';
SYNC_FROM: 'from';
SYNC_DO: 'do';
SYNC_EVERY: 'every';
SYNC_CANCEL: 'cancel';
RETURN: 'ret';
CALL: 'call';
CALL_P: 'pcall';
CALL_EXT: 'extcall';
CALL_PATH: 'callpath';
CALL_BLOCK: 'block';
CALL_MODAL: 'mcall';
CALL_MODAL_OFF: 'mcallof';

// operators (operation type O)
ASSIGNMENT: '=';

ADD: '+';
SUB: '-';
MUL: '*';
SLASH: '/';
DIV: 'div';
MOD: 'mod';

EQUAL: '==';
NOT_EQUAL: '<>';
LESS: '<';
GREATER: '>';
LESS_EQUAL: '<=';
GREATER_EQUAL: '>=';

CONCAT: '<<';

NOT: 'not';
AND: 'and';
OR: 'or';
XOR: 'xor';

NOT_B: 'b_not';
AND_B: 'b_and';
OR_B: 'b_or';
XOR_B: 'b_xor';

// modifier
NCK: 'nck'; // there seems to be an error in the documentation. its 'nck' not 'nc'
CHAN: 'chan';
SYNR: 'synr';
SYNW: 'synw';
SYNRW: 'synrw';
ACCESS_READ: 'apr';
ACCESS_WRITE: 'apw';
READ_PROGRAM: 'aprp';
WRITE_PROGRAM: 'apwp';
READ_OPI: 'aprb';
WRITE_OPI: 'apwb';
PHYS_UNIT: 'phy';
UPPER_LIMIT: 'uli';
LOWER_LIMIT: 'lli';

// symbol
PROC: 'proc';
PROC_END: 'endproc';
EXTERN: 'extern';
DEFINE: 'def';
VAR: 'var';
MACRO_DEFINE: 'define';
MACRO_AS: 'as';
REDEFINE: 'redef';
SET: 'set';

// other keywords (operation type K)
AC: 'ac';
IC: 'ic';

// other keywords under procedures (operation type K)
ACC: 'acc';
ACCLIMA: 'acclima';
ACN: 'acn';
ACP: 'acp';
APX: 'apx';
AX: 'ax';
BLSYNC: 'blsync';
CAC: 'cac';
CACN: 'cacn';
CACP: 'cacp';
CDC: 'cdc';
CIC: 'cic';
COARSEA: 'coarsea';
CPBC: 'cpbc';
CPDEF: 'cpdef';
CPDEL: 'cpdel';
CPFMOF: 'cpfmof';
CPFMON: 'cpfmon';
CPFMSON: 'cpfmson';
CPFPOS: 'cpfpos';
CPFRS: 'cpfrs';
CPLA: 'cpla';
CPLCTID: 'cplctid';
CPLDEF: 'cpldef';
CPLDEL: 'cpldel'; 
CPLDEN: 'cplden';
CPLINSC: 'cplinsc';
CPLINTR: 'cplintr';
CPLNUM: 'cplnum';
CPLOF: 'cplof';
CPLON: 'cplon';
CPLOUTSC: 'cploutsc';
CPLOUTTR: 'cplouttr';
CPLPOS: 'cplpos';
CPLSETVAL: 'cplsetval';
CPMALARM: 'cpmalarm';
CPMBRAKE: 'cpmbrake';
CPMPRT: 'cpmprt';
CPMRESET: 'cpmreset';
CPMSTART: 'cpmstart';
CPMVDI: 'cpmvdi';
CPOF: 'cpof';
CPON: 'cpon';
CPRES: 'cpres';
CPSETTYPE: 'cpsettype';
CPSYNCOP: 'cpsyncop';
CPSYNCOP2: 'cpsyncop2';
CPSYNCOV: 'cpsyncov';
CPSYNFIP: 'cpsynfip';
CPSYNFIP2: 'cpsynfip2';
CPSYNFIV: 'cpsynfiv';
DAC: 'dac';
DC: 'dc';
DIACYCOFA: 'diacycofa';
DIAM90A: 'diam90a';
DIAMCHAN: 'diamchan';
DIAMCHANA: 'diamchana';
DIAMOFA: 'diamofa';
DIAMONA: 'diamona';
DIC: 'dic';
FA: 'fa';
FDA: 'fda';
FGREF: 'fgref';
FI: 'fi';
FINEA: 'finea';
FL: 'fl';
FMA: 'fma';
FOC: 'foc';
FOCOF: 'focof';
FOCON: 'focon';
FPO: 'fpo';
FXS: 'fxs';
FXST: 'fxst';
FXSW: 'fxsw';
FZ: 'fz';
GP: 'gp';
ID: 'id';
IDS: 'ids';
INICF: 'inicf';
INIPO: 'inipo';
INIRE: 'inire';
IP: 'ip';
IPOENDA: 'ipoenda';
ISOCALL: 'isocall';
JERKLIM: 'jerklim';
JERKLIMA: 'jerklima';
LIFTFAST: 'liftfast';
LIMS: 'lims';
MI: 'mi';
MOV: 'mov';
OS: 'os';
OSB: 'osb';
OSCILL: 'oscill';
OSCTRL: 'osctrl';
OSE: 'ose';
OSNSC: 'osnsc';
OSP1: 'osp1';
OSP2: 'osp2';
OST1: 'ost1';
OST2: 'ost2';
OVR: 'ovr';
OVRA: 'ovra';
OVRRAP: 'ovrrap';
PHI: 'phi';
PHU: 'phu';
PM: 'pm';
PO: 'po';
POLF: 'polf';
POS: 'pos';
POSA: 'posa';
POSP: 'posp';
PR: 'pr';
PRIO: 'prio';
PRLOC: 'prloc';
PSISYNRW: 'psisynrw';
QU: 'qu';
RAC: 'rac';
REP: 'rep';
RIC: 'ric';
RT: 'rt';
SC: 'sc';
SCC: 'scc';
SCPARA: 'scpara';
SETINT: 'setint';
SPOS: 'spos';
SPOSA: 'sposa';
SRA: 'sra';
STA: 'sta';
SVC: 'svc';
TR: 'tr';
VELOLIM: 'velolim';
VELOLIMA: 'velolima';


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

// functions (operation type F)
// math
SIN: 'sin';
COS: 'cos';
TAN: 'tan';
ASIN: 'asin';
ACOS: 'acos';
ATAN2: 'atan2';
SQRT: 'sqrt';
POT: 'pot';
LN: 'ln';
EXP: 'exp';
ABS: 'abs';
TRUNC: 'trunc';
ROUND: 'round';
ROUNDUP: 'roundup';
BOUND: 'bound';
MAXVAL: 'maxval';
MINVAL: 'minval';

CALCDAT: 'calcdat';
CTRANS: 'ctrans';
CMIRROR: 'cmirror';
CSCALE: 'cscale';
CROT: 'crot';
CROTS: 'crots';
CRPL: 'crpl';

// string
STRLEN: 'strlen';
SPRINT: 'sprint';
STRINGIS: 'stringis';
SUBSTR: 'substr';
AXNAME: 'axname';
AXSTRING: 'axstring';
INDEX: 'index';
MINDEX: 'mindex';
RINDEX: 'rindex';
ISNUMBER: 'isnumber';
NUMBER: 'number';
MATCH: 'match';
TOLOWER: 'tolower';
TOUPPER: 'toupper';

// curve table
CTAB: 'ctab';
CTABEXISTS: 'ctabexists';
CTABFNO: 'ctabfno';
CTABFPOL: 'ctabfpol';
CTABFSEG: 'ctabfseg';
CTABID: 'ctabid';
CTABINV: 'ctabinv';
CTABISLOCK: 'ctabislock';
CTABMEMTYP: 'ctabmemtyp';
CTABMPOL: 'ctabmpol';
CTABMSEG: 'ctabmseg';
CTABNO: 'ctabno';
CTABNOMEM: 'ctabnomem';
CTABPERIOD: 'ctabperiod';
CTABPOL: 'ctabpol';
CTABPOLID: 'ctabpolid';
CTABSEG: 'ctabseg';
CTABSEGID: 'ctabsegid';
CTABSEV: 'ctabsev';
CTABSSV: 'ctabssv';
CTABTEP: 'ctabtep';
CTABTEV: 'ctabtev';
CTABTMAX: 'ctabtmax';
CTABTMIN: 'ctabtmin';
CTABTSP: 'ctabtsp';
CTABTSV: 'ctabtsv';

// other
ADDFRAME: 'addframe';
AXTOSPI: 'axtospi';
CALCPOSI: 'calcposi';
CALCTRAVAR: 'calctravar';
CFINE: 'cfine';
CHKDM: 'chkdm';
CHKDNO: 'chkdno';
COLLPAIR: 'collpair';
CORRTC: 'corrtc';
CORRTRAFO: 'corrtrafo';
CSPLINE: 'cspline';
DELDL: 'deldl';
DELMLOWNER: 'delmlowner';
DELMLRES: 'delmlres';
DELOBJ: 'delobj';
DELTOOLENV: 'deltoolenv';
GETACTT: 'getactt';
GETACTTD: 'getacttd';
GETDNO: 'getdno';
GETT: 'gett';
GETTCOR: 'gettcor';
GETTENV: 'gettenv';
GETVARAP: 'getvarap';
GETVARDFT: 'getvardft';
GETVARDIM: 'getvardim';
GETVARLIM: 'getvarlim';
GETVARPHU: 'getvarphu';
GETVARTYP: 'getvartyp';
INTERSEC: 'intersec';
INVFRAME: 'invframe';
ISAXIS: 'isaxis';
ISFILE: 'isfile';
ISVAR: 'isvar';
LENTOAX: 'lentoax';
MEAFRAME: 'meaframe';
MEASURE: 'measure';
MODAXVAL: 'modaxval';
NAMETOINT: 'nametoint';
NEWMT: 'newmt';
NEWT: 'newt';
ORISOLH: 'orisolh';
POSRANGE: 'posrange';
PROTD: 'protd';
SETDNO: 'setdno';
SETTCOR: 'settcor';
SIRELAY: 'sirelay';
SPINDLE_IDENTIFIER: 'spi';
TOOLENV: 'toolenv';
TOOLGNT: 'toolgnt';
TOOLGT: 'toolgt';

// procedures (operation type P)
ACTBLOCNO: 'actblocno';
ADISPOSA: 'adisposa';
AFISOF: 'afisof';
AFISON: 'afison';
AUXFUDEL: 'auxfudel';
AUXFUDELG: 'auxfudelg';
AUXFUMSEQ: 'auxfumseq';
AUXFUSYNC: 'auxfusync';
AXCTSWE: 'axctswe';
AXCTSWEC: 'axctswec';
AXCTSWED: 'axctswed';
AXTOCHAN: 'axtochan';
BRISKA: 'briska';
CADAPTOF: 'cadaptof';
CADAPTON: 'cadapton';
CALCFIR: 'calcfir';
CANCELSUB: 'cancelsub';
CHANDATA: 'chandata';
CLEARM: 'clearm';
CLRINT: 'clrint';
CONTDCON: 'contdcon';
CONTPRON: 'contpron';
CORROF: 'corrof';
COUPDEF: 'coupdef';
COUPDEL: 'coupdel';
COUPOF: 'coupof';
COUPOFS: 'coupofs';
COUPON: 'coupon';
COUPONC: 'couponc';
COUPRES: 'coupres';
CPROT: 'cprot';
CPROTDEF: 'cprotdef';
CTABDEF: 'ctabdef';
CTABDEL: 'ctabdel';
CTABEND: 'ctabend';
CTABLOCK: 'ctablock';
CTABUNLOCK: 'ctabunlock';
DELAYFSTOF: 'delayfstof';
DELAYFSTON: 'delayfston';
DELDTG: 'deldtg';
DELETE: 'delete';
DELMT: 'delmt';
DELT: 'delt';
DELTC: 'deltc';
DISABLE: 'disable';
DRFOF: 'drfof';
DRIVEA: 'drivea';
DRVPRD: 'drvprd';
DRVPWR: 'drvpwr';
DZERO: 'dzero';
EGDEF: 'egdef';
EGDEL: 'egdel';
EGOFC: 'egofc';
EGOFS: 'egofs';
EGON: 'egon';
EGONSYN: 'egonsyn';
EGONSYNE: 'egonsyne';
ENABLE: 'enable';
ESRR: 'esrr';
ESRS: 'esrs';
EXECSTRING: 'execstring';
EXECTAB: 'exectab';
EXECUTE: 'execute';
EXTCLOSE: 'extclose';
EXTOPEN: 'extopen';
FCTDEF: 'fctdef';
FGROUP: 'fgroup';
FILEDATE: 'filedate';
FILEINFO: 'fileinfo';
FILESIZE: 'filesize';
FILESTAT: 'filestat';
FILETIME: 'filetime';
FPR: 'fpr';
FPRAOF: 'fpraof';
FPRAON: 'fpraon';
FTOC: 'ftoc';
GEOAX: 'geoax';
GET: 'get';
GETD: 'getd';
GETEXET: 'getexet';
GETFREELOC: 'getfreeloc';
GETSELT: 'getselt';
GWPSOF: 'gwpsof';
GWPSON: 'gwpson';
ICYCOF: 'icycof';
ICYCON: 'icycon';
INIT: 'init';
IPOBRKA: 'ipobrka';
IPTRLOCK: 'iptrlock';
IPTRUNLOCK: 'iptrunlock';
JERKA: 'jerka';
LEADOF: 'leadof';
LEADON: 'leadon';
LOCK: 'lock';
MASLDEF: 'masldef';
MASLDEL: 'masldel';
MASLOF: 'maslof';
MASLOFS: 'maslofs';
MASLON: 'maslon';
MMC: 'mmc';
MSG: 'msg';
MVTOOL: 'mvtool';
NEWCONF: 'newconf';
NPROT: 'nprot';
NPROTDEF: 'nprotdef';
ORIRESET: 'orireset';
POLFA: 'polfa';
POLFMASK: 'polfmask';
POLFMLIN: 'polfmlin';
POLYPATH: 'polypath';
POSM: 'posm';
POSMT: 'posmt';
PRESETON: 'preseton';
PRESETONS: 'presetons';
PROTA: 'prota';
PROTS: 'prots';
PUNCHACC: 'punchacc';
PUTFTOC: 'putftoc';
PUTFTOCF: 'putftocf';
RDISABLE: 'rdisable';
READ: 'read';
RELEASE: 'release';
RESETMON: 'resetmon';
RETB: 'retb';
SBLOF: 'sblof';
SBLON: 'sblon';
SETAL: 'setal';
SETM: 'setm';
SETMS: 'setms';
SETMTH: 'setmth';
SETPIECE: 'setpiece';
SETTA: 'setta';
SETTIA: 'settia';
SIRELIN: 'sirelin';
SIRELOUT: 'sirelout';
SIRELTIME: 'sireltime';
SOFTA: 'softa';
SPCOF: 'spcof';
SPCON: 'spcon';
SPLINEPATH: 'splinepath';
START: 'start';
STOPRE: 'stopre';
STOPREOF: 'stopreof';
SYNFCT: 'synfct';
TANG: 'tang';
TANGDEL: 'tangdel';
TANGOF: 'tangof';
TANGON: 'tangon';
TCA: 'tca';
TCI: 'tci';
TLIFT: 'tlift';
TML: 'tml';
TMOF: 'tmof';
TMON: 'tmon';
TOFFOF: 'toffof';
TOFFON: 'toffon';
TRAANG: 'traang';
TRACON: 'tracon';
TRACYL: 'tracyl';
TRAFOOF: 'trafoof';
TRAFOON: 'trafoon';
TRAILOF: 'trailof';
TRAILON: 'trailon';
TRANSMIT: 'transmit';
TRAORI: 'traori';
UNLOCK: 'unlock';
WAITC: 'waitc';
WAITE: 'waite';
WAITENC: 'waitenc';
WAITM: 'waitm';
WAITMC: 'waitmc';
WAITP: 'waitp';
WAITS: 'waits';
WRITE: 'write';
WRTPR: 'wrtpr';

// commands (operation types A, G and M code)
GCODE:'g';
MCODE:'m';
ADIS:'adis';
ADISPOS:'adispos';
ALF:'alf';
AMIRROR:'amirror';
ANG:'ang';
AP:'ap';
AR:'ar';
AROT:'arot';
AROTS:'arots';
ASCALE:'ascale';
ASPLINE:'aspline';
ATOL:'atol';
ATRANS:'atrans';
BAUTO:'bauto';
BNAT:'bnat';
BRISK:'brisk';
BSPLINE:'bspline';
BTAN:'btan';
CDOF:'cdof';
CDOF2:'cdof2';
CDON:'cdon';
CFC:'cfc';
CFIN:'cfin';
CFTCP:'cftcp';
CHF:'chf';
CHR:'chr';
CIP:'cip';
COMPCAD:'compcad';
COMPCURV:'compcurv';
COMPOF:'compof';
COMPON:'compon';
COMPPATH:'comppath';
COMPSURF:'compsurf';
CP:'cp';
CPRECOF:'cprecof';
CPRECON:'cprecon';
CR:'cr';
CT:'ct';
CTOL:'ctol';
CTOLG0:'ctolg0';
CUT2D:'cut2d';
CUT2DD:'cut2dd';
CUT2DF:'cut2df';
CUT2DFD:'cut2dfd';
CUT3DC:'cut3dc';
CUT3DCC:'cut3dcc';
CUT3DCCD:'cut3dccd';
CUT3DCD:'cut3dcd';
CUT3DF:'cut3df';
CUT3DFD:'cut3dfd';
CUT3DFF:'cut3dff';
CUT3DFS:'cut3dfs';
CUTCONOF:'cutconof';
CUTCONON:'cutconon';
CUTMOD:'cutmod';
CUTMODK:'cutmodk';
D:'d';
D0:'d0';
DIAM90:'diam90';
DIAMCYCOF:'diamcycof';
DIAMOF:'diamof';
DIAMON:'diamon';
DILF:'dilf';
DISC:'disc';
DISCL:'discl';
DISPR:'dispr';
DISR:'disr';
DISRP:'disrp';
DITE:'dite';
DITS:'dits';
DL:'dl';
DRIVE:'drive';
DYNFINISH:'dynfinish';
DYNNORM:'dynnorm';
DYNPOS:'dynpos';
DYNPREC:'dynprec';
DYNROUGH:'dynrough';
DYNSEMIFIN:'dynsemifin';
EAUTO:'eauto';
ENAT:'enat';
ETAN:'etan';
F:'f';
FAD:'fad';
FB:'fb';
FCUB:'fcub';
FD:'fd';
FENDNORM:'fendnorm';
FFWOF:'ffwof';
FFWON:'ffwon';
FIFOCTRL:'fifoctrl';
FLIM:'flim';
FLIN:'flin';
FNORM:'fnorm';
FP:'fp';
FRC:'frc';
FRCM:'frcm';
FTOCOF:'ftocof';
FTOCON:'ftocon';
//GCODE_NUMBERED: GCODE INT_UNSIGNED;
GFRAME:'gframe';
HCODE:'h';
//HCODE_NUMBERED: HCODE INT_UNSIGNED;
I:'i';
I1:'i1';
INVCCW:'invccw';
INVCW:'invcw';
IR:'ir';
ISD:'isd';
J:'j';
J1:'j1';
JR:'jr';
K:'k';
K1:'k1';
KONT:'kont';
KONTC:'kontc';
KONTT:'kontt';
KR:'kr';
L:'l';
LEAD:'lead';
LFOF:'lfof';
LFON:'lfon';
LFPOS:'lfpos';
LFTXT:'lftxt';
LFWP:'lfwp';
//MCODE_NUMBERED: MCODE INT_UNSIGNED;
MEAC:'meac';
MEAS:'meas';
MEASA:'measa';
MEASF:'measf';
MEAW:'meaw';
MEAWA:'meawa';
MIRROR:'mirror';
MOVT:'movt';
NORM:'norm';
OEMIPO1:'oemipo1';
OEMIPO2:'oemipo2';
OFFN:'offn';
OMA:'oma' [1-9];
ORIANGLE:'oriangle';
ORIAXES:'oriaxes';
ORIAXESFR:'oriaxesfr';
ORIAXPOS:'oriaxpos';
ORIC:'oric';
ORICONCCW:'oriconccw';
ORICONCW:'oriconcw';
ORICONIO:'oriconio';
ORICONTO:'oriconto';
ORICURINV:'oricurinv';
ORICURVE:'oricurve';
ORID:'orid';
ORIEULER:'orieuler';
ORIMKS:'orimks';
ORIPATH:'oripath';
ORIPATHS:'oripaths';
ORIPLANE:'oriplane';
ORIROTA:'orirota';
ORIROTC:'orirotc';
ORIROTR:'orirotr';
ORIROTT:'orirott';
ORIRPY:'orirpy';
ORIRPY2:'orirpy2';
ORIS:'oris';
ORISOF:'orisof';
ORISON:'orison';
ORIVECT:'orivect';
ORIVIRT1:'orivirt1';
ORIVIRT2:'orivirt2';
ORIWKS:'oriwks';
OSC:'osc';
OSD:'osd';
OSOF:'osof';
OSS:'oss';
OSSE:'osse';
OST:'ost';
OTOL:'otol';
OTOLG0:'otolg0';
P:'p';
PACCLIM:'pacclim';
PAROT:'parot';
PAROTOF:'parotof';
PDELAYOF:'pdelayof';
PDELAYON:'pdelayon';
PL:'pl';
POLY:'poly';
PON:'pon';
PONS:'pons';
PTP:'ptp';
PTPG0:'ptpg0';
PTPWOC:'ptpwoc';
PW:'pw';
REPOSA:'reposa';
REPOSH:'reposh';
REPOSHA:'reposha';
REPOSL:'reposl';
REPOSQ:'reposq';
REPOSQA:'reposqa';
RMB:'rmb';
RMBBL:'rmbbl';
RME:'rme';
RMEBL:'rmebl';
RMI:'rmi';
RMIBL:'rmibl';
RMN:'rmn';
RMNBL:'rmnbl';
RND:'rnd';
RNDM:'rndm';
ROT:'rot';
ROTS:'rots';
RP:'rp';
RPL:'rpl';
RTLIOF:'rtliof';
RTLION:'rtlion';
S:'s';
SCALE:'scale';
SD:'sd';
SF:'sf';
SOFT:'soft';
SON:'son';
SONS:'sons';
SPATH:'spath';
SPIF1:'spif1';
SPIF2:'spif2';
SPN:'spn';
SPOF:'spof';
SPP:'spp';
SR:'sr';
ST:'st';
STARTFIFO:'startfifo';
STOLF:'stolf';
STOPFIFO:'stopfifo';
SUPA:'supa';
SUPD:'supd';
T:'t';
TCARR:'tcarr';
TCOABS:'tcoabs';
TCOFR:'tcofr';
TCOFRX:'tcofrx';
TCOFRY:'tcofry';
TCOFRZ:'tcofrz';
THETA:'theta';
TILT:'tilt';
TOFF:'toff';
TOFFL:'toffl';
TOFFLR:'tofflr';
TOFFR:'toffr';
TOFRAME:'toframe';
TOFRAMEX:'toframex';
TOFRAMEY:'toframey';
TOFRAMEZ:'toframez';
TOROT:'torot';
TOROTOF:'torotof';
TOROTX:'torotx';
TOROTY:'toroty';
TOROTZ:'torotz';
TOWBCS:'towbcs';
TOWKCS:'towkcs';
TOWMCS:'towmcs';
TOWSTD:'towstd';
TOWTCS:'towtcs';
TOWWCS:'towwcs';
TRANS:'trans';
TURN:'turn';
UPATH:'upath';
WALCS:'walcs';
WALIMOF:'walimof';
WALIMON:'walimon';

// symbols
OPEN_PAREN: '(';
CLOSE_PAREN: ')';
OPEN_BRACKET: '[';
CLOSE_BRACKET: ']';

DOLLAR: '$';
POINT: '.';
DOUBLE_COLON: ':';
COMMA: ',';

// other (operation types C, PA, empty)
BLOCK_NUMBER: 'n';
CYCLE: 'cycle';
GROUP_ADDEND:'group_addend';
GROUP_BEGIN:'group_begin';
GROUP_END:'group_end';
HOLES1:'holes1';
HOLES2:'holes2';
LONGHOLE:'longhole';
POCKET3:'pocket3';
POCKET4:'pocket4';
SLOT1:'slot1';
SLOT2:'slot2';
DISPLOF:'displof';
DISPLON:'displon';
PREPRO:'prepro';
SAVE:'save';
COMPLETE:'complete';
INITIAL:'initial';
STAT:'stat';
TU:'tu';


// reserved words
RESERVED: 'con' | 'prn' | 'aux' | 'nul' | 'com'[1-9] | 'lpt'[1-9];

// variables
SYS_VAR: (('$'[$acmnopstv]+)|'syg_')[a-z0-9_]*; // could be improved
AXIS: [abcxyz];
R_PARAM: 'r';

// names
NAME: NAME_NON_NUMMERIC NAME_NON_NUMMERIC NAME_ALL*;
fragment NAME_NON_NUMMERIC: [a-z_];
fragment NAME_ALL: [a-z0-9_];


/*
 * Parser Rules
 */

file: NEWLINE* (content | procedureDefinition) EOF;

content: declarationScope scope;
declarationScope: declarationBlock*;
declarationBlock: (lineStart? declaration | lineStart) NEWLINE+?;

//scope: block* returnStatement?;
scope: block*;
block: (lineStart? labelDefinition? statement | lineStart? labelDefinition | lineStart) NEWLINE+?;
//returnStatement: RETURN (OPEN_PAREN expression (COMMA expression?)? (COMMA expression?)? (COMMA expression)? CLOSE_PAREN)? NEWLINE+;

lineStart: SLASH? blockNumberDefinition | SLASH;
blockNumberDefinition: blockNumber;
blockNumber: BLOCK_NUMBER intUnsigned;
labelDefinition: NAME DOUBLE_COLON;

// procedure
procedureDefinition: procedureDefinitionHeader NEWLINE+ content PROC_END NEWLINE+?;
procedureDefinitionHeader: PROC NAME parameterDefinitions? procedureModifier*;
procedureModifier: SBLOF | DISPLON | DISPLOF | ACTBLOCNO;

parameterDefinitions: OPEN_PAREN (parameterDefinition (COMMA parameterDefinition)*)? CLOSE_PAREN;
parameterDefinition: parameterDefinitionByValue | parameterDefinitionByReference;
parameterDefinitionByValue: type NAME (ASSIGNMENT defaultValue=expression)?;
parameterDefinitionByReference: VAR type NAME arrayDeclaration?;

// declaration
declaration: macroDeclaration | procedureDeclaration | variableDeclaration | variableRedecleration;

macroDeclaration: MACRO_DEFINE NAME MACRO_AS macroValue;
macroValue: statement |;

procedureDeclaration: EXTERN NAME parameterDeclarations?;
parameterDeclarations: OPEN_PAREN (parameterDeclaration (COMMA parameterDeclaration)*)? CLOSE_PAREN;
parameterDeclaration
    : type                          #parameterDeclarationByValue 
    | VAR type arrayDeclaration?    #parameterDeclarationByReference
    ;
arrayDeclaration: OPEN_BRACKET first=expression? (COMMA second=expression?)? (COMMA third=expression?)? CLOSE_BRACKET;

variableDeclaration: DEFINE globalVariableModifiers type variableModifiers variableNameDeclaration (COMMA variableNameDeclaration)*;
globalVariableModifiers: range? preprocessingStop? accessRights?;
range: NCK | CHAN;
preprocessingStop: SYNR | SYNW | SYNRW;
accessRights: (accessDesignation intUnsigned)+;
accessDesignation: ACCESS_READ | ACCESS_WRITE | READ_PROGRAM | WRITE_PROGRAM | READ_OPI | WRITE_OPI;
variableModifiers: physicalUnit? limitValues?;
physicalUnit: PHYS_UNIT intUnsigned;
limitValues: ((LOWER_LIMIT | UPPER_LIMIT) numeric)+;

variableNameDeclaration: NAME (variableAssignmentExpression | arrayDefinition arrayAssignmentExpression?)?;

arrayDefinition: OPEN_BRACKET expression (COMMA expression)? (COMMA expression)? CLOSE_BRACKET;
variableAssignmentExpression: ASSIGNMENT expression;
arrayAssignmentExpression: ASSIGNMENT (expression | SET? freeArguments | REP OPEN_PAREN expression (COMMA expression)? CLOSE_PAREN);

variableRedecleration: REDEFINE (NAME | rParam | SYS_VAR) globalVariableModifiers variableModifiers;

// assignment
variableAssignment
    : NAME variableAssignmentExpression                     #userVariableAssignment
    | rParam variableAssignmentExpression                   #RParamAssignment
    | SYS_VAR variableAssignmentExpression                  #SysVarAssignment
    | NAME arrayDefinition? arrayAssignmentExpression       #arrayVariableAssignment
    | rParam arrayAssignmentExpression                      #arrayRParamAssignment
    | SYS_VAR arrayDefinition? arrayAssignmentExpression    #arraySysVarAssignment
    ;

type
    : BOOL_TYPE
    | CHAR_TYPE
    | INT_TYPE
    | REAL_TYPE
    | STRING_TYPE OPEN_BRACKET expression CLOSE_BRACKET
    | AXIS_TYPE
    | FRAME_TYPE;

// statement
statement
    : ifStatement
    | caseStatement
    | iterativeStatement
    | jumpStatement
    | syncActionStatement
    | expression
    | variableAssignment
    | command+
    | procedure
    | keyword;

ifStatement: IF expression (NEWLINE* scope ifStatementElse? lineStart? IF_END | gotoStatement);
ifStatementElse: lineStart? ELSE NEWLINE* scope;

caseStatement: CASE expression CASE_OF NEWLINE* (lineStart? primaryExpression gotoStatement NEWLINE*)+ (lineStart? CASE_DEFAULT gotoStatement)?;

iterativeStatement: iterativeWhile | iterativeFor | iterativeRepeat | iterativeLoop;
iterativeWhile: WHILE expression NEWLINE* scope lineStart? WHILE_END;
iterativeFor: FOR variableAssignment TO expression NEWLINE* scope lineStart? FOR_END;
iterativeRepeat: REPEAT NEWLINE* scope lineStart? REPEAT_END expression;
iterativeLoop: LOOP NEWLINE* scope lineStart? LOOP_END;

jumpStatement
    : gotoStatement
    | callStatement
    | returnStatement;

gotoStatement: (GOTO | GOTO_B | GOTO_C | GOTO_F) gotoTarget | GOTO_S;
gotoTarget
    : NAME              #gotoLabel
    | blockNumber       #gotoBlock
    ;

callStatement
    : CALL (expression | primaryExpression? CALL_BLOCK NAME TO NAME)                    #call
    | CALL_P primaryExpression ownProcedure?                                            #procedureCall
    | CALL_EXT OPEN_PAREN expression CLOSE_PAREN                                        #externalCall
    | CALL_PATH OPEN_PAREN expression? CLOSE_PAREN                                      #callPath
    | CALL_MODAL (NAME (OPEN_BRACKET expression (COMMA expression)* CLOSE_BRACKET)? )?  #modalCall
    ;

returnStatement: RETURN (OPEN_PAREN expression (COMMA expression?)? (COMMA expression?)? (COMMA expression)? CLOSE_PAREN)?;

syncActionStatement
    : syncActionId? syncActionCondition? SYNC_DO syncActionAction+ (ELSE syncActionAction+)?
    | SYNC_CANCEL OPEN_PAREN expression (COMMA expression)* CLOSE_PAREN;

syncActionId: (ID | IDS) ASSIGNMENT expression;

syncActionCondition: (SYNC_WHEN | SYNC_WHENEVER | SYNC_FROM | SYNC_EVERY) expression;

syncActionAction: command | procedure | variableAssignment;

expression
    : (NOT|NOT_B) primaryExpression                                                 #unaryExpression
    | (ADD|SUB) primaryExpression                                                   #signExpression
    | CONCAT primaryExpression                                                      #toStringExpression
    | expression (MUL|(DIV|SLASH)|MOD) expression                                   #multiplicativeExpression
    | expression (ADD|SUB) expression                                               #additiveExpression
    | expression AND_B expression                                                   #binaryAndExpression
    | expression XOR_B expression                                                   #binaryExclusiveOrExpression
    | expression OR_B expression                                                    #binaryInclusiveOrExpression
    | expression AND expression                                                     #andExpression
    | expression XOR expression                                                     #exclusiveOrExpression
    | expression OR expression                                                      #inclusiveOrExpression
    | expression CONCAT expression                                                  #concatExpression
    | expression (EQUAL|NOT_EQUAL|LESS_EQUAL|GREATER_EQUAL|LESS|GREATER) expression #relationalExpression
    | primaryExpression                                                             #primaryExpressionLabel
    ;

primaryExpression
    : NAME arrayDefinition?                 #variableUse // technically #symbolUse
    | SYS_VAR arrayDefinition?              #systemVariableUse
    | rParam                                #rParamUse
    | constant                              #constantUse
    | function                              #functionUse
    | OPEN_PAREN expression CLOSE_PAREN     #nestedExpression
    | axis_spindle_identifier               #axisUse
    | path                                  #pathUse
    | macroUse+                             #macroUseLabel
    ;

rParam: DOLLAR? R_PARAM (intUnsigned | OPEN_BRACKET expression CLOSE_BRACKET);

constant
    : numeric
    | HEX
    | BIN
    | STRING
    | BOOL;

numeric: intUnsigned | realUnsigned;
intUnsigned: INT_UNSIGNED;
realUnsigned: REAL_UNSIGNED;

macroUse: NAME;

path: pathElements+;
pathElements: SLASH | NAME;

// command
command
    : ACC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | ACCLIMA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | expression ASSIGNMENT ACN OPEN_PAREN expression CLOSE_PAREN
    | expression ASSIGNMENT ACP OPEN_PAREN expression CLOSE_PAREN
    | ADIS ASSIGNMENT expression
    | ADISPOS ASSIGNMENT expression
    | ALF freeArguments?
    | AMIRROR freeArguments?
    | ANG freeArguments?
    | AP freeArguments?
    | AR freeArguments?
    | AROT freeArguments?
    | AROTS freeArguments?
    | ASCALE freeArguments?
    | ASPLINE freeArguments?
    | ATOL freeArguments?
    | ATRANS freeArguments?
    | BAUTO freeArguments?
    | BNAT freeArguments?
    | BRISK
    | BRISKA freeArguments?
    | BSPLINE freeArguments?
    | BTAN freeArguments?
    | CDOF freeArguments?
    | CDOF2 freeArguments?
    | CDON freeArguments?
    | CFC freeArguments?
    | CFIN freeArguments?
    | CFTCP freeArguments?
    | CHF freeArguments?
    | CHR freeArguments?
    | CIP freeArguments?
    | COMPCAD freeArguments?
    | COMPCURV freeArguments?
    | COMPOF freeArguments?
    | COMPON freeArguments?
    | COMPPATH freeArguments?
    | COMPSURF freeArguments?
    | CP freeArguments?
    | CPRECOF freeArguments?
    | CPRECON freeArguments?
    | CR freeArguments?
    | CT freeArguments?
    | CTOL freeArguments?
    | CTOLG0 freeArguments?
    | CUT2D freeArguments?
    | CUT2DD freeArguments?
    | CUT2DF freeArguments?
    | CUT2DFD freeArguments?
    | CUT3DC freeArguments?
    | CUT3DCC freeArguments?
    | CUT3DCCD freeArguments?
    | CUT3DCD freeArguments?
    | CUT3DF freeArguments?
    | CUT3DFD freeArguments?
    | CUT3DFF freeArguments?
    | CUT3DFS freeArguments?
    | CUTCONOF freeArguments?
    | CUTCONON freeArguments?
    | CUTMOD freeArguments?
    | CUTMODK freeArguments?
    | D freeArguments?
    | D0 freeArguments?
    | DIAM90 freeArguments?
    | DIAMCYCOF freeArguments?
    | DIAMOF freeArguments?
    | DIAMON freeArguments?
    | DILF freeArguments?
    | DISC freeArguments?
    | DISCL freeArguments?
    | DISPR freeArguments?
    | DISR freeArguments?
    | DISRP freeArguments?
    | DITE freeArguments?
    | DITS freeArguments?
    | DL freeArguments?
    | DRIVE
    | DRIVEA freeArguments?
    | DYNFINISH freeArguments?
    | DYNNORM freeArguments?
    | DYNPOS freeArguments?
    | DYNPREC freeArguments?
    | DYNROUGH freeArguments?
    | DYNSEMIFIN freeArguments?
    | EAUTO freeArguments?
    | ENAT freeArguments?
    | ETAN freeArguments?
    | F (numeric | ASSIGNMENT expression)
    | FA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FAD freeArguments?
    | FB freeArguments?
    | FCUB freeArguments?
    | FD ASSIGNMENT expression
    | FENDNORM freeArguments?
    | FFWOF freeArguments?
    | FFWON freeArguments?
    | FIFOCTRL freeArguments?
    | FLIM freeArguments?
    | FLIN freeArguments?
    | FNORM freeArguments?
    | FP freeArguments?
    | FRC freeArguments?
    | FRCM freeArguments?
    | FTOCOF freeArguments?
    | FTOCON freeArguments?
    | FXS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FXST OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FXSW OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | GFRAME freeArguments?
    | I freeArguments?
    | I1 freeArguments?
    | INVCCW freeArguments?
    | INVCW freeArguments?
    | IR freeArguments?
    | ISD freeArguments?
    | J freeArguments?
    | J1 freeArguments?
    | JERKLIM OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | JERKLIMA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | JR freeArguments?
    | K freeArguments?
    | K1 freeArguments?
    | KONT freeArguments?
    | KONTC freeArguments?
    | KONTT freeArguments?
    | KR freeArguments?
    | L freeArguments?
    | LEAD freeArguments?
    | LFOF freeArguments?
    | LFON freeArguments?
    | LFPOS freeArguments?
    | LFTXT freeArguments?
    | LFWP freeArguments?
    | MEAC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT freeArguments?
    | MEAS ASSIGNMENT expression
    | MEASA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT freeArguments?
    | MEASF ASSIGNMENT expression
    | MEAW ASSIGNMENT expression
    | MEAWA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT freeArguments?
    | MIRROR freeArguments?
    | MOV OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | MOVT freeArguments?
    | NORM freeArguments?
    | OEMIPO1 freeArguments?
    | OEMIPO2 freeArguments?
    | OFFN freeArguments?
    | OMA freeArguments?
    | ORIANGLE freeArguments?
    | ORIAXES freeArguments?
    | ORIAXESFR freeArguments?
    | ORIAXPOS freeArguments?
    | ORIC freeArguments?
    | ORICONCCW freeArguments?
    | ORICONCW freeArguments?
    | ORICONIO freeArguments?
    | ORICONTO freeArguments?
    | ORICURINV freeArguments?
    | ORICURVE freeArguments?
    | ORID freeArguments?
    | ORIEULER freeArguments?
    | ORIMKS freeArguments?
    | ORIPATH freeArguments?
    | ORIPATHS freeArguments?
    | ORIPLANE freeArguments?
    | ORIROTA freeArguments?
    | ORIROTC freeArguments?
    | ORIROTR freeArguments?
    | ORIROTT freeArguments?
    | ORIRPY freeArguments?
    | ORIRPY2 freeArguments?
    | ORIS freeArguments?
    | ORISOF freeArguments?
    | ORISON freeArguments?
    | ORIVECT freeArguments?
    | ORIVIRT1 freeArguments?
    | ORIVIRT2 freeArguments?
    | ORIWKS freeArguments?
    | OSC freeArguments?
    | OSD freeArguments?
    | OSOF freeArguments?
    | OSS freeArguments?
    | OSSE freeArguments?
    | OST freeArguments?
    | OTOL freeArguments?
    | OTOLG0 freeArguments?
    | P freeArguments?
    | PACCLIM freeArguments?
    | PAROT freeArguments?
    | PAROTOF freeArguments?
    | PDELAYOF freeArguments?
    | PDELAYON freeArguments?
    | PL freeArguments?
    | POLY freeArguments?
    | PON freeArguments?
    | PONS freeArguments?
    | POS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT axisAssignmentExpression
    | POSA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT axisAssignmentExpression
    | POSM
    | POSP OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT OPEN_PAREN expression COMMA expression COMMA expression CLOSE_PAREN
    | PTP freeArguments?
    | PTPG0 freeArguments?
    | PTPWOC freeArguments?
    | PW freeArguments?
    | REPOSA freeArguments?
    | REPOSH freeArguments?
    | REPOSHA freeArguments?
    | REPOSL freeArguments?
    | REPOSQ freeArguments?
    | REPOSQA freeArguments?
    | RMB freeArguments?
    | RMBBL freeArguments?
    | RME freeArguments?
    | RMEBL freeArguments?
    | RMI freeArguments?
    | RMIBL freeArguments?
    | RMN freeArguments?
    | RMNBL freeArguments?
    | RND freeArguments?
    | RNDM ASSIGNMENT expression
    | ROT freeArguments?
    | ROTS freeArguments?
    | RP freeArguments?
    | RPL freeArguments?
    | RTLIOF freeArguments?
    | RTLION freeArguments?
    | SCALE freeArguments?
    | SD freeArguments?
    | SF freeArguments?
    | SOFT
    | SOFTA freeArguments?
    | SON freeArguments?
    | SONS freeArguments?
    | SPATH freeArguments?
    | SPIF1 freeArguments?
    | SPIF2 freeArguments?
    | SPN freeArguments?
    | SPOF freeArguments?
    | SPOS (OPEN_BRACKET expression CLOSE_BRACKET)? ASSIGNMENT axisAssignmentExpression
    | SPP freeArguments?
    | SR freeArguments?
    | ST freeArguments?
    | STARTFIFO freeArguments?
    | STOLF freeArguments?
    | STOPFIFO freeArguments?
    | SUPA freeArguments?
    | SUPD freeArguments?
    | T freeArguments?
    | TCARR freeArguments?
    | TCOABS freeArguments?
    | TCOFR freeArguments?
    | TCOFRX freeArguments?
    | TCOFRY freeArguments?
    | TCOFRZ freeArguments?
    | THETA freeArguments?
    | TILT freeArguments?
    | TOFF freeArguments?
    | TOFFL freeArguments?
    | TOFFLR freeArguments?
    | TOFFR freeArguments?
    | TOFRAME freeArguments?
    | TOFRAMEX freeArguments?
    | TOFRAMEY freeArguments?
    | TOFRAMEZ freeArguments?
    | TOROT freeArguments?
    | TOROTOF freeArguments?
    | TOROTX freeArguments?
    | TOROTY freeArguments?
    | TOROTZ freeArguments?
    | TOWBCS freeArguments?
    | TOWKCS freeArguments?
    | TOWMCS freeArguments?
    | TOWSTD freeArguments?
    | TOWTCS freeArguments?
    | TOWWCS freeArguments?
    | TRANS freeArguments?
    | TURN freeArguments?
    | UPATH freeArguments?
    | VELOLIM OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | VELOLIMA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | WALCS freeArguments?
    | WALIMOF freeArguments?
    | WALIMON freeArguments?
    | CALL_MODAL_OFF                // done
    | gCode
    | hCode
    | mCode
    | spindleSpeed
    | axisCode
    | macroUse
    ;

gCode: GCODE codeAssignment;
hCode: HCODE (codeAssignment | codeAssignmentParameterized);
mCode: MCODE (codeAssignment | codeAssignmentParameterized);

codeAssignment: intUnsigned | ASSIGNMENT codeAssignmentExpression; // m1 | m = 1
codeAssignmentExpression: expression | QU OPEN_PAREN expression CLOSE_PAREN;

codeAssignmentParameterized: commandParameterAssignment ASSIGNMENT codeAssignmentExpression; //m1 = 3 | m[1] = 3
commandParameterAssignment: intUnsigned | OPEN_BRACKET expression CLOSE_BRACKET;

spindleSpeed: S (speedAssignment | speedAssignmentParameterized);
speedAssignment:  numeric | ASSIGNMENT expression;
speedAssignmentParameterized: commandParameterAssignment ASSIGNMENT expression;

axisCode: AXIS SUB? numeric | expression ASSIGNMENT axisAssignmentExpression;
axisAssignmentExpression: expression | (AC | IC | CAC | CACN | CACP | CDC | CIC | DC) OPEN_PAREN expression CLOSE_PAREN;

// axis
// todo how about expressions that lead to an axis
axis_spindle_identifier: axis_identifier | spindle_identifier;
axis_identifier: AXIS intUnsigned? | AX OPEN_BRACKET expression CLOSE_BRACKET;
spindle_identifier: SPINDLE_IDENTIFIER OPEN_PAREN intUnsigned CLOSE_PAREN;

// procedure
procedure
    : predefinedProcedure   #predefinedProcedureUse
    | ownProcedure          #ownProcedureUse
    ;

ownProcedure: NAME arguments?;
arguments: OPEN_PAREN (expression (COMMA expression)*)? CLOSE_PAREN;

freeArguments: OPEN_PAREN expression? (COMMA expression?)* CLOSE_PAREN;

//// predefined procedure
predefinedProcedure
    : ACTBLOCNO
    | ADISPOSA freeArguments?
    | AFISOF freeArguments?
    | AFISON freeArguments?
    | AUXFUDEL freeArguments?
    | AUXFUDELG freeArguments?
    | AUXFUMSEQ freeArguments?
    | AUXFUSYNC freeArguments?
    | AXCTSWE freeArguments?
    | AXCTSWEC freeArguments?
    | AXCTSWED freeArguments?
    | AXTOCHAN freeArguments?
    | CADAPTOF freeArguments?
    | CADAPTON freeArguments?
    | CALCFIR freeArguments?
    | CANCELSUB freeArguments?
    | CHANDATA freeArguments?
    | CLEARM freeArguments?
    | CLRINT freeArguments?
    | CONTDCON freeArguments?
    | CONTPRON freeArguments?
    | CORROF freeArguments?
    | COUPDEF freeArguments?
    | COUPDEL freeArguments?
    | COUPOF freeArguments?
    | COUPOFS freeArguments?
    | COUPON freeArguments?
    | COUPONC freeArguments?
    | COUPRES freeArguments?
    | CPROT freeArguments?
    | CPROTDEF freeArguments?
    | CTABDEF freeArguments?
    | CTABDEL freeArguments?
    | CTABEND freeArguments?
    | CTABLOCK freeArguments?
    | CTABUNLOCK freeArguments?
    | DELAYFSTOF freeArguments?
    | DELAYFSTON freeArguments?
    | DELDTG freeArguments?
    | DELETE freeArguments?
    | DELMT freeArguments?
    | DELT freeArguments?
    | DELTC freeArguments?
    | DISABLE freeArguments?
    | DRFOF freeArguments?
    | DRVPRD freeArguments?
    | DRVPWR freeArguments?
    | DZERO freeArguments?
    | EGDEF freeArguments?
    | EGDEL freeArguments?
    | EGOFC freeArguments?
    | EGOFS freeArguments?
    | EGON freeArguments?
    | EGONSYN freeArguments?
    | EGONSYNE freeArguments?
    | ENABLE freeArguments?
    | ESRR freeArguments?
    | ESRS freeArguments?
    | EXECSTRING freeArguments?
    | EXECTAB freeArguments?
    | EXECUTE freeArguments?
    | EXTCLOSE freeArguments?
    | EXTOPEN freeArguments?
    | FCTDEF freeArguments?
    | FGROUP OPEN_PAREN CLOSE_PAREN
    | FILEDATE freeArguments?
    | FILEINFO freeArguments?
    | FILESIZE freeArguments?
    | FILESTAT freeArguments?
    | FILETIME freeArguments?
    | FPR freeArguments?
    | FPRAOF freeArguments?
    | FPRAON freeArguments?
    | FTOC freeArguments?
    | GEOAX freeArguments?
    | GET freeArguments?
    | GETD freeArguments?
    | GETEXET freeArguments?
    | GETFREELOC freeArguments?
    | GETSELT freeArguments?
    | GWPSOF freeArguments?
    | GWPSON freeArguments?
    | ICYCOF freeArguments?
    | ICYCON freeArguments?
    | INIT freeArguments?
    | IPOBRKA freeArguments?
    | IPTRLOCK freeArguments?
    | IPTRUNLOCK freeArguments?
    | JERKA freeArguments?
    | LEADOF freeArguments?
    | LEADON freeArguments?
    | LOCK freeArguments?
    | MASLDEF freeArguments?
    | MASLDEL freeArguments?
    | MASLOF freeArguments?
    | MASLOFS freeArguments?
    | MASLON freeArguments?
    | MMC freeArguments?
    | MSG OPEN_PAREN expression? CLOSE_PAREN
    | MVTOOL freeArguments?
    | NEWCONF freeArguments?
    | NPROT freeArguments?
    | NPROTDEF freeArguments?
    | ORIRESET freeArguments?
    | POLFA freeArguments?
    | POLFMASK freeArguments?
    | POLFMLIN freeArguments?
    | POLYPATH freeArguments?
    | POSM freeArguments?
    | POSMT freeArguments?
    | PRESETON freeArguments?
    | PRESETONS freeArguments?
    | PROTA freeArguments?
    | PROTS freeArguments?
    | PUNCHACC freeArguments?
    | PUTFTOC freeArguments?
    | PUTFTOCF freeArguments?
    | RDISABLE freeArguments?
    | READ freeArguments?
    | RELEASE freeArguments?
    | RESETMON freeArguments?
    | RETB freeArguments?
    | SBLOF
    | SBLON
    | SETAL freeArguments?
    | SETM freeArguments?
    | SETMS freeArguments?
    | SETMTH freeArguments?
    | SETPIECE freeArguments?
    | SETTA freeArguments?
    | SETTIA freeArguments?
    | SIRELIN freeArguments?
    | SIRELOUT freeArguments?
    | SIRELTIME freeArguments?
    | SPCOF freeArguments?
    | SPCON freeArguments?
    | SPLINEPATH freeArguments?
    | START freeArguments?
    | STOPRE freeArguments?
    | STOPREOF freeArguments?
    | SYNFCT freeArguments?
    | TANG freeArguments?
    | TANGDEL freeArguments?
    | TANGOF freeArguments?
    | TANGON freeArguments?
    | TCA freeArguments?
    | TCI freeArguments?
    | TLIFT freeArguments?
    | TML freeArguments?
    | TMOF freeArguments?
    | TMON freeArguments?
    | TOFFOF freeArguments?
    | TOFFON freeArguments?
    | TRAANG freeArguments?
    | TRACON freeArguments?
    | TRACYL freeArguments?
    | TRAFOOF freeArguments?
    | TRAFOON freeArguments?
    | TRAILOF freeArguments?
    | TRAILON freeArguments?
    | TRANSMIT freeArguments?
    | TRAORI freeArguments?
    | UNLOCK freeArguments?
    | WAITC freeArguments?
    | WAITE freeArguments?
    | WAITENC freeArguments?
    | WAITM freeArguments?
    | WAITMC freeArguments?
    | WAITP freeArguments?
    | WAITS freeArguments?
    | WRITE freeArguments?
    | WRTPR freeArguments?
    ;


// function
function
    : mathFunction
    | stringFunction
    | CTAB freeArguments?
    | CTABEXISTS freeArguments?
    | CTABFNO freeArguments?
    | CTABFPOL freeArguments?
    | CTABFSEG freeArguments?
    | CTABID freeArguments?
    | CTABINV freeArguments?
    | CTABISLOCK freeArguments?
    | CTABMEMTYP freeArguments?
    | CTABMPOL freeArguments?
    | CTABMSEG freeArguments?
    | CTABNO freeArguments?
    | CTABNOMEM freeArguments?
    | CTABPERIOD freeArguments?
    | CTABPOL freeArguments?
    | CTABPOLID freeArguments?
    | CTABSEG freeArguments?
    | CTABSEGID freeArguments?
    | CTABSEV freeArguments?
    | CTABSSV freeArguments?
    | CTABTEP freeArguments?
    | CTABTEV freeArguments?
    | CTABTMAX freeArguments?
    | CTABTMIN freeArguments?
    | CTABTSP freeArguments?
    | CTABTSV freeArguments?
    | ADDFRAME freeArguments?
    | AXTOSPI freeArguments?
    | CALCPOSI freeArguments?
    | CALCTRAVAR freeArguments?
    | CFINE freeArguments?
    | CHKDM freeArguments?
    | CHKDNO freeArguments?
    | COLLPAIR freeArguments?
    | CORRTC freeArguments?
    | CORRTRAFO freeArguments?
    | CSPLINE freeArguments?
    | DELDL freeArguments?
    | DELMLOWNER freeArguments?
    | DELMLRES freeArguments?
    | DELOBJ freeArguments?
    | DELTOOLENV freeArguments?
    | GETACTT freeArguments?
    | GETACTTD freeArguments?
    | GETDNO freeArguments?
    | GETT freeArguments?
    | GETTCOR freeArguments?
    | GETTENV freeArguments?
    | GETVARAP freeArguments?
    | GETVARDFT freeArguments?
    | GETVARDIM freeArguments?
    | GETVARLIM freeArguments?
    | GETVARPHU freeArguments?
    | GETVARTYP freeArguments?
    | INTERSEC freeArguments?
    | INVFRAME freeArguments?
    | ISAXIS freeArguments?
    | ISFILE freeArguments?
    | ISVAR freeArguments?
    | LENTOAX freeArguments?
    | MEAFRAME freeArguments?
    | MEASURE freeArguments?
    | MODAXVAL freeArguments?
    | NAMETOINT freeArguments?
    | NEWMT freeArguments?
    | NEWT freeArguments?
    | ORISOLH freeArguments?
    | POSRANGE freeArguments?
    | PROTD freeArguments?
    | SETDNO freeArguments?
    | SETTCOR freeArguments?
    | SIRELAY freeArguments?
    | TOOLENV freeArguments?
    | TOOLGNT freeArguments?
    | TOOLGT freeArguments?
    ;

mathFunction // done
    : SIN OPEN_PAREN expression CLOSE_PAREN
    | COS OPEN_PAREN expression CLOSE_PAREN
    | TAN OPEN_PAREN expression CLOSE_PAREN
    | ASIN OPEN_PAREN expression CLOSE_PAREN
    | ACOS OPEN_PAREN expression CLOSE_PAREN
    | ATAN2 OPEN_PAREN expression COMMA expression CLOSE_PAREN
    | EXP OPEN_PAREN expression CLOSE_PAREN
    | SQRT OPEN_PAREN expression CLOSE_PAREN
    | POT OPEN_PAREN expression (COMMA expression)? CLOSE_PAREN
    | LN OPEN_PAREN expression CLOSE_PAREN
    | ABS OPEN_PAREN expression CLOSE_PAREN
    | TRUNC OPEN_PAREN expression CLOSE_PAREN
    | ROUND OPEN_PAREN expression CLOSE_PAREN
    | ROUNDUP OPEN_PAREN expression CLOSE_PAREN
    | MINVAL OPEN_PAREN expression COMMA expression CLOSE_PAREN
    | MAXVAL OPEN_PAREN expression COMMA expression CLOSE_PAREN
    | BOUND OPEN_PAREN expression COMMA expression COMMA expression CLOSE_PAREN
    | CALCDAT OPEN_PAREN expression COMMA expression COMMA NAME CLOSE_PAREN
    ;

stringFunction // done
    : STRLEN OPEN_PAREN expression CLOSE_PAREN
    | SPRINT OPEN_PAREN expression (COMMA expression)* CLOSE_PAREN
    | STRINGIS OPEN_PAREN expression CLOSE_PAREN
    | SUBSTR OPEN_PAREN expression COMMA expression (COMMA expression)? CLOSE_PAREN
    | AXNAME OPEN_PAREN expression CLOSE_PAREN
    | AXSTRING OPEN_PAREN expression CLOSE_PAREN
    | INDEX OPEN_PAREN expression COMMA expression CLOSE_PAREN
    | MINDEX OPEN_PAREN expression COMMA expression CLOSE_PAREN
    | RINDEX OPEN_PAREN expression COMMA expression CLOSE_PAREN
    | ISNUMBER OPEN_PAREN expression CLOSE_PAREN
    | NUMBER OPEN_PAREN expression CLOSE_PAREN
    | MATCH OPEN_PAREN expression COMMA expression CLOSE_PAREN
    | TOLOWER OPEN_PAREN expression CLOSE_PAREN
    | TOUPPER OPEN_PAREN expression CLOSE_PAREN
    ;

keyword
    : APX
    | BLSYNC
    | COARSEA
    | CPBC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPDEF ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPDEL ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPFMOF OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPFMON OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPFMSON OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | cpfpos
    | CPFRS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPLCTID OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLDEF OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPLDEL OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPLDEN OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLINSC OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLINTR OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLNUM OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLOF OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLON OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLOUTSC OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLOUTTR OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | cplpos
    | CPLSETVAL OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMALARM OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMBRAKE OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMPRT OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMRESET OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMSTART OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMVDI OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | cpof cpfpos?
    | cpon cpfpos? cplpos* cpfpos?
    | CPRES ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPSETTYPE OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPSYNCOP OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPSYNCOP2 OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPSYNCOV OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPSYNFIP OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPSYNFIP2 OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPSYNFIV OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | DAC
    | DIACYCOFA
    | DIAM90A
    | DIAMCHAN
    | DIAMCHANA
    | DIAMOFA
    | DIAMONA
    | DIC
    | FDA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FGREF
    | FI
    | FINEA
    | FL
    | FMA
    | FOC
    | FOCOF
    | FOCON
    | FPO
    | FZ
    | GP
    | ID
    | IDS
    | INICF
    | INIPO
    | INIRE
    | IP
    | IPOENDA
    | ISOCALL
    | LIFTFAST
    | LIMS
    | MI
    | OS
    | OSB
    | OSCILL
    | OSCTRL
    | OSE
    | OSNSC
    | OSP1
    | OSP2
    | OST1
    | OST2
    | OVR ASSIGNMENT expression
    | OVRA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | OVRRAP ASSIGNMENT expression
    | PHI
    | PHU
    | PM
    | PO
    | POLF OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT axisAssignmentExpression
    | PR
    | PRIO
    | PRLOC
    | PSISYNRW
    | RAC
    | RIC
    | RT
    | SC
    | SCC
    | SCPARA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | SETINT
    | SPOSA
    | SRA
    | STA
    | SVC
    | TR
    ;

cpon: CPON ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN;
cpof: CPOF ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN;
cplpos: CPLPOS OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression;
cpfpos: CPFPOS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression;