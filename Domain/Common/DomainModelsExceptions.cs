using System.Collections.Generic;

namespace Domain.Common
{
    public sealed class DomainModelExceptions : ValueObject
    {

        public string Code { get; }
        public string Message { get; }
        public string PropName { get; }

        internal DomainModelExceptions(string code, string propName, string message)
        {
            Code = code;
            PropName = propName;
            Message = message;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }


    }

    public static class DomainExceptions
    {

        public static class General
        {

            public static DomainModelExceptions ValueIsInvalid(string propName, string code = null) =>
                new(string.IsNullOrEmpty(code) ? $"Value is invalid" : code, propName, propName);

            public static DomainModelExceptions ValueIsRequired(string propName) =>
                 ValueIsRequired(propName, propName);


            public static DomainModelExceptions ValueIsRequired(string propName, string labelName) =>
                new($"Value is required", propName, labelName);

            public static DomainModelExceptions InvalidLength(int length = 0, string propName = null, string labelName = "")
            {
                return new DomainModelExceptions($"Length is invalid", string.IsNullOrEmpty(labelName) ? propName : labelName, $"{propName}|{length} Max Char");
            }

        }
    }
}
