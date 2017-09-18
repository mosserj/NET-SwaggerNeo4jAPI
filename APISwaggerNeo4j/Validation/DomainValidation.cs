using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace APISwaggerNeo4j.Validation
{
    public class DomainValidation
    {
        private static readonly Regex validHostnameRegex = new Regex(@"^(([a-z]|[a-z][a-z0-9-]*[a-z0-9]).)*([a-z]|[a-z][a-z0-9-]*[a-z0-9])$", RegexOptions.IgnoreCase);

        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        public static bool IsValidDomainName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return validHostnameRegex.IsMatch(name.Trim());
            }

            return false;
        }

        public static void ValidateObject(object target, string message)
        {
            if(target == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}