using FluentResults;
using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Headers;
using System.Text;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Reserve.Update
{
    public class UpdateReserve_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public UpdateReserve_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task Update_ValidObjectPass_Return200()
        {
            var reserve = new
            {
                Id = Guid.Parse("b28aa75a-58ae-4561-8a0d-20546424f8bc"),
                ReserveDate = DateTime.Now.AddDays(6),
                LocationId = Guid.Parse("1ce36dd3-6441-447d-a622-cf082322438d"),
                Price = 70000
            };

            var response = await _client.PutAsync("/api/Reserve/Update", CreateContent(reserve));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
