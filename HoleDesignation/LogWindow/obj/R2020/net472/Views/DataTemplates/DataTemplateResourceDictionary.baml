   M S B A M L   `   `   `  ���� E  ALogWindow., Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:LogWindow.AbstractionsLogWindow.Abstractions  A%clr-namespace:LogWindow.Views.HelpersLogWindow.Views.Helpers  3clr-namespace:LogWindow.ModelsLogWindow.Models  G(clr-namespace:LogWindow.Views.ConvertersLogWindow.Views.Converters  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� Color   SecondBackground  SeparatorBackground  DarkBlue  
ErrorColor �� BasedOn �� 
TargetType �� Property �� Value �� DataType,    &LogWindow.Abstractions.BaseInfoMessage �� Mode5   /LogWindow.Abstractions.BaseLinkedElementMessage � 	Resources*   $LogWindow.Views.Helpers.BindingProxy  Key   Data	 �� RelativeSource
 �� AncestorType
 � Tag �� Command �� Path �� CommandParameter �� StringFormat � ContextMenu � DataContext 	 Proxy �� Source m� Command m� CommandParameter �� Triggers �� Binding �� ElementName �� Value$   LogWindow.Models.ResultMessage �� 
IsReadOnly �� TextWrapping'   !LogWindow.Models.ErrorMessageMany 9� 
IsExpanded ��Name �� 	Converter.   (LogWindow.Views.Converters.MathConverter �� ConverterParameter'   !LogWindow.Models.UnionInfoMessage)   #LogWindow.Models.CommonErrorMessage�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  :x,http://schemas.microsoft.com/winfx/2006/xaml     5abstractions$clr-namespace:LogWindow.Abstractions  1helpers%clr-namespace:LogWindow.Views.Helpers  )modelsclr-namespace:LogWindow.Models  7
converters(clr-namespace:LogWindow.Views.Converters  %�
  &	        &	      &	 &     &	 9     '�� L     7��(�� /    *,  +)(�� ~    *, +)7  (�� �    *, +)(�� 3    *, +)7  (�� 	    *, +)(�� T	    *, +)�� $  #BDBDBD���� $  #4F4F4F���� $  #222d44���� $  #F12A66���� 8   " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 14(���  � ��$ Medium"���  � ��# �  ��  �@��
IsReadOnly$
 True����  �@��TextWrapping$
 Wrap=���  � ��$ 0q��� "   .R��� $��36,4,0,4q�$	��0,4q�#���@������ *.+$ OneTime=��� "  .R���  ��  	Proxy  �� 	 �� "
 �.+��� $��0,4,0,4q�$��0q�#���@������ *Text+$ OneTime=��� $��32,8,0,8q�$��0,0,0,0q�#���@������ *	Element+$ OneTime=��� $��8,8,0,8q� �� *	ElementId+��  �� 	 �� "
 �$# DataContext.SelectedIdCommand� �� *	ElementId+.�.��� ��� *	ElementId+$ OneTime=�$
 {0};�� ��  �� $ Data.DataContext�8   .@�m� $0��*Скопировать Id элемента�� �� *CopyIdCommand+ �� $  Parent.PlacementTarget.Tag�	 �� *Self+����  ��  �� *	IsChecked+$ CheckBoxGroup�� True.a���  �@��Orientation$ Vertical=���  �� *	IsChecked+$ CheckBoxGroup��	 False.a���  �@��Orientation$ 
Horizontal=��� "  .R��� 
������ 
������ $��14(�$��Medium"�#���  $
 True��$��36,4,0,4q�$	��0,4q�$
 Wrap=����� *.+$ OneTime=��� "  .R�9� #���   $ False��$��0q� ��  	Proxy  �� 	 �� "
 ����� $��0,4,0,4q�$��0q�#���@������ *Title+.���� $ ItemsControl������ $ ElementsList����� .R��� .+��� $��32,8,0,8q�$��0,0,0,0q�#���@������ *ElementName+�� ���� *Ids+���� $ ActualWidth�$ ItemsControl��   $
 x-56������ .R�� ���� .R��� .+��� $��8,8,0,8q� �� *.+��  �� 	 �� "
 �$# DataContext.SelectedIdCommand� �� .�.��� ��� *.+$ OneWay=�$
 {0};�� ��  �� $ Data.DataContext�8   .@�m� $0��*Скопировать Id элемента�� �� *CopyIdCommand+ �� $  Parent.PlacementTarget.Tag�	 �� *Self+����  ��  �� *	IsChecked+$ CheckBoxGroup�� True.a���  �@��Orientation$ Vertical=���  �� *	IsChecked+$ CheckBoxGroup��	 False.a���  �@��Orientation$ 
Horizontal=��� "  .R��� $��36,4,0,4q�$	��0,4q�#���@������ *.+$ OneTime=��� "  .R�9� #���   $ False��$��0q����� $��0,4,0,4q�$��0q�#���@��#���  ���� *
ErrorTitle+$ OneTime=�.���� $��32,4,0,4q�#���@������ *.+$ OneTime=�