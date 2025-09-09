using BtOtp.Api.Models;

namespace BtOtp.Api.Services;

public class OtpService(SecureOtpGenerator generator, int expirySeconds = 120) : IOtpService
{
    private readonly Dictionary<string, OtpEntry> _otpStore = new();

    public OtpResponseDto IssueOtp(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId is required.");

        var code = generator.Generate();
        var expiry = DateTimeOffset.UtcNow.AddSeconds(expirySeconds);

        _otpStore[userId] = new OtpEntry { Code = code, Expiry = expiry };

        return new OtpResponseDto
        {
            Code = code,
            ExpiresIn = expirySeconds
        };
    }

    public bool ValidateOtp(string userId, string code)
    {
        if (!_otpStore.ContainsKey(userId))
            throw new KeyNotFoundException("OTP not found for this user.");

        var entry = _otpStore[userId];

        if (DateTimeOffset.UtcNow > entry.Expiry)
        {
            _otpStore.Remove(userId);
            throw new Exception("OTP has expired.");
        }

        if (entry.Code != code)
            throw new Exception("OTP is invalid.");

        _otpStore.Remove(userId);
        return true;
    }
}