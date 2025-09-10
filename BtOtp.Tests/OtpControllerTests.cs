using BtOtp.Api.Controllers;
using BtOtp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BtOtp.Tests
{
    public class OtpControllerTests
    {
        private readonly Mock<IOtpService> _serviceMock;
        private readonly OtpController _controller;

        public OtpControllerTests()
        {
            _serviceMock = new Mock<IOtpService>();
            _controller = new OtpController(_serviceMock.Object);
        }
        
        [Fact]
        public void RequestOtp_ShouldReturnOk_WhenUserIdIsValid()
        {
            const string userId = "user1";
            var responseDto = new OtpResponseDto { Code = "123456", ExpiresIn = 120 };

            _serviceMock.Setup(s => s.IssueOtp(userId)).Returns(responseDto);

            var actionResult = _controller.RequestOtp(new OtpRequestDto { UserId = userId });
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultValue = Assert.IsType<OtpResponseDto>(okResult.Value);

            Assert.Equal("123456", resultValue.Code);
            Assert.Equal(120, resultValue.ExpiresIn);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void RequestOtp_ShouldReturnBadRequest_WhenUserIdIsInvalid(string invalidUserId)
        {
            var actionResult = _controller.RequestOtp(new OtpRequestDto { UserId = invalidUserId });
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            var errorMessageProp = badRequest.Value?.GetType().GetProperty("errorMessage");
            var errorMessage = errorMessageProp?.GetValue(badRequest.Value)?.ToString();

            Assert.Equal("UserId is required.", errorMessage);
        }

        [Fact]
        public void RequestOtp_ShouldReturnBadRequest_WhenRequestIsNull()
        {
            var actionResult = _controller.RequestOtp(null);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            var errorMessageProp = badRequest.Value?.GetType().GetProperty("errorMessage");
            var errorMessage = errorMessageProp?.GetValue(badRequest.Value)?.ToString();

            Assert.Equal("UserId is required.", errorMessage);
        }

        [Fact]
        public void VerifyOtp_ShouldReturnOk_WhenOtpIsValid()
        {
            const string userId = "user1";
            const string code = "123456";

            _serviceMock.Setup(s => s.ValidateOtp(userId, code)).Returns(true);

            var actionResult = _controller.VerifyOtp(new OtpVerifyDto { UserId = userId, Code = code });
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var successProp = okResult.Value?.GetType().GetProperty("success");
            var messageProp = okResult.Value?.GetType().GetProperty("message");

            var success = (bool)(successProp?.GetValue(okResult.Value) ?? throw new InvalidOperationException());
            var message = messageProp?.GetValue(okResult.Value)?.ToString();

            Assert.True(success);
            Assert.Equal("OTP verified successfully.", message);
        }

        [Theory]
        [InlineData(null, "123456")]
        [InlineData("user1", null)]
        [InlineData("", "")]
        public void VerifyOtp_ShouldReturnBadRequest_WhenUserIdOrCodeInvalid(string userId, string code)
        {
            var actionResult = _controller.VerifyOtp(new OtpVerifyDto { UserId = userId, Code = code });
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            var errorMessageProp = badRequest.Value?.GetType().GetProperty("errorMessage");
            var errorMessage = errorMessageProp?.GetValue(badRequest.Value)?.ToString();

            Assert.Equal("UserId and Code are required.", errorMessage);
        }

        [Fact]
        public void VerifyOtp_ShouldReturnNotFound_WhenOtpDoesNotExist()
        {
            const string userId = "user1";
            const string code = "123456";

            _serviceMock
                .Setup(s => s.ValidateOtp(userId, code))
                .Throws(new KeyNotFoundException("OTP not found for this user."));

            var actionResult = _controller.VerifyOtp(new OtpVerifyDto { UserId = userId, Code = code });
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);

            var errorMessageProp = notFoundResult.Value?.GetType().GetProperty("errorMessage");
            var errorMessage = errorMessageProp?.GetValue(notFoundResult.Value)?.ToString();

            Assert.Equal("OTP not found for this user.", errorMessage);
        }

        [Fact]
        public void VerifyOtp_ShouldReturnBadRequest_WhenOtpInvalidOrExpired()
        {
            const string userId = "user1";
            const string code = "123456";

            _serviceMock
                .Setup(s => s.ValidateOtp(userId, code))
                .Throws(new Exception("OTP has expired."));

            var actionResult = _controller.VerifyOtp(new OtpVerifyDto { UserId = userId, Code = code });
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            var errorMessageProp = badRequestResult.Value?.GetType().GetProperty("errorMessage");
            var errorMessage = errorMessageProp?.GetValue(badRequestResult.Value)?.ToString();

            Assert.Equal("OTP has expired.", errorMessage);
        }
    }
}
