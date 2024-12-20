using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace LabTest.ViewModels
{
    public partial class QuestionThreeViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseAddress = "https://jsonplaceholder.typicode.com/posts";
        
        [ObservableProperty]
        private ObservableCollection<PostRecord> posts;
        
        [ObservableProperty]
        private PostRecord selectedPost;
        
        [ObservableProperty]
        private string title;
        
        [ObservableProperty]
        private string body;
        
        public QuestionThreeViewModel()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            Posts = new ObservableCollection<PostRecord>();
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadPosts();
        }

        [RelayCommand]
        private async Task LoadPosts()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PostRecord>>("/posts");
                if (response != null)
                {
                    Posts.Clear();
                    foreach (var post in response)
                    {
                        Posts.Add(post);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task AddPost()
        {
            try
            {
                var post = new PostRecord
                {
                    Title = Title,
                    Body = Body,
                    UserId = 1
                };
                    var response = await _httpClient.PostAsJsonAsync("/posts", post);
                response.EnsureSuccessStatusCode();
                var createdPost = await response.Content.ReadFromJsonAsync<PostRecord>();
                if (createdPost != null)
                {
                    Posts.Add(createdPost);
                    Title = string.Empty;
                    Body = string.Empty;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task UpdatePost(PostRecord post)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/posts/{post.Id}", post);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task DeletePost(PostRecord post)
        {
            if (await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this post?", "Yes", "No"))
            {
                try
                {
                    var response = await _httpClient.DeleteAsync($"/posts/{post.Id}");
                    response.EnsureSuccessStatusCode();
                    Posts.Remove(post);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }

    public class PostRecord
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }

} 