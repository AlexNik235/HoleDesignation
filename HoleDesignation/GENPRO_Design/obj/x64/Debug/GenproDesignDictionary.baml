   M S B A M L   `   `   `  ����W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089_ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962a  �� MergedDictionaries �� Source   GenproColorMain  GenproColorSecond  GenproColorAccent  GenproColorFont  PrimaryHueMidBrush �� Color   PrimaryHueMidForegroundBrush  PrimaryHueLightBrush " PrimaryHueLightForegroundBrush  PrimaryHueDarkBrush !	 PrimaryHueDarkForegroundBrush �� 
TargetType 
 GenproButtonMain # MaterialDesignRaisedLightButton �� BasedOn �� Property �� Value  GenproButtonClose & "MaterialDesignIconForegroundButton'   !MaterialDesignThemes.Wpf.PackIcon    Kind �� Triggers   System.Windows.WindowState	   	Maximized
 �� Value �� Binding �� RelativeSource �� AncestorType �� Path  GenproButtonMinimize  GenproButtonUnselected  GenproButtonSelected  GenproWindowHeader �� BitmapScalingMode  GenproWindowHeaderClose � ColumnDefinitions # GenproWindowHeaderMinimizeClose  GenproWindowStyle :� RoutedEvent �� TargetProperty W� From	 W� To g� Duration  MaterialDesignBody&   System.Windows.Media.TextOptions  MaterialDesignLightCheckBox  MaterialDesignRadioButton  MaterialDesignSlider�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      8d2http://schemas.microsoft.com/expression/blend/2008  5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        5      �� 5
   
   $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?�5      �� 5   
   $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?�5      %�  &	        &	 %     &	 H     &	 k     &	 �     7   &	 �     7  &	 �     7  &	 �     7   &	 �     7  &		     7   &	
     7  &	 m    7  &	 w    7  &	 _    7  &	     7  &	 �    7  &	 �    7  7  &	 d	    7  7  &	 M    7  '�� +    '�� �    7  '	� �    7  7  '�� !    7  7  ��5      	LightGray��5      ��5      #363636��5      ��5      #1B5583��5      ��5      #404040��5!      �� 5#      8   61   �� 5$      8   6;   �� 5&      8   63   �� 5'      8   6=   �� 5)      8   62   �� 5*      8   6<   �� 5,      " ��6   8   .�5/   
   ��  � ��
 �����6<   50      �� 52      " ��6   8   .�54   
   ��  �@�Cursor$
 Hand��6#   �� 55   
    �@�ToolTip$ Закрыть��6$   �� 56   
    � ��$ Right=�60   �� 57   
    � �� 58         59      $ WindowClose=�6*   $��25��5:   *   $��25��5;   *   ��5<      �� 5=      $ White��6*   5?      5A       5B   
   �� 5C      #
 Z	 5D       5C      ��  �� *FindAncestor+" �$ WindowState�.a�5E      ��  �@�ToolTip$ Enlarge��6,   5F      5H      �� 5J      " ��6   8   .�5M   
   ��  �@�Cursor$
 Hand��6#   �� 5N   
    �@�ToolTip$ Свернуть��6$   �� 5O   
    � �� 5P         5Q      $ WindowMinimize=�6*   $��15��5R   *   $��15��5S   *   ��5T      �� 5U      $ White��6*   5W      5Y       5[   
   �� 5\      #
 Z	 5]       5\      ��  �� *FindAncestor+" �$ WindowState�.a�5^      ��  �@�ToolTip$ Enlarge��64   5_      5a      �� 5d      " ��6   8   .�5e   
   ��  � ��$ 0q�6,   68   �� 5f   
    � �� 5g      �� 5h      $ 	IndianRed��6"   66   5j      5k      �� 5p      " ��6   8   .�5q   
   ��  � �� 5r      �� 5s      $ SeaGreen��6"   65   5u      5v      �� 5y      .R�5z   
   � $��50��6   $	��200��6   .+�5{      �� $
��Left=�6   $��10q�6/   $��0��5|      $Y��Spack://application:,,,/GENPRO_Design;component/Resources/genpro_logo_white_full.png��5}      $ HighQuality=�5~      ��5      �� 5�      8   6"   5�      5�      �� 5�      .R�5�   
   � $��50��6   $	��200��6    5�      �� 5�      6%   �� 5�      $��45 �6#   60   .+�5�      �� $
��Left=�6   $��10q�6/   $��0��5�      $Y��Spack://application:,,,/GENPRO_Design;component/Resources/genpro_logo_white_full.png��5�      $ HighQuality=�5�      �� 5�      $��2��6   $��0,0,10,0q�5�      8��  5�   B   ��5�      �� 5�      8  6"   5�      5�      �� 5�      .R�5�   
   � $��50��6   $	��200��6    5�      �� 5�      6%   �� 5�      $��35 �6#   60   �� 5�      $��45 �6#   60   .+�5�      �� $
��Left=�6   $��10q�6/   $��0��5�      $Y��Spack://application:,,,/GENPRO_Design;component/Resources/genpro_logo_white_full.png��5�      $ HighQuality=�5�      �� 5�      $��2��6   $��Close��5�      $��0,0,10,0q�5�      8��  5�   B   ��5�      �� 5�      8  6"   5�      5�      �� 5�      " �6    5�   
   :� 5�      $ Loaded��6   [�5�      �� 5�      .��5�      �� .��5�      W� $ Opacity�5�   !   $	 0.1X�5�   !   $ 1X�5�   !   $ 0:0:0.25D�5�   !   5�      5�      5�      .�5�   
   ��  � ��$ 1q�6,   �� 5�   
    � �� 5�      �� 5�      8   6"   5�      �� 5�   
    �@�
ResizeMode$ NoResize=�6'   �� 5�   
    �@�WindowStyle$
 None=�6(   �� 5�   
    �@�AllowsTransparency . 6/   �� 5�   
    � ��$ San Francisco)�6'   �� 5�   
    � ��
 �����6'   �� 5�   
    �@�SizeToContent$ WidthAndHeight=�6*   �� 5�   
    �@�Topmost . 6$   �� 5�   
    � ��# �  63   �� 5�   
    � ��$ Regular"�63   �� 5�   
    � ��$ 12(�61   �� 5�   
    �@ TextFormattingMode$ Ideal=�6;   �� 5�   
    �@ TextRenderingMode$
 Auto=�6:   �� 5�   
    �@�Icon$ Resources/gp_icon.ico��6!   6A   5�      �� 5�      " ��6   .�5�   
   ��  �@��BitmapScalingMode$ HighQuality=�6<   5�      �� 5�      " ��6   8   5�      �� 5�      " 	�6   8   .�5�   
   ��  � �� 5�      �� 5�      8  6O   5�      5�      �� 5�      " ��6   8   .�5�   
   ��  � �� 5�      �� 5�      8  6"   5�      5�      5�      