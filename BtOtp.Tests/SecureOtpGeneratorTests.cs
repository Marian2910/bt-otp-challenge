using BtOtp.Api.Services;
using Xunit;

namespace BtOtp.Tests
{
    public class SecureOtpGeneratorTests
    {
        [Fact]
        public void Generate_ShouldReturn6DigitOtp_ByDefault()
        {
            var generator = new SecureOtpGenerator();
            var otp = generator.Generate();

            Assert.Equal(6, otp.Length);
            Assert.Matches(@"^\d{6}$", otp);
        }

        [Fact]
        public void Generate_ShouldReturnOtpOfCustomLength()
        {
            var generator = new SecureOtpGenerator();
            var otp = generator.Generate(8);

            Assert.Equal(8, otp.Length);
            Assert.Matches(@"^\d{8}$", otp);
        }

        [Fact]
        public void Generate_ShouldReturnDifferentOtps()
        {
            var generator = new SecureOtpGenerator();
            var otp1 = generator.Generate();
            var otp2 = generator.Generate();

            Assert.NotEqual(otp1, otp2);
        }
    }
}