using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace GameShop.Test.Repositories
{
    public class OrderRepositoryTests
    {
        IOrderRepository _orderRepository;
        Mock<IGameOrderRepository> _mockGameOrderRepository;
        GameShopContext _context;
        DbContextOptions<GameShopContext> _contextOptions = new DbContextOptionsBuilder<GameShopContext>()
            .UseInMemoryDatabase("OrderRepositoryTests")
            .Options;

        private static readonly Order _mockOrder = new Order { Id = 1 };

        [SetUp]
        public void Setup()
        {
            _context = new GameShopContext(_contextOptions);

            _mockGameOrderRepository = new Mock<IGameOrderRepository>();
            _mockGameOrderRepository.Setup(go => go.AddGameOrdersAsync(It.IsAny<List<int>>(), It.IsAny<Order>()));

            _orderRepository = new OrderRepository(_context, _mockGameOrderRepository.Object);

            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task AddOrderAsync()
        {
            var result = await _orderRepository.AddOrderAsync(_mockOrder, new List<int> { 1, 2 });

            result.Id.ShouldBe(1);
            result.ShouldBeOfType<Order>();
        }

        [Test]
        public async Task AddOrderAsync_GivenInvalidOrder_ShouldThrow_ArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _orderRepository.AddOrderAsync(null, new List<int> { 1, 2 }));
        }

        [Test]
        public async Task AddOrderAsync_GivenInvalidGameIds_ShouldThrow_ArgumentOutOfRangeException()
        {
            await Should.ThrowAsync<ArgumentOutOfRangeException>(async () => await _orderRepository.AddOrderAsync(new Order(), new List<int>()));
        }

        [Test]
        public async Task GetByIdAsync()
        {
            await _context.Orders.AddAsync(_mockOrder);
            await _context.SaveChangesAsync();

            var result = await _orderRepository.GetByIdAsync(_mockOrder.Id);

            result.Id.ShouldBe(1);
            result.ShouldBeOfType<Order>();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetByIdAsync_GivenInvalidId_ShouldThrow_ArgumentOutOfRangeException(int id)
        {
            await Should.ThrowAsync<ArgumentOutOfRangeException>(async () => await _orderRepository.GetByIdAsync(id));
        }

        [Test]
        public async Task GetByIdAsync_GivenIdForOrderThatDoesNotExist_ShouldThrow_NullReferenceException()
        {
            await Should.ThrowAsync<NullReferenceException>(async () => await _orderRepository.GetByIdAsync(1));
        }
    }
}
