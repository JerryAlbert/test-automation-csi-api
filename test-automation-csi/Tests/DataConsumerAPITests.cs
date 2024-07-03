using System.Net;
using System.Text.Json;
using FluentAssertions;
using RestSharp;
using test_automation_csi_api.Model;

namespace test_automation_csi_api.Tests
{
    [TestFixture]
    public class DataConsumerAPITests
    {
        private RestClientOptions _restClientOptions;
        private JwtToken _jwtToken;

        public DataConsumerAPITests()
        {
            _restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://uat.data.nextbase.cloud"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };
        }

        public async Task<JwtToken> RetrieveToken()
        {
            // Rest Client
            var client = new RestClient(_restClientOptions);
            // Rest Request
            var request = new RestRequest(resource: "api/v1/DataConsumer/token");
            // Specify the content type
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new DataConsumerCredentials
                {
                    UserToken = "Nextbase Test_9bbaa57b-03d1-4a1e-89fd-b9c4382d0475",
                    ApiKey = "1e5cb346-d98b-4468-898b-fc1ea6b06b35"
                }
            );
            // Perform Post operation
            var response = await client.PostAsync<JwtToken>(request);

            // Validate the response
            response.Should().NotBeNull();
            response.Token.Should().NotBeNullOrEmpty();

            TestContext.WriteLine($"User has retrieved the token successfully: {response?.Token}");

            return response;
        }

        [Test, Order(1)]
        [NonParallelizable]
        public async Task GetToken()
        {
            _jwtToken = await RetrieveToken();
        }

        [Test, Order(2)]
        [NonParallelizable]
        public async Task GetExistingImageRequest()
        {
            // Ensure _jwtToken is not null
            if (_jwtToken == null || string.IsNullOrEmpty(_jwtToken.Token))
            {
                _jwtToken = await RetrieveToken();
            }

            // Rest Client
            var client = new RestClient(_restClientOptions);
            // Rest Request
            var request = new RestRequest("api/v1/DataConsumer/ImageRequest/9bbaa57b-03d1-4a1e-89fd-b9c4382d0475/333333");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_jwtToken.Token}");

            // Perform Get operation
            var response = await client.PostAsync(request);

            // Validate the response
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            TestContext.WriteLine("User has retrieved an existing image successfully");
        }
        
        [Test, Order(3)]
        [NonParallelizable]
        public async Task GetQueryExistingImageRequest()
        {
            // Ensure _jwtToken is not null
            if (_jwtToken == null || string.IsNullOrEmpty(_jwtToken.Token))
            {
                _jwtToken = await RetrieveToken();
            }

            // Rest Client
            var client = new RestClient(_restClientOptions);
            // Rest Request
            var request = new RestRequest("api/v1/DataConsumer/ImageRequests");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_jwtToken.Token}");
            request.AddJsonBody(new DataConsumerImageryRequest
                {
                    Id = "9bbaa57b-03d1-4a1e-89fd-b9c4382d0475",
                    DataConsumerId = "9bbaa57b-03d1-4a1e-89fd-b9c4382d0475",
                    DataConsumerReference = "333333",
                    RequestStatus = "Pending"
                }
            );

            // Perform Get operation
            var response = await client.PostAsync(request);

            // Validate the response
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            TestContext.WriteLine("User has retrieved an existing image with a query successfully");
        }
        
        [Test, Order(4)]
        [NonParallelizable]
        public async Task CreateNewClientImageRequest()
        {
            // Ensure _jwtToken is not null
            if (_jwtToken == null || string.IsNullOrEmpty(_jwtToken.Token))
            {
                _jwtToken = await RetrieveToken();
            }

            // Rest Client
            var client = new RestClient(_restClientOptions);
            // Rest Request
            var request = new RestRequest("api/v1/DataConsumer/CreateRequest", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_jwtToken.Token}");
            request.AddJsonBody(new DataConsumerImageryRequest
                {
                    DataConsumerId = "9bbaa57b-03d1-4a1e-89fd-b9c4382d0475",
                    DataConsumerReference = "333333",
                    SegmentGeometryJson = "{\u0022type\u0022: \u0022LineString\u0022,\u0022coordinates\u0022: [[-3.332058573831551, 51.60731265682864], [-3.331286097635262, 51.607685789988494], [-3.3307281981601644, 51.60808557211378]]}",
                    RequestType = "video",
                    RequestTypePurpose = "video",
                    EarliestAllowedTime = DateTime.Parse("2024-07-03T08:17:12.588Z"),
                    LatestAllowedTime = DateTime.Parse("2024-07-03T08:17:12.588Z"),
                    EarliestTimeOfDayLocal = TimeSpan.Parse("01:00:00"),
                    LatestTimeOfDayLocal = TimeSpan.Parse("17:00:00")
                }
            );

            // Perform Post operation
            var response = await client.ExecuteAsync(request);

            // Log the response content
            TestContext.WriteLine($"Response Content: {response.Content}");

            // Validate the response
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Deserialize the response
            var responseObject = JsonSerializer.Deserialize<DataConsumerImageryResponse>(response.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            responseObject.Should().NotBeNull();
            TestContext.WriteLine("Created an image request successfully");
        }

    }
}
