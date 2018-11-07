namespace Tapas.Cms.FlatFile.Backend.Areas.Backend.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GrapesEditorLoadViewModel
    {
        [ JsonProperty( PropertyName = "gjs-html" ) ]
        public string Html { get; set; }

        [ JsonProperty( PropertyName = "gjs-css" ) ]
        public string Css { get; set; }
//
//        [ JsonProperty( PropertyName = "gjs-assets" ) ]
//        public List<string> Assets { get; set; } = new List<string>();
//
//        [ JsonProperty( PropertyName = "gjs-styles" ) ]
//        public List<string> Styles { get; set; } = new List<string>();
//
//        [ JsonProperty( PropertyName = "gjs-components" ) ]
//        public List<string> Components { get; set; } = new List<string>();
    }
}