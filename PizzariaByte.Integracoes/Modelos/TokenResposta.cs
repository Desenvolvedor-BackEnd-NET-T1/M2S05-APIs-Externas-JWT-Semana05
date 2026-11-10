using Newtonsoft.Json;

// Aula 3. Resposta do POST oauth/token (padrão do OAuth 2.0)
public class TokenResposta
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    public string Scope { get; set; }
}
