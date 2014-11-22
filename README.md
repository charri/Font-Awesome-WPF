# Font-Awesome-WPF

WPF controls for the iconic font and CSS toolkit Font Awesome.

Font Awesome: http://fortawesome.github.io/Font-Awesome/
- Current Version: v4.2

## Getting started

### Install

To install FontAwesome.WPF, run the following command in the Package Manager Console:
```
PM> Install-Package FontAwesome.WPF
```

### Usage XAML

```
<Window x:Class="Example.FontAwesome.WPF.Single"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:fa="clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPF"
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

> The Image base <fa:ImageAwesome /> control is useful when you need to fill an entire space. Whereas the TextBlock base <fa:FontAwesome /> is advantages when you need a certain FontSize. 

### Usage Code-Behind

```C#
Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.Black);
```

## WPF Example

![alt text](/doc/screen-example.png "Example")

Can be found in /example/ folder.