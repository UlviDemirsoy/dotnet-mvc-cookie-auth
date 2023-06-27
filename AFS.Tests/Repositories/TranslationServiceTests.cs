using AFS.Dtos;
using AFS.Models;
using AFS.Repositories.Concrete;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AFS.Tests.Repositories
{
    public class TranslationServiceTests
    {
        private async Task<DatabaseContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DatabaseContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Translations.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Translations.Add(
                      new Translation()
                      {
                          Text = "mock data",
                          TextTranslation = "mock data translated",
                          
                      });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void TranslationService_Add_ReturnsBool()
        {
            //Arrange
            var Translation = new Translation()
            {
                Text = "mock data 2",
                TextTranslation = "mock data translated 2",
            };
            var dbContext = await GetDbContext();
            var translationRepository = new TranslationService(dbContext);
           
            //Act
            var result = translationRepository.Add(Translation);
            dbContext.SaveChanges();
            //Assert
            result.Should().BeTrue();
        }


        [Fact]
        public async void TranslationService_SuccessfulGetById_ReturnsTranslation()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var translationRepository = new TranslationService(dbContext);
            var Translation = new Translation()
            {
                Text = "mock data 2",
                TextTranslation = "mock data translated 2",
            };

            //Act
            translationRepository.Add(Translation);
            dbContext.SaveChanges();
            var result = translationRepository.GetById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Translation>();
        }


        [Fact]
        public async void TranslationService_FailedGetById_ReturnsTranslation()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var translationRepository = new TranslationService(dbContext);
           

            //Act
            var result = translationRepository.GetById(id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void TranslationService_List_ReturnsList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var translationRepository = new TranslationService(dbContext);

            //Act
            var result = translationRepository.List();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TranslationListVm>();
        }


       
    }
}
