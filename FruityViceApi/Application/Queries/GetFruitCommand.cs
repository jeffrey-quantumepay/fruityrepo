using System.Net.Http;
using System.Xml.Linq;
using FluentValidation;
using FruityViceApi.Application.Models;
using MediatR;
using Newtonsoft.Json;

namespace FruityViceApi.Application.Queries
{
    public class GetFruitCommand : IRequest<GetFruitCommandResponse>
    {
        public string name { get; set; }
    }

    public class GetFruitCommandResponse
    {
        public GetFruitCommandResponse() 
        {

        }

        public Fruit model { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class GetFruitCommandValidator : AbstractValidator<GetFruitCommand>
    {
        public GetFruitCommandValidator()
        {
            RuleFor(v => v.name)
               .NotNull();
            RuleFor(v => v.name)
              .NotEmpty();
        }
    }


    public class GetFruitCommandHandler : IRequestHandler<GetFruitCommand, GetFruitCommandResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public GetFruitCommandHandler(
             IHttpClientFactory httpClientFactory,
            IConfiguration configration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configration;
        }
        public async Task<GetFruitCommandResponse> Handle(GetFruitCommand request, CancellationToken cancellationToken)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var endpoint = _configuration["FruityConfig:BaseUrl"] + $"api/fruit/{request.name}";

                var response = await client.GetAsync(endpoint); // Use await for async call
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(); // Await the async call
                    var fruit = JsonConvert.DeserializeObject<Fruit>(content); // Deserialize JSON to List<Fruit>
                    return new GetFruitCommandResponse
                    {
                        model = fruit ?? new Fruit(),
                        Success = true
                    };
                }
                else
                {
                    // Handle error response
                    return new GetFruitCommandResponse
                    {
                        model = new Fruit(),
                        Success = false
                    };
                }
            }

        }

    }

}
