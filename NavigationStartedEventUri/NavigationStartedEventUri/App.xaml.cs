using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xam.Plugin.Abstractions;
using Xamarin.Forms;
using System.Diagnostics;

namespace NavigationStartedEventUri
{
    public partial class App : Application
    {
        public string htmlString(string wvName)
        {
            return $@"
<html>
<head>
<meta charset='utf-8'/>
</head>
<body>
<div id='main'>
<h1>Title</h1>
<a href='http://www.google.com'>google</a>
</div>
<div id='debug'>
initial
</div>
<script>
function getH(){{
    var debug = document.getElementById('debug');
    var main = document.getElementById('main');
    var h = main.scrollHeight;
    debug.innerHTML = h;
    test(h.toString());
}}
</script>
</body>
</html>
";

        }
        Button button = new Button();
        StackLayout stack = new StackLayout();
        public App()
        {

            //FormsWebView wv = new FormsWebView();
            //wv.ContentType = Xam.Plugin.Abstractions.Enumerations.WebViewContentType.StringData;
            //wv.RegisterCallback("testA", s => { Debug.WriteLine("from A: " + s); });
            //wv.Source = htmlString("A");
            //wv.HeightRequest = 300.0;
            //wv.WidthRequest = 300.0;
            //wv.OnContentLoaded += Wv_OnContentLoaded;


            //FormsWebView wv2 = new FormsWebView();
            //wv2.ContentType = Xam.Plugin.Abstractions.Enumerations.WebViewContentType.StringData;
            //wv2.RegisterCallback("testB",s => { Debug.WriteLine("from B: "+s); });
            //wv2.Source = htmlString("B");
            //wv2.HeightRequest = 300.0;
            //wv2.WidthRequest = 300.0;
            //wv2.OnContentLoaded += Wv2_OnContentLoaded;

            button.Text = "Initial";
            button.Clicked += Button_Clicked;

            //stack.Children.Add(wv);
            
            // The root page of your application
            var content = new ContentPage
            {
                Title = "TestApp",
                Content = new StackLayout() {
                    Children = {
                        button,
                        stack
                    }
                }
            };
            //content.Appearing += Content_Appearing;
            MainPage = new NavigationPage(content);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            stack.Children.Clear();
            FormsWebView wv3 = new FormsWebView();
            int id = wv3.GetHashCode();
            wv3.ContentType = Xam.Plugin.Abstractions.Enumerations.WebViewContentType.StringData;
            //wv3.RemoveAllLocalCallbacks();
            //wv3.RemoveAllGlobalCallbacks();
            wv3.RegisterLocalCallback("test", s => { Debug.WriteLine($"from {id}: " + s); });
            wv3.Source = htmlString(id.ToString());
            wv3.HeightRequest = 300.0;
            wv3.WidthRequest = 300.0;
            wv3.OnContentLoaded += Wv3_OnContentLoaded;
            stack.Children.Add(wv3);
        }

        private void Wv3_OnContentLoaded(Xam.Plugin.Abstractions.Events.Inbound.ContentLoadedDelegate eventObj)
        {
            Debug.WriteLine("content loaded");
            eventObj.Sender.InjectJavascript($"getH();");
        }

        //private void Wv2_OnContentLoaded(Xam.Plugin.Abstractions.Events.Inbound.ContentLoadedDelegate eventObj)
        //{
        //    Debug.WriteLine("content loaded");
        //    eventObj.Sender.InjectJavascript("getHB();");
        //}

        //private void Wv_OnContentLoaded(Xam.Plugin.Abstractions.Events.Inbound.ContentLoadedDelegate eventObj)
        //{
        //    Debug.WriteLine("content loaded");
        //    eventObj.Sender.InjectJavascript("getHA();");
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
