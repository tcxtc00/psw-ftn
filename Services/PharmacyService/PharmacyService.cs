using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Net.Client;
using HelloGrpc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using psw_ftn.Dtos.PharmacyDtos;
using psw_ftn.Models;
using RestSharp;

namespace psw_ftn.Services.PharmacyService
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        private const string POST_PHARMACY_RECIPE_URL = "https://localhost:7176/Recipe";

        public PharmacyService(IConfiguration config, IMapper mapper)
        {
            this.mapper = mapper;
            this.config = config;
        }

        public ServiceResponse<RecipeDto> PostRecipe(RecipeDto recipe)
        {
            var response = new ServiceResponse<RecipeDto>();

            try
            {
               response = JsonConvert.DeserializeObject<ServiceResponse<RecipeDto>>
                (RequestToExternalServer(POST_PHARMACY_RECIPE_URL, Method.POST, recipe));
            }
            catch (System.Exception e)
            {
                response.Data = null;
                response.Success = false;
                response.Message = e.Message;
                return response;
            }
        
            return response;
        }

        public async Task<ServiceResponse<MedicineResponseDto>> GetMedicine(string name, int quantity)
        {
            var response = new ServiceResponse<MedicineResponseDto>();

            var httpClientHandler = new HttpClientHandler();
            //enables communication without certificate
            httpClientHandler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            var channel = GrpcChannel.ForAddress("http://localhost:5259",
            new GrpcChannelOptions { HttpClient = httpClient });
            var client =  new Medicine.MedicineClient(channel);
            
            try
            {
                MedicineRequest medicine = new MedicineRequest() { Name = name, Quantity = quantity};
                using (var call = client.GetMedicine(medicine))
                {
                    while (await call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var currentMedicine = call.ResponseStream.Current;
                        response.Data = new MedicineResponseDto();
                        response.Data.name = currentMedicine.Name;
                        response.Data.quantity = currentMedicine.Quantity;
                        response.Data.supplies = currentMedicine.Supplies;
                        response.Message = currentMedicine.ErrorMsg;
                    }
                }

                if(response.Data.errorMsg != "Success")
                {
                    response.Success = false;
                }
            }
            catch (System.Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
           
            return response;
        }

        private string RequestToExternalServer(string urlPath, Method typeOfRequest, object requestDto)
        {
            var clientUniRest = new RestClient(urlPath);
            clientUniRest.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        
            var requestUniRest = new RestRequest(typeOfRequest);
            requestUniRest.AddHeader("content-type", "application/json");

            if(typeOfRequest == Method.POST)
            {
                string json = System.Text.Json.JsonSerializer.Serialize(requestDto);
                requestUniRest.AddParameter("application/json", json, ParameterType.RequestBody);  
            }

            IRestResponse responseUniRest = clientUniRest.Execute(requestUniRest);
            return responseUniRest.Content;
        }
    }
}