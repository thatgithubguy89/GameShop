using GameShop.Data;
using GameShop.Models;
using GameShop.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace GameShop.Test
{
    public class ReviewRepositoryTests
    {
        DbContextOptions<ApplicationDbContext> _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ReviewRepositoryTests")
            .Options;
        ApplicationDbContext _context;
        IReviewRepository _reviewRepository;

        Review review = new Review
        {
            Id = 1,
            Content = "test",
            Username = "test@gmail.com",
            GameId = 1,
            CreateDate = DateTime.Now,
            LastEditDate = DateTime.Now,
        };

        Review review2 = new Review
        {
            Id = 2,
            Content = "test2",
            Username = "test2@gmail.com",
            GameId = 2,
            CreateDate = DateTime.Now,
            LastEditDate = DateTime.Now,
        };

        [SetUp]
        public void Setup()
        {
            _context = new ApplicationDbContext(_contextOptions);
            _reviewRepository = new ReviewRepository(_context);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task AddAsync()
        {
            await _reviewRepository.AddReviewAsync(review);

            var result = await _reviewRepository.GetReviewByIdAsync(1);
            result.Content.ShouldBe("test");
        }

        [Test]
        public async Task AddAsync_Exception()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _reviewRepository.AddReviewAsync(null));
        }

        [Test]
        public async Task DeleteAsync()
        {
            await _reviewRepository.AddReviewAsync(review);
            await _reviewRepository.DeleteReviewAsync(review);

            var result = await _reviewRepository.GetReviewByIdAsync(1);
            result.ShouldBeNull();
        }

        [Test]
        public async Task DeleteAsync_Exception()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _reviewRepository.DeleteReviewAsync(null));
        }

        [Test]
        public async Task GetAllAsync()
        {
            await _reviewRepository.AddReviewAsync(review);
            await _reviewRepository.AddReviewAsync(review2);

            var result = await _reviewRepository.GetAllReviewsAsync();
            result.Count.ShouldBe(2);
        }

        [Test]
        public async Task UpdateAsync()
        {
            await _reviewRepository.AddReviewAsync(review);
            review.Content = "test2";
            await _reviewRepository.UpdateReviewAsync(review);

            var result = await _reviewRepository.GetReviewByIdAsync(1);
            result.Content.ShouldBe("test2");
        }

        [Test]
        public async Task UpdateAsync_Exception()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _reviewRepository.UpdateReviewAsync(null));
        }
    }
}