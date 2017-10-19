# MarkdownTextView

![alt tag](https://alexdunndev.files.wordpress.com/2017/02/xamagonmarkdown.png?w=1462)

A Xamarin.Forms component to display markdown text in a TextView
# Installation

Now on NuGet!

`Install-Package MarkdownTextView.Forms`

https://www.nuget.org/packages/MarkdownTextView.Forms


## Usage
- Call `MarkdownTextView.Init()` before calling `Xamarin.Forms.Init()`
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
<a target='_blank' rel='nofollow' href='https://app.codesponsor.io/link/JvDSfZ39KwEWLYqSChESgBc9/SuavePirate/MarkdownTextView'>
  <img alt='Sponsor' width='888' height='68' src='https://app.codesponsor.io/embed/JvDSfZ39KwEWLYqSChESgBc9/SuavePirate/MarkdownTextView.svg' />
</a>
