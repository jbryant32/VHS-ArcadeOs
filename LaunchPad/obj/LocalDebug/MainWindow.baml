   M S B A M L   `   `   `  ���� D  @LaunchPad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:LaunchPad.ViewModelLaunchPad.ViewModel  1clr-namespace:LaunchPad.ViewsLaunchPad.Views      LaunchPad.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8
viewModels!clr-namespace:LaunchPad.ViewModel     /viewsclr-namespace:LaunchPad.Views       � Title$  VHS_ArcadeOS��$	��600��$	��800�� � Left$ 0��
 � Top$ 0��$��0q� � WindowStyle$
 None=� � 	Resources  �� DataType+   %LaunchPad.ViewModel.ViewModelsDrivers%   LaunchPad.Views.ControlsDrivers � DataContext'   !LaunchPad.ViewModel.ViewModelHome   LaunchPad.Views.Home*   $LaunchPad.ViewModel.ViewModelOptions$   LaunchPad.Views.ControlOptions��%�   (��       *, +)(�� 3     *, +)(�� c     *, +)�� driversViewTemplate��"  .R�   �� �� homeViewTemplate��"  .R�   �� �� optionsViewTemplate��"  .R�   �� .��� -    �Name$ 
LayoutRoot�� � ColumnDefinitions �� $��350px ��� $��* �.+��� -   $!��StackPanelLaunchPadControls��	 �� Orientation$	 Vertical=�$��0��.+��� -   $��ButtonHomeArcade��$��0 50 0 0q�$	��100��
�������$��0q�
�������.����    BadMofo#��[  $��Normal$�$��24(�Home�� -   $��ButtonStartArcade��$��0 50 0 0q�$	��100��
�������$��0q�
�������.���� #��[  $��Normal$�$��24(�Start Arcade Cab�� -   $��ButtonConfigureArcade��$��0 50 0 0q�$	��100��
�������$��0q�
�������.���� #��[  $��Normal$�$��24(�Configure Cab Settings�� -   $��ButtonShutDownArcade��$��0 50 0 0q�$	��100��
�������$��0q�
�������.���� #��[  $��Normal$�$��24(�Shut Down Arcade�� $��1������ 