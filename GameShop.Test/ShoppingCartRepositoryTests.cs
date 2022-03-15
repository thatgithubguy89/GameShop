using GameShop.Data;
using GameShop.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace GameShop.Test
{
    public class ShoppingCartRepositoryTests
    {
        DbContextOptions<ApplicationDbContext> _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingCartRepositoryTests")
            .Options;
        ApplicationDbContext _context;
        IShoppingCartRepository _repository;

        [SetUp]
        public void Setup()
        {
            _context = new ApplicationDbContext(_contextOptions);
            _repository = new ShoppingCartRepository(_context);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task UpsertItem_Add()
        {
            await _repository.UpsertItem("test@gmail.com", "test", 59.99M);

            var result = await _repository.GetItems("test@gmail.com");

            result[0].Title.ShouldBe("test");
            result[0].Price.ShouldBe(59.99M);
        }

        [Test]
        public async Task UpsertItem_Update()
        {
            await _repository.UpsertItem("test@gmail.com", "test", 59.99M);
            await _repository.UpsertItem("test@gmail.com", "test", 59.99M);

            var result = await _repository.GetItems("test@gmail.com");

            result[0].Title.ShouldBe("test");
            result[0].Amount.ShouldBe(2);
        }

        [TestCase("", "test")]
        [TestCase("test@gmail.com", "")]
        public async Task UpsertItem_Exceptions(string username, string title)
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _repository.UpsertItem(username, title, 59.99M));
        }

        [Test]
        public async Task ClearCart()
        {
            await _repository.UpsertItem("test@gmail.com", "test", 59.99M);
            await _repository.UpsertItem("test@gmail.com", "test2", 59.99M);

            await _repository.ClearCart("test@gmail.com");
            var result = await _repository.GetItems("test@gmail.com");

            result.ShouldBeEmpty();
        }

        [Test]
        public async Task ClearCart_Exceptions()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _repository.ClearCart(""));
        }

        [Test]
        public async Task DeleteItem()
        {
            await _repository.UpsertItem("test@gmail.com", "test", 59.99M);
            await _repository.UpsertItem("test@gmail.com", "test", 59.99M);

            await _repository.DeleteItem("test@gmail.com", "test");
            var result = await _repository.GetItems("test@gmail.com");

            result[0].Amount.ShouldBe(1);
        }

        [TestCase("", "test")]
        [TestCase("test@gmail.com", "")]
        public async Task DeleteItem_Exceptions(string username, string title)
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _repository.DeleteItem(username, title));
        }
    }
}