unit execom_TLB;

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
// File generated on 2017-2-9 14:40:10 from Type Library described below.

// ************************************************************************  //
// Type Lib: E:\GitHub\CodeLab\Delphi\ExeCOM\execom.tlb (1)
// LIBID: {B6AECEEE-3BAE-4E11-B8F9-821CB72F027E}
// LCID: 0
// Helpfile: 
// HelpString: execom Library
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
  execomMajorVersion = 1;
  execomMinorVersion = 0;

  LIBID_execom: TGUID = '{B6AECEEE-3BAE-4E11-B8F9-821CB72F027E}';

  IID_ITestExeCOM: TGUID = '{F860C3FB-2B4C-4C52-B939-9F46F9EF3DFF}';
  CLASS_TestExeCOM: TGUID = '{882FBA27-1C4B-46A0-B56B-E5EF1F6006D6}';
type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  ITestExeCOM = interface;
  ITestExeCOMDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  TestExeCOM = ITestExeCOM;


// *********************************************************************//
// Interface: ITestExeCOM
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {F860C3FB-2B4C-4C52-B939-9F46F9EF3DFF}
// *********************************************************************//
  ITestExeCOM = interface(IDispatch)
    ['{F860C3FB-2B4C-4C52-B939-9F46F9EF3DFF}']
    procedure Method1; safecall;
    procedure Method2(const p1: WideString; out p2: WideString); safecall;
  end;

// *********************************************************************//
// DispIntf:  ITestExeCOMDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {F860C3FB-2B4C-4C52-B939-9F46F9EF3DFF}
// *********************************************************************//
  ITestExeCOMDisp = dispinterface
    ['{F860C3FB-2B4C-4C52-B939-9F46F9EF3DFF}']
    procedure Method1; dispid 201;
    procedure Method2(const p1: WideString; out p2: WideString); dispid 202;
  end;

// *********************************************************************//
// The Class CoTestExeCOM provides a Create and CreateRemote method to          
// create instances of the default interface ITestExeCOM exposed by              
// the CoClass TestExeCOM. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoTestExeCOM = class
    class function Create: ITestExeCOM;
    class function CreateRemote(const MachineName: string): ITestExeCOM;
  end;

implementation

uses ComObj;

class function CoTestExeCOM.Create: ITestExeCOM;
begin
  Result := CreateComObject(CLASS_TestExeCOM) as ITestExeCOM;
end;

class function CoTestExeCOM.CreateRemote(const MachineName: string): ITestExeCOM;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_TestExeCOM) as ITestExeCOM;
end;

end.
