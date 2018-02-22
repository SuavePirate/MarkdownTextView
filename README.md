# MarkdownTextView

![alt tag](https://alexdunndev.files.wordpress.com/2017/02/xamagonmarkdown.png?w=1462)

A Xamarin.Forms component to display markdown text in a TextView
# Installation

Now on NuGet!

`Install-Package MarkdownTextView.Forms`

https://www.nuget.org/packages/MarkdownTextView.Forms


## Usage
- Call `Init` before calling `Xamarin.Forms.Init()`
- iOS: SPControls.MarkdownTextView.iOS.MarkdownTextView.Init();
- Android: SPControls.MarkdownTextView.Droid.MarkdownTextView.Init();
- Use the control
- In Xaml:

``` 
<ContentPage ...
             xmlns:spcontrols="clr-namespace:SPControls.Forms;assembly=SPControls.MarkdownTextView"
             ...>
  <spcontrols:MarkdownTextView Markdown="{Binding MarkdownString}" />
</ContentPage>
```
- or in C#:
```
var mdTextView = new MarkdownTextView();
mdTextView.Markdown = "# this is my *header* tag";
```
             
## TODO
- Add other properties for updating markdown settings
- Add text color settings
- Add UWP Support

# Sponsors

