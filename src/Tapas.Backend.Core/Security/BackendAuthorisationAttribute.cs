namespace Tapas.Backend.Core.Security
{
    using Microsoft.AspNetCore.Authorization;

    public class BackendAuthorisationAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "Backend";

        public BackendAuthorisationAttribute( string feature )
        {
            Feature = feature;
        }

        public string Feature
        {
            get => Policy.Substring( POLICY_PREFIX.Length );
            set => Policy = $"{POLICY_PREFIX}:{value}";
        }
    }
}