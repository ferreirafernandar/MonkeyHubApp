using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMonkeyHubApiService _monkeyHubApiService;

        public ObservableCollection<Tag> Tags { get; }

        public Command AboutCommand { get; }

        public Command SearchCommand { get; }

        public Command<Tag> ShowCategoryCommand { get; }


        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            _monkeyHubApiService = monkeyHubApiService;

            Tags = new ObservableCollection<Tag>();

            AboutCommand = new Command(ExecuteAboutCommand);

            SearchCommand = new Command(ExecuteSearchCommand);

            ShowCategoryCommand = new Command<Tag>(ExecuteShowCategoryCommand);

        }

        private async void ExecuteSearchCommand()
        {
            // await PushAsync<SearchViewModel>;
        }

        private async void ExecuteShowCategoryCommand(Tag tag)
        {
            await PushAsync<CategoryViewModel>(tag);
        }

        private async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        public override async Task LoadAsync()
        {
            var tags = await _monkeyHubApiService.GetTagsAsync();

            Tags.Clear();
            foreach (var tag in tags)
            {
                Tags.Add(tag);
            }
        }
    }
}