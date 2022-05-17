namespace Tests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using RestSharp;
    using RestSharp.Authenticators;

    public class ApiTestsGitHub
    {
        private RestClient client;

            [SetUp]
        public void Setup()
        {
            client               = new RestClient("https://api.github.com");
            client.Authenticator = new HttpBasicAuthenticator("Shtereva", "");
        }

        [Test]
        public async Task Test_GitHub_ApiRequest()
        {
            var request  = new RestRequest("/repos/dimosoftuni/postman/issues");
            var response = await client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.OK == response.StatusCode);


            var issues = JsonConvert.DeserializeObject<List<Issue>>(response.Content!);

            Assert.That(issues.Count > 1);

            foreach (var issue in issues)
            {
                Assert.That(issue.Id, Is.GreaterThan(0));
                Assert.That(issue.Number, Is.GreaterThan(0));
                Assert.That(issue.Title, Is.Not.Empty);
            }
        }

        [Test]
        public async Task Test_GitHub_GetIssueByNumber()
        {
            var request  = new RestRequest("/repos/dimosoftuni/postman/issues/1");
            var response = await client.ExecuteAsync(request);
            var issue    = JsonConvert.DeserializeObject<Issue>(response.Content!);

            Assert.That(HttpStatusCode.OK == response.StatusCode);
            Assert.That(issue.Id,     Is.GreaterThan(0));
            Assert.That(issue.Number, Is.GreaterThan(0));
        }

        [Test]
        public async Task Test_GitHub_GetIssueByInvalidNumber()
        {
            var request  = new RestRequest("/repos/dimosoftuni/postman/issues/0");
            var response = await client.ExecuteAsync(request);

            Assert.That(HttpStatusCode.NotFound == response.StatusCode);
        }

        [Test]
        public async Task Test_GitHub_CreateIssue()
        {
            var issue = await CreateIssue("Created via RestSharp", "Some body Here");

            Assert.That(issue!.Id, Is.GreaterThan(0));
            Assert.That(issue!.Number, Is.GreaterThan(0));
            Assert.That(issue.Title, Is.Not.Empty);
        }

        private async Task<Issue> CreateIssue(string title, string body)
        {
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues");
            request.AddBody(new { title, body });
            var response = await client.ExecuteAsync(request, Method.Post);
            var issue    = JsonConvert.DeserializeObject<Issue>(response.Content!);
            return issue;
        }
    }
}