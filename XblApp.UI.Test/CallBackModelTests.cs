using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using XblApp.Application.UseCases;
using XblApp.Pages.Auth;

namespace XblApp.Tests.Pages.Auth
{
    public class CallBackModelTests
    {
        private readonly Mock<AuthenticationUseCase> _mockAuthServ;
        private readonly CallBackModel _callbackModel;

        public CallBackModelTests()
        {
            // Мокирование AuthenticationUseCase
            _mockAuthServ = new Mock<AuthenticationUseCase>();

            // Создание экземпляра CallBackModel с замокированным сервисом
            _callbackModel = new CallBackModel(_mockAuthServ.Object);

            // Мокирование HttpContext и HttpRequest
            var mockHttpContext = new DefaultHttpContext();

            _callbackModel.PageContext = new Microsoft.AspNetCore.Mvc.RazorPages.PageContext
            {
                HttpContext = mockHttpContext
            };
        }

        [Fact]
        public async Task OnGet_CodeIsNull_ReturnsNotFound()
        {
            // Act
            var result = await _callbackModel.OnGet(null);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("er", notFoundResult.Value);
        }

        [Fact]
        public async Task OnGet_ValidCode_CallsAuthServiceAndRedirectsToIndex()
        {
            // Arrange
            string testCode = "M.C556_SN1.2.U.ddb5d44e-28e7-102c-64e7-14ddde9e1ebe";
            _mockAuthServ.Setup(x => x.RequestTokens(It.IsAny<string>()))
                         .Returns(Task.CompletedTask);

            // Act
            var result = await _callbackModel.OnGet(testCode);

            // Assert
            _mockAuthServ.Verify(x => x.RequestTokens(testCode), Times.Once);
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("Index", redirectResult.PageName);
        }

        [Fact]
        public async Task OnGet_ExceptionThrown_RedirectsToError()
        {
            // Arrange
            string testCode = "M.C556_SN1.2.U.ddb5d44e-28e7-102c-64e7-14ddde9e1ebe";
            _mockAuthServ.Setup(x => x.RequestTokens(It.IsAny<string>()))
                         .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _callbackModel.OnGet(testCode);

            // Assert
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("Error", redirectResult.PageName);
        }
    }
}
