using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SessionHelper
{
    //Session variable constants
    public const string COUNTERVAR = "Counter";
    public const string TEXTVAR = "Text";

    public static T Read<T>(string variable)
    {
        object value = HttpContext.Current.Session[variable];
        if (value == null)
            return default(T);
        else
            return ((T)value);
    }

    public static void Write(string variable, object value)
    {
        HttpContext.Current.Session[variable] = value;
    }

    public static int Counter
    {
        get
        {
            return Read<int>(COUNTERVAR);
        }
        set
        {
            Write(COUNTERVAR, value);
        }
    }

    public static string Text
    {
        get
        {
            return Read<string>(TEXTVAR);
        }
        set
        {
            Write(TEXTVAR, value);
        }
    }

}