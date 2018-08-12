namespace Tapas.Backend.Core.Infrastructure.Menu
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;

    public class MenuItem
    {
        /// <summary>
        ///     If not empty, all these values are required (access granted if all matching Permission claims are possessed by
        ///     current user).
        ///     Menu item is decorated with either Microsoft.AspNetCore.Authorization.AuthorizeAttribute with Policy
        ///     or Infrastructure.Attributes.PermissionRequirementAttribute.
        /// </summary>
        public List<string> AllPolicies { get; private set; }= new List<string>();

        /// <summary>
        ///     If not empty, any of these roles is required (access granted if a single role is possessed by current user).
        ///     Me  nu item is decorated with Microsoft.AspNetCore.Authorization.AuthorizeAttribute with Roles.
        /// </summary>
        public List<string> AnyRoles { get; private set; } = new List<string>();

        public string Name { get; }
        public string DisplayText { get; }
        public uint Position { get; }
        public string Url { get; }
        public string CssClass { get; }

        public MenuItem( string name, string displayText, uint position, string url, string cssClass = "fa fa-circle-o", List<AuthorizeAttribute> microsoftAuthorizeAttributes = null )
        {
            Name = name;
            Position = position;
            Url = url;
            DisplayText = displayText;
            CssClass = cssClass;

            if ( microsoftAuthorizeAttributes != null )
            {
                // Get the authorized roles and policies
                foreach ( var attr in microsoftAuthorizeAttributes )
                {
                    if ( !string.IsNullOrWhiteSpace( attr.Roles ) )
                    {
                        AnyRoles.AddRange( attr.Roles.Split( ',' ) );
                    }

                    if ( !string.IsNullOrWhiteSpace( attr.Policy ) )
                    {
                        AllPolicies.Add( attr.Policy );
                    }
                }
            }
        }
    }
}