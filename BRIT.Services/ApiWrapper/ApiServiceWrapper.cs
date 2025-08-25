using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BRIT.Models.Entities;
using Microsoft.Extensions.Options;

namespace BRIT.Services.ApiWrapper
{
    public class ApiServiceWrapper : IApiServiceWrapper
    {
        private readonly HttpClient _client;
        private readonly ApiServiceSettings _settings;
        public ApiServiceWrapper(HttpClient client, IOptionsMonitor<ApiServiceSettings> settings)
        {
            _client = client;
            _settings = settings.CurrentValue;
            _client = client;
            _client.BaseAddress = new Uri(_settings.Uri);
        }

        //Get Angestellte
        public async Task<IList<Angestellte>> GetAllAngestellteAsync()
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.AngestellteBaseUri}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Angestellte>>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }

        public async Task<Angestellte> GetAngestellteAsync(int angestellteId)
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.AngestellteBaseUri}/{angestellteId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Angestellte>();
            return result;
        }


        //Get Arbeitszeiterfassung
        public async Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenAsync()
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.ArbeitszeiterfassungBaseUri}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Arbeitszeiterfassung>>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }

        public async Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenByAngestellteIdAsync(int angestellteId)
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.ArbeitszeiterfassungBaseUri}/byangestellte/{angestellteId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Arbeitszeiterfassung>>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }

        public async Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenByDateSpanAsync(DateTime startDate, DateTime endDate)
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.ArbeitszeiterfassungBaseUri}/bydatespan/from_{startDate}_until{endDate}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Arbeitszeiterfassung>>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }

        public async Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenByAngestellteIdWithDateSpanAsync(int angestellteId, DateTime startDate, DateTime endDate)
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.ArbeitszeiterfassungBaseUri}/byangestellteid/{angestellteId}/withdatespan/from{startDate}_until{endDate}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Arbeitszeiterfassung>>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }

        public async Task<Arbeitszeiterfassung> GetArbeitszeiterfassungAsync(int arbeitszeiterfassungId)
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.ArbeitszeiterfassungBaseUri}/arbeitszeiterfassungid/{arbeitszeiterfassungId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Arbeitszeiterfassung>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }


        //Get Arbeitsandauer
        public Task<IList<Arbeitsandauer>> GetAllArbeitsandauerAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Arbeitsandauer>> GetAllArbeitsandauerByAngestellteIdAsync(int angestellteId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Arbeitsandauer>> GetAllArbeitsandauerByDateSpanAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Arbeitsandauer>> GetAllArbeitsandauerByAngestellteIdWithDateSpanAsync(int angestellteId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitsandauer> GetArbeitsandauerAsync(int arbeitszeiterfassungId)
        {
            throw new NotImplementedException();
        }


        /*
public async Task<Angestellte> AddAngestellteAsync(Angestellte entity)
{
var response = await PostAsJson($"{_settings.Uri}{_settings.AngestellteBaseUri}", JsonSerializer.Serialize(entity));
if (response == null)
{
throw new Exception("Unable to communicate with the service");
}

var location = response.Headers?.Location?.OriginalString;
//var updatedResponse = await _client.GetAsync(location);
//if (updatedResponse.StatusCode == HttpStatusCode.NotFound)
//{
//    return null;
//}

return await response.Content.ReadFromJsonAsync<Angestellte>();
}
*/

        // Add Angestellte
        public async Task<Angestellte> AddAngestellteAsync(Angestellte entity)
        {
            var response = await PostAsJson($"{_settings.Uri}{_settings.AngestellteBaseUri}",
                                 JsonSerializer.Serialize(entity));
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }

            var location = response.Headers?.Location?.OriginalString;
            //var updatedResponse = await _client.GetAsync(location);
            //if (updatedResponse.StatusCode == HttpStatusCode.NotFound)
            //{
            //    return null;
            //}

            return await response.Content.ReadFromJsonAsync<Angestellte>();
        }

        public Task<Stadt> AddStadtAsync(Stadt entity)
        {
            throw new NotImplementedException();
        }

        public Task<Hausanschrift> AddHausanschriftAsync(Hausanschrift entity)
        {
            throw new NotImplementedException();
        }

        public Task<Rolle> AddRolleAsync(Rolle entity)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitsort> AddArbeitsortAsync(Arbeitsort entity)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitsort> AddKennworttAsync(Kennwort entity)
        {
            throw new NotImplementedException();
        }

        public Task<Fundort> AddFundortAsync(Fundort entity)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitszeiterfassung> AddArbeitszeiterfassungAsync(Arbeitszeiterfassung entity)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitsandauer> AddArbeitsandauerAsync(Arbeitsandauer entity)
        {
            throw new NotImplementedException();
        }

        public Task<Angestellte> UpdateAngestellteAsync(int id, Angestellte entity)
        {
            throw new NotImplementedException();
        }

        public Task<Stadt> UpdateStadtAsync(int id, Stadt entity)
        {
            throw new NotImplementedException();
        }

        public Task<Hausanschrift> UpdateHausanschriftAsync(int id, Hausanschrift entity)
        {
            throw new NotImplementedException();
        }

        public Task<Rolle> UpdateRolleAsync(int id, Rolle entity)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitsort> UpdateArbeitsortAsync(int id, Arbeitsort entity)
        {
            throw new NotImplementedException();
        }

        public Task<Kennwort> UpdateKennworttAsync(int id, Kennwort entity)
        {
            throw new NotImplementedException();
        }

        public Task<Fundort> UpdateFundortAsync(int id, Fundort entity)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitszeiterfassung> UpdateArbeitszeiterfassungAsync(int id, Arbeitszeiterfassung entity)
        {
            throw new NotImplementedException();
        }

        public Task<Arbeitsandauer> UpdateArbeitsandauerAsync(int id, Arbeitsandauer entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAngestellteAsync(int id, Angestellte entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStadtAsync(int id, Stadt entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHausanschriftAsync(int id, Hausanschrift entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRolleAsync(int id, Rolle entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArbeitsortAsync(int id, Arbeitsort entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteKennworttAsync(int id, Kennwort entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFundortAsync(int id, Fundort entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArbeitszeiterfassungAsync(int id, Arbeitszeiterfassung entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArbeitsandauerAsync(int id, Arbeitsandauer entity)
        {
            throw new NotImplementedException();
        }



        // internal Methoden. Auxiliary methods for POST, PUT, Delete
        internal async Task<HttpResponseMessage> PostAsJson(string uri, string json)
        {
            return await _client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        internal async Task<HttpResponseMessage> PutAsJson(string uri, string json)
        {
            return await _client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        internal async Task<HttpResponseMessage> DeleteAsJson(string uri, string json)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri)
            };
            return await _client.SendAsync(request);
        }


        //
        //
        //
        /*
        public async Task<IList<Car>> GetCarsAsync()
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.CarBaseUri}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Car>>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }
        */

        /*
        public async Task<IList<Car>> GetCarsByMakeAsync(int id)
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.CarBaseUri}/bymake/{id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Car>>();
            //var result = await JsonSerializer.DeserializeAsync<IList<Category>>(await response.Content.ReadAsStreamAsync());
            return result;
        }
        */

        /*
        public async Task<Car> GetCarAsync(int id)
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.CarBaseUri}/{id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Car>();
            return result;
        }
        */

        /*
        public async Task<Car> AddCarAsync(Car entity)
        {
            var response = await PostAsJson($"{_settings.Uri}{_settings.CarBaseUri}",
                JsonSerializer.Serialize(entity));
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }

            var location = response.Headers?.Location?.OriginalString;
            //var updatedResponse = await _client.GetAsync(location);
            //if (updatedResponse.StatusCode == HttpStatusCode.NotFound)
            //{
            //    return null;
            //}

            return await response.Content.ReadFromJsonAsync<Car>();
        }
        */

        /*
        public async Task<Car> UpdateCarAsync(int id, Car entity)
        {
            var response = await PutAsJson($"{_settings.Uri}{_settings.CarBaseUri}/{id}",
                JsonSerializer.Serialize(entity));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Car>();
        }
        */

        /*
        public async Task DeleteCarAsync(int id, Car entity)
        {
            var response = await DeleteAsJson($"{_settings.Uri}{_settings.CarBaseUri}/{id}",
                JsonSerializer.Serialize(entity));
            response.EnsureSuccessStatusCode();
        }
        */

        /*
        public async Task<IList<Make>> GetMakesAsync()
        {
            var response = await _client.GetAsync($"{_settings.Uri}{_settings.MakeBaseUri}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IList<Make>>();
            return result;
        }
        */

        /*
        internal async Task<HttpResponseMessage> PostAsJson(string uri, string json)
        {
            return await _client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        /*
        internal async Task<HttpResponseMessage> PutAsJson(string uri, string json)
        {
            return await _client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }
        */

        /*
        internal async Task<HttpResponseMessage> DeleteAsJson(string uri, string json)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri)
            };
            return await _client.SendAsync(request);
        }
        */
    }
}
