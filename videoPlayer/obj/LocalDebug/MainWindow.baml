   M S B A M L   `   `   `  ���� F  BvideoPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:videoPlayervideoPlayer      videoPlayer.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:videoPlayer       � Title$  
MainWindow�� � Left$ 0��
 � Top$ 0��$	��800��$	��600�� � 
ResizeMode$ NoResize=�$��0q� � WindowStyle$
 None=� � AllowsTransparency . 
������  � Topmost .  � Cursor$
 None��.��� .+��� -    ��Name$ ImageBackGround��$��.\Content\aof3.png��$
��Fill=��� -   $ 
ImageFlyer��$��.\Content\aof2.PNG��$	��300��$	��300��$	��500��$	��500��$��
399,-5,1,5q�z�V� .~��� 	 �� Angle$	 5Q�T� '� $��0q�.x�g� 
 g� Content
 h� .����  �� Color$ White���  � Geometry k�  k� 	Positions5 �   x����h���* �h���* X� x���X�  k� TriangleIndices �      k� TextureCoordinates �    � Material u�  u� Brush �� -   MarqueeImageBrush�� �� ImageSource$ .\Content\3wonders.PNG�� j� 	Transform Y� .���  �� Rotation ��  �� Axis$ 0,1,05� �� Angle$ 12Q���  �� ScaleY$
 0.15Q� �� ScaleX$
 0.50Q�S�  S� OffsetX$
 -1.1Q� S� OffsetY$ 1Q�� -   
VideoModel�� k� 5 �   x����h���* �h���* X� x���X�  �      �    u�  !�  !� Visual r� -   $��VideoPLayer�� r� LoadedBehavior$
 Play=� r� UnloadedBehavior$ Manual=� Y� .���  �� $ 0,1,0.35�$ 22Q��� $
 0.35Q�$
 0.50Q�S� $ -1Q�$
 0.28Q� '� Camera =�  � Position$ 	0, 0, 3.51�  � LookDirection$  0, 0, -15�! � UpDirection$! 0, 1, 05�" =� FieldOfView$" 60Q��� -   $ 	ImageLeft���� -   $ ImageCabinent��$
��Left=�$	��Top=�$	��500��$��.\Content\cab_body.png���� -   $ ImageManufactuer��$��.\Content\Konami.png��$	��300��$	��150��$��460,450,40,0q�