   M S B A M L   `   `   `  ����B  >ISystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ISystemISystem  5   	       ISystem.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ISystem     5   	     � Title$  Lee's Diamonds��5   	   $	��680��6    $
��1024��6-    � WindowStartupLocation$ CenterScreen=�6:    � Icon$ diamond-512.png��6_   .��5
      � .+�5   
   �� -   5        $��gb_Tools��5      $��Tools��6#   $��843,20,26,538q�62   .��5      � $��10,0,0,0q�6   .+�5      �� -   5         ��Name$ btn_mw_Search��5      $��Search��60   $��Right=�6A   $	��Top=�6]   $��75��6u   $��	0,10,31,0q�6�   �� 5      -   5        $ btn_mw_Items��5      $��Items��6/   $��Right=�6?   $��	0,35,31,0q�6[   $	��Top=�6n   $��75��6�   6�   5      5      �� 5   
   -   5         ��Name$ gb_InvoiceTools��5      $��Invoice Tools��6,   $
��Left=�6C   $��714,405,0,0q�6^   $	��Top=�6s   $	��234��6�   $	��292��6�   .��5      � $
��Left=�6   $	��224��6.   $��	0,0,0,-12q�6;   $	��Top=�6N   $	��276��6f   .+�5      �� -   5        $ btn_mw_NewInvoice��5      $��New Invoice��64   $
��Left=�6J   $	��Top=�6e   $	��110��6}   $��	10,10,0,0q�6�   $��20��6�   �� 5      -   5        $ btn_mw_DeleteInvoice��5      $��Delete Invoice��67   $
��Left=�6P   $	��Top=�6k   $	��110��6�   $��
156,42,0,0q�6�   �� 5      -   5        $ btn_mw_EditInvoice��5      $��Edit Invoice��65   $
��Left=�6L   $	��Top=�6g   $	��110��6   $��	10,42,0,0q�6�   $��20��6�   � 5      -   5        $��grid_AddItem��5      $
��Left=�6+   $	��132��6F   $��	10,82,0,0q�6S   $	��Top=�6f   $	��256��6~   .+�5      �� -	   5         ��Name$ lbl_itemSelect��5      $��Select Item to Add:��64   $
��Left=�6R   $	��Top=�6m   {�.  $��0,1,0,0q�6�   �� 5      -
   5         ��Name$ cbo_mw_ItemList��5      $
��Left=�68   $	��Top=�6S   $	��140��6k    J� IsHitTestVisible .  |�.  $��0,32,0,0q�6�   {�.  6�   �� 5      -   5        $ lbl_Cost��5      $��Cost:��6.   $
��Left=�6>   $��	145,1,0,0q�6Y   $	��Top=�6l   {�.  6�   �� 5      -   5         ��Name$ tbx_mw_Cost��5      $
��Left=�63   $��23��6N   	 �� TextWrapping$
	 Wrap=�6Z   $	��Top=�6n   $	��106��6�   |�.   .  $��
145,31,0,0q�6�   {�.  6�   �� 5      -   5        $ btn_mw_AddItem��5      $��Add Item��65   $
��Left=�6H   $	��Top=�6c   $	��100��6{   {�.  $��22��6�   $��	81,68,0,0q�6�   �� 5      -   5        $ btn_mw_Save��5      $
��Save��62   $
��Left=�6A   $	��Top=�6\   $	��100��6t   {�.  $��
81,100,0,0q�6�   $��22��6�   5      �� 5      -   5        
 ��Name$
 tblk_mw_SaveError��5      $
��Left=�67   $��	20,67,0,0q�6R   $
��Wrap=�6e   $.��(Fix Items in Red! Then click Save again.��6y   $	��Top=�6�   $	��230��6�   
���  ��$
��Bold"�6�   $y�Hidden=�6�   5       5!      � 5"   
   $
��Left=�6   $	��629��6*   $��	10,10,0,0q�67   $	��Top=�6J   $	��696��6b   .+�5#      �� $��0 0 1 0q�6   
���   �$��	0,-12,0,0q�6C   �� 5$      -   5         ��Name$ stk_mw_Invoice��5$      $��6,0,10,0q�61   .+�5%      � .+�5&      �� -   5         ��Name$ image��5&      $
��Left=�6+   $	��100��6F   $	��Top=�6S   $	��334��6k    J� OpacityMask 5'      �� 5(       �� EndPoint$ 0.5,1&�62    �� 
StartPoint$ 0.5,0&�6C   .��5)   "   �  � Color$ Black��6/    � Offset$ 0Q�6=   � 5*   "   $	 Red��6/   $ 1Q�6;   5+      5-      �� 5.      -   5        $
 tblk_mw_Business1��5.       $
��Left=�6;   $��	374,8,0,0q�6V   $
��Wrap=�6i   $��	Business:��6}   $	��Top=�6�   $��16(�6�   $��Tahoma)�6�   �� 5/      -   5        $
 tblk_mw_Business��5/       $
��Left=�6:   $��	445,0,0,0q�6U   $
��Wrap=�6h   $	��Top=�6|   $	��100��6�   $)��#228                                ��6�   $��16(�6�   $��Tahoma)�6�    �� TextAlignment$ Right=�6�   �� 6  $��24(�6
  $�Lee's Diamonds��6  �� 60  �� 6<  $�720 S. State Street��6@  �� 6]  �� 6i  $�Salt Lake City, UT��6m  6�  50      �� 52       �� Orientation$ 
Horizontal=�6   .+�53      � -   5         �Name$ grd_mw_Invoice��53      $	��100��63   $	��300��6@   $
��Left=�6L   .+�54      �� -   5        $
 tblk_mw_Invoice��54   $   $
��Left=�6=   $��10,0,0,0q�6X   $
��Wrap=�6j   $��Invoice��6~   $	��Top=�6�   $��64(�6�   $	��100��6�   $��Tahoma)�6�   �� 56      -   5        $
 tblk_mw_InvoiceNumbertext��56   $   $
��Left=�6G   $
��Wrap=�6b   $��Invoice Number: ��6v   $	��Top=�6�   $	��100��6�   $��16��6�   $ Right=�6�   $��0,84,0,0q�6�   �� 57      -   5        $
 tblk_mw_InvoiceNumber��57   $   $
��Left=�6C   $��
100,84,0,0q�6^   $
��Wrap=�6r   $��   UNK��6�   $	��Top=�6�   $	��100��6�   $��16��6�   (  "System.Windows.Controls.DatePicker  58      $
��Left=�6%   $��200,76,-2,0q�6@   $	��Top=�6U    J� RenderTransformOrigin$ -5,2.292&�6m   {�.   .  6�   5:      � 5;      -   5        $ grd_mw_Customer��5;      $	��100��64   $
�� 370��6A   $��Right=�6N   .+�5<      �� -   5        $ tbx_mw_CustomerName��5<   "   $
��Left=�6?   $��23��6Z   $��
152,-5,0,0q�6f   $
	 Wrap=�6z   $��CustomerName��6�   $	��Top=�6�   $	��208��6�   $��16(�6�   $��Tahoma)�6�    �� TextAlignment$ Right=�6�   ��6�   Z� 6�   �� 5=      -   5        $ tbx_mw_CustomerStreet��5=   "   $
��Left=�6A   $��23��6\   $��
152,18,0,0q�6h   $
	 Wrap=�6|   $��CustomerStreet��6�   $	��Top=�6�   $	��208��6�   $��16(�6�   $��Tahoma)�6�   $ Right=�6  ��6�   Z� 6  �� 5>      -   5        $ tbx_mw_CustomerCity��5>   "   $
��Left=�6?   $��23��6Z   $��	87,41,0,0q�6f   $
	 Wrap=�6y   $��Customer City,��6�   $	��Top=�6�   $	��170��6�   $��16(�6�   $��Tahoma)�6�   $ Right=�6   ��6�   Z� 6   �� 5?      -   5        $ tbx_mw_CustomerState��5?   "   $
��Left=�6@   $��23��6[   $��
257,41,0,0q�6g   $
	 Wrap=�6{   $��ST��6�   $	��Top=�6�   $��38��6�   $��16(�6�   $��Tahoma)�6�   $ Right=�6�    �� 	MaxLength$ 2��6  ��6�   Z� 6  �� 5@      -   5        $ 
tbx_mw_ZIP��5@   "   $
��Left=�66   $��23��6Q   $��
300,41,0,0q�6]   $
	 Wrap=�6q   $	��ZIP��6�   $	��Top=�6�   $��60��6�   $��16(�6�   $��Tahoma)�6�   $ Right=�6�   $ 5��6  ��6�   Z� 6  �� 5A      -   5        $
 	textBlock��5A   $   $
��Left=�67   $��69,2,0,0q�6R   $
��Wrap=�6d   $��	Customer:��6x   $	��Top=�6�   5B      5C      �� 5D      
���   �$��0,2,0,1q�6-   ._�5E      � $��18��6   $	��100��6'   $
��Left=�63   .+�5F      �� -   5        $
 tblk_mw_ItemNumberColumn��5F   $   $
��Left=�6F   $��
40,0,-40,0q�6a   $
��Wrap=�6u   $��Item Number:��6�   $	��Top=�6�   $	��100��6�   $��16��6�   $ Center=�6�   �� 5G      -    5        $
 tblk_mw_ItemNameColumn��5G   $   $
��Left=�6D   $��145,0,-369,0q�6_   $
��Wrap=�6u   $��
Item Name:��6�   $	��Top=�6�   $	��324��6�   $��16��6�   $ Center=�6�   �� 5H      -!   5        $#
 tblk_mw_ItemNumberColumn_Copy��5H   $   $
��Left=�6K   $��469,0,-469,-3q�6f   $
��Wrap=�6}   $	��Top=�6�   $	��100��6�   $��16��6�   $ Center=�6�   �� 6�   $�Cost:��6�   6�   �� 5I      -"   5        $$
 tblk_mw_ItemNumberColumn_Copy1��5I   $   $
��Left=�6L   $��569,0,-569,-3q�6g   $
��Wrap=�6~   $	��Top=�6�   $	��100��6�   $��16��6�   $ Center=�6�   $��Total��6�   �� 5J      -#   5        $$
 tblk_mw_ItemNumberColumn_Copy2��5J   $   $
��Left=�6L   $
��Wrap=�6g   $	��Top=�6{   $��40��6�   $��16��6�   $ Center=�6�   $��#��6�   5L      5M      �� 5N      $	��409��6   $
��Auto=�6,   .��5O      � -$   5        $ grd_mw_InvoiceItems��5O      5Q      5R      5U      5V      �� 5W   
   -%   5        $ 	btn_About��5W      $��About this Application��6$   $
��Left=�6E   $��790,327,0,0q�6`   $	��Top=�6u   $	��149��6�   $��54��6�   6�   5Y      5Z      