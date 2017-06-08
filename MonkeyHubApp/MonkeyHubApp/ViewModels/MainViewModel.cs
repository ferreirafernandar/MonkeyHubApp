using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Tag> Tags { get; }

        public Command SearchCommand { get; }

        public Command AboutCommand { get; }

        public Command<Tag> ShowCategoryCommand { get; }

        private readonly IMonkeyHubApiService _monkeyHubApiService;

        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            _monkeyHubApiService = monkeyHubApiService;

            Tags = new ObservableCollection<Tag>();

           SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            AboutCommand = new Command(ExecuteAboutCommand);

            ShowCategoryCommand = new Command<Tag>(ExecuteShowCategoryCommand);

        }

        private async void ExecuteShowCategoryCommand(Tag tag)
        {
            await PushAsync<CategoryViewModel>(_monkeyHubApiService, tag);
        }

        private async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        private async void ExecuteSearchCommand()
        {
            await Task.Delay(2000);
            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");

            await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado", "OK");

            var tagsRetornadasDoServico = await _monkeyHubApiService.GetTagsAsync();

            Tags.Clear();

            if (tagsRetornadasDoServico != null)
            {
                foreach (var tag in tagsRetornadasDoServico)
                {
                    Tags.Add(tag);
                }

            }
        }

        private bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }
    }
}