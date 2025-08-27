using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Domain.Common
{
    public class ValueObjectString : ValueObject
    {
        public string Value { get; private set; }
        private static string name = "Name";
        private static string tag = "tag";
        protected ValueObjectString()
        {

        }

        public ValueObjectString(string value)
        {
            Value = value;
        }

        protected  static Result<ValueObjectString, DomainModelExceptions> CreateValueObject(string value, int lenght, int maxLength = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DomainExceptions.General.ValueIsRequired(name, tag);

            string nameValue = value?.Trim();

            if (nameValue?.Length > lenght)
                return DomainExceptions.General.InvalidLength(lenght, name, tag);

            if (nameValue?.Length < maxLength)
                return DomainExceptions.General.InvalidLength(maxLength, name, tag);


            return new ValueObjectString(value);
        }

        public static Result<ValueObjectString, DomainModelExceptions> Create(string value, Metadata metadata)
        {
            tag = string.IsNullOrEmpty(metadata.Tag) ? tag : metadata.Tag;
            name = string.IsNullOrEmpty(metadata.LogicName) ? name : metadata.LogicName;
            return CreateValueObject(value, metadata.Length);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
