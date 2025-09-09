using BtOtp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BtOtp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OtpController : ControllerBase
{
    private readonly IOtpService _otpService;

    public OtpController(IOtpService otpService)
    {
        _otpService = otpService;
    }

    /// <summary>
    /// Generate a one-time password for a given user.
    /// </summary>
    /// <param name="request">Contains the UserId</param>
    /// <returns>OtpResponseDto with the code and expiry in seconds</returns>
    [HttpPost("request")]
    public ActionResult<OtpResponseDto> RequestOtp([FromBody] OtpRequestDto? request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.UserId))
            return BadRequest(new { errorMessage = "UserId is required." });

        var otpResponse = _otpService.IssueOtp(request.UserId);
        return Ok(otpResponse);
    }

    /// <summary>
    /// Verify the OTP submitted by the user.
    /// </summary>
    /// <param name="request">Contains the UserId and Code</param>
    /// <returns>Success message if verified</returns>
    [HttpPost("verify")]
    public ActionResult<object> VerifyOtp([FromBody] OtpVerifyDto? request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Code))
            return BadRequest(new { errorMessage = "UserId and Code are required." });

        try
        {
            _otpService.ValidateOtp(request.UserId, request.Code);
            return Ok(new { success = true, message = "OTP verified successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { errorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { errorMessage = ex.Message });
        }
    }
}