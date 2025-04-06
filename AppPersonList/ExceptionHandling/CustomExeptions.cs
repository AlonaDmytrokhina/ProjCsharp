using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPersonList.ExceptionHandling
{
    [Serializable]
    internal class FutureBirthDateException : Exception
    {
        public FutureBirthDateException() { }
        public FutureBirthDateException(DateTime birthDate)
            : base(String.Format("Birth date cannot be in the future: {0}", birthDate))
        {

        }
    }

    [Serializable]
    internal class UnrealisticBirthDateException : Exception
    {

        public UnrealisticBirthDateException() { }

        public UnrealisticBirthDateException(DateTime birthDate)
            : base(string.Format("Birth date is too far in the past: {0}", birthDate))
        {

        }


    }

    [Serializable]
    internal class InvalidEmailException : Exception
    {
        public InvalidEmailException()
        {

        }

        public InvalidEmailException(string email)
            : base(string.Format("Invalid email format: {0}", email))
        {

        }
    }

    [Serializable]
    internal class UnrealisticNameException : Exception
    {
        public UnrealisticNameException()
        {

        }

        public UnrealisticNameException(string name)
            : base(string.Format("Name or lastname cannot contain symbols: {0}", name))
        {

        }
    }

    [Serializable]
    internal class IsNotAdultException : Exception
    {
        public IsNotAdultException()
            : base(string.Format("18+ access only!"))
        {

        }
    }
}
