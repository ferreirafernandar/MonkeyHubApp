using MonkeyHubApp.Models;
using MonkeyHubApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyHubApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as CategoryViewModel)?.LoadAsync();
            base.OnAppearing();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var content = (sender as ListView)?.SelectedItem as Content;
            (BindingContext as CategoryViewModel)?.ShowContentCommand.Execute(content);
        }
    }
}