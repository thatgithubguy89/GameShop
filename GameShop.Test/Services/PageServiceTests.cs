using AutoMapper;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;
using GameShop.Api.Profiles;
using GameShop.Api.Services;
using Shouldly;

namespace GameShop.Test.Services
{
    public class PageServiceTests
    {
        IPageService<Game, GameDto> _pageService;
        IMapper _mapper;

        private static readonly List<Game> _mockGames = new List<Game>
        {
            new Game { Id = 1, Title = "test" },
            new Game { Id = 2, Title = "test" },
            new Game { Id = 3, Title = "test" }
        };

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(c => c.AddProfile(typeof(MappingProfile)));
            _mapper = new Mapper(config);

            _pageService = new PageService<Game, GameDto>(_mapper);
        }

        [Test]
        public void Page()
        {
            var result = _pageService.Page(_mockGames, 1, 2);

            result.Payload.Count.ShouldBe(2);
            result.StartingIndex.ShouldBe(1);
            result.PageTotal.ShouldBe(2);
        }
    }
}
