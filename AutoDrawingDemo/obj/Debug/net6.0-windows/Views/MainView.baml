   M S B A M L   `   `   `  ????J  FAutoDrawingDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AutoDrawingDemoAutoDrawingDemo  5   	   G(clr-namespace:AutoDrawingDemo.ExtensionsAutoDrawingDemo.Extensions  5   	   $    AutoDrawingDemo.Views.MainView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:AutoDrawingDemo     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   ;
extensions(clr-namespace:AutoDrawingDemo.Extensions   5   	     ? WindowStyle$
  None=?5   	    ? WindowStartupLocation$ CenterScreen=?5	   	    ? AllowsTransparency$
 True??5
   	   $	??640??5   	   $
??1024??5   	    ? 
ResizeMode$ CanResizeWithGrip=?5   	      MaterialDesignBody#???   5   	   $??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	     MaterialDesignPaper#???  5   	   $??微软雅黑)?5   	   !  Prism.Mvvm.ViewModelLocator   AutoWireViewModel True5   	   .??5      )  #MaterialDesignThemes.Wpf.DialogHost  -   5          Name$ 
DialogHost??5          
Identifier$ 
RootDialog??5       	   DialogTheme$	 Inherit=?5       .??5   
   ? 
 ? RowDefinitions
 5   
   ?? 5      $
??Auto ?6   ?? 5      .+?5"      '  !MaterialDesignThemes.Wpf.Snackbar  -   5          Name$ Snackbar??5"   &    K? ZIndex$ 1??5$   &      MessageQueue 5#   &   4  .MaterialDesignThemes.Wpf.MessageQueueExtension  5$   &   (  "MaterialDesignThemes.Wpf.ColorZone  5%      -   5          Name$ 	ColorZone??5%   '   $??0??6:   $??10q?5&   #   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=?5'   #      Mode$ 
PrimaryMid=?5(   #   $	??Top=?5)   #   
???????.??5+      ]? $??False??6   .+?5-      ??  ?? Orientation$ 
Horizontal=?6   $??Right=?5.      .+?5/      ?? -   5         ??Name$ BtnMin??5/      $	??—??50      
??????? ! MaterialDesignFlatMidBgButton#??[ 52      ?? 53      -   5        $ BtnMax??53      $	??☐??54      
???????#??[ 56      ?? 57      -   5        $ BtnClose??57      $	??✕??58      
???????#??[ 5:      5;      ?? 5=      $ 
Horizontal=?6   .+?5>      ?? $??16,0,0,0q?6     MaterialDesignToolButton#??[ 5D       ? ToolTip$ 主页??5E      +  %MaterialDesignThemes.Wpf.RippleAssist   Feedback 5?      ??  ?? RelativeSource ?? *Self+ ?? Path$ 
Foreground?  BrushRoundConverter ?? 	Converter# [ ??5@      ?? *HomeCommand+??5A      0	  *MaterialDesignThemes.Wpf.PackIconExtension	   	  Kind$
 Home=? 	  Size 24??5C      ??  ??  ?? AncestorType" ?$ (TextElement.Foreground)?5E      ?? 5F      $??Center=?6    $??Center=?5G       $??22(?5H       $??Auto Drawing Demo??5I       $
??20,0q?5J       5K      5M      5O      ?? 5Q   
   $??1??6   -
   'AutoDrawingDemo.Extensions.PrismManager 
  MainViewRegionName!  Prism.Regions.RegionManager   
RegionName# Z 5R      5T      5U      5V      