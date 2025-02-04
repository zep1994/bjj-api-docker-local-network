using BjjTrainer.Models.Schools;
using System.Net.Http.Json;

namespace BjjTrainer.Services.Schools
{
    public class SchoolService : ApiService
    {
        // Get all schools
        public async Task<List<School>> GetAllSchoolsAsync()
        {
            try
            {
                var schools = await HttpClient.GetFromJsonAsync<List<School>>("school");
                return schools ?? new List<School>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching schools: {ex.Message}");
            }
        }

        // Get school by ID
        public async Task<School> GetSchoolByIdAsync(string id)
        {
            try
            {
                var school = await HttpClient.GetFromJsonAsync<School>($"school/{id}");
                if (school == null)
                {
                    throw new Exception("School not found.");
                }
                return school;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching school by ID: {ex.Message}");
            }
        }

        // Create a new school
        public async Task CreateSchoolAsync(School school)
        {
            try
            {
                var response = await HttpClient.PostAsJsonAsync("school/create", school);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error creating school: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating school: {ex.Message}");
            }
        }

        // Update an existing school
        public async Task UpdateSchoolAsync(int id, School school)
        {
            try
            {
                var response = await HttpClient.PutAsJsonAsync($"school/update/{id}", school);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error updating school: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating school: {ex.Message}");
            }
        }

        // Delete a school
        public async Task DeleteSchoolAsync(int id)
        {
            try
            {
                var response = await HttpClient.DeleteAsync($"school/delete/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error deleting school: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting school: {ex.Message}");
            }
        }
    }
}
