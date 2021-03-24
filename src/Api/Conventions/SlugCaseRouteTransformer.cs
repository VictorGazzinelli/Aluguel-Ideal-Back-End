using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace AluguelIdeal.Api.Conventions
{
    public class SlugCaseRouteTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value) =>
            value != null ?
                Slugify(value) :
                null;

        private static string Slugify(object value) =>
            Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
