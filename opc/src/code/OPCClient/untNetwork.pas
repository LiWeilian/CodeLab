unit untNetwork;

interface

uses
  SysUtils, WinSock, Windows;

  function GetHostByIPAddr(IPAddr: String): String;

implementation

function GetHostByIPAddr(IPAddr: String): String;
var
  WSData: TWSAData;
  HostEnt: PHostEnt;
  addr: DWORD;
begin
  try
    Result := '';

    WSAStartup(2, WSData);
    addr := inet_addr(PChar(IPAddr));
    try
      HostEnt := gethostbyaddr(@addr, Length(IPAddr), PF_INET);
      Result := HostEnt.h_name;
    except

    end;
  finally
    WSACleanup;
  end;
end;

end.
