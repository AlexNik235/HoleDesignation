   M S B A M L   `   `   `  ���� H  DGENPRO_Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:GENPRO_Design.DialogWindowGENPRO_Design.DialogWindow  8    2GENPRO_Design.DialogWindow.GenproProgressBarWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:GENPRO_Design.DialogWindow     -   $
��auto��$	��400��  � WindowStartupLocation$  CenterScreen=� � Title$' !Процесс выполения��   GenproWindowStyle#���    � 	Resources  �� Source�� $P Jpack://application:,,,/GENPRO_Design;component/GenproDesignDictionary.xaml?�%    .���  � RowDefinitions �� $��50 ��� $
��auto ��� $
��auto ��� $
��auto �.+���   GenproWindowHeader#��[ � $��1��.+�� -    �Name$ progressBar��$��1��
�������
�������$��Center=�$
��Left=�$��0q�$��5q�$��25��$	��385���� -    ��Name$ TextBlockProgressBar��$��Center=�$��Center=��� -   $ 
messageLbl��$��2��$��5q�$	��385��$
��Wrap=�$
��Left=�$��Center=�$��11(�$0��*Подождите пожалуйста...��$��Italic$�
�������
���iii��� -    ��Name$ ButtonCancel��$��3��$��	5,5,10,10q�$��11(�$��80��$��25��$��Right=�.��Отмена