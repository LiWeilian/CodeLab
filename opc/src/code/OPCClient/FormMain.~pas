unit FormMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, ComCtrls, Types, ActiveX,
  OPCAutomation_TLB,
  untNetwork, OleServer;

type
  TfrmMain = class(TForm)
    sbStatus: TStatusBar;
    Panel1: TPanel;
    gpServers: TGroupBox;
    ledtHost: TLabeledEdit;
    btnRefreshServers: TButton;
    Label1: TLabel;
    cbServers: TComboBox;
    btnConnectToServer: TButton;
    gpGroups: TGroupBox;
    gpGroupProperties: TGroupBox;
    lvGroups: TListView;
    btnAddGroup: TButton;
    btnRemoveGroup: TButton;
    ledtGrpPropName: TLabeledEdit;
    LabeledEdit1: TLabeledEdit;
    Memo1: TMemo;
    opcGrp: TOPCGroup;
    procedure btnRefreshServersClick(Sender: TObject);
    procedure btnAddGroupClick(Sender: TObject);
    procedure btnRemoveGroupClick(Sender: TObject);
    procedure btnConnectToServerClick(Sender: TObject);
  private
    { Private declarations }
    FOPCServer: OPCServer;

    procedure ListOPCServers;
    procedure ReleaseOPCServers;
    function ConnectOPCServer(serverName, hostIP: String; out msg: String): Boolean;
    procedure AddGroupToList(group: TOPCGroup);
    procedure RemoveGroupFromList(group: TOPCGroup);

    procedure OnDataChange(ASender: TObject;
                            TransactionID: Integer;
                            NumItems: Integer;
                            var ClientHandles: {??PSafeArray}OleVariant;
                            var ItemValues: {??PSafeArray}OleVariant;
                            var Qualities: {??PSafeArray}OleVariant;
                            var TimeStamps: {??PSafeArray}OleVariant);
    procedure OnAsyncReadComplete(ASender: TObject;
                             TransactionID: Integer;
                             NumItems: Integer;
                             var ClientHandles: {??PSafeArray}OleVariant; 
                             var ItemValues: {??PSafeArray}OleVariant; 
                             var Qualities: {??PSafeArray}OleVariant; 
                             var TimeStamps: {??PSafeArray}OleVariant; 
                             var Errors: {??PSafeArray}OleVariant);
  public
    { Public declarations }
  end;

var
  frmMain: TfrmMain;

implementation

{$R *.dfm}

procedure TfrmMain.ListOPCServers;
var
  serverListObj: OleVariant;
  serverList: TStringDynArray;
  i, iLow, iHigh: Integer;
  hostName: String;
begin
  hostName := untNetwork.GetHostByIPAddr(ledtHost.Text);
  if hostName = '' then
  begin
    ShowMessage('无效的IP地址');
    Exit;
  end;
  try
    FOPCServer := CoOPCServer.Create;
    serverListObj := FOPCServer.GetOPCServers(hostName); //IP也可以
    if (not VarIsNull(serverListObj)) and VarIsArray(serverListObj) then
    begin
      iLow := VarArrayLowBound(serverListObj, 1);
      iHigh := VarArrayHighBound(serverListObj, 1);
      for i := iLow to iHigh do
      begin
        cbServers.Items.Add(serverListObj[i]);
      end;
    end;
    if cbServers.Items.Count > 0 then
      cbServers.ItemIndex := 0;
  except
    on e: Exception do
    begin
      ShowMessage(e.Message);
    end;
  end;
end;

procedure TfrmMain.ReleaseOPCServers;
begin
  cbServers.Items.Clear;
  FOPCServer := nil;
end;

function TfrmMain.ConnectOPCServer(serverName, hostIP: String; out msg: String): Boolean;
begin
  Result := True;
  try
    if FOPCServer = nil then
    begin
      Result := False;
      Exit;
    end;
    FOPCServer.Connect(serverName, hostIP);
    if FOPCServer.ServerState = OPCRunning then
    begin
      Result := True;
    end else
    begin
      Result := False;
      msg := IntToStr(FOPCServer.ServerState);
    end;
  except
    on e: Exception do
    begin
      msg := e.Message;
    end;
  end;
end;

procedure TfrmMain.AddGroupToList(group: TOPCGroup);
var
  listItem: TListItem;
begin
  if group = nil then
    Exit;
  listItem := lvGroups.Items.Add;
  listItem.Caption := group.Name;
  listItem.Data := group;
end;

procedure TfrmMain.RemoveGroupFromList(group: TOPCGroup);
var
  i: Integer;
begin
  for i := 0 to lvGroups.Items.Count - 1 do
  begin
    if TOPCGroup(lvGroups.Items[i].Data) = group then
    begin
      lvGroups.Items.Delete(i);
      Exit;
    end;
  end;
end;

procedure TfrmMain.OnDataChange(ASender: TObject; TransactionID,
  NumItems: Integer; var ClientHandles, ItemValues, Qualities,
  TimeStamps: OleVariant);
var
  i, iLow, iHigh: Integer;
begin
  Memo1.Lines.Add('OnDataChange TransactionID: ' + IntToStr(TransactionID));
  if not VarIsNull(TimeStamps) then
  begin
    iLow := VarArrayLowBound(TimeStamps, 1);
    iHigh := VarArrayHighBound(TimeStamps, 1);
    for i := iLow to iHigh do
    begin
      Memo1.Lines.Add('ItemValue: ' + ItemValues[i] + ', Quality: ' + Qualities[i] + ', TimeStamp: ' + TimeStamps[i]);
    end;
  end;
end;

procedure TfrmMain.OnAsyncReadComplete(ASender: TObject; TransactionID,
  NumItems: Integer; var ClientHandles, ItemValues, Qualities, TimeStamps,
  Errors: OleVariant);
var
  i, iLow, iHigh: Integer;
begin
  Memo1.Lines.Add('OnAsyncReadComplete TransactionID: ' + IntToStr(TransactionID));
  if not VarIsNull(TimeStamps) then
  begin
    iLow := VarArrayLowBound(TimeStamps, 1);
    iHigh := VarArrayHighBound(TimeStamps, 1);
    for i := iLow to iHigh do
    begin
      Memo1.Lines.Add('ItemValue: ' + ItemValues[i] + ', Quality: ' + Qualities[i] + ', TimeStamp: ' + TimeStamps[i]);
    end;
  end;
end;

procedure TfrmMain.btnRefreshServersClick(Sender: TObject);
begin
  try
    btnRefreshServers.Enabled := False;
    btnRefreshServers.Caption := '刷新...';
    Application.ProcessMessages;

    ReleaseOPCServers;
    ListOPCServers;
  finally
    btnRefreshServers.Enabled := True;
    btnRefreshServers.Caption := '刷新';
    Application.ProcessMessages;
  end;
end;

procedure TfrmMain.btnAddGroupClick(Sender: TObject);
var
  opcGroupIntf: OPCGroup;
  //opcGrp: TOPCGroup;
  opcItm: OPCItem;
  servHdls: PSafeArray;
  bound:TSAFEARRAYBOUND;
  value, quality, timeStamp: OleVariant;
  index: Integer;
  itemServHDL: Integer;
  values, errors: PSafeArray;
  servHdls2: array of Integer;
begin
  opcGroupIntf := FOPCServer.OPCGroups.Add('Group_' + FormatDateTime('YYYYMMDDHHNNSS', now));

  //opcGrp := TOPCGroup.Create(Self);
  opcGrp.ConnectTo(opcGroupIntf);
  opcGrp.DeadBand := 0;
  opcGrp.IsActive := True;
  opcGrp.UpdateRate := 888;
  opcGrp.ConnectKind := ckRunningOrNew;

  //opcGrp.OnAsyncReadComplete := self.OnAsyncReadComplete;
  opcGrp.IsSubscribed := True;

  AddGroupToList(opcGrp);

  opcGrp.OPCItems.AddItem('Random.Real4', 1234);

  //opcGrp.OPCItems.Item('Random.Real4').Read(OPCDevice, value, quality, timeStamp);
  //Memo1.Lines.Add(VarToStr(value) + ' ' + VarToStr(quality) + ' ' + VarToStr(timeStamp));


  opcGrp.OnDataChange := self.OnDataChange;

  {
  bound.lLbound:=0;
  bound.cElements:=1;
  servHdls := SafeArrayCreate(VT_I4, 1, bound);
  index := 0;
  itemServHDL := 0;
  SafeArrayPutElement(servHdls, index, itemServHDL);
  ShowMessage('0');
  index := 1;
  itemServHDL := opcGrp.OPCItems.Item('Random.Real4').ServerHandle;
  SafeArrayPutElement(servHdls, index, itemServHDL);
  ShowMessage('1');

  SetLength(servHdls2, 2);
  servHdls2[0] := 0;
  servHdls2[1] := itemServHDL;

  opcGrp.SyncRead(OPCCache, 1, servHdls2[0], values, errors);
  }
end;

procedure TfrmMain.btnRemoveGroupClick(Sender: TObject);
var
  opcGrp: TOPCGroup;
begin
  if lvGroups.Selected <> nil then
  begin
    opcGrp := TOPCGroup(lvGroups.Selected.Data);
    RemoveGroupFromList(opcGrp);
    opcGrp.Disconnect;
    FreeAndNil(opcGrp);
  end;
end;

procedure TfrmMain.btnConnectToServerClick(Sender: TObject);
var
  msg: String;
begin
  try
    btnConnectToServer.Enabled := False;
    btnConnectToServer.Caption := '连接...';
    Application.ProcessMessages;

    if ConnectOPCServer(cbServers.Text, ledtHost.Text, msg) then
    begin
      sbStatus.Panels[0].Text := '已连接到 ' + cbServers.Text;
    end else
    begin
      sbStatus.Panels[0].Text := '连接 ' + cbServers.Text + ' 失败';
    end;
  finally
    btnConnectToServer.Enabled := True;
    btnConnectToServer.Caption := '连接';
    Application.ProcessMessages;
  end;
end;

end.
