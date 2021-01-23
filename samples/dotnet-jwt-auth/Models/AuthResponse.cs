
using System;
using System.Collections.Generic;

namespace Dotnet_Backend
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Message { get; set; }
        public string RefreshToken { get; set; }
    }
}