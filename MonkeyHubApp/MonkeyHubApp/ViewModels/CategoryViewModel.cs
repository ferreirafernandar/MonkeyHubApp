using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{

    public class CategoryViewModel : BaseViewModel
    {
        private readonly IMonkeyHubApiService _monkeyHubApiService;
        private readonly Tag _tag;

        public ObservableCollection<Content> Contents { get; }

        public Command<Content> ShowContentCommand { get; }

        public CategoryViewModel(IMonkeyHubApiService monkeyHubApiService, Tag tag)
        {
            _monkeyHubApiService = monkeyHubApiService;
            _tag = tag;

            Contents = new ObservableCollection<Content>();
            ShowContentCommand = new Command<Content>(ExecuteShowContentCommand);

           // Title = "Eventos";
        }

        private async void ExecuteShowContentCommand(Content content)
        {
            await PushAsync<ContentWebViewModel>(content);
        }

        public async Task LoadAsync()
        {
            var contents = await _monkeyHubApiService.GetContentsByTagIdAsync(_tag.Id);

            Contents.Clear();
            foreach (var content in contents)
            {
                Contents.Add(content);
            }
        }
    }
}