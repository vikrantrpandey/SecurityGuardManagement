using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SecurityGuardManagement.Application.Auth.DTOs;
using SecurityGuardManagement.Application.Auth.Services;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace SecurityGuardManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request">Registration details</param>
        /// <response code="200">Returns the authentication token and user details</response>
        /// <response code="400">If the request is invalid or email already exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid registration request: {Errors}", 
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return ValidationProblem(ModelState);
            }

            try
            {
                _logger.LogInformation("Processing registration request for email: {Email}, role: {Role}", 
                    request.Email, request.Role);
                var response = await _authService.RegisterAsync(request);
                _logger.LogInformation("Successfully registered user with email: {Email}", request.Email);
                
                return Ok(new
                {
                    token = response.Token,
                    email = response.Email,
                    fullName = response.FullName,
                    role = response.Role
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Registration failed for email: {Email}", request.Email);
                return BadRequest(new ProblemDetails
                {
                    Title = "Registration Failed",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during registration for email: {Email}", request.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred during registration.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        /// <summary>
        /// Login with existing credentials
        /// </summary>
        /// <response code="200">Returns the authentication token and user details</response>
        /// <response code="400">If the credentials are invalid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(new
                {
                    token = response.Token,
                    email = response.Email,
                    fullName = response.FullName,
                    role = response.Role
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Login Failed",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login for email: {Email}", request.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred during login.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
