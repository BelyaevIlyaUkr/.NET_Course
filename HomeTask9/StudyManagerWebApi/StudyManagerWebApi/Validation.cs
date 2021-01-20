using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyManagerWebApi
{
    public class Validation
    {
        public static bool IsValidFirstOrLastName(string name)
        {
            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}'\s\.\-]+$"))
                return false;

            return true;
        }

        public static bool IsValidPhoneNumber(string number)
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();

            PhoneNumber ukrNumberProto;

            try
            {
                ukrNumberProto = phoneUtil.Parse(number, "UA");
            }
            catch (Exception)
            {
                return false;
            }

            if (!phoneUtil.IsValidNumber(ukrNumberProto))
                return false;

            return true;
        }

        public static bool IsValidEmailAddress(string emailAddressString)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddressString);
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }

        public static bool ValidateDateTimeAndGetParsed(string dateString, out DateTime resultDateTime)
        {
            if (!DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out resultDateTime))
                return false;

            return true;
        }

        public static bool ValidateIntAndGetParsed(string numberString, out int resultNumber)
        {
            if (!int.TryParse(numberString, out resultNumber))
                return false;

            return true;
        }

        public static bool ValidateBoolAndGetParsed(string boolString, out bool resultBool)
        {
            resultBool = false;

            if (boolString.ToLower() == "yes")
                resultBool = true;
            else if (boolString.ToLower() != "no")
                return false;

            return true;
        }

        public static bool IsAnyFieldNotFilled(List<object> fields)
        {
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i] == null)
                    return true;
                if (fields[i].GetType().Equals(typeof(int)))
                {
                    if((int)fields[i] == 0)
                        return true;
                }
                else if ((string)fields[i] == string.Empty)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
