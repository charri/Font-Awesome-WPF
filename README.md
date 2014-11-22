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

Add Namespace to XAML
```
xmlns:fa="clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPF"
```

Use the TextBlock based control
```
<fa:FontAwesome Icon="Flag" />
```

Or the Image rendered based control
```
<fa:ImageAwesome Icon="Flag" />
```

### Usage Code-Behind

```C#
Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.Black);
```

## WPF Example

![alt text](/doc/screen-example.png "Example")

Can be found in /example/ folder.