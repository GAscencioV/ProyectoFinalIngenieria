namespace ProyectoFinalIngenieria.DTOs.Auth
{
    public class UserTokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
