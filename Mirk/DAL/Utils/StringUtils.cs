using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Utils
{
    public class StringUtils
    {
        public const String EMPTY = "";
        public const String SPACE = " ";
        public const String UNDERSCORE = "_";
        public const String SLASH = "/";

        public static String replace(String fileOrSentence, String replaceString, String stringToReplace)
        {
            String newFileOrSentence = EMPTY;
            if (StringUtils.isNotNullAndNotEmpty(fileOrSentence)
                && StringUtils.isNotNullAndNotEmpty(replaceString) && StringUtils.isNotNullAndNotEmpty(stringToReplace))
            {
                newFileOrSentence = fileOrSentence.Replace(stringToReplace, replaceString);
            }

            return newFileOrSentence;
        }

        public static bool isNotNullAndNotEmpty(String stringToCheck)
        {
            bool isNotNulAndNotEmpty = false;

            if (stringToCheck != null && !EMPTY.Equals(stringToCheck))
            {
                isNotNulAndNotEmpty = true;
            }

            return isNotNulAndNotEmpty;
        }
    }
}