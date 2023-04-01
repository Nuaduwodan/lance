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
BLOCK_NUMBER: 'n' [0-9]?[0-9]?[0-9]?[0-9]?[1-9];
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

blockNumber: BLOCK_NUMBER;

// procedure
procedureDefinition: PROC NAME parameterDefinitions? content PROC_END;

parameterDefinitions: OPEN_PAREN parameterDefinition (COMMA parameterDefinition)* CLOSE_PAREN;
parameterDefinition: parameterDefinitionByValue | parameterDefinitionByReference;
parameterDefinitionByValue: type NAME (ASSIGNMENT expression)?;
parameterDefinitionByReference: VAR type NAME arrayDeclaration?;

// declaration
declaration: macroDeclaration | variableDeclaration | procedureDeclaration;

macroDeclaration: MACRO_DEFINE NAME MACRO_AS macroValue;
macroValue: expression | command+ | procedure | gotoStatement;

variableDeclaration: DEFINE type variableNameDeclaration (COMMA variableNameDeclaration)*;
variableNameDeclaration
    : NAME variableAssignmentExpression?                #simpleVariableNameDeclaration
    | NAME arrayDefinition arrayAssignmentExpression?   #arrayVariableNameDeclaration
    ;
arrayDefinition: OPEN_BRACKET expression (COMMA expression)? (COMMA expression)? CLOSE_BRACKET;
variableAssignmentExpression: ASSIGNMENT expression;
arrayAssignmentExpression: ASSIGNMENT (expression | SET? parameters);

procedureDeclaration: EXTERN NAME parameterDeclarations?;
parameterDeclarations: OPEN_PAREN parameterDeclaration? (COMMA parameterDeclaration)* CLOSE_PAREN;
parameterDeclaration
    : type                          #parameterDeclarationByValue 
    | VAR type arrayDeclaration?    #parameterDeclarationByReference
    ;
arrayDeclaration: OPEN_BRACKET first=expression? (COMMA second=expression?)? (COMMA third=expression?)? CLOSE_BRACKET;

labelDefinition: NAME DOUBLE_COLON;

// assignment
variableAssignment
    : NAME variableAssignmentExpression                 #simpleVariableAssignment
    | NAME arrayDefinition arrayAssignmentExpression    #arrayVariableAssignment
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
    | callStatement
    | CALL NAME? CALL_BLOCK NAME TO NAME
    | RETURN (OPEN_PAREN expression (COMMA expression)? (COMMA expression)? (COMMA expression)? CLOSE_PAREN)?;

gotoStatement
    : gotoCondition? GOTO gotoTarget
    | gotoCondition? GOTO_B gotoTarget
    | gotoCondition? GOTO_C gotoTarget
    | gotoCondition? GOTO_F gotoTarget
    | gotoCondition? GOTO_S;

gotoCondition: IF expression;
gotoTarget
    : NAME              #gotoLabel
    | BLOCK_NUMBER      #gotoBlock
    ;

callStatement
    : CALL NAME? CALL_BLOCK NAME TO NAME
    | CALL_P NAME (OPEN_PAREN expression (COMMA expression)* CLOSE_PAREN)?
    | CALL_EXT OPEN_PAREN expression CLOSE_PAREN
    | CALL_PATH OPEN_PAREN expression CLOSE_PAREN
    | CALL_MODAL (NAME (OPEN_BRACKET expression (COMMA expression)* CLOSE_BRACKET)? )?
    ;

syncActionStatement
    : syncActionId? syncActionCondition? SYNC_DO syncActionAction+ (ELSE syncActionAction+)?;

syncActionId: (ID | IDS) ASSIGNMENT INT;

syncActionCondition: (SYNC_WHEN | SYNC_WHENEVER | SYNC_FROM | SYNC_EVERY) expression;

syncActionAction: command | procedure | variableAssignment;

expression
    : (NOT|NOT_B) expression                                                        #unaryExpression
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
    | primaryExpression                                                             #primaryExpressionLabel
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
    | CALL_MODAL_OFF                // done
    | ADIS parameters?
    | ADISPOS parameters?
    | ALF parameters?
    | AMIRROR parameters?
    | ANG parameters?
    | AP parameters?
    | AR parameters?
    | AROT parameters?
    | AROTS parameters?
    | ASCALE parameters?
    | ASPLINE parameters?
    | ATOL parameters?
    | ATRANS parameters?
    | BAUTO parameters?
    | BNAT parameters?
    | BRISK parameters?
    | BSPLINE parameters?
    | BTAN parameters?
    | CDOF parameters?
    | CDOF2 parameters?
    | CDON parameters?
    | CFC parameters?
    | CFIN parameters?
    | CFTCP parameters?
    | CHF parameters?
    | CHR parameters?
    | CIP parameters?
    | COMPCAD parameters?
    | COMPCURV parameters?
    | COMPOF parameters?
    | COMPON parameters?
    | COMPPATH parameters?
    | COMPSURF parameters?
    | CP parameters?
    | CPRECOF parameters?
    | CPRECON parameters?
    | CR parameters?
    | CT parameters?
    | CTOL parameters?
    | CTOLG0 parameters?
    | CUT2D parameters?
    | CUT2DD parameters?
    | CUT2DF parameters?
    | CUT2DFD parameters?
    | CUT3DC parameters?
    | CUT3DCC parameters?
    | CUT3DCCD parameters?
    | CUT3DCD parameters?
    | CUT3DF parameters?
    | CUT3DFD parameters?
    | CUT3DFF parameters?
    | CUT3DFS parameters?
    | CUTCONOF parameters?
    | CUTCONON parameters?
    | CUTMOD parameters?
    | CUTMODK parameters?
    | D parameters?
    | D0 parameters?
    | DIAM90 parameters?
    | DIAMCYCOF parameters?
    | DIAMOF parameters?
    | DIAMON parameters?
    | DILF parameters?
    | DISC parameters?
    | DISCL parameters?
    | DISPR parameters?
    | DISR parameters?
    | DISRP parameters?
    | DITE parameters?
    | DITS parameters?
    | DL parameters?
    | DRIVE parameters?
    | DYNFINISH parameters?
    | DYNNORM parameters?
    | DYNPOS parameters?
    | DYNPREC parameters?
    | DYNROUGH parameters?
    | DYNSEMIFIN parameters?
    | EAUTO parameters?
    | ENAT parameters?
    | ETAN parameters?
    | F ASSIGNMENT expression
    | FAD parameters?
    | FB parameters?
    | FCUB parameters?
    | FD parameters?
    | FENDNORM parameters?
    | FFWOF parameters?
    | FFWON parameters?
    | FIFOCTRL parameters?
    | FLIM parameters?
    | FLIN parameters?
    | FNORM parameters?
    | FP parameters?
    | FRC parameters?
    | FRCM parameters?
    | FTOCOF parameters?
    | FTOCON parameters?
    | GFRAME parameters?
    | I parameters?
    | I1 parameters?
    | INVCCW parameters?
    | INVCW parameters?
    | IR parameters?
    | ISD parameters?
    | J parameters?
    | J1 parameters?
    | JR parameters?
    | K parameters?
    | K1 parameters?
    | KONT parameters?
    | KONTC parameters?
    | KONTT parameters?
    | KR parameters?
    | L parameters?
    | LEAD parameters?
    | LFOF parameters?
    | LFON parameters?
    | LFPOS parameters?
    | LFTXT parameters?
    | LFWP parameters?
    | MEAC parameters?
    | MEAS parameters?
    | MEASA parameters?
    | MEASF parameters?
    | MEAW parameters?
    | MEAWA parameters?
    | MIRROR parameters?
    | MOVT parameters?
    | NORM parameters?
    | OEMIPO1 parameters?
    | OEMIPO2 parameters?
    | OFFN parameters?
    | OMA parameters?
    | ORIANGLE parameters?
    | ORIAXES parameters?
    | ORIAXESFR parameters?
    | ORIAXPOS parameters?
    | ORIC parameters?
    | ORICONCCW parameters?
    | ORICONCW parameters?
    | ORICONIO parameters?
    | ORICONTO parameters?
    | ORICURINV parameters?
    | ORICURVE parameters?
    | ORID parameters?
    | ORIEULER parameters?
    | ORIMKS parameters?
    | ORIPATH parameters?
    | ORIPATHS parameters?
    | ORIPLANE parameters?
    | ORIROTA parameters?
    | ORIROTC parameters?
    | ORIROTR parameters?
    | ORIROTT parameters?
    | ORIRPY parameters?
    | ORIRPY2 parameters?
    | ORIS parameters?
    | ORISOF parameters?
    | ORISON parameters?
    | ORIVECT parameters?
    | ORIVIRT1 parameters?
    | ORIVIRT2 parameters?
    | ORIWKS parameters?
    | OSC parameters?
    | OSD parameters?
    | OSOF parameters?
    | OSS parameters?
    | OSSE parameters?
    | OST parameters?
    | OTOL parameters?
    | OTOLG0 parameters?
    | P parameters?
    | PACCLIM parameters?
    | PAROT parameters?
    | PAROTOF parameters?
    | PDELAYOF parameters?
    | PDELAYON parameters?
    | PL parameters?
    | POLY parameters?
    | PON parameters?
    | PONS parameters?
    | PTP parameters?
    | PTPG0 parameters?
    | PTPWOC parameters?
    | PW parameters?
    | REPOSA parameters?
    | REPOSH parameters?
    | REPOSHA parameters?
    | REPOSL parameters?
    | REPOSQ parameters?
    | REPOSQA parameters?
    | RMB parameters?
    | RMBBL parameters?
    | RME parameters?
    | RMEBL parameters?
    | RMI parameters?
    | RMIBL parameters?
    | RMN parameters?
    | RMNBL parameters?
    | RND parameters?
    | RNDM parameters?
    | ROT parameters?
    | ROTS parameters?
    | RP parameters?
    | RPL parameters?
    | RTLIOF parameters?
    | RTLION parameters?
    | SCALE parameters?
    | SD parameters?
    | SF parameters?
    | SOFT parameters?
    | SON parameters?
    | SONS parameters?
    | SPATH parameters?
    | SPIF1 parameters?
    | SPIF2 parameters?
    | SPN parameters?
    | SPOF parameters?
    | SPP parameters?
    | SR parameters?
    | ST parameters?
    | STARTFIFO parameters?
    | STOLF parameters?
    | STOPFIFO parameters?
    | SUPA parameters?
    | SUPD parameters?
    | T parameters?
    | TCARR parameters?
    | TCOABS parameters?
    | TCOFR parameters?
    | TCOFRX parameters?
    | TCOFRY parameters?
    | TCOFRZ parameters?
    | THETA parameters?
    | TILT parameters?
    | TOFF parameters?
    | TOFFL parameters?
    | TOFFLR parameters?
    | TOFFR parameters?
    | TOFRAME parameters?
    | TOFRAMEX parameters?
    | TOFRAMEY parameters?
    | TOFRAMEZ parameters?
    | TOROT parameters?
    | TOROTOF parameters?
    | TOROTX parameters?
    | TOROTY parameters?
    | TOROTZ parameters?
    | TOWBCS parameters?
    | TOWKCS parameters?
    | TOWMCS parameters?
    | TOWSTD parameters?
    | TOWTCS parameters?
    | TOWWCS parameters?
    | TRANS parameters?
    | TURN parameters?
    | UPATH parameters?
    | WALCS parameters?
    | WALIMOF parameters?
    | WALIMON parameters?
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
    : ACTBLOCNO parameters?
    | ADISPOSA parameters?
    | AFISOF parameters?
    | AFISON parameters?
    | AUXFUDEL parameters?
    | AUXFUDELG parameters?
    | AUXFUMSEQ parameters?
    | AUXFUSYNC parameters?
    | AXCTSWE parameters?
    | AXCTSWEC parameters?
    | AXCTSWED parameters?
    | AXTOCHAN parameters?
    | BRISKA parameters?
    | CADAPTOF parameters?
    | CADAPTON parameters?
    | CALCFIR parameters?
    | CANCELSUB parameters?
    | CHANDATA parameters?
    | CLEARM parameters?
    | CLRINT parameters?
    | CONTDCON parameters?
    | CONTPRON parameters?
    | CORROF parameters?
    | COUPDEF parameters?
    | COUPDEL parameters?
    | COUPOF parameters?
    | COUPOFS parameters?
    | COUPON parameters?
    | COUPONC parameters?
    | COUPRES parameters?
    | CPROT parameters?
    | CPROTDEF parameters?
    | CTABDEF parameters?
    | CTABDEL parameters?
    | CTABEND parameters?
    | CTABLOCK parameters?
    | CTABUNLOCK parameters?
    | DELAYFSTOF parameters?
    | DELAYFSTON parameters?
    | DELDTG parameters?
    | DELETE parameters?
    | DELMT parameters?
    | DELT parameters?
    | DELTC parameters?
    | DISABLE parameters?
    | DRFOF parameters?
    | DRIVEA parameters?
    | DRVPRD parameters?
    | DRVPWR parameters?
    | DZERO parameters?
    | EGDEF parameters?
    | EGDEL parameters?
    | EGOFC parameters?
    | EGOFS parameters?
    | EGON parameters?
    | EGONSYN parameters?
    | EGONSYNE parameters?
    | ENABLE parameters?
    | ESRR parameters?
    | ESRS parameters?
    | EXECSTRING parameters?
    | EXECTAB parameters?
    | EXECUTE parameters?
    | EXTCLOSE parameters?
    | EXTOPEN parameters?
    | FCTDEF parameters?
    | FGROUP OPEN_PAREN CLOSE_PAREN
    | FILEDATE parameters?
    | FILEINFO parameters?
    | FILESIZE parameters?
    | FILESTAT parameters?
    | FILETIME parameters?
    | FPR parameters?
    | FPRAOF parameters?
    | FPRAON parameters?
    | FTOC parameters?
    | GEOAX parameters?
    | GET parameters?
    | GETD parameters?
    | GETEXET parameters?
    | GETFREELOC parameters?
    | GETSELT parameters?
    | GWPSOF parameters?
    | GWPSON parameters?
    | ICYCOF parameters?
    | ICYCON parameters?
    | INIT parameters?
    | IPOBRKA parameters?
    | IPTRLOCK parameters?
    | IPTRUNLOCK parameters?
    | JERKA parameters?
    | LEADOF parameters?
    | LEADON parameters?
    | LOCK parameters?
    | MASLDEF parameters?
    | MASLDEL parameters?
    | MASLOF parameters?
    | MASLOFS parameters?
    | MASLON parameters?
    | MMC parameters?
    | MSG OPEN_PAREN expression CLOSE_PAREN
    | MVTOOL parameters?
    | NEWCONF parameters?
    | NPROT parameters?
    | NPROTDEF parameters?
    | ORIRESET parameters?
    | POLFA parameters?
    | POLFMASK parameters?
    | POLFMLIN parameters?
    | POLYPATH parameters?
    | POSM parameters?
    | POSMT parameters?
    | PRESETON parameters?
    | PRESETONS parameters?
    | PROTA parameters?
    | PROTS parameters?
    | PUNCHACC parameters?
    | PUTFTOC parameters?
    | PUTFTOCF parameters?
    | RDISABLE parameters?
    | READ parameters?
    | RELEASE parameters?
    | RESETMON parameters?
    | RETB parameters?
    | SBLOF parameters?
    | SBLON parameters?
    | SETAL parameters?
    | SETM parameters?
    | SETMS parameters?
    | SETMTH parameters?
    | SETPIECE parameters?
    | SETTA parameters?
    | SETTIA parameters?
    | SIRELIN parameters?
    | SIRELOUT parameters?
    | SIRELTIME parameters?
    | SOFTA parameters?
    | SPCOF parameters?
    | SPCON parameters?
    | SPLINEPATH parameters?
    | START parameters?
    | STOPRE parameters?
    | STOPREOF parameters?
    | SYNFCT parameters?
    | TANG parameters?
    | TANGDEL parameters?
    | TANGOF parameters?
    | TANGON parameters?
    | TCA parameters?
    | TCI parameters?
    | TLIFT parameters?
    | TML parameters?
    | TMOF parameters?
    | TMON parameters?
    | TOFFOF parameters?
    | TOFFON parameters?
    | TRAANG parameters?
    | TRACON parameters?
    | TRACYL parameters?
    | TRAFOOF parameters?
    | TRAFOON parameters?
    | TRAILOF parameters?
    | TRAILON parameters?
    | TRANSMIT parameters?
    | TRAORI parameters?
    | UNLOCK parameters?
    | WAITC parameters?
    | WAITE parameters?
    | WAITENC parameters?
    | WAITM parameters?
    | WAITMC parameters?
    | WAITP parameters?
    | WAITS parameters?
    | WRITE parameters?
    | WRTPR parameters?
    ;

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
    : mathFunction
    | stringFunction
    | CTAB parameters?
    | CTABEXISTS parameters?
    | CTABFNO parameters?
    | CTABFPOL parameters?
    | CTABFSEG parameters?
    | CTABID parameters?
    | CTABINV parameters?
    | CTABISLOCK parameters?
    | CTABMEMTYP parameters?
    | CTABMPOL parameters?
    | CTABMSEG parameters?
    | CTABNO parameters?
    | CTABNOMEM parameters?
    | CTABPERIOD parameters?
    | CTABPOL parameters?
    | CTABPOLID parameters?
    | CTABSEG parameters?
    | CTABSEGID parameters?
    | CTABSEV parameters?
    | CTABSSV parameters?
    | CTABTEP parameters?
    | CTABTEV parameters?
    | CTABTMAX parameters?
    | CTABTMIN parameters?
    | CTABTSP parameters?
    | CTABTSV parameters?
    | ADDFRAME parameters?
    | AXTOSPI parameters?
    | CALCPOSI parameters?
    | CALCTRAVAR parameters?
    | CFINE parameters?
    | CHKDM parameters?
    | CHKDNO parameters?
    | COLLPAIR parameters?
    | CORRTC parameters?
    | CORRTRAFO parameters?
    | CSPLINE parameters?
    | DELDL parameters?
    | DELMLOWNER parameters?
    | DELMLRES parameters?
    | DELOBJ parameters?
    | DELTOOLENV parameters?
    | GETACTT parameters?
    | GETACTTD parameters?
    | GETDNO parameters?
    | GETT parameters?
    | GETTCOR parameters?
    | GETTENV parameters?
    | GETVARAP parameters?
    | GETVARDFT parameters?
    | GETVARLIM parameters?
    | GETVARPHU parameters?
    | GETVARTYP parameters?
    | INTERSEC parameters?
    | INVFRAME parameters?
    | ISAXIS parameters?
    | ISFILE parameters?
    | ISVAR parameters?
    | LENTOAX parameters?
    | MEAFRAME parameters?
    | MEASURE parameters?
    | MODAXVAL parameters?
    | NAMETOINT parameters?
    | NEWMT parameters?
    | NEWT parameters?
    | ORISOLH parameters?
    | POSRANGE parameters?
    | PROTD parameters?
    | SETDNO parameters?
    | SETTCOR parameters?
    | SIRELAY parameters?
    | TOOLENV parameters?
    | TOOLGNT parameters?
    | TOOLGT parameters?
    ;

mathFunction // done
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