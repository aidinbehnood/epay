using System.Text.RegularExpressions;

namespace epay.Server.Utils
{
    public static class Extension
    {
        public static bool HasSpecial(this string code)
        => !string.IsNullOrWhiteSpace(code) && !Regex.IsMatch(code, "^[A-Za-z0-9]*$");

        public static bool IsValidAge(this int age)
    => age > 18 ;

        public static bool CheckDigitExpireDate(this string expireDate)
        {
            var digit = int.Parse(expireDate.Substring(2, 2));
            if (digit >= 1 && digit <= 12)
                return true;
            return false;
        }
        //public static bool CheckDuplicateId(this int id)
        //{
            
        //    //var digit = int.Parse(expireDate.Substring(2, 2));
        //    //if (digit >= 1 && digit <= 12)
        //    //    return true;
        //    return false;
        //}


        
        public static bool IsValidStan(this int stan)
      => !(stan < 1 || stan > 999999);
    }
}
