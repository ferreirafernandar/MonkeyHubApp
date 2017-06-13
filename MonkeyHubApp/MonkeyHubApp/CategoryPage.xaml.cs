using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
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
    public partial class CategoryPage : ContentPage
    {
        private CategoryViewModel ViewModel => BindingContext as CategoryViewModel;
        public CategoryPage()
        {
            InitializeComponent();
            //var monkeyHubApiService = DependencyService.Get<IMonkeyHubApiService>();
            //var tag = new Tag();
            //BindingContext = new CategoryViewModel(monkeyHubApiService, tag);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                await ViewModel.LoadAsync();
            }
        }
    }

}