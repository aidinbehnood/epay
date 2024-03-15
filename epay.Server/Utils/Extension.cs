using System.Text.RegularExpressions;

namespace epay.Server.Utils
{
    public static class Extension
    {
        public static bool HasSpecial(this string code)
        => !string.IsNullOrWhiteSpace(code) && !Regex.IsMatch(code, "^[A-Za-z0-9]*$");

        public static bool IsValidAge(this int age)
    => age > 18 ;

    
        
    }
}
