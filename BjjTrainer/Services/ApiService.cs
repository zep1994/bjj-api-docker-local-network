using BjjTrainer.Models.Lessons;
using System.Net.Http.Json;

namespace BjjTrainer.Services
{
    public class ApiService
    {
        protected HttpClient HttpClient { get; private set; }

        public ApiService()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = GetApiBaseUrl()
            };
        }

        private Uri GetApiBaseUrl()
        {
            return DeviceInfo.Platform == DevicePlatform.Android
                ? new Uri("http://10.0.2.2:5057/api/")
                : new Uri("http://localhost:5057/api/");
        }
    }
}
