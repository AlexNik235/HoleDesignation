   M S B A M L   `   `   `  ����H  DGENPRO_Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:GENPRO_Design.DialogWindowGENPRO_Design.DialogWindow  5      /    )GENPRO_Design.DialogWindow.QuestionWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:GENPRO_Design.DialogWindow     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5        $
��auto��5	      $	��300��6   $
��auto��5
        � WindowStartupLocation$  CenterScreen=�5       � Title$- 'Подтвердите действие��5         GenproWindowStyle#���   5       � 	Resources 5       �� Source�� 5   
   $P Jpack://application:,,,/GENPRO_Design;component/GenproDesignDictionary.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5"      ��  K� ZIndex$ 0��6     GenproWindowHeader#��[ 5#      $��4��5$      �� 5&      -   5         ��Name$ ButtonClose��5,      $��1��5&      $ 2��5'      $��30��5(      $��30��5)      $��5q�5*      $��Right=�5+        GenproButtonClose#��[ 5.       � Cursor$
 Hand��6@   5/      � 51   
   $��1��6   $��15q�6   $	��400��6(    52      �� 53      $
��Auto �6#   62   �� 54      6%   .+�57      �� $T��Npack://application:,,,/GENPRO_Design;component/Resources/gp_question_trans.png��6   $��30��58      $��0 0 15 0q�6   $��0��61   � 5:      $��1��6    5;      �� 5<      6&   �� 5=      $
��Auto �6$   64   .+�5@      �� -   5        	 ��Name$	 QuestionTextBlock��5@       ) %MaterialDesignComboBoxEditableTextBox#��[ 5A      $��0��5B      
 �� 
IsReadOnly
 . $
��Auto=�5D       �� TextWrapping$
 Wrap=�5E      $��Center=�5F      .��61   &$Выполнить операцию?5H      �� 5J      -   5         ��Name$ TextBlockCaption��5K      $��1��5L      $��11(�5M      $��Italic$�5N      $��0,15,0,0q�5O      
�����p�$
��Wrap=�5Q      $�� ��5R      ��5S      �� 5T       �� 
TargetType" ��6     �� Triggers 5U      P� 5V   "    P� Property � �� P� Value$  ��6:   . �5W   &   ��  �� Property � y� �� Value$ 	Collapsed=�6C   5X   #   5Z      5\      5^      5_      �� 5`   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��5q�6W   .+�5a      �� -   5        $ 	YesButton��5b      $��1��5c      $��5q�5d      $��70��5e      $��25��6     GenproButtonMain#��[ 5f      $��11(�5g      $��Right=�5h      $
 Hand��5i   )   .��67   Да6;   �� 5j      -   5        $ NoButton��5k      $��2��5l      $��5q�5m      $��70��5n      $��25��6   $��11(�5o      $��Right=�5p      .��5q   (   Нет6-   5r      5s      5t      