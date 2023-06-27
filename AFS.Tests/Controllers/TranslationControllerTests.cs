using AFS.Controllers;
using AFS.Dtos;
using AFS.Models;
using AFS.Repositories.Abstract;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFS.Tests.Controllers
{

    public class TranslationControllerTests
    {
        private TranslationController _translationController;
        private readonly IHttpClientFactory _httpClientFactory;
        private ITranslationService _translationService;

        public TranslationControllerTests()
        {
            //Dependencies
            _translationService = A.Fake<ITranslationService>();
            _httpClientFactory = A.Fake<IHttpClientFactory>();


            //SUT
            _translationController = new TranslationController(_translationService, _httpClientFactory);
        }

        [Fact]
        public void TranlationController_Index_ShouldReturnsViewResult()
        {
            //Arrange
            var translations = A.Fake<TranslationListVm>();
            string searchterm = "";
            bool paging = false;
            int currentpage = 0;
            A.CallTo(() => _translationService.List(searchterm, paging, currentpage)).Returns(translations);

            //Act

            var result = _translationController.Index();

            //Assert

            result.Should().BeOfType<ViewResult>();

        }



    }



}
