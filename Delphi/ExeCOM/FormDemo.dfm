object Form1: TForm1
  Left = 487
  Top = 282
  Width = 339
  Height = 337
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Button1: TButton
    Left = 32
    Top = 56
    Width = 107
    Height = 25
    Caption = 'Create COM Object'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 32
    Top = 104
    Width = 75
    Height = 25
    Caption = 'Method1'
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 32
    Top = 152
    Width = 75
    Height = 25
    Caption = 'Method2'
    TabOrder = 2
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 160
    Top = 56
    Width = 145
    Height = 25
    Caption = 'Create Remote COM Object'
    TabOrder = 3
    OnClick = Button4Click
  end
end
