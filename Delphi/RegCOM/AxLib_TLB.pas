unit AxLib_TLB;

// ************************************************************************ //
// WARNING                                                                    
// -------                                                                    
// The types declared in this file were generated from data read from a       
// Type Library. If this type library is explicitly or indirectly (via        
// another type library referring to this type library) re-imported, or the   
// 'Refresh' command of the Type Library Editor activated while editing the   
// Type Library, the contents of this file will be regenerated and all        
// manual modifications will be lost.                                         
// ************************************************************************ //

// PASTLWTR : 1.2
// File generated on 2017-1-10 19:02:45 from Type Library described below.

// ************************************************************************  //
// Type Lib: E:\work_Lab\Delphi\Code\×¢²áCOM\AxLib.tlb (1)
// LIBID: {32D25BDC-BF26-478B-92AD-DA11985E785D}
// LCID: 0
// Helpfile: 
// HelpString: AxLib Library
// DepndLst: 
//   (1) v2.0 stdole, (C:\Windows\SysWOW64\stdole2.tlb)
//   (2) v4.0 StdVCL, (C:\windows\SysWOW64\stdvcl40.dll)
// ************************************************************************ //
{$TYPEDADDRESS OFF} // Unit must be compiled without type-checked pointers. 
{$WARN SYMBOL_PLATFORM OFF}
{$WRITEABLECONST ON}
{$VARPROPSETTER ON}
interface

uses Windows, ActiveX, Classes, Graphics, StdVCL, Variants;
  

// *********************************************************************//
// GUIDS declared in the TypeLibrary. Following prefixes are used:        
//   Type Libraries     : LIBID_xxxx                                      
//   CoClasses          : CLASS_xxxx                                      
//   DISPInterfaces     : DIID_xxxx                                       
//   Non-DISP interfaces: IID_xxxx                                        
// *********************************************************************//
const
  // TypeLibrary Major and minor versions
  AxLibMajorVersion = 1;
  AxLibMinorVersion = 0;

  LIBID_AxLib: TGUID = '{32D25BDC-BF26-478B-92AD-DA11985E785D}';

  IID_IAutoObj: TGUID = '{AED288B6-E2DA-419D-9C6F-1D9E730FBCEC}';
  CLASS_AutoObj: TGUID = '{73C53904-58F6-4BA8-9134-EACE5C07AED7}';
type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  IAutoObj = interface;
  IAutoObjDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  AutoObj = IAutoObj;


// *********************************************************************//
// Interface: IAutoObj
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {AED288B6-E2DA-419D-9C6F-1D9E730FBCEC}
// *********************************************************************//
  IAutoObj = interface(IDispatch)
    ['{AED288B6-E2DA-419D-9C6F-1D9E730FBCEC}']
    procedure Method1; safecall;
    procedure Method2(p1: Integer; out p2: WideString); safecall;
  end;

// *********************************************************************//
// DispIntf:  IAutoObjDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {AED288B6-E2DA-419D-9C6F-1D9E730FBCEC}
// *********************************************************************//
  IAutoObjDisp = dispinterface
    ['{AED288B6-E2DA-419D-9C6F-1D9E730FBCEC}']
    procedure Method1; dispid 201;
    procedure Method2(p1: Integer; out p2: WideString); dispid 202;
  end;

// *********************************************************************//
// The Class CoAutoObj provides a Create and CreateRemote method to          
// create instances of the default interface IAutoObj exposed by              
// the CoClass AutoObj. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAutoObj = class
    class function Create: IAutoObj;
    class function CreateRemote(const MachineName: string): IAutoObj;
  end;

implementation

uses ComObj;

class function CoAutoObj.Create: IAutoObj;
begin
  Result := CreateComObject(CLASS_AutoObj) as IAutoObj;
end;

class function CoAutoObj.CreateRemote(const MachineName: string): IAutoObj;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AutoObj) as IAutoObj;
end;

end.
