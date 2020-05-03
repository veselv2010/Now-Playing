﻿using Newtonsoft.Json;

namespace NowPlaying.Api.SpotifyResponses
{
    internal class TokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; private set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; private set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; private set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; private set; }
    }
}
