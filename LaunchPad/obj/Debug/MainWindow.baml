   M S B A M L   `   `   `  ����D  @LaunchPad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:LaunchPad.ViewModelLaunchPad.ViewModel  5   	   1clr-namespace:LaunchPad.ViewsLaunchPad.Views  5   	       LaunchPad.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8
viewModels!clr-namespace:LaunchPad.ViewModel     5   	   /viewsclr-namespace:LaunchPad.Views     5   	     � Title$  VHS_ArcadeOS��5	   	   $	��600��6   $	��800��6+    � Left$ 0��5
      
 � Top$ 0��5   	   $��0q�5   	    � WindowStyle$
 None=�6    � 	Resources 5       �� DataType+   %LaunchPad.ViewModel.ViewModelsDrivers%   LaunchPad.Views.ControlsDrivers � DataContext'   !LaunchPad.ViewModel.ViewModelHome   LaunchPad.Views.Home*   $LaunchPad.ViewModel.ViewModelOptions$   LaunchPad.Views.ControlOptions��%P  (��       5   4   *, +)(�� X     5   1   *, +)(�� �     5   4   *, +)�� 5   
   driversViewTemplate��"  64   .R�5         6$   �� 5      �� 5   
   homeViewTemplate��"  61   .R�5         6   �� 5      �� 5   
   optionsViewTemplate��"  64   .R�5         6#   �� 5      5      .��5      � -   5         �Name$ 
LayoutRoot��5       � ColumnDefinitions 5   
   �� 5      $��350px �6   �� 5      $��* �6   .+�5   
   �� -   5        $!��StackPanelLaunchPadControls��5      	 �� Orientation$	 Vertical=�68   $��0��6O   .+�5      �� -   5        $��ButtonHomeArcade��5      $��0 50 0 0q�6-   $	��100��6?   
�������$��0q�6_   
�������.��5      ��    BadMofo#��[  6   $��Normal$�6B   $��24(�6U   Home6k   5       �� 5!      -   5        $��ButtonStartArcade��5!      $��0 50 0 0q�6.   $	��100��6@   
�������$��0q�6`   
�������.��5"      �� #��[  6   $��Normal$�6B   $��24(�6U   Start Arcade Cab6w   5#      �� 5$      -   5        $��ButtonConfigureArcade��5$      $��0 50 0 0q�62   $	��100��6D   
�������$��0q�6d   
�������.��5%      �� #��[  6   $��Normal$�6B   $��24(�6U   Configure Cab Settings6}   5&      �� 5'      -   5        $��ButtonShutDownArcade��5'      $��0 50 0 0q�61   $	��100��6E   
�������$��0q�6e   
�������.��5(      �� #��[  6   $��Normal$�6B   $��24(�6U   Shut Down Arcade6w   5)      5*      �� 5+   
   $��1��6   ��6+   �� 6A   5-      5.      