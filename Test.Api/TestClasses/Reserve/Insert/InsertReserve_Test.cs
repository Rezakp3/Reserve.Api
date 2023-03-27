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

namespace Test.Api.TestClasses.Reserve.Insert
{
    public class InsertReserve_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public InsertReserve_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task InsertReserve_ValidObjectPass_Return200()
        {
            var reserve = new
            {
                ReserveDate = DateTime.Now.AddDays(10),
                ReserverId = Guid.Parse("ad52d995-ba99-402c-b465-d59776aa47a4"),
                LocationId = Guid.Parse("533a8fec-fbbb-4dea-8809-bc8b11174d1c"),
                Price = 60000
            };

            var response = await _client.PostAsync("/api/Reserve/Insert", CreateContent(reserve));
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task InsertReserve_NotValidObjectPass_Return500()
        {
            var reserve = new
            {
                ReserveDate = new DateTime().AddYears(2023).AddMonths(03).AddDays(25),
                ReserverId = Guid.Parse("0cb50386-bd92-48e6-8c99-5d983f777fbf"),
                LocationId = Guid.Parse("ab9a749d-331a-42e7-95bf-4cc62202e988"),
                Price = 60000
            };

            var response = await _client.PostAsync("/api/Reserve/Insert", CreateContent(reserve));
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
