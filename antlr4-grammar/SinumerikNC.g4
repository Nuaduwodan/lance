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

ITOR: 'itor';
RTOI: 'rtoi';

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
SYS_VAR: ('$$' | '$'[$acmnopstv]+ | 'syg_')[a-z0-9_]*; // could be improved
AXIS: [abcquvwxyz];
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
procedureModifier: SBLOF | DISPLON | DISPLOF | ACTBLOCNO | SAVE;

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
limitValues: ((LOWER_LIMIT | UPPER_LIMIT) expression)+;

variableNameDeclaration: NAME (variableAssignmentExpression | arrayDefinition arrayAssignmentExpression?)?;

arrayDefinition: OPEN_BRACKET expression (COMMA expression)? (COMMA expression)? CLOSE_BRACKET;
variableAssignmentExpression: ASSIGNMENT expression;
arrayAssignmentExpression: ASSIGNMENT (expression | SET? arguments | REP OPEN_PAREN expression (COMMA expression)? CLOSE_PAREN);

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
    | frameComponent                        #frameComponentUse
    | macroUse+                             #macroUseLabel
    ;

rParam: DOLLAR? R_PARAM (intUnsigned | OPEN_BRACKET expression CLOSE_BRACKET);

constant
    : numericUnsigned
    | HEX
    | BIN
    | STRING
    | BOOL;

numericUnsigned: intUnsigned | realUnsigned;
intUnsigned: INT_UNSIGNED;
realUnsigned: REAL_UNSIGNED;

macroUse: NAME;

path: pathElements+;
pathElements: SLASH | NAME;

frameComponent: TR | FI | RT | SC | MI;

// command
command
    : ACC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | ACCLIMA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | ADIS ASSIGNMENT expression
    | ADISPOS ASSIGNMENT expression
    | ALF arguments?
    | AMIRROR arguments?
    | ANG arguments?
    | AP arguments?
    | AR arguments?
    | AROT arguments?
    | AROTS arguments?
    | ASCALE arguments?
    | ASPLINE arguments?
    | ATOL arguments?
    | ATRANS arguments?
    | BAUTO arguments?
    | BNAT arguments?
    | BRISK
    | BRISKA arguments?
    | BSPLINE arguments?
    | BTAN arguments?
    | CDOF arguments?
    | CDOF2 arguments?
    | CDON arguments?
    | CFC arguments?
    | CFIN arguments?
    | CFTCP arguments?
    | CHF arguments?
    | CHR arguments?
    | CIP arguments?
    | COMPCAD arguments?
    | COMPCURV arguments?
    | COMPOF arguments?
    | COMPON arguments?
    | COMPPATH arguments?
    | COMPSURF arguments?
    | CP arguments?
    | CPRECOF arguments?
    | CPRECON arguments?
    | CR arguments?
    | CT arguments?
    | CTOL arguments?
    | CTOLG0 arguments?
    | CUT2D arguments?
    | CUT2DD arguments?
    | CUT2DF arguments?
    | CUT2DFD arguments?
    | CUT3DC arguments?
    | CUT3DCC arguments?
    | CUT3DCCD arguments?
    | CUT3DCD arguments?
    | CUT3DF arguments?
    | CUT3DFD arguments?
    | CUT3DFF arguments?
    | CUT3DFS arguments?
    | CUTCONOF arguments?
    | CUTCONON arguments?
    | CUTMOD arguments?
    | CUTMODK arguments?
    | D arguments?
    | D0 arguments?
    | DIAM90 arguments?
    | DIAMCYCOF arguments?
    | DIAMOF arguments?
    | DIAMON arguments?
    | DILF arguments?
    | DISC arguments?
    | DISCL arguments?
    | DISPR arguments?
    | DISR arguments?
    | DISRP arguments?
    | DITE arguments?
    | DITS arguments?
    | DL arguments?
    | DRIVE
    | DRIVEA arguments?
    | DYNFINISH arguments?
    | DYNNORM arguments?
    | DYNPOS arguments?
    | DYNPREC arguments?
    | DYNROUGH arguments?
    | DYNSEMIFIN arguments?
    | EAUTO arguments?
    | ENAT arguments?
    | ETAN arguments?
    | F (numericUnsigned | ASSIGNMENT expression)
    | FA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FAD arguments?
    | FB arguments?
    | FCUB arguments?
    | FD ASSIGNMENT expression
    | FENDNORM arguments?
    | FFWOF arguments?
    | FFWON arguments?
    | FIFOCTRL arguments?
    | FLIM arguments?
    | FLIN arguments?
    | FNORM arguments?
    | FP arguments?
    | FRC arguments?
    | FRCM arguments?
    | FTOCOF arguments?
    | FTOCON arguments?
    | FXS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FXST OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FXSW OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | GFRAME arguments?
    | I arguments?
    | I1 arguments?
    | INVCCW arguments?
    | INVCW arguments?
    | IR arguments?
    | ISD arguments?
    | J arguments?
    | J1 arguments?
    | JERKLIM OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | JERKLIMA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | JR arguments?
    | K arguments?
    | K1 arguments?
    | KONT arguments?
    | KONTC arguments?
    | KONTT arguments?
    | KR arguments?
    | L arguments?
    | LEAD arguments?
    | LFOF arguments?
    | LFON arguments?
    | LFPOS arguments?
    | LFTXT arguments?
    | LFWP arguments?
    | MEAC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT arguments?
    | MEAS ASSIGNMENT expression
    | MEASA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT arguments?
    | MEASF ASSIGNMENT expression
    | MEAW ASSIGNMENT expression
    | MEAWA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT arguments?
    | MIRROR arguments?
    | MOV OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | MOVT arguments?
    | NORM arguments?
    | OEMIPO1 arguments?
    | OEMIPO2 arguments?
    | OFFN arguments?
    | OMA arguments?
    | ORIANGLE arguments?
    | ORIAXES arguments?
    | ORIAXESFR arguments?
    | ORIAXPOS arguments?
    | ORIC arguments?
    | ORICONCCW arguments?
    | ORICONCW arguments?
    | ORICONIO arguments?
    | ORICONTO arguments?
    | ORICURINV arguments?
    | ORICURVE arguments?
    | ORID arguments?
    | ORIEULER arguments?
    | ORIMKS arguments?
    | ORIPATH arguments?
    | ORIPATHS arguments?
    | ORIPLANE arguments?
    | ORIROTA arguments?
    | ORIROTC arguments?
    | ORIROTR arguments?
    | ORIROTT arguments?
    | ORIRPY arguments?
    | ORIRPY2 arguments?
    | ORIS arguments?
    | ORISOF arguments?
    | ORISON arguments?
    | ORIVECT arguments?
    | ORIVIRT1 arguments?
    | ORIVIRT2 arguments?
    | ORIWKS arguments?
    | OSC arguments?
    | OSD arguments?
    | OSOF arguments?
    | OSS arguments?
    | OSSE arguments?
    | OST arguments?
    | OTOL arguments?
    | OTOLG0 arguments?
    | P arguments?
    | PACCLIM arguments?
    | PAROT arguments?
    | PAROTOF arguments?
    | PDELAYOF arguments?
    | PDELAYON arguments?
    | PL arguments?
    | POLY arguments?
    | PON arguments?
    | PONS arguments?
    | POS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT axisAssignmentExpression
    | POSA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT axisAssignmentExpression
    | POSM
    | POSP OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT OPEN_PAREN expression COMMA expression COMMA expression CLOSE_PAREN
    | PTP arguments?
    | PTPG0 arguments?
    | PTPWOC arguments?
    | PW arguments?
    | REPOSA arguments?
    | REPOSH arguments?
    | REPOSHA arguments?
    | REPOSL arguments?
    | REPOSQ arguments?
    | REPOSQA arguments?
    | RMB arguments?
    | RMBBL arguments?
    | RME arguments?
    | RMEBL arguments?
    | RMI arguments?
    | RMIBL arguments?
    | RMN arguments?
    | RMNBL arguments?
    | RND arguments?
    | RNDM ASSIGNMENT expression
    | ROT arguments?
    | ROTS arguments?
    | RP arguments?
    | RPL arguments?
    | RTLIOF arguments?
    | RTLION arguments?
    | S (numericUnsigned | commandParameterAssignment? ASSIGNMENT expression)
    | SCALE arguments?
    | SCPARA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | SD arguments?
    | SETINT OPEN_PAREN expression CLOSE_PAREN (PRIO ASSIGNMENT expression)? ownProcedure (LIFTFAST | BLSYNC)?
    | SF arguments?
    | SOFT
    | SOFTA arguments?
    | SON arguments?
    | SONS arguments?
    | SPATH arguments?
    | SPIF1 arguments?
    | SPIF2 arguments?
    | SPN arguments?
    | SPOF arguments?
    | SPOS (OPEN_BRACKET expression CLOSE_BRACKET)? ASSIGNMENT axisAssignmentExpression
    | SPOSA (OPEN_BRACKET expression CLOSE_BRACKET)? ASSIGNMENT axisAssignmentExpression
    | SPP arguments?
    | SR arguments?
    | ST arguments?
    | STARTFIFO arguments?
    | STOLF arguments?
    | STOPFIFO arguments?
    | SUPA arguments?
    | SUPD arguments?
    | T arguments?
    | TCARR arguments?
    | TCOABS arguments?
    | TCOFR arguments?
    | TCOFRX arguments?
    | TCOFRY arguments?
    | TCOFRZ arguments?
    | THETA arguments?
    | TILT arguments?
    | TOFF arguments?
    | TOFFL arguments?
    | TOFFLR arguments?
    | TOFFR arguments?
    | TOFRAME arguments?
    | TOFRAMEX arguments?
    | TOFRAMEY arguments?
    | TOFRAMEZ arguments?
    | TOROT arguments?
    | TOROTOF arguments?
    | TOROTX arguments?
    | TOROTY arguments?
    | TOROTZ arguments?
    | TOWBCS arguments?
    | TOWKCS arguments?
    | TOWMCS arguments?
    | TOWSTD arguments?
    | TOWTCS arguments?
    | TOWWCS arguments?
    | TRANS arguments?
    | TURN arguments?
    | UPATH arguments?
    | VELOLIM OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | VELOLIMA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | WALCS arguments?
    | WALIMOF arguments?
    | WALIMON arguments?
    | CALL_MODAL_OFF                // done
    | gCode
    | hCode
    | mCode
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

// is this SUB? really valid, if so what about all the other numericUnsigned uses?
axisCode: AXIS SUB? numericUnsigned | expression ASSIGNMENT axisAssignmentExpression;
axisAssignmentExpression: expression | (AC | ACN | ACP | CAC | CACN | CACP | DC | IC | CDC | CIC) OPEN_PAREN expression CLOSE_PAREN;

// axis
// todo how about expressions that lead to an axis
axis_spindle_identifier: axis_identifier | spindle_identifier;
axis_identifier: AXIS intUnsigned? | AX OPEN_BRACKET expression CLOSE_BRACKET;
spindle_identifier: SPINDLE_IDENTIFIER OPEN_PAREN expression CLOSE_PAREN;

// procedure
procedure
    : predefinedProcedure   #predefinedProcedureUse
    | ownProcedure          #ownProcedureUse
    ;

ownProcedure: NAME arguments?;
arguments: OPEN_PAREN expression? (COMMA expression?)* CLOSE_PAREN;

//// predefined procedure
predefinedProcedure
    : ACTBLOCNO
    | ADISPOSA arguments?
    | AFISOF arguments?
    | AFISON arguments?
    | AUXFUDEL arguments?
    | AUXFUDELG arguments?
    | AUXFUMSEQ arguments?
    | AUXFUSYNC arguments?
    | AXCTSWE arguments?
    | AXCTSWEC arguments?
    | AXCTSWED arguments?
    | AXTOCHAN arguments?
    | CADAPTOF arguments?
    | CADAPTON arguments?
    | CALCFIR arguments?
    | CANCELSUB arguments?
    | CHANDATA arguments?
    | CLEARM arguments?
    | CLRINT arguments?
    | CONTDCON arguments?
    | CONTPRON arguments?
    | CORROF arguments?
    | COUPDEF OPEN_PAREN expression (COMMA expression?)* CLOSE_PAREN
    | COUPDEL OPEN_PAREN expression (COMMA expression?)* CLOSE_PAREN
    | COUPOF OPEN_PAREN expression (COMMA expression?)* CLOSE_PAREN
    | COUPOFS OPEN_PAREN expression (COMMA expression?)* CLOSE_PAREN
    | COUPON OPEN_PAREN expression (COMMA expression?)* CLOSE_PAREN
    | COUPONC OPEN_PAREN expression (COMMA expression?)* CLOSE_PAREN
    | COUPRES OPEN_PAREN expression (COMMA expression?)* CLOSE_PAREN
    | CPROT arguments?
    | CPROTDEF arguments?
    | CTABDEF arguments?
    | CTABDEL arguments?
    | CTABEND arguments?
    | CTABLOCK arguments?
    | CTABUNLOCK arguments?
    | DELAYFSTOF arguments?
    | DELAYFSTON arguments?
    | DELDTG arguments?
    | DELETE arguments?
    | DELMT arguments?
    | DELT arguments?
    | DELTC arguments?
    | DISABLE arguments?
    | DRFOF arguments?
    | DRVPRD arguments?
    | DRVPWR arguments?
    | DZERO arguments?
    | EGDEF arguments?
    | EGDEL arguments?
    | EGOFC arguments?
    | EGOFS arguments?
    | EGON arguments?
    | EGONSYN arguments?
    | EGONSYNE arguments?
    | ENABLE arguments?
    | ESRR arguments?
    | ESRS arguments?
    | EXECSTRING arguments?
    | EXECTAB arguments?
    | EXECUTE arguments?
    | EXTCLOSE arguments?
    | EXTOPEN arguments?
    | FCTDEF arguments?
    | FGROUP OPEN_PAREN (expression (COMMA expression)*)? CLOSE_PAREN
    | FILEDATE arguments?
    | FILEINFO arguments?
    | FILESIZE arguments?
    | FILESTAT arguments?
    | FILETIME arguments?
    | FPR arguments?
    | FPRAOF arguments?
    | FPRAON arguments?
    | FTOC arguments?
    | GEOAX arguments?
    | GET arguments?
    | GETD arguments?
    | GETEXET arguments?
    | GETFREELOC arguments?
    | GETSELT arguments?
    | GWPSOF arguments?
    | GWPSON arguments?
    | ICYCOF arguments?
    | ICYCON arguments?
    | INIT arguments?
    | IPOBRKA arguments?
    | IPTRLOCK arguments?
    | IPTRUNLOCK arguments?
    | JERKA arguments?
    | LEADOF arguments?
    | LEADON arguments?
    | LOCK arguments?
    | MASLDEF arguments?
    | MASLDEL arguments?
    | MASLOF arguments?
    | MASLOFS arguments?
    | MASLON arguments?
    | MMC arguments?
    | MSG OPEN_PAREN expression? CLOSE_PAREN
    | MVTOOL arguments?
    | NEWCONF arguments?
    | NPROT arguments?
    | NPROTDEF arguments?
    | ORIRESET arguments?
    | POLFA arguments?
    | POLFMASK arguments?
    | POLFMLIN arguments?
    | POLYPATH arguments?
    | POSM arguments?
    | POSMT arguments?
    | PRESETON arguments?
    | PRESETONS arguments?
    | PROTA arguments?
    | PROTS arguments?
    | PUNCHACC arguments?
    | PUTFTOC arguments?
    | PUTFTOCF arguments?
    | RDISABLE arguments?
    | READ arguments?
    | RELEASE arguments?
    | RESETMON arguments?
    | RETB arguments?
    | SBLOF
    | SBLON
    | SETAL arguments?
    | SETM arguments?
    | SETMS arguments?
    | SETMTH arguments?
    | SETPIECE arguments?
    | SETTA arguments?
    | SETTIA arguments?
    | SIRELIN arguments?
    | SIRELOUT arguments?
    | SIRELTIME arguments?
    | SPCOF arguments?
    | SPCON arguments?
    | SPLINEPATH arguments?
    | START arguments?
    | STOPRE arguments?
    | STOPREOF arguments?
    | SYNFCT arguments?
    | TANG arguments?
    | TANGDEL arguments?
    | TANGOF arguments?
    | TANGON arguments?
    | TCA arguments?
    | TCI arguments?
    | TLIFT arguments?
    | TML arguments?
    | TMOF arguments?
    | TMON arguments?
    | TOFFOF arguments?
    | TOFFON arguments?
    | TRAANG arguments?
    | TRACON arguments?
    | TRACYL arguments?
    | TRAFOOF arguments?
    | TRAFOON arguments?
    | TRAILOF arguments?
    | TRAILON arguments?
    | TRANSMIT arguments?
    | TRAORI arguments?
    | UNLOCK arguments?
    | WAITC OPEN_PAREN expression (COMMA expression?)? (COMMA expression)? (COMMA expression)? CLOSE_PAREN
    | WAITE arguments?
    | WAITENC arguments?
    | WAITM arguments?
    | WAITMC arguments?
    | WAITP arguments?
    | WAITS arguments?
    | WRITE arguments?
    | WRTPR arguments?
    ;


// function
function
    : mathFunction
    | stringFunction
    | CTAB arguments?
    | CTABEXISTS arguments?
    | CTABFNO arguments?
    | CTABFPOL arguments?
    | CTABFSEG arguments?
    | CTABID arguments?
    | CTABINV arguments?
    | CTABISLOCK arguments?
    | CTABMEMTYP arguments?
    | CTABMPOL arguments?
    | CTABMSEG arguments?
    | CTABNO arguments?
    | CTABNOMEM arguments?
    | CTABPERIOD arguments?
    | CTABPOL arguments?
    | CTABPOLID arguments?
    | CTABSEG arguments?
    | CTABSEGID arguments?
    | CTABSEV arguments?
    | CTABSSV arguments?
    | CTABTEP arguments?
    | CTABTEV arguments?
    | CTABTMAX arguments?
    | CTABTMIN arguments?
    | CTABTSP arguments?
    | CTABTSV arguments?
    | ADDFRAME arguments?
    | AXTOSPI arguments?
    | CALCPOSI arguments?
    | CALCTRAVAR arguments?
    | CFINE arguments?
    | CHKDM arguments?
    | CHKDNO arguments?
    | COLLPAIR arguments?
    | CORRTC arguments?
    | CORRTRAFO arguments?
    | CSPLINE arguments?
    | DELDL arguments?
    | DELMLOWNER arguments?
    | DELMLRES arguments?
    | DELOBJ arguments?
    | DELTOOLENV arguments?
    | GETACTT arguments?
    | GETACTTD arguments?
    | GETDNO arguments?
    | GETT arguments?
    | GETTCOR arguments?
    | GETTENV arguments?
    | GETVARAP arguments?
    | GETVARDFT arguments?
    | GETVARDIM arguments?
    | GETVARLIM arguments?
    | GETVARPHU arguments?
    | GETVARTYP arguments?
    | INTERSEC arguments?
    | INVFRAME arguments?
    | ISAXIS arguments?
    | ISFILE arguments?
    | ISVAR arguments?
    | LENTOAX arguments?
    | MEAFRAME arguments?
    | MEASURE arguments?
    | MODAXVAL arguments?
    | NAMETOINT arguments?
    | NEWMT arguments?
    | NEWT arguments?
    | ORISOLH arguments?
    | POSRANGE arguments?
    | PROTD arguments?
    | SETDNO arguments?
    | SETTCOR arguments?
    | SIRELAY arguments?
    | TOOLENV arguments?
    | TOOLGNT arguments?
    | TOOLGT arguments?
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
    | ITOR OPEN_PAREN expression CLOSE_PAREN
    | RTOI OPEN_PAREN expression CLOSE_PAREN
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
    | FGREF OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
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
    | LIMS
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
    | PRLOC
    | PSISYNRW
    | RAC
    | RIC
    | SCC
    | SRA
    | STA
    | SVC
    ;

cpon: CPON ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN;
cpof: CPOF ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN;
cplpos: CPLPOS OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression;
cpfpos: CPFPOS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression;