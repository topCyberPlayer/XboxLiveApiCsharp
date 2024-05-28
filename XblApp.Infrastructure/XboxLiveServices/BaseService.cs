﻿using System.Net.Http.Json;
using XblApp.Domain.Interfaces;

namespace XblApp.Infrastructure.XboxLiveServices
{
    public abstract class BaseService //: IBaseService
    {
        public async Task<T> DeserializeJson<T>(HttpResponseMessage httpResponse)// where T: ITokenJson
        {
            //// Пример обработки HTTP-ответа
            //string responseContent = await httpResponse.Content.ReadAsStringAsync();

            //// Пример десериализации JSON-ответа в экземпляр типа T
            //T result = JsonSerializer.Deserialize<T>(responseContent);

            //return result;

            T? result = default;

            if (httpResponse.IsSuccessStatusCode)
            {
                //string content = await httpResponse.Content.ReadAsStringAsync();
                //result = JsonSerializer.Deserialize<T>(content);

                result = await httpResponse.Content.ReadFromJsonAsync<T>();
            }

            return result;
        }
    }
}
