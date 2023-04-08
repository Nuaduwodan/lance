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
REAL_UNSIGNED: [0-9]* POINT [0-9]+;
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
GCODE:'g';
GCODE_NUMBERED: GCODE INT_UNSIGNED;
GFRAME:'gframe';
HCODE:'h';
HCODE_NUMBERED: HCODE INT_UNSIGNED;
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
MCODE:'m';
MCODE_NUMBERED: MCODE INT_UNSIGNED;
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
S_REAL:'s' REAL_UNSIGNED;
S_NUMBERED:'s' INT_UNSIGNED;
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
BLOCK_NUMBER: 'n' [0-9]?[0-9]?[0-9]?[0-9]?[0-9];
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
SYS_VAR: '$'[$acmnopstv]+[a-z0-9_]*; // could be improved
AXIS: [abcxyz];
AXIS_NUMBERED: AXIS [0-9]?[0-9];
R_PARAM: '$'?'r'[0-9]+;
SPINDLE_IDENTIFIER: 'spi';

// names
NAME: [a-z0-9_]+;


/*
 * Parser Rules
 */

file: NEWLINE* (content | procedureDefinition) NEWLINE* EOF;

content: declarationBlock* block*;
declarationBlock: (lineStart? declaration | lineStart) NEWLINE+;
block: (lineStart? labelDefinition? statement | lineStart? labelDefinition | lineStart) NEWLINE+;

lineStart: SLASH? blockNumber | SLASH;
blockNumber: BLOCK_NUMBER;
labelDefinition: NAME DOUBLE_COLON;

// procedure
procedureDefinition: procedureDefinitionHeader NEWLINE+ content PROC_END;
procedureDefinitionHeader: PROC NAME parameterDefinitions? procedureModifier*;
procedureModifier: SBLOF | DISPLON | DISPLOF | ACTBLOCNO;

parameterDefinitions: OPEN_PAREN parameterDefinition (COMMA parameterDefinition)* CLOSE_PAREN;
parameterDefinition: parameterDefinitionByValue | parameterDefinitionByReference;
parameterDefinitionByValue: type NAME (ASSIGNMENT expression)?;
parameterDefinitionByReference: VAR type NAME arrayDeclaration?;

// declaration
declaration: macroDeclaration | procedureDeclaration | variableDeclaration | variableRedecleration;

macroDeclaration: MACRO_DEFINE NAME MACRO_AS macroValue;
macroValue: expression | variableAssignment | command+ | procedure | gotoStatement | path | axis_spindle_identifier;
path: pathElements+;
pathElements: SLASH | NAME;

procedureDeclaration: EXTERN NAME parameterDeclarations?;
parameterDeclarations: OPEN_PAREN parameterDeclaration? (COMMA parameterDeclaration)* CLOSE_PAREN;
parameterDeclaration
    : type                          #parameterDeclarationByValue 
    | VAR type arrayDeclaration?    #parameterDeclarationByReference
    ;
arrayDeclaration: OPEN_BRACKET first=expression? (COMMA second=expression?)? (COMMA third=expression?)? CLOSE_BRACKET;

variableDeclaration: DEFINE globalVariableModifiers type variableModifiers variableNameDeclaration (COMMA variableNameDeclaration)*;
globalVariableModifiers: range? preprocessingStop? accessRights?;
range: NCK | CHAN;
preprocessingStop: SYNR | SYNW | SYNRW;
accessRights: (accessDesignation INT_UNSIGNED)+;
accessDesignation: ACCESS_READ | ACCESS_WRITE | READ_PROGRAM | WRITE_PROGRAM | READ_OPI | WRITE_OPI;
variableModifiers: physicalUnit? limitValues?;
physicalUnit: PHYS_UNIT INT_UNSIGNED;
limitValues: ((LOWER_LIMIT | UPPER_LIMIT) numeric)+;

variableNameDeclaration: NAME (variableAssignmentExpression | arrayDefinition arrayAssignmentExpression?)?;

arrayDefinition: OPEN_BRACKET expression (COMMA expression)? (COMMA expression)? CLOSE_BRACKET;
variableAssignmentExpression: ASSIGNMENT expression;
arrayAssignmentExpression: ASSIGNMENT (expression | SET? parameters | REP OPEN_PAREN expression (COMMA expression)? CLOSE_PAREN);

variableRedecleration: REDEFINE (NAME | R_PARAM | SYS_VAR) globalVariableModifiers variableModifiers;

// assignment
variableAssignment
    : NAME variableAssignmentExpression                 #userVariableAssignment
    | R_PARAM variableAssignmentExpression              #RParamAssignment
    | SYS_VAR variableAssignmentExpression              #SysVarAssignment
    | NAME arrayDefinition arrayAssignmentExpression    #arrayVariableAssignment
    | R_PARAM arrayDefinition arrayAssignmentExpression #arrayRParamAssignment
    | SYS_VAR arrayDefinition arrayAssignmentExpression #arraySysVarAssignment
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

ifStatement: IF expression NEWLINE* block* (lineStart? ELSE NEWLINE* block*)? lineStart? IF_END;

caseStatement: CASE expression CASE_OF NEWLINE* (lineStart? constant gotoStatement NEWLINE+)+ (lineStart? CASE_DEFAULT gotoStatement)?;

iterativeStatement: iterativeWhile | iterativeFor | iterativeRepeat | iterativeLoop;
iterativeWhile: WHILE expression NEWLINE* block* lineStart? WHILE_END;
iterativeFor: FOR variableAssignment TO expression NEWLINE* block* lineStart? FOR_END;
iterativeRepeat: REPEAT NEWLINE* block* lineStart? REPEAT_END expression;
iterativeLoop: LOOP NEWLINE* block* lineStart? LOOP_END;

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
    | blockNumber       #gotoBlock
    ;

callStatement
    : CALL (expression | primaryExpression? CALL_BLOCK NAME TO NAME)
    | CALL_P primaryExpression (OPEN_PAREN expression (COMMA expression)* CLOSE_PAREN)?
    | CALL_EXT OPEN_PAREN expression CLOSE_PAREN
    | CALL_PATH OPEN_PAREN expression? CLOSE_PAREN
    | CALL_MODAL (NAME (OPEN_BRACKET expression (COMMA expression)* CLOSE_BRACKET)? )?
    ;

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
    | R_PARAM arrayDefinition?              #rParamUse
    | constant                              #constantUse
    | function                              #functionUse
    | OPEN_PAREN expression CLOSE_PAREN     #nestedExpression
    | macroUse                              #macroUseLabel
    | axis_spindle_identifier               #axisUse
    ;

constant
    : numeric
    | HEX
    | BIN
    | STRING
    | BOOL;

numeric: REAL_UNSIGNED | INT_UNSIGNED;

macroUse: NAME+;

// command
command
    : expression ASSIGNMENT ACN OPEN_PAREN expression CLOSE_PAREN
    | expression ASSIGNMENT ACP OPEN_PAREN expression CLOSE_PAREN
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
    | expression ASSIGNMENT DC OPEN_PAREN expression CLOSE_PAREN
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
    | FA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
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
    | FXS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FXST OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | FXSW OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
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
    | MEAC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT parameters?
    | MEAS ASSIGNMENT expression
    | MEASA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT parameters?
    | MEASF ASSIGNMENT expression
    | MEAW ASSIGNMENT expression
    | MEAWA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT parameters?
    | MIRROR parameters?
    | MOV OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
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
    | POS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT axisAssignmentExpression
    | POSA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT axisAssignmentExpression
    | POSM
    | POSP OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT OPEN_PAREN expression COMMA expression COMMA expression CLOSE_PAREN
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
    | CALL_MODAL_OFF                // done
    | gCode
    | hCode
    | mCode
    | spindleSpeed
    | axisCode
    | macroUse
    ;

gCode: GCODE_NUMBERED | GCODE ASSIGNMENT codeAssignmentExpression;
hCode: HCODE_NUMBERED | HCODE ASSIGNMENT codeAssignmentExpression;
mCode: MCODE_NUMBERED | (MCODE_NUMBERED | MCODE) ASSIGNMENT codeAssignmentExpression;
spindleSpeed: S_NUMBERED | S_REAL | (S_NUMBERED | S | expression) ASSIGNMENT expression;
codeAssignmentExpression: expression | QU OPEN_PAREN expression CLOSE_PAREN;

axisCode: AXIS_NUMBERED | expression ASSIGNMENT axisAssignmentExpression;
axisAssignmentExpression: expression | (AC | IC | CAC | CACN | CACP | CDC | CIC) OPEN_PAREN expression CLOSE_PAREN;

// axis
// todo how about expressions that lead to an axis
axis_spindle_identifier: axis_identifier | spindle_identifier;
axis_identifier: AXIS | AXIS_NUMBERED;
spindle_identifier: SPINDLE_IDENTIFIER OPEN_PAREN INT_UNSIGNED CLOSE_PAREN;

// procedure
procedure: predefinedProcedure | ownProcedure | function | otherKeywords;
ownProcedure: NAME parameters?;
parameters: OPEN_PAREN expression? (COMMA expression)* CLOSE_PAREN;

//// predefined procedure
predefinedProcedure
    : ACTBLOCNO
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
    | MSG OPEN_PAREN expression? CLOSE_PAREN
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
    | SBLOF
    | SBLON
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

// acceleration compensation
acceleration_compensation: ACC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression;

// feedrate override handwheel
feedrate_override_path_handwheel: FD ASSIGNMENT expression;
feedrate_override_axial_handwheel: FDA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression;

// function
function
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
    | GETVARDIM parameters?
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

otherKeywords
    : ACC
    | ACCLIMA
    | APX
    | AX
    | BLSYNC
    | COARSEA
    | CPBC OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPDEF ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPDEL ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPFMOF OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPFMON OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPFMSON OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPFPOS OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
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
    | CPLPOS OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPLSETVAL OPEN_BRACKET expression COMMA expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMALARM OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMBRAKE OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMPRT OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMRESET OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMSTART OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPMVDI OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | CPOF ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
    | CPON ASSIGNMENT OPEN_PAREN expression CLOSE_PAREN
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
    | EX
    | FDA
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
    | JERKLIM
    | JERKLIMA
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
    | REP
    | RIC
    | RT
    | SC
    | SCC
    | SCPARA OPEN_BRACKET expression CLOSE_BRACKET ASSIGNMENT expression
    | SETINT
    | SPOS
    | SPOSA
    | SRA
    | STA
    | SVC
    | TR
    | VELOLIM
    | VELOLIMA
    ;