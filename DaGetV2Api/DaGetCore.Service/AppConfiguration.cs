using System;

namespace DaGetCore.Service
{
    public class AppConfiguration
    {
        public Uri DaOAuthIntrospectUri { get; set; }
        public string Login { get; set; }
        public string ServerSecret { get; set; }
        public string Audience { get; set; }
        public bool RequireHttps { get; set; }
    }
}
