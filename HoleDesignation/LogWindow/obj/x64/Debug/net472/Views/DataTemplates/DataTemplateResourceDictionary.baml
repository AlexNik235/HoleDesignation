   M S B A M L   `   `   `  ����E  ALogWindow., Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:LogWindow.AbstractionsLogWindow.Abstractions  5      A%clr-namespace:LogWindow.Views.HelpersLogWindow.Views.Helpers  5      3clr-namespace:LogWindow.ModelsLogWindow.Models  5	      G(clr-namespace:LogWindow.Views.ConvertersLogWindow.Views.Converters  5
      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� Color   SecondBackground  SeparatorBackground  DarkBlue  
ErrorColor �� BasedOn �� 
TargetType �� Property �� Value �� DataType,    &LogWindow.Abstractions.BaseInfoMessage �� Mode5   /LogWindow.Abstractions.BaseLinkedElementMessage � 	Resources*   $LogWindow.Views.Helpers.BindingProxy  Key   Data	 �� RelativeSource
 �� AncestorType
 � Tag �� Command �� Path �� CommandParameter �� StringFormat � ContextMenu � DataContext 	 Proxy �� Source m� Command m� CommandParameter �� Triggers �� Binding �� ElementName �� Value$   LogWindow.Models.ResultMessage �� 
IsReadOnly �� TextWrapping'   !LogWindow.Models.ErrorMessageMany 9� 
IsExpanded ��Name �� 	Converter.   (LogWindow.Views.Converters.MathConverter �� ConverterParameter'   !LogWindow.Models.UnionInfoMessage)   #LogWindow.Models.CommonErrorMessage�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      5abstractions$clr-namespace:LogWindow.Abstractions  5      1helpers%clr-namespace:LogWindow.Views.Helpers  5      )modelsclr-namespace:LogWindow.Models  5	      7
converters(clr-namespace:LogWindow.Views.Converters  5
      %�  &	        &	 &     &	 L     &	 r     '�� �     7��(��     5      *,  +)(�� �    5$      *, +)7  (�� q    5P      *, +)(�� s    5^      *, +)7  (�� �    5�      *, +)(�� j    5�      *, +)�� 5      $  #BDBDBD��6   6&   �� 5      $  #4F4F4F��6   6&   �� 5      $  #222d44��6   6&   �� 5      $  #F12A66��6   6&   �� 5      8   " ��68   .�5   
   ��  � ��
 ���� 6'   �� 5   
    � ��
 ���� 6(   �� 5   
    � ��$ 14(�6%   �� 5   
    � ��$ Medium"�6'   �� 5   
    � ��# �  6'   �� 5   
    �@��
IsReadOnly$
 True��6'   �� 5   
    �@��TextWrapping$
 Wrap=�6)   �� 5   
    � ��$ 0q�6,   5      �� 5      "   6   .R�5   
   �� $��36,4,0,4q�5      $	��0,4q�5      #���@��5       ��5!      �� *.+$ OneTime=�5"      �� 5$      "  6   .R�5%   
   ��  5&      ��  5'      	Proxy  6'   �� 	 �� "
 �6u   5(      .+�5)      �� $��0,4,0,4q�5*      $��0q�5+      #���@��5,      ��5-      �� *Text+$ OneTime=��� 5.      $��32,8,0,8q�5/      $��0,0,0,0q�50      #���@��51      ��52      �� *	Element+$ OneTime=��� 53      $��8,8,0,8q�6    6)   �� *	ElementId+�� 54       6   �� 	 �� "
 �$# DataContext.SelectedIdCommand� 6�   �� *	ElementId+.�6�   .�55      �� �6   �� *	ElementId+$ OneTime=�$
 {0};��56       58      �� 59       6"   �� $ Data.DataContext�8   .@�5:      m� $0��*Скопировать Id элемента��5=       5;      �� *CopyIdCommand+ 5<      �� $  Parent.PlacementTarget.Tag�	 �� *Self+5=      5>      5@      ��5A      �� 5B       5C      �� 5D       6&   �� *	IsChecked+$ CheckBoxGroup�� True6_   .a�5E      ��  �@��Orientation$ Vertical=�6G   5F      �� 5G       6&   �� *	IsChecked+$ CheckBoxGroup��	 False6_   .a�5H      ��  �@��Orientation$ 
Horizontal=�6G   5I      5K      5M      5N      �� 5P      "  6   .R�5Q   
   �� 
������ 
������ $��14(�5T      $��Medium"�5U      #���  5V      $
 True��5W      $��36,4,0,4q�5X      $	��0,4q�5Y      $
 Wrap=�5[      ��5Z      �� *.+$ OneTime=�5[      5\      �� 5^      "  6   .R�5_   
   9� #���   5`      $ False��5a      $��0q�5b       5c      ��  5d      	Proxy  6'   �� 	 �� "
 �6u   5e      ��5g      �� 5h      $��0,4,0,4q�5i      $��0q�5j      #���@��5k      ��5l      �� *Title+.��5o      �� $ ItemsControl��6E   ��6   �� $ ElementsList���5p      �� 5q      .R�5r      �� .+�5s      �� $��32,8,0,8q�5t   !   $��0,0,0,0q�5u   !   #���@��5v   !   ��5w   !   �� *ElementName+�� 5x      ��6+   �� *Ids+��6G   �� $ ActualWidth�$ ItemsControl��   $
 x-56����5y   "   �� 5z   &   .R�5{   *   � 5|   '   ��5~   "   �� 5   &   .R�5�   *   �� .+�5�   .   �� $��8,8,0,8q�68    6I   �� *.+�� 5�   2    6<   �� 	 �� "
 �$# DataContext.SelectedIdCommand� 6�   �� .�6�   .�5�   6   �� �6:   �� *.+$ OneWay=�$
 {0};��5�   3    5�   2   �� 5�   6    6B   �� $ Data.DataContext�8   .@�5�   :   m� $0��*Скопировать Id элемента��5�   =    5�   =   �� *CopyIdCommand+ 5�   =   �� $  Parent.PlacementTarget.Tag�	 �� *Self+5�   =   5�   7   5�   /   5�   +   5�   '   5�      ��5�      �� 5�   "    5�   &   �� 5�   *    66   �� *	IsChecked+$ CheckBoxGroup�� True6o   .a�5�   .   ��  �@��Orientation$ Vertical=�6W   5�   +   �� 5�   *    66   �� *	IsChecked+$ CheckBoxGroup��	 False6o   .a�5�   .   ��  �@��Orientation$ 
Horizontal=�6W   5�   +   5�   #   5�      5�      5�      5�      5�      �� 5�      "  6   .R�5�   
   �� $��36,4,0,4q�5�      $	��0,4q�5�      #���@��5�      ��5�      �� *.+$ OneTime=�5�      �� 5�      "  6   .R�5�   
   9� #���   5�      $ False��5�      $��0q�5�      ��5�      �� 5�      $��0,4,0,4q�5�      $��0q�5�      #���@��5�      #���  5�      ��5�      �� *
ErrorTitle+$ OneTime=�5�      .��5�      �� $��32,4,0,4q�5�      #���@��5�      ��5�      �� *.+$ OneTime=�5�      5�      5�      