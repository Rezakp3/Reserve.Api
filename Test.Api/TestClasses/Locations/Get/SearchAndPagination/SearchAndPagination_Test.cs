using Core.Dtos;
using FluentResults;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Locations.Get.SearchAndPagination
{
    public class SearchAndPagination_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public SearchAndPagination_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task SearchAndPagination_ValidObjectPass_Return200()
        {
            var searchDto = new LocationSearchDto
            {
                Title = "title",
                pageNumber = 1,
                itemCount = 10
            };

            var myContent = JsonConvert.SerializeObject(new {dto = searchDto});
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PostAsync("/api/Locatios/SearchAndPagination", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<List<Core.Entities.Locations>>>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
