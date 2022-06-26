using Domain.Common;
using Domain.ToDoListDomain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.ToDoListDomain.ValueObjects
{
    public class ColorValueObject : BaseValueObject<ColorValueObject>
    {
        public string Value { get; private set; }
        private ColorValueObject()
        {

        }
        public ColorValueObject(string color)
        {
            Value = color;
            Validate();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var temp = (ColorValueObject)obj;
            return Value.Equals(temp.Value);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool ObjectIsEqual(ColorValueObject otherObject)
        {
            return (Value == otherObject.Value);
        }
        private void Validate()
        {
            var arry = "0123456789ABCDEF";
            if (Value.Length != 7 || !Value.StartsWith("#") || Value.Substring(1).ToUpper().Any(p => !arry.Contains(p)))
                throw new UnsupportedColourException(Value);
        }

        public override int ObjectGetHashCode()
        {
            return base.GetHashCode();
        }
    }
}