using CSharpFunctionalExtensions;
using Domain.Common;

namespace Domain.ValueObject
{
    public class NameValueObject : ValueObjectString
    {
        private static string tag = "Name";

        private static string name = tag;


        public NameValueObject() { }

        private NameValueObject(string value) : base(value)
        {
        }

        private static Result<NameValueObject, DomainModelExceptions> CreateNameValueObject (string value, int length, int maxLength = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DomainExceptions.General.ValueIsRequired(name, tag);

            string nameValue = value?.Trim();

            if (nameValue?.Length > length)
                return DomainExceptions.General.InvalidLength(length, name, tag);

            if (nameValue?.Length < maxLength)
                return DomainExceptions.General.InvalidLength(maxLength, name, tag);



            return new NameValueObject(value);
        }


        public static Result<NameValueObject, DomainModelExceptions> Create(string value, Metadata metadata)
        {
            tag = string.IsNullOrEmpty(metadata.Tag) ? tag : metadata.Tag;
            name = string.IsNullOrEmpty(metadata.LogicName) ? name : metadata.LogicName;
            return CreateNameValueObject(value, metadata.Length, metadata.MinLength);
        }

        public static Result<NameValueObject, DomainModelExceptions> CreateEqual(string value, Metadata metadata)
        {
            tag = string.IsNullOrEmpty(metadata.Tag) ? tag : metadata.Tag;
            name = string.IsNullOrEmpty(metadata.LogicName) ? name : metadata.LogicName;
            if (value?.Trim()?.Length != metadata.Length)
                return DomainExceptions.General.InvalidLength(metadata.Length, name, tag);
            return CreateNameValueObject(value, metadata.Length);
        }

        public static Result<NameValueObject, DomainModelExceptions> CreateEmpty(string value, Metadata metadata)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Create(value, metadata);
            }
            else
                return new NameValueObject(value);
        }
    }
}
