using BtOtp.Api.Services;

namespace BtOtp.Tests
{
    public class OtpServiceTests
    {
        private readonly SecureOtpGenerator _generator = new();

        [Fact]
        public void IssueOtp_ShouldReturnOtpAndExpiry()
        {
            var service = new OtpService(_generator, expirySeconds: 120);
            const string userId = "user1";

            var response = service.IssueOtp(userId);

            Assert.NotNull(response);
            Assert.Matches(@"^\d{6}$", response.Code);
            Assert.Equal(120, response.ExpiresIn);
        }

        [Fact]
        public void IssueOtp_ShouldThrow_WhenUserIdIsEmpty()
        {
            var service = new OtpService(_generator);

            Assert.Throws<ArgumentException>(() => service.IssueOtp(""));
            Assert.Throws<ArgumentException>(() => service.IssueOtp(null));
            Assert.Throws<ArgumentException>(() => service.IssueOtp("   "));
        }

        [Fact]
        public void ValidateOtp_ShouldReturnTrue_WhenOtpIsCorrect()
        {
            var service = new OtpService(_generator);
            const string userId = "user1";
            var otp = service.IssueOtp(userId).Code;

            var result = service.ValidateOtp(userId, otp);

            Assert.True(result);
        }

        [Fact]
        public void ValidateOtp_ShouldThrow_WhenOtpIsInvalid()
        {
            var service = new OtpService(_generator);
            const string userId = "user1";
            service.IssueOtp(userId);

            var ex = Assert.Throws<Exception>(() => service.ValidateOtp(userId, "000000"));
            Assert.Equal("OTP is invalid.", ex.Message);
        }

        [Fact]
        public void ValidateOtp_ShouldThrow_WhenOtpExpired()
        {
            var service = new OtpService(_generator, expirySeconds: 1);
            const string userId = "user1";
            var otp = service.IssueOtp(userId).Code;

            Thread.Sleep(1500);

            var ex = Assert.Throws<Exception>(() => service.ValidateOtp(userId, otp));
            Assert.Equal("OTP has expired.", ex.Message);
        }

        [Fact]
        public void ValidateOtp_ShouldThrow_WhenOtpDoesNotExist()
        {
            var service = new OtpService(_generator);
            const string userId = "unknown";

            var ex = Assert.Throws<KeyNotFoundException>(() => service.ValidateOtp(userId, "123456"));
            Assert.Equal("OTP not found for this user.", ex.Message);
        }
        
        [Fact]
        public void ValidateOtp_ShouldThrow_WhenOtpWasAlreadyValidated()
        {
            var service = new OtpService(_generator);
            const string userId = "user1";
            var otp = service.IssueOtp(userId).Code;
            service.ValidateOtp(userId, otp);

            var ex = Assert.Throws<KeyNotFoundException>(() => service.ValidateOtp(userId, otp));
            Assert.Equal("OTP not found for this user.", ex.Message);
        }
    }
}
