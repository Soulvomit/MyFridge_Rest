﻿using Client_Library.Repository.Abstract;
using Client_Library.Repository.Interface;
using Client_Model.Model;
using System.Net.Http.Json;

namespace Client_Library.Repository
{
    public class RecipeClientRepository : ClientRepository<RecipeCto>, IRecipeClientRepository
    {
        public RecipeClientRepository(string baseAddress) : base(baseAddress) { }

        public IEnumerable<RecipeCto> MakeableLazies { get; private set; }

        public async Task<IEnumerable<RecipeCto>> GetMakeableAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"api/{ResolveName}/GetMakeable?userId={userId}");
            response.EnsureSuccessStatusCode();
            MakeableLazies = await response.Content.ReadFromJsonAsync<IEnumerable<RecipeCto>>();
            return MakeableLazies;
        }
    }
}
