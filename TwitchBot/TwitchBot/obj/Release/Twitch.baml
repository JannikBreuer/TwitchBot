   M S B A M L   `   `   `  ���� D  @TwitchBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TwitchBot	TwitchBot      TwitchBot.TwitchBotWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:TwitchBot       � Title$  	TwtichWin��$	��450��$	��800�� � WindowStyle$
 None=� � 	Resources    MyButton �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property	 P� Value��%  &	        �� " ��.���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R��� $��border��$��0q�
���   �#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver	 . . ���  �@J�Opacity$ 1Q�.��� 
 � RowDefinitions
 �� $
��289* ��� $
��131* �.+�� $	��202��$
��Left=�$	��420�����	#FF3F3F46$��2��$��Bottom=�$��	0,0,0,0.2q�.+��� .+��� -    ��Name$ img_Icon��$8��2C:\Users\j.breuer\source\repos\TwitchBot\icon2.png��$��70��$��0,10,10.6,0q�� -   $��grid_Button1��.+��� -   $��btn_UserList��$��61��$��
0,20,0.6,0q����	#FF3F3F46$��0q�$��Userlist��$��Book Antiqua)�$��20(�
�������$��Right=�$��Bottom=�$	��132��#��[  �� -   $��img_UserList��$��	0,0,0,9.8q�$��60��$��60��$J��DC:\Users\j.breuer\source\repos\TwitchBot\UserListBackroundNormal.jpg��$
��Left=�$��Bottom=�� -   $��grid_Button2��.+��� -   $��btn_Messages��$��61��$��
0,20,0.6,0q����	#FF3F3F46$��0q�$��Userlist��$��Book Antiqua)�$��20(�
�������$��Right=�$��Bottom=�$	��132��#��[  �� -   $��img_Messages��$��	0,0,0,9.8q�$��60��$��60��$J��DC:\Users\j.breuer\source\repos\TwitchBot\UserListBackroundNormal.jpg��$
��Left=�$��Bottom=�� -   $��grid_Button3��.+��� -	   $��btn_Roulette��$��61��$��
0,20,0.6,0q����	#FF3F3F46$��0q�$��Userlist��$��Book Antiqua)�$��20(�
�������$��Right=�$��Bottom=�$	��132��#��[  �� -
   $��img_Roulette��$��	0,0,0,9.8q�$��60��$��60��$J��DC:\Users\j.breuer\source\repos\TwitchBot\UserListBackroundNormal.jpg��$
��Left=�$��Bottom=��� $��Right=�$��0,0,-0.4,0.2q�$��2��$	��592��.��� $��Right=�$��Bottom=�$	��576��$	��420�����	#FF796F6F.+�� -   $��grid_UserList��$y�Visible=�.+�� $��Bottom=�$��20�����	#FF3F3F46.+��� -   $��btn_RefreshUserList��$��
545,0,10,0q����	#FF3F3F46$��0q�.���� $:��4C:\Users\j.breuer\source\repos\TwitchBot\refresh.png���� -   $��stackPanel_UserList��� -   $��grid_Message��.+��� -   $��stackPanel_Message��� $	��Top=�$��25�����	#FF1686FF.+��� $��X��$��19(�$��25��$��Medium"�
����������	#FF1686FF$��749,0,9.6,0q�$��0q��� $��-��$��25��$��16(�$��Heavy"�
����������	#FF1686FF$��706,0,62.6,0q�$��0q�