object frmMain: TfrmMain
  Left = 456
  Top = 207
  Width = 925
  Height = 573
  Caption = 'OPC'#25968#25454#35835#21462
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = #23435#20307
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 12
  object sbStatus: TStatusBar
    Left = 0
    Top = 523
    Width = 917
    Height = 19
    Panels = <
      item
        Width = 50
      end>
  end
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 265
    Height = 523
    Align = alLeft
    TabOrder = 1
    object gpServers: TGroupBox
      Left = 1
      Top = 1
      Width = 263
      Height = 136
      Align = alTop
      Caption = #26381#21153#22120
      TabOrder = 0
      object Label1: TLabel
        Left = 12
        Top = 67
        Width = 36
        Height = 12
        Caption = #26381#21153#22120
      end
      object ledtHost: TLabeledEdit
        Left = 74
        Top = 24
        Width = 121
        Height = 20
        EditLabel.Width = 60
        EditLabel.Height = 12
        EditLabel.Caption = #26381#21153#22120#23487#20027
        LabelPosition = lpLeft
        TabOrder = 0
        Text = '172.16.1.107'
      end
      object btnRefreshServers: TButton
        Left = 202
        Top = 21
        Width = 49
        Height = 25
        Caption = #21047#26032
        TabOrder = 1
        OnClick = btnRefreshServersClick
      end
      object cbServers: TComboBox
        Left = 74
        Top = 64
        Width = 177
        Height = 20
        Style = csDropDownList
        ItemHeight = 12
        TabOrder = 2
      end
      object btnConnectToServer: TButton
        Left = 176
        Top = 96
        Width = 75
        Height = 25
        Caption = #36830#25509
        TabOrder = 3
        OnClick = btnConnectToServerClick
      end
    end
    object gpGroups: TGroupBox
      Left = 1
      Top = 137
      Width = 263
      Height = 112
      Align = alTop
      Caption = #32452
      TabOrder = 1
      object lvGroups: TListView
        Left = 8
        Top = 16
        Width = 161
        Height = 78
        Columns = <
          item
            Caption = #21517#31216
            Width = 150
          end>
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        TabOrder = 0
        ViewStyle = vsReport
      end
      object btnAddGroup: TButton
        Left = 176
        Top = 24
        Width = 75
        Height = 25
        Caption = #28155#21152
        TabOrder = 1
        OnClick = btnAddGroupClick
      end
      object btnRemoveGroup: TButton
        Left = 176
        Top = 56
        Width = 75
        Height = 25
        Caption = #31227#38500
        TabOrder = 2
        OnClick = btnRemoveGroupClick
      end
    end
    object gpGroupProperties: TGroupBox
      Left = 1
      Top = 249
      Width = 263
      Height = 273
      Align = alClient
      Caption = #32452#23646#24615
      TabOrder = 2
      object ledtGrpPropName: TLabeledEdit
        Left = 74
        Top = 24
        Width = 177
        Height = 20
        EditLabel.Width = 60
        EditLabel.Height = 12
        EditLabel.Caption = #21517#31216'      '
        LabelPosition = lpLeft
        TabOrder = 0
      end
      object LabeledEdit1: TLabeledEdit
        Left = 74
        Top = 48
        Width = 177
        Height = 20
        EditLabel.Width = 60
        EditLabel.Height = 12
        EditLabel.Caption = #26356#26032#21608#26399'  '
        LabelPosition = lpLeft
        TabOrder = 1
      end
    end
  end
  object Memo1: TMemo
    Left = 336
    Top = 128
    Width = 369
    Height = 201
    TabOrder = 2
  end
  object opcGrp: TOPCGroup
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 336
    Top = 48
  end
end
