   M S B A M L   `   `   `  ����  Schwimmbad_Release)    #SchwimmbadNachhaltigkeit.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Schwimmbad Nachhaltigkeit��5   	   $	��641��6+   $
��1058��68   ���#F4F4F4 � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%^  '��       '�� �     �� 5   
   " ��6   .�5	      ��  � �� �#F3F4F66+   �� 5
       � ��
 ���� 6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 5q�6(   5      �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   #��z��5   /   $
��10,2q�5   /   5      5      5      �� 5       � ��$ 14(�6)   �� 5       � ��$ 10q�6(   �� 5       � ��
 �����6+   5       5!      .��5#      � $��0,0,0,-6q�6    � ColumnDefinitions 5$   
   �� 5%      $	��26* �6   �� 5&      $
��297* �6   �� 5'      $
��115* �6   �� 5(      $
��538* �6   .+�5+   
   �� $
��Left=�6   $	��Top=�60   $��
201,20,0,0q�6H   $��1��6\   $��3��6l   .+�5,      �� $/��)🌊 Schwimmbad Nachhaltigkeitsmanagement��6   $��24(�5-      $
��Bold"�6&   ���#1E3A8A �� TextAlignment$ Center=�6M   �� 5.      $L��FErfassen und Berichten Sie nachhaltige Maßnahmen für Ihr Schwimmbad.��6   $��14(�5/      ���#666666$ Center=�6;   $��0,5,0,20q�6R   50      ]� 53   
   $��20,80,20,20q�6   $��4��6)   .+�55      �� 
�������$ 10��6(   	 �� Padding$	 20q�6:   $��0,0,20,0q�6G   $	��300��6Y   ._�56      �� .+�57      �� $��Wasserverbrauch (Liter):��6    $��14(�6@   $��0,0,0,5q�6N   �� 58      -   5        
 ��Name$
 WasserverbrauchInput��58      $��30��6<   $��0,0,0,10q�6H   ���#F3F4F6$��0q�6o   $��5q�6�   �� 5:      $��Reinigungsmittel:��6    $��14(�69   $��0,0,0,5q�6G   �� 5;      -   5         ��Name$ ReinigungsmittelComboBox��5;      $��30��6A   $��0,0,0,10q�6M    �� 
IsEditable$
 True��5<       �� IsDropDownOpen$
 True��6   $	��Top=�63   �� 5@      $��Solarenergie (kWh):��6    $��14(�6;   $��0,0,0,5q�6I   �� 5A      -   5        $
 SolarEnergieInput��5A      $��30��69   $��0,0,0,20q�6E   ���#F3F4F6$��0q�6l   $��5q�6�   �� 5D      $ ��Optionen für den Bericht:��6    $��14(�6A   $��0,0,0,10q�6O   ���#3333336a   �� 5E      -   5         ��Name$ AiSuggestionCheckBox��5E      $7��1AI generierte Verbesserungsvorschläge generieren��6=   $��0,0,0,10q�6x   �� 5F      -   5        $ AllowImagesCheckBox��5F      $#��Grafiken im Bericht einbinden��6<   $��0,0,0,10q�6d   �� 5G      -   5        $ EcoFriendlyCheckBox��5G      $��ECO-Bewertung hinzufügen��6<   $��0,0,0,20q�6_   �� 5J      -   5        $��📋 Daten hinzufügen��5J      ���#4ADE80$
��Bold"�5K   2   $��35��6D   $��0,0,0,10q�6P   �� 5L      -   5        $.��(🗑️ Ausgewählten Datensatz löschen��5L      ���#F43F5E$
��Bold"�5M   2   $��35��6D   $��0,0,0,10q�6P   �� 5N      -	   5        $&�� 📄 Bericht als PDF exportieren��5N      ���#3B82F6$
��Bold"�5O   2   $��35��6D   5P      5Q      �� 5T      
�������$ 10��6(   $	 10q�6:   $��10,0,0,0q�6G   $��Right=�6Y   ._�5U      �� -
   5         ��Name$ DataListView��5U      $��0q�61    �� View 5V      �� 5W      .P�5X      �� $��
📅 Datum��6-    �� Width$	 150��6f    �� DisplayMemberBinding 6?   �� *Datum+6f   �� 5Y      $��🌊 Wasserverbrauch��6-   $	 150��6z    6I   �� *Wasserverbrauch+6z   �� 5Z      $��🧴 Reinigungsmittel��6-   $	 150��6|    6J   �� *Reinigungsmittel+6|   �� 5[      $��☀️ Solarenergie��6-   $	 150��6t    6F   �� *SolarEnergie+6t   5\      5^      5_      5`      �� 5c   
   $
��Left=�6   $��57��6+   $��	10,20,0,0q�67   $	��Top=�6J   $	��130��6b   $��logo.png��6n   $��2��6�   5d      5e      