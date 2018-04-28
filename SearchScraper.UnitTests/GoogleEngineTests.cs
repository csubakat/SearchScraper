using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SearchScraper.Contracts;
using SearchScraper.Entitities;
using SearchScraper.Modules.SearchEngines;
using SearchScraper.UnitTests.Fakes;
using Xunit;

namespace SearchScraper.UnitTests
{
    public class GoogleEngineTests
    {
        private readonly Mock<IWebClientFactory> _webClientFactory;
        private readonly Mock<IFakeWebClient> _webClient;

        private readonly GoogleEngine _engine;

        public GoogleEngineTests()
        {
            _webClientFactory = new Mock<IWebClientFactory>();
            _webClient = new Mock<IFakeWebClient>();

            var settings = new SearchEngineProviderSetting
            {
                BaseUrl = "https://www.test.com/",
                NumberOfResultsParameter = "q=",
                SearchStringParameter = "s="
            };

            _engine = new GoogleEngine(settings, _webClientFactory.Object);

            _webClientFactory.Setup(x => x.Create()).Returns(_webClient.Object);
        }

        [Fact]
        public async Task GetOccurencesAsync_Should_Return_Correct_Occurence_Indexes()
        {
            // Arrange
            const string stringToFind = "but not as much as bacon";
            _webClient.Setup(x => x.DownloadStringTaskAsync(It.IsAny<Uri>())).ReturnsAsync(StringWithOccurences(stringToFind));

            var expectedResult = new List<int> {1, 3, 4};

            // Act
            var result = await _engine.GetOccurencesAsync("not important", stringToFind, 100);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetOccurencesAsync_Should_Return_Nothing_When_No_Occurence_Found()
        {
            // Arrange
            _webClient.Setup(x => x.DownloadStringTaskAsync(It.IsAny<Uri>())).ReturnsAsync(StringWithoutOccurences);

            // Act
            var result = await _engine.GetOccurencesAsync("not important", "hiya", 100);

            // Assert
            result.Should().BeEmpty();
        }

        private static string StringWithOccurences(string occuringString)
        {
            return
                $"<div class=\"g\">{occuringString}</div><div class=\"g\">i like meat pies</div><div class=\"g\">{occuringString}</div><div class=\"g\">{occuringString}</div>";
        }

        private static string StringWithoutOccurences()
        {
            return "<div class=\"m\">this doesn't look like anything to me</div>";
        }
    }
}
