unit TestCOM_TLB;

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
// File generated on 2017-2-9 9:41:23 from Type Library described below.

// ************************************************************************  //
// Type Lib: E:\work_Lab\Delphi\Code\×¢²áCOM\TestCOM.tlb (1)
// LIBID: {65FE0DA3-6627-43DB-8BB7-12C74646861F}
// LCID: 0
// Helpfile: 
// HelpString: TestCOM Library
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
  TestCOMMajorVersion = 1;
  TestCOMMinorVersion = 0;

  LIBID_TestCOM: TGUID = '{65FE0DA3-6627-43DB-8BB7-12C74646861F}';

  IID_ITestCOM: TGUID = '{A7E786EA-C03E-4FC4-A05C-5C5444ED911C}';
  CLASS_TestCOM_: TGUID = '{1FDCE511-906B-490C-A9CE-4CBEECB7DB56}';
  IID_IAutoObj: TGUID = '{4A6E5058-3965-4433-933D-12DB0DAFEFF4}';
  CLASS_AutoObj: TGUID = '{A26A7E1C-EA36-451D-9B4D-6FC9FCA534AF}';
type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  ITestCOM = interface;
  IAutoObj = interface;
  IAutoObjDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  TestCOM_ = ITestCOM;
  AutoObj = IAutoObj;


// *********************************************************************//
// Interface: ITestCOM
// Flags:     (256) OleAutomation
// GUID:      {A7E786EA-C03E-4FC4-A05C-5C5444ED911C}
// *********************************************************************//
  ITestCOM = interface(IUnknown)
    ['{A7E786EA-C03E-4FC4-A05C-5C5444ED911C}']
    function Method1: HResult; stdcall;
    function Method2(p1: Integer): HResult; stdcall;
    function Method3(p1: Integer; out p2: WideString): HResult; stdcall;
  end;

// *********************************************************************//
// Interface: IAutoObj
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {4A6E5058-3965-4433-933D-12DB0DAFEFF4}
// *********************************************************************//
  IAutoObj = interface(IDispatch)
    ['{4A6E5058-3965-4433-933D-12DB0DAFEFF4}']
    procedure Method1; safecall;
    procedure Method2(p1: Integer; out p2: WideString); safecall;
  end;

// *********************************************************************//
// DispIntf:  IAutoObjDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {4A6E5058-3965-4433-933D-12DB0DAFEFF4}
// *********************************************************************//
  IAutoObjDisp = dispinterface
    ['{4A6E5058-3965-4433-933D-12DB0DAFEFF4}']
    procedure Method1; dispid 201;
    procedure Method2(p1: Integer; out p2: WideString); dispid 202;
  end;

// *********************************************************************//
// The Class CoTestCOM_ provides a Create and CreateRemote method to          
// create instances of the default interface ITestCOM exposed by              
// the CoClass TestCOM_. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoTestCOM_ = class
    class function Create: ITestCOM;
    class function CreateRemote(const MachineName: string): ITestCOM;
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

class function CoTestCOM_.Create: ITestCOM;
begin
  Result := CreateComObject(CLASS_TestCOM_) as ITestCOM;
end;

class function CoTestCOM_.CreateRemote(const MachineName: string): ITestCOM;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_TestCOM_) as ITestCOM;
end;

class function CoAutoObj.Create: IAutoObj;
begin
  Result := CreateComObject(CLASS_AutoObj) as IAutoObj;
end;

class function CoAutoObj.CreateRemote(const MachineName: string): IAutoObj;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AutoObj) as IAutoObj;
end;

end.
