# MarkdownTextView

![alt tag](https://alexdunndev.files.wordpress.com/2017/02/xamagonmarkdown.png?w=1462)

A Xamarin.Forms component to display markdown text in a TextView

## Usage
- Clone repository
- Reference forms project in your Xamarin.Forms project
- Reference the platform libraries in your platform projects
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
- Create nuget package
- Add other properties for updating markdown settings
- Add text color settings
