   M S B A M L   `   `   `  ���� H  DGENPRO_Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:GENPRO_Design.DialogWindowGENPRO_Design.DialogWindow  /    )GENPRO_Design.DialogWindow.QuestionWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:GENPRO_Design.DialogWindow     _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -   $
��auto��$	��300��$
��auto��  � WindowStartupLocation$  CenterScreen=� � Title$- 'Подтвердите действие��   GenproWindowStyle#���    � 	Resources  �� Source�� $P Jpack://application:,,,/GENPRO_Design;component/GenproDesignDictionary.xaml?�%    .���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto �.+�� $��0�� � ColumnDefinitions �� �� .+���  K� ZIndex$ 0��  GenproWindowHeader#��[ $��4���� -    ��Name$ ButtonClose��$��1��$ 2��$��30��$��30��$��5q�$��Right=�  GenproButtonClose#��[  � Cursor$
 Hand��� $��1��$��15q�$	��400�� �� $
��Auto ��� .+��� $T��Npack://application:,,,/GENPRO_Design;component/Resources/gp_question_trans.png��$��30��$��0 0 15 0q�$��0��� $��1�� �� �� $
��Auto �.+��� -   	 ��Name$	 QuestionTextBlock�� ) %MaterialDesignComboBoxEditableTextBox#��[ $��0��
 �� 
IsReadOnly
 . $
��Auto=� �� TextWrapping$
 Wrap=�$��Center=�.��&$Выполнить операцию?�� -    ��Name$ TextBlockCaption��$��1��$��11(�$��Italic$�$��0,15,0,0q�
�����p�$
��Wrap=�$�� ������  �� 
TargetType" �� �� Triggers P�  P� Property � �� P� Value$  ��. ���  �� Property � y� �� Value$ 	Collapsed=��� $��2�� �� Orientation$ 
Horizontal=�$��Right=�$��5q�.+��� -   $ 	YesButton��$��1��$��5q�$��70��$��25��  GenproButtonMain#��[ $��11(�$��Right=�$
 Hand��.��Да�� -   $ NoButton��$��2��$��5q�$��70��$��25��$��11(�$��Right=�.��Нет