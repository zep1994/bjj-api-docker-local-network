using System.Net.Http.Headers;

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


        // HELPERS
        protected void AttachAuthorizationHeader()
        {
            var token = Preferences.Get("AuthToken", string.Empty);
            if (!string.IsNullOrEmpty(token))
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
