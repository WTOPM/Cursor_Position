   M S B A M L   `   `   `  ����H  DBSFG AutoFarm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:BSFG_AutoFarmBSFG_AutoFarm  5   	   C&clr-namespace:BSFG_AutoFarm.PropertiesBSFG_AutoFarm.Properties  5   	       BSFG_AutoFarm.MainWindow   5   	   F BNHotkey.Wpf, Version=1.2.1.0, Culture=neutral, PublicKeyToken=nullR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:BSFG_AutoFarm     5   	   =
Properties&clr-namespace:BSFG_AutoFarm.Properties     5   	   -   5          �Name$
  name��5   C    � Title$ Cursor Position��5	   	   $	��155��6!   $	��270��6.      MaterialDesignBody#���   5
   	     MaterialDesignPaper#���  5   	   $��Medium"�5   	   $��12(�5   	   $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5   	    � 
ResizeMode$ CanMinimize=�6i   .��5      � .+�5   
   �� -   5         ��Name$ button��5      $��Включить��6!   $
��Left=�64   $��220,195,0,0q�6O   $	��Top=�6d   $	��110��6|   ���	#4CFFFFFF
�������$y�Hidden=�6�   �� 5   
   -   5        $ 	buttonOff��5   %   $y�Hidden=�6   $��Выключить��68   $
��Left=�6L   $��220,195,0,0q�6g   $	��Top=�6|   $	��110��6�   ���	#4CFFFFFF
�������6�   �� 5   
   �� 5   
   -   5         ��Name$ x_Text��5      $
��Left=�6"   $��	10,10,0,0q�6=    �� TextWrapping$
 Wrap=�6P   $��X��6d   $	��Top=�6m   $	��120��6�   �� 5   
   -   5        $ y_Text��5      $
��Left=�6"   $��
135,10,0,0q�6=   $
 Wrap=�6Q   $��Y��6e   $	��Top=�6n   $	��120��6�   �� 5   
   -   5        $ 
x_Text_Abs��5      $
��Left=�6&   $��	10,31,0,0q�6A   $
 Wrap=�6T   $��X_Abs��6h   $	��Top=�6u   $	��120��6�   �� 5   
   -   5        $ 
y_Text_Abs��5      $
��Left=�6&   $��
135,31,0,0q�6A   $
 Wrap=�6U   $��Y_Abs��6i   $	��Top=�6v   $	��120��6�   �� 5   
   -   5        $ R_Color_Text��5      $��Center=�6(   $��0,58,0,0q�6E   $
 Wrap=�6W   $��	R - Color��6k   $	��Top=�6|   $	��120��6�   �� 5   
   -	   5        $ G_Color_Text��5      $��Center=�6(   $��0,79,0,0q�6E   $
 Wrap=�6W   $��	G - Color��6k   $	��Top=�6|   $	��120��6�   �� 5   
   -
   5        $ B_Color_Text��5      $��Center=�6(   $��	0,100,0,0q�6E   $
 Wrap=�6X   $��	B - Color��6l   $	��Top=�6}   $	��120��6�   5       � 	Resources 5        
MyCommand1 �� Text  
MyCommand2��%P   &	       &	      �� 5   
   $
 Text��6-   �� 5   
   $
 Text��6-   5       J� InputBindings 5#      �� 5$   
   
	 �� Key$	 PageUp��6   
 �� Command#
 [ 6"     NHotkey.Wpf.HotkeyManager   RegisterGlobalHotkey . 6H   �� 5%   
   $	 PageDown��6   #
 [ 6$    . 6J   5'      