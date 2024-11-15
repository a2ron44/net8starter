using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Net8StarterAuthApi.Auth.Models;

namespace Net8StarterAuthApi.Auth.Services;

public class CognitoAuthService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly IAmazonCognitoIdentityProvider _cognitoClient;
    private readonly ILogger<CognitoAuthService> _logger;
    private readonly string _userPoolId;

    public CognitoAuthService(IAmazonCognitoIdentityProvider cognitoClient, IConfiguration configuration,
        ILogger<CognitoAuthService> logger)
    {
        _cognitoClient = cognitoClient;
        _logger = logger;
        _userPoolId = configuration["AWS:Cognito:UserPoolId"] ?? throw new InvalidOperationException();
        _clientId = configuration["AWS:Cognito:ClientId"] ?? throw new InvalidOperationException();
        _clientSecret = configuration["AWS:Cognito:ClientSecret"] ?? throw new InvalidOperationException();
    }

    public async Task<AuthTokenBundle> Login(string username, string password)
    {
        try
        {
            var secretHash = CognitoHelper.GenerateSecretHash(username, _clientId, _clientSecret);

            var request = new AdminInitiateAuthRequest
            {
                UserPoolId = _userPoolId,
                ClientId = _clientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
                
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", username },
                    { "PASSWORD", password },
                    { "SECRET_HASH", secretHash }
                },
                ClientMetadata = new Dictionary<string, string>
                {
                    {"scope", "email openid"}
                }
            };
            var authResponse = await _cognitoClient.AdminInitiateAuthAsync(request);

            return new AuthTokenBundle
            {
                TokenType = authResponse.AuthenticationResult.TokenType,
                AccessToken = authResponse.AuthenticationResult.AccessToken,
                RefreshToken = authResponse.AuthenticationResult.RefreshToken,
                ExpiresIn = authResponse.AuthenticationResult.ExpiresIn
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for user: {Email}, Message: {Message}", username, ex.Message);
            throw new InvalidCredentialException("Invalid Credentials");
        }
    }

    public async Task<AuthTokenBundle> RefreshToken(string username, string refreshToken)
    {
        var secretHash = CognitoHelper.GenerateSecretHash(username, _clientId, _clientSecret);

        try
        {
            // Create the request for initiating authentication using the refresh token
            var initiateAuthRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.REFRESH_TOKEN,
                ClientId = _clientId, // Your Cognito App Client ID
                AuthParameters = new Dictionary<string, string>
                {
                    { "REFRESH_TOKEN", refreshToken },
                    { "SECRET_HASH", secretHash }
                }
            };

            // Make the call to Cognito to get new tokens
            var authResponse = await _cognitoClient.InitiateAuthAsync(initiateAuthRequest);

            // Check if the response contains the authentication result with new tokens
            if (authResponse.AuthenticationResult != null)
                return new AuthTokenBundle
                {
                    TokenType = authResponse.AuthenticationResult.TokenType,
                    AccessToken = authResponse.AuthenticationResult.AccessToken,
                    RefreshToken = authResponse.AuthenticationResult.RefreshToken,
                    ExpiresIn = authResponse.AuthenticationResult.ExpiresIn
                };

            _logger.LogWarning("Invalid Refresh token: {RefreshToken}", refreshToken);
            throw new InvalidCredentialException("Invalid Refresh Token");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Refresh Token Failed: Message: {Message}", ex.Message);
            throw;
        }
    }
    
    // public async Task<bool> SignUpAsync(string email)
    // {
    //     var secretHash = CognitoHelper.GenerateSecretHash(email, _clientId, _clientSecret);
    //     
    //     var userAttrs = new AttributeType
    //     {
    //         Name = "email",
    //         Value = email,
    //     };
    //
    //     var userAttrsList = new List<AttributeType> { userAttrs };
    //
    //     var signUpRequest = new SignUpRequest
    //     {
    //         Username = email,
    //         UserAttributes = userAttrsList,
    //         ClientId = _clientId,
    //         Password = "Password2",
    //         SecretHash = secretHash,
    //     };
    //
    //     var response = await _cognitoClient.SignUpAsync(signUpRequest);
    //     return response.HttpStatusCode == HttpStatusCode.OK;
    // }
    
    public async Task<bool> AdminCreateUser(string email)
    {
        var secretHash = CognitoHelper.GenerateSecretHash(email, _clientId, _clientSecret);
        
        var userAttrs = new AttributeType
        {
            Name = "email",
            Value = email,
        };

        var userAttrsList = new List<AttributeType> { userAttrs };

        var createRequest = new AdminCreateUserRequest()
        {
            Username = email,
            UserAttributes = userAttrsList,
            UserPoolId = _userPoolId,
            
        };

        var response = await _cognitoClient.AdminCreateUserAsync(createRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> AdminAddUserToGroup(string email, string roleName)
    {
        var secretHash = CognitoHelper.GenerateSecretHash(email, _clientId, _clientSecret);

        var request = new AdminAddUserToGroupRequest
        {
            UserPoolId = _userPoolId,
            Username = email,
            GroupName = roleName,
        };
        
        var response = await _cognitoClient.AdminAddUserToGroupAsync(request);
        Console.WriteLine($"User '{email}' has been added to group '{roleName}'.");
        
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}

public static class CognitoHelper
{
    public static string GenerateSecretHash(string username, string clientId, string clientSecret)
    {
        var message = Encoding.UTF8.GetBytes(username + clientId);
        var key = Encoding.UTF8.GetBytes(clientSecret);

        using var hmac = new HMACSHA256(key);
        var hash = hmac.ComputeHash(message);

        return Convert.ToBase64String(hash);
    }
}