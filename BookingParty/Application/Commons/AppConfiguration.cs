namespace Application.Commons
{
    public class JWTSection
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class AppConfiguration
    {
        public string DatabaseConnection { get; set; }
        public JWTSection JWTSection { get; set; }
        public string DatabaseConnections { get; set; }
    }
}
