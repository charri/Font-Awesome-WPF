# Font-Awesome-UWP

UWP glyph control for the iconic font and CSS toolkit Font Awesome.

Font Awesome: http://fontawesome.io/
- Current Version: v4.7.0


## Getting started

### Install

To install FontAwesome.UWP, run the following command in the Package Manager Console:
```
PM> Install-Package FontAwesome.UWP
```

Or search & install the package via the NuGet Package Manager.

> Please note: The FontAwesome.WPF package also targets for UWP. (As "WPF" is part of the package name it makes more sense to have a different package for UWP aswell.)


### Usage XAML

```
<Window x:Class="Example.FontAwesome.UWP.Single"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:fa="using:FontAwesome.UWP"
        Title="Example" Height="300" Width="300">
    <Grid  Margin="20">
        <fa:FontAwesome Icon="Flag" FontSize="90" HorizontalAlignment="Center" />
    </Grid>
</Window>
```


#### Binding

The Icon Property is a DependencyProperty so it can be used with-in a {Binding}. There is an example in the example project.

```
<fa:FontAwesome 
    Icon="{Binding CurrentIcon.Icon}"
    FontSize="{Binding ElementName=FontSizeSlider, Path=Value}"
    HorizontalAlignment="Center"
 />
```

## UWP Example

![alt text](/doc/screen-example-uwp.png "Example")

Can be found in /example-uwp/ folder.

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


## Credits
* @robertmuehsig - UWP proof of concept
* @Alex-Witkowski - UWP tips
* @dotnetnoobie - Further proof of concept
* @lufka - Performance improvements