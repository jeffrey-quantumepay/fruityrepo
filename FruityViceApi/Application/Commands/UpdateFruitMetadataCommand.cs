

using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using FluentValidation;
using FruityViceApi.Application.Models;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FruityViceApi.Application.Commands
{
    public class UpdateFruitMetadataCommand : IRequest<UpdateFruitMetadataResponse>
    {
        public Fruit model { get; set; }
    }

    public class UpdateFruitMetadataResponse
    {
        public UpdateFruitMetadataResponse()
        {

        }

        public Fruit model { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class UpdateFruitMetadataValidator : AbstractValidator<UpdateFruitMetadataCommand>
    {
        public UpdateFruitMetadataValidator()
        {
            RuleFor(v => v.model)
               .NotNull();
        }
    }


    public class UpdateFruitMetadataCommandHandler : IRequestHandler<UpdateFruitMetadataCommand, UpdateFruitMetadataResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UpdateFruitMetadataCommandHandler(
             IHttpClientFactory httpClientFactory,
            IConfiguration configration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configration;
        }
        public async Task<UpdateFruitMetadataResponse> Handle(UpdateFruitMetadataCommand request, CancellationToken cancellationToken)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var endpoint = _configuration["FruityConfig:BaseUrl"] + $"api/fruit";
                var response = await client.PutAsJsonAsync(endpoint, request.model); 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(); 
                    var fruit = JsonConvert.DeserializeObject<Fruit>(content); 
                    return new UpdateFruitMetadataResponse
                    {
                        model = fruit ?? new Fruit(),
                        Success = true
                    };
                }
                else
                {
                    // Handle error response
                    return new UpdateFruitMetadataResponse
                    {
                        model = new Fruit(),
                        Success = false,
                        Message = "Error"
                    };
                }
            }

        }

    }

}
