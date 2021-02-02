using System.Collections.Generic;

namespace MediaCreationLib.Planning.NET
{
    internal static class Constants
    {
        internal static Dictionary<string, string[]> EditionDowngradeDict = new Dictionary<string, string[]>()
        {
            //{ "PPIPro", new string[] { "Professional", "ProfessionalN" } },
            { "Professional", new string[] { "Core" } },
            { "Core", new string[] { "CoreCountrySpecific" } },
            { "CoreCountrySpecific", new string[] { "Starter" } },
            { "ProfessionalN", new string[] { "CoreN" } },
            { "CoreN", new string[] { "StarterN" } }
        };
    }
}
