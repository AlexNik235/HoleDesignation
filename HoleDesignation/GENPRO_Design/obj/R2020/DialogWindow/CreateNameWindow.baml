   M S B A M L   `   `   `  ���� H  DGENPRO_Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:GENPRO_Design.DialogWindowGENPRO_Design.DialogWindow  1    +GENPRO_Design.DialogWindow.CreateNameWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:GENPRO_Design.DialogWindow     _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -   $	��360��$	��150��  � WindowStartupLocation$  CenterScreen=� � Title$ Новое имя��   GenproWindowStyle#���    � 	Resources  �� Source�� $P Jpack://application:,,,/GENPRO_Design;component/GenproDesignDictionary.xaml?�%    .���  � RowDefinitions �� $
��auto ��� $��50 ��� $
��auto �$��30���� $
��auto �$��40��.+���   GenproWindowHeader#��[ � $��1��$��5,0,0,0q� � ColumnDefinitions �� $��75 ��� $	��220 � �� $��40 �.+��� $��Center=�.��Новое имя:�� -    ��Name$ TextBoxNewName��$��1��$��Center=�$��25��$	��210��$��0q��� -    ��Name$ LabelErrorWrite��
���\\��$��11(�$��Italic$�$��2��$
��auto��$
��Wrap=�$��5q�� $��3��$
��auto��$��Right=� �� $
��auto ��� $
��auto �.+��� -    ��Name$ ButtonApply��  GenproButtonMain#��[ $��11(�$��0��$
��auto��$��25��$��5q�$��Right=�.��Применить�� -   $ ButtonCancel��$��1��$
��auto��$��25��$��5q�$��11(�$��Right=�.��Отмена