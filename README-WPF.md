# Font-Awesome-WPF

WPF controls for the iconic font and CSS toolkit Font Awesome.

Font Awesome: http://fortawesome.github.io/Font-Awesome/
- Current Version: v4.5.0

*Warning* Breaking changes from version 4.4.0.6 and 4.5.0.7 (enum generation was changed due to overlapping names in Font Awesome 4.5).

## Getting started

### Install

To install FontAwesome.WPF, run the following command in the Package Manager Console:
```
PM> Install-Package FontAwesome.WPF
```

Or search & install the package via the NuGet Package Manager.


### Usage XAML

```
<Window x:Class="Example.FontAwesome.WPF.Single"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:fa="http://schemas.fontawesome.io/icons/"
        Title="Single" Height="300" Width="300">
    <Grid  Margin="20">
        <fa:ImageAwesome Icon="Flag" VerticalAlignment="Center" HorizontalAlignment="Center" />
    </Grid>
</Window>
```

You can also use the TextBlock based control.
```
<fa:FontAwesome Icon="Flag" />
```

> The Image based `ImageAwesome` control is useful when you need to fill an entire space. Whereas the TextBlock base `FontAwesome` is useful when you need a certain FontSize. 

You can also work with existing ContentControl based controls, like Button, without having to go back to the Font Awesome cheatsheet and look for that Unicode sequence. Use the `Awesome.Content` attached property and select an icon enum value through IntelliSense. Do not forget to set the FontFamily on element or in its style.  

```xaml
<Button fa:Awesome.Content="Flag" 
        TextElement.FontFamily="pack://application:,,,/FontAwesome.WPF;component/#FontAwesome"/>
```

> VS2013 XAML Designer has issues when using fonts embedded in another assembly (like this scenario), which prevents it to dispaly the glyph properly.  
You could either grab a copy of TTF font file and include it in you Project as a Resource, so to use it in FontFamily, or you could follow the advices proposed in this [StackOverflow thread](http://stackoverflow.com/questions/29615572/visual-studio-designer-isnt-displaying-embedded-font/29636373#29636373). 

#### Binding

The Icon Property is a DependencyProperty so it can be used with-in a {Binding}. There is an example in the example project.


### Usage Code-Behind

If you want to create an Image from Code-Behind (e.g. setting the Window.Icon):

```C#
Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.Black);
```

## WPF Example

![alt text](/doc/screen-example.png "Example")

Can be found in /example-wpf/ folder.

## Spinning Icons
```
<fa:ImageAwesome Icon="Spinner" Spin="True" SpinDuration="10" />
```
Further examples: http://fontawesome.io/examples/#spinning

Please note: It is advised to use the ImageAwesome Control for spinning icon's due to a line height side-effect with glyph fonts. (The rotation does not occur in the middle of the icon.)

## Rotated / Flipped
```
<fa:ImageAwesome Icon="Spinner" FlipOrientation="Horizontal" Rotation="90" />
```
http://fontawesome.io/examples/#rotated-flipped

## Icons

All icons including their aliases are generated from FontAwesomes' icons.yaml. 

```C#
public enum FontAwesomeIcon {
....
///<summary>cog (created: 1.0)</summary>
///<see cref="http://fontawesome.io/icon/cog/" />
[IconCategory("Web Application Icons"),IconCategory("Spinner Icons")]
[Description("cog")]
Cog = 0xf013,
///<summary>Alias of: Cog</summary>
///<see cref="F:FontAwesome.WPF.FontAwesomeIcon.Cog" />
[IconAlias]
Gear = Cog,
....
}
```

Following meta data is added:
* Icon
	* XML-Doc <summary> from name with created reference.
	* XML-DOC <see /> for direct link to icon web page.
	* IconCategory Attributes, one per category
	* Description Attribute, name
* Alias
	* XML-Doc <summary> Alias of: referencing icon
	* XML-Doc <see /> to referencing field (to reduce code file length)
	* IconAlias Attribute
	
## Converters

The converters are also MarkupExtension, so than can be used in-place without creating a StaticRessource.

### CssClassNameConverter

Say you have (or need) the css class name of a font awesome icon (e.g. "flag"), this converter takes the FontAwesomeIcon enum and translates its value (using reflection) to its css class name (or vice-versa).

There are two modes:
* FromStringToIcon (Default mode. Expects a string and converts to a FontAwesomeIcon.)
* FromIconToString (Expects a FontAwesomeIcon and converts it to a string.)

Example usage:
```
<TextBlock Text="{Binding Path=FontAwesomeIcon, Converter={fa:CssClassNameConverter Mode=FromIconToString}}" Grid.Column="1" Grid.Row="1" />
```

### ImageSourceConverter

Use this converter if you require an ImageSource of a FontAwesomeIcon. Use the ConverterParameter to pass the Brush (default is a solid black brush). This is useful for existing wpf controls.

Example usage:
```
<SolidColorBrush x:Key="ImageBrush"  Color="LightBlue" />
...
<Image 
    Source="{Binding Path=FontAwesomeIcon, Converter={fa:ImageSourceConverter}, ConverterParameter={StaticResource ImageBrush}}"
    />
```	


## Credits
* @punker76 - hangable Duration for Spinning Icons, Rotated & Flipped icons
* @BrainCrumbz - Add Awesome.Content attached dependency property
* @MendelMonteiro - better nuget pacakge
* @robertmuehsig - enhanced sample with animation