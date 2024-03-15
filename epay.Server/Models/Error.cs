using System.Runtime.Serialization;

namespace epay.Server.Models
{
    public class Error
    {
        public string ErrorDescription { get; set; }
        public string ReferenceName { get; set; }
        public string OriginalValue { get; set; }
        public string ExtraData { get; set; }
    }

    public enum Errordescription
    {
        NotNull = 1,
        NotFound = 2,
        BadRequest = 3,
        LogicError = 4,
        UnAuthorize = 5,
        ServerError
    }
}
