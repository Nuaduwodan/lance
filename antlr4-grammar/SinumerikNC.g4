grammar SinumerikNC;

// it is not possible to use regex flags but the options can be overwritten per rule
// see: https://github.com/antlr/antlr4/blob/master/doc/lexer-rules.md#lexer-rule-options
options { caseInsensitive=true; }

/*
 * Lexer Rules
 */

// general
WHITESPACE: [ \t]+ -> skip;
NEWLINE: ('\r' '\n'? | '\n') -> skip;
COMMENT: ';' ~[\r\n]* -> skip;
HIDE: [ \t]*'/'[0-7]?;

////
//// constant
////
// numeric
INT: [0-9]+;
REAL: [0-9]* POINT [0-9]+;
BIN: '\'B'[01]+'\'';
HEX: '\'H'[0-9A-F]+'\'';

// language
BOOL: 'true'|'false';
PI: '$PI';
STRING: '"'~[\r\n]*'"';

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
ACC: 'acc';
ACCLIMA: 'acclima';
ACN: 'acn';
ACP: 'acp';
APR: 'apr';
APW: 'apw';
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
DCI: 'dci';
DCM: 'dcm';
DCU: 'dcu';
DIACYCOFA: 'diacycofa';
DIAM90A: 'diam90a';
DIAMCHAN: 'diamchan';
DIAMCHANA: 'diamchana';
DIAMOFA: 'diamofa';
DIAMONA: 'diamona';
DIC: 'dic';
EX: 'ex';
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
FROM: 'from';
FXS: 'fxs';
FXST: 'fxst';
FXSW: 'fxsw';
FZ: 'fz';
GP: 'gp';
IC: 'ic';
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
SYNR: 'synr';
SYNRW: 'synrw';
SYNW: 'synw';
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
RINDEX: 'rindex';
SETDNO: 'setdno';
SETTCOR: 'settcor';
SIRELAY: 'sirelay';
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
GCODE:'g' [0-9]?[0-9]?[1-9];
GFRAME:'gframe';
HCODE:'h' [0-9]?[0-9]?[1-9];
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
MCODE:'m' [0-9]?[0-9]?[1-9];
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
SYS_VAR: '$'[$acmnopstv]*[a-z_]*; // could be improved
AXIS: [abcxyz];
AXIS_NUMBERED: AXIS [0-9]?[1-9];
SPINDLE: 's' [0-9]?[1-9];
R_PARAM: '$'?'r'[0-9]+;
SPINDLE_IDENTIFIER: 'spi';

// names
NAME: [a-z0-9_]+;


/*
 * Parser Rules
 */

file: (content | procedureDefinition) EOF;

content: declarationSpace codeSpace;
declarationSpace: declarationBlock*;
codeSpace: block*;
declarationBlock: blockNumber? declaration | blockNumber;
block: blockNumber? labelDefinition? statement | blockNumber? labelDefinition | blockNumber;

blockNumber: BLOCK_NUMBER INT;

// definition
procedureDefinition: PROC NAME parameterDefinitions? content PROC_END;

parameterDefinitions: OPEN_PAREN parameterDefinition (COMMA parameterDefinition)* CLOSE_PAREN;
parameterDefinition: parameterDefinitionByValue | parameterDefinitionByReference;
parameterDefinitionByValue: type NAME (ASSIGNMENT expression)?;
parameterDefinitionByReference: VAR type NAME arrayDeclaration?;

labelDefinition: NAME DOUBLE_COLON;

// declaration
declaration: macroDeclaration | variableDeclaration | procedureDeclaration;

macroDeclaration: MACRO_DEFINE NAME MACRO_AS macroValue;
macroValue: expression | command+ | procedure | gotoStatement;
variableDeclaration: DEFINE type variableAssignment (COMMA variableAssignment)*;
procedureDeclaration: EXTERN NAME parameterDeclarations?;

parameterDeclarations: OPEN_PAREN parameterDeclaration? (COMMA parameterDeclaration)* CLOSE_PAREN;
parameterDeclaration: parameterDeclarationByValue | parameterDeclarationByReference;
parameterDeclarationByValue: type;
parameterDeclarationByReference: VAR type arrayDeclaration?;

arrayDeclaration: OPEN_BRACKET expression? arrayDeclarationDimension? arrayDeclarationDimension? CLOSE_BRACKET;
arrayDeclarationDimension: COMMA expression?;

// assignment
variableAssignment: NAME arrayDefinition? (ASSIGNMENT expression)?;
arrayDefinition: OPEN_BRACKET expression (COMMA expression)? (COMMA expression)? CLOSE_BRACKET;

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
    | expression
    | variableAssignment
    | command+
    | procedure;

ifStatement: IF expression block* (ELSE block*)? IF_END;

caseStatement: CASE expression CASE_OF (constant gotoStatement)+ (CASE_DEFAULT gotoStatement)?;

iterativeStatement: iterativeWhile | iterativeFor | iterativeRepeat | iterativeLoop;
iterativeWhile: WHILE expression block* WHILE_END;
iterativeFor: FOR variableAssignment TO expression block* FOR_END;
iterativeRepeat: REPEAT block* REPEAT_END expression;
iterativeLoop: LOOP block* LOOP_END;

jumpStatement
    : gotoStatement
    | CALL NAME? CALL_BLOCK NAME TO NAME
    | RETURN expression?;

gotoStatement
    : gotoCondition? GOTO gotoTarget
    | gotoCondition? GOTO_B gotoTarget
    | gotoCondition? GOTO_C gotoTarget
    | gotoCondition? GOTO_F gotoTarget
    | gotoCondition? GOTO_S;

gotoCondition: IF expression;
gotoTarget
    : NAME              #gotoLabel
    | BLOCK_NUMBER INT  #gotoBlock
    ;

expression
    : (NOT|NOT_B)? primaryExpression                                                #unaryExpression
    | expression (MUL|DIV|MOD) expression                                           #multiplicativeExpression
    | expression (ADD|SUB) expression                                               #additiveExpression
    | expression AND_B expression                                                   #binaryAndExpression
    | expression XOR_B expression                                                   #binaryExclusiveOrExpression
    | expression OR_B expression                                                    #binaryInclusiveOrExpression
    | expression AND expression                                                     #andExpression
    | expression XOR expression                                                     #exclusiveOrExpression
    | expression OR expression                                                      #inclusiveOrExpression
    | expression CONCAT expression                                                  #stringExpression
    | expression (EQUAL|NOT_EQUAL|LESS_EQUAL|GREATER_EQUAL|LESS|GREATER) expression #relationalExpression
    ;

primaryExpression
    : NAME arrayDefinition?                 #variableUse
    | SYS_VAR arrayDefinition?              #systemVariableUse
    | R_PARAM arrayDefinition?              #rParamUse
    | constant                              #constantUse
    | predefinedFunction                    #functionUse
    | OPEN_PAREN expression CLOSE_PAREN     #nestedExpression
    ;

constant
    : numeric
    | HEX
    | BIN
    | STRING
    | BOOL;

numeric: (SUB|ADD)? (INT|REAL);

// command
command
    : macroUse
    | gCode
    | mCode
    | hCode
    | axisCode
    | ADIS
    | ADISPOS
    | ALF
    | AMIRROR
    | ANG
    | AP
    | AR
    | AROT
    | AROTS
    | ASCALE
    | ASPLINE
    | ATOL
    | ATRANS
    | BAUTO
    | BNAT
    | BRISK
    | BSPLINE
    | BTAN
    | CDOF
    | CDOF2
    | CDON
    | CFC
    | CFIN
    | CFTCP
    | CHF
    | CHR
    | CIP
    | COMPCAD
    | COMPCURV
    | COMPOF
    | COMPON
    | COMPPATH
    | COMPSURF
    | CP
    | CPRECOF
    | CPRECON
    | CR
    | CT
    | CTOL
    | CTOLG0
    | CUT2D
    | CUT2DD
    | CUT2DF
    | CUT2DFD
    | CUT3DC
    | CUT3DCC
    | CUT3DCCD
    | CUT3DCD
    | CUT3DF
    | CUT3DFD
    | CUT3DFF
    | CUT3DFS
    | CUTCONOF
    | CUTCONON
    | CUTMOD
    | CUTMODK
    | D
    | D0
    | DIAM90
    | DIAMCYCOF
    | DIAMOF
    | DIAMON
    | DILF
    | DISC
    | DISCL
    | DISPR
    | DISR
    | DISRP
    | DITE
    | DITS
    | DL
    | DRIVE
    | DYNFINISH
    | DYNNORM
    | DYNPOS
    | DYNPREC
    | DYNROUGH
    | DYNSEMIFIN
    | EAUTO
    | ENAT
    | ETAN
    | F ASSIGNMENT expression
    | FAD
    | FB
    | FCUB
    | FD
    | FENDNORM
    | FFWOF
    | FFWON
    | FIFOCTRL
    | FLIM
    | FLIN
    | FNORM
    | FP
    | FRC
    | FRCM
    | FTOCOF
    | FTOCON
    | GFRAME
    | I
    | I1
    | INVCCW
    | INVCW
    | IR
    | ISD
    | J
    | J1
    | JR
    | K
    | K1
    | KONT
    | KONTC
    | KONTT
    | KR
    | L
    | LEAD
    | LFOF
    | LFON
    | LFPOS
    | LFTXT
    | LFWP
    | MEAC
    | MEAS
    | MEASA
    | MEASF
    | MEAW
    | MEAWA
    | MIRROR
    | MOVT
    | NORM
    | OEMIPO1
    | OEMIPO2
    | OFFN
    | OMA
    | ORIANGLE
    | ORIAXES
    | ORIAXESFR
    | ORIAXPOS
    | ORIC
    | ORICONCCW
    | ORICONCW
    | ORICONIO
    | ORICONTO
    | ORICURINV
    | ORICURVE
    | ORID
    | ORIEULER
    | ORIMKS
    | ORIPATH
    | ORIPATHS
    | ORIPLANE
    | ORIROTA
    | ORIROTC
    | ORIROTR
    | ORIROTT
    | ORIRPY
    | ORIRPY2
    | ORIS
    | ORISOF
    | ORISON
    | ORIVECT
    | ORIVIRT1
    | ORIVIRT2
    | ORIWKS
    | OSC
    | OSD
    | OSOF
    | OSS
    | OSSE
    | OST
    | OTOL
    | OTOLG0
    | P
    | PACCLIM
    | PAROT
    | PAROTOF
    | PDELAYOF
    | PDELAYON
    | PL
    | POLY
    | PON
    | PONS
    | PTP
    | PTPG0
    | PTPWOC
    | PW
    | REPOSA
    | REPOSH
    | REPOSHA
    | REPOSL
    | REPOSQ
    | REPOSQA
    | RMB
    | RMBBL
    | RME
    | RMEBL
    | RMI
    | RMIBL
    | RMN
    | RMNBL
    | RND
    | RNDM
    | ROT
    | ROTS
    | RP
    | RPL
    | RTLIOF
    | RTLION
    | SCALE
    | SD
    | SF
    | SOFT
    | SON
    | SONS
    | SPATH
    | SPIF1
    | SPIF2
    | SPN
    | SPOF
    | SPP
    | SR
    | ST
    | STARTFIFO
    | STOLF
    | STOPFIFO
    | SUPA
    | SUPD
    | T
    | TCARR
    | TCOABS
    | TCOFR
    | TCOFRX
    | TCOFRY
    | TCOFRZ
    | THETA
    | TILT
    | TOFF
    | TOFFL
    | TOFFLR
    | TOFFR
    | TOFRAME
    | TOFRAMEX
    | TOFRAMEY
    | TOFRAMEZ
    | TOROT
    | TOROTOF
    | TOROTX
    | TOROTY
    | TOROTZ
    | TOWBCS
    | TOWKCS
    | TOWMCS
    | TOWSTD
    | TOWTCS
    | TOWWCS
    | TRANS
    | TURN
    | UPATH
    | WALCS
    | WALIMOF
    | WALIMON
    ;

macroUse: NAME;
gCode: GCODE;
mCode: MCODE;
hCode: HCODE;

axisCode: AXIS numeric | axis_identifier ASSIGNMENT axisAssignmentExpression;
axisAssignmentExpression: expression | (AC | IC) OPEN_PAREN expression CLOSE_PAREN;

// axis
axis_spindle_identifier: axis_identifier | spindle_identifier;
axis_identifier: AXIS_NUMBERED | NAME;
spindle_identifier: SPINDLE_IDENTIFIER OPEN_PAREN INT CLOSE_PAREN | SPINDLE | NAME;

// procedure
procedure: predefinedProcedure | ownProcedure | predefinedFunction;
ownProcedure: NAME parameters?;
parameters: OPEN_PAREN expression? (COMMA expression)* CLOSE_PAREN;

//// predefined procedure
predefinedProcedure
    : modalSubprogramCall
    | ACTBLOCNO
    | ADISPOSA
    | AFISOF
    | AFISON
    | AUXFUDEL
    | AUXFUDELG
    | AUXFUMSEQ
    | AUXFUSYNC
    | AXCTSWE
    | AXCTSWEC
    | AXCTSWED
    | AXTOCHAN
    | BRISKA
    | CADAPTOF
    | CADAPTON
    | CALCFIR
    | CANCELSUB
    | CHANDATA
    | CLEARM
    | CLRINT
    | CONTDCON
    | CONTPRON
    | CORROF
    | COUPDEF
    | COUPDEL
    | COUPOF
    | COUPOFS
    | COUPON
    | COUPONC
    | COUPRES
    | CPROT
    | CPROTDEF
    | CTABDEF
    | CTABDEL
    | CTABEND
    | CTABLOCK
    | CTABUNLOCK
    | DELAYFSTOF
    | DELAYFSTON
    | DELDTG
    | DELETE
    | DELMT
    | DELT
    | DELTC
    | DISABLE
    | DRFOF
    | DRIVEA
    | DRVPRD
    | DRVPWR
    | DZERO
    | EGDEF
    | EGDEL
    | EGOFC
    | EGOFS
    | EGON
    | EGONSYN
    | EGONSYNE
    | ENABLE
    | ESRR
    | ESRS
    | EXECSTRING
    | EXECTAB
    | EXECUTE
    | EXTCLOSE
    | EXTOPEN
    | FCTDEF
    | FGROUP OPEN_PAREN CLOSE_PAREN
    | FILEDATE
    | FILEINFO
    | FILESIZE
    | FILESTAT
    | FILETIME
    | FPR
    | FPRAOF
    | FPRAON
    | FTOC
    | GEOAX
    | GET
    | GETD
    | GETEXET
    | GETFREELOC
    | GETSELT
    | GWPSOF
    | GWPSON
    | ICYCOF
    | ICYCON
    | INIT
    | IPOBRKA
    | IPTRLOCK
    | IPTRUNLOCK
    | JERKA
    | LEADOF
    | LEADON
    | LOCK
    | MASLDEF
    | MASLDEL
    | MASLOF
    | MASLOFS
    | MASLON
    | MMC
    | MSG
    | MVTOOL
    | NEWCONF
    | NPROT
    | NPROTDEF
    | ORIRESET
    | POLFA
    | POLFMASK
    | POLFMLIN
    | POLYPATH
    | POSM
    | POSMT
    | PRESETON
    | PRESETONS
    | PROTA
    | PROTS
    | PUNCHACC
    | PUTFTOC
    | PUTFTOCF
    | RDISABLE
    | READ
    | RELEASE
    | RESETMON
    | RETB
    | SBLOF
    | SBLON
    | SETAL
    | SETM
    | SETMS
    | SETMTH
    | SETPIECE
    | SETTA
    | SETTIA
    | SIRELIN
    | SIRELOUT
    | SIRELTIME
    | SOFTA
    | SPCOF
    | SPCON
    | SPLINEPATH
    | START
    | STOPRE
    | STOPREOF
    | SYNFCT
    | TANG
    | TANGDEL
    | TANGOF
    | TANGON
    | TCA
    | TCI
    | TLIFT
    | TML
    | TMOF
    | TMON
    | TOFFOF
    | TOFFON
    | TRAANG
    | TRACON
    | TRACYL
    | TRAFOOF
    | TRAFOON
    | TRAILOF
    | TRAILON
    | TRANSMIT
    | TRAORI
    | UNLOCK
    | WAITC
    | WAITE
    | WAITENC
    | WAITM
    | WAITMC
    | WAITP
    | WAITS
    | WRITE
    | WRTPR;

modalSubprogramCall: CALL_MODAL (NAME (OPEN_BRACKET expression (COMMA expression)* CLOSE_BRACKET)? )?;

// feedrate override
feedrate_override_path: OVR ASSIGNMENT expression;
feedrate_override_rapid_traverse_velocity: OVRRAP ASSIGNMENT expression;
feedrate_override_position_or_spindle: OVRA OPEN_BRACKET axis_spindle_identifier CLOSE_BRACKET ASSIGNMENT expression;

// acceleration compensation
acceleration_compensation: ACC OPEN_BRACKET axis_spindle_identifier CLOSE_BRACKET ASSIGNMENT expression;

// feedrate override handwheel
feedrate_override_path_handwheel: FD ASSIGNMENT expression;
feedrate_override_axial_handwheel: FDA OPEN_BRACKET axis_identifier CLOSE_BRACKET ASSIGNMENT expression;

// function
predefinedFunction
    : mathFunction;

mathFunction 
    : SIN OPEN_PAREN expression CLOSE_PAREN
    | COS OPEN_PAREN expression CLOSE_PAREN
    | TAN OPEN_PAREN expression CLOSE_PAREN
    | ASIN OPEN_PAREN expression CLOSE_PAREN
    | ACOS OPEN_PAREN expression CLOSE_PAREN
    | ATAN2 OPEN_PAREN expression CLOSE_PAREN
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
    | CALCDAT OPEN_PAREN expression COMMA expression COMMA NAME CLOSE_PAREN;