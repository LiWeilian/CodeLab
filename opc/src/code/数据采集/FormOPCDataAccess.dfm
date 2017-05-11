object frmOPCDataAccess: TfrmOPCDataAccess
  Left = 302
  Top = 193
  Width = 769
  Height = 577
  Caption = 'OPC'#25968#25454#37319#38598
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = #23435#20307
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 12
  object Panel1: TPanel
    Left = 0
    Top = 192
    Width = 761
    Height = 153
    TabOrder = 0
    object GroupBox3: TGroupBox
      Left = 1
      Top = 1
      Width = 112
      Height = 151
      Align = alLeft
      Caption = #32452
      TabOrder = 0
      object ToolBar2: TToolBar
        Left = 2
        Top = 14
        Width = 108
        Height = 29
        ButtonHeight = 20
        ButtonWidth = 43
        Caption = 'ToolBar2'
        EdgeInner = esNone
        EdgeOuter = esNone
        ShowCaptions = True
        TabOrder = 0
        object ToolButton2: TToolButton
          Left = 0
          Top = 2
          Caption = #28155#21152#32452
          ImageIndex = 0
        end
      end
      object lvGroups: TListView
        Left = 2
        Top = 43
        Width = 108
        Height = 106
        Align = alClient
        Columns = <
          item
            Caption = #32452
            Width = 100
          end>
        TabOrder = 1
        ViewStyle = vsReport
      end
    end
    object GroupBox4: TGroupBox
      Left = 113
      Top = 1
      Width = 647
      Height = 151
      Align = alClient
      Caption = #25968#25454#39033
      TabOrder = 1
      object ToolBar1: TToolBar
        Left = 2
        Top = 14
        Width = 643
        Height = 29
        Caption = 'ToolBar1'
        EdgeInner = esNone
        EdgeOuter = esNone
        TabOrder = 0
        object ToolButton1: TToolButton
          Left = 0
          Top = 2
          Caption = 'ToolButton1'
          ImageIndex = 0
        end
      end
      object ListView1: TListView
        Left = 2
        Top = 43
        Width = 643
        Height = 106
        Align = alClient
        Columns = <>
        TabOrder = 1
        ViewStyle = vsReport
      end
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 527
    Width = 761
    Height = 19
    Panels = <>
  end
  object GroupBox2: TGroupBox
    Left = 0
    Top = 400
    Width = 761
    Height = 127
    Align = alBottom
    Caption = #36816#34892#20449#24687
    TabOrder = 2
    object mmRunMsg: TMemo
      Left = 2
      Top = 14
      Width = 757
      Height = 111
      Align = alClient
      ScrollBars = ssVertical
      TabOrder = 0
    end
  end
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 761
    Height = 145
    Align = alTop
    TabOrder = 3
    object GroupBox1: TGroupBox
      Left = 1
      Top = 1
      Width = 256
      Height = 143
      Align = alLeft
      Caption = #26381#21153#22120#36830#25509
      TabOrder = 0
      object ledtServersHost: TLabeledEdit
        Left = 8
        Top = 40
        Width = 241
        Height = 20
        EditLabel.Width = 108
        EditLabel.Height = 12
        EditLabel.Caption = #26381#21153#22120#23487#20027#21517#31216#25110'IP'
        TabOrder = 0
        Text = 'gddst-xp-vpn'
      end
      object ledtOPCServerGUID: TLabeledEdit
        Left = 8
        Top = 80
        Width = 241
        Height = 20
        EditLabel.Width = 78
        EditLabel.Height = 12
        EditLabel.Caption = 'OPC'#26381#21153#22120'GUID'
        TabOrder = 1
        Text = '{F8582CF2-88FB-11D0-B850-00C0F0104305}'
      end
      object btnConnectToServer: TButton
        Left = 8
        Top = 109
        Width = 75
        Height = 25
        Caption = #36830#25509
        TabOrder = 2
        OnClick = btnConnectToServerClick
      end
    end
  end
end
