using System;

using Xamarin.Forms;

namespace PresentationApp
{
    public class chat : ContentPage
    {
        public chat()
        {
            //ボタンを生成
            var audience_button = new Button { Text = "聴衆者" };
            //ボタンクリック時の処理

            this.Content = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Label { Text = "Audience Main Page" }
                }
            };
        }
    }
}


