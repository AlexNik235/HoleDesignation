   M S B A M L   `   `   `  ����  GENPRO_Design,    &GENPRO_Design.DialogWindow.InputWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � WindowStartupLocation$  CenterScreen=�5	   	      GenproWindowStyle#���   5
   	   $	��300��5   	   $	��100��5   	    � 	Resources 5       �� Source�� 5   
   $P Jpack://application:,,,/GENPRO_Design;component/GenproDesignDictionary.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ��   GenproWindowHeader#��[ 6   �� 5   
   -   5          GenproButtonClose#��[ 5      5      � 5   
   $��1��6   $��20q�6   $	��400��6(    5      �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   .+�5"      �� -   5         ��Name$ MainText��5"      $
��Wrap=�6*   $J��DВведите новое значение для параметра��6>   $��16(�6j   6z   �� 5#      -   5         ��Name$ 
InputValue��5#   #   $��1��6   $��	0 10 0 10q�5$      6+   �� 5%      -   5         ��Name$ ButtonApprove��5&      $��2��5%        GenproButtonMain#��[ 6"   .��5&   J   Подтвердить5(      5)      5*      5,      