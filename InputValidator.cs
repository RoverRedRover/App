namespace App
{
    static class InputValidator
    {
        /// <summary>
        /// Determine whether a string minimally qualifies to be a 
        /// credit card number, i.e., is not null, empty or whitespace;
        /// is sixteen characters in length; and all of its characters
        /// are numeric.
        /// </summary>
        /// <param name="ccn">the string that should be a credit card number</param>
        /// <returns>true if all of the above criteria are satisfied</returns>
        public static (bool, string) ValidateCardNumber(string ccn)
        {
            ccn = RemoveDashesAndSpaces(ccn);

            if (IsNullEmptyOrWhite(ccn))    {return (false, ccn);}
            if (IsNot16Chars(ccn))          {return (false, ccn);}
            if (NotAllCharsAreNumeric(ccn)) {return (false, ccn);}
            
            // else
            return                          (true, ccn);
        }

        static string RemoveDashesAndSpaces(string ccn)
        {
            string[] charsToRemove = {" ", "-"};

            try
            {
                foreach (string c in charsToRemove)
                {
                    ccn = ccn.Replace(c, string.Empty);
                }
            }

            catch (NullReferenceException)
            {
                ccn = "";
            }

            return ccn;
        }

        static bool IsNullEmptyOrWhite(string ccn) =>
            (String.IsNullOrEmpty(ccn) || String.IsNullOrWhiteSpace(ccn));

        static bool IsNot16Chars(string ccn) => ccn.Length != 16;

        static bool NotAllCharsAreNumeric(string ccn)
        {
            foreach (char c in ccn)
            {
                if (!Char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }

    }
}