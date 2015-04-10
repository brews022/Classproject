using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class PhoneNumber
    {
        private string _number;
        private string _type;

        public void Call()
        {
            Console.WriteLine("Calling {0} ({1})", this._number, this._type);
        }

        public void SendSms(string message)
        {
            Console.WriteLine("Sending '");
        }
        public string GetNumberType()
        {
            return _type;
        }

        public void SetType(string value)
        {
            _type = value;
        }

        public string GetNumber()
        {
            return _number;
        }

        public void SetNumber(string value)
        {
            this._number = value;
        }
    }
}
