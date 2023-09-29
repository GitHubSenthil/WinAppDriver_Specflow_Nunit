using System;
using System.Collections.Generic;
using System.Text;

namespace TeamsWindowsApp.Helper
{
    public static class PasswordEncryptor
    {
        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(this string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }

    //class encryptor
    //{
    //    public static void Main(string[] args)
    //    {
    //        String passwordTxt, encodeData;
    //        Console.WriteLine("Do you want to encrypt/decrypt: 1 - encrypt: 2- Decrypt ");
            
    //        switch (Console.ReadLine())
    //        {
    //            case "1":
    //                Console.WriteLine("Enter the string to Encrypt:");
    //                passwordTxt = Console.ReadLine();
    //                encodeData = ExtensionMethods.EncodeBase64(passwordTxt);
    //                Console.WriteLine("Encrypt string value: {0}", encodeData);
    //                break;
    //            case "2":
    //                Console.WriteLine("Enter the string to Decrypt:");
    //                passwordTxt = Console.ReadLine();
    //                encodeData = ExtensionMethods.DecodeBase64(passwordTxt);
    //                Console.WriteLine("Encrypt string value: {0}", encodeData);
    //                break;
    //            default:
    //                Console.WriteLine("Please enter 1 - Encrypt or 2 - Decrypt");
    //                break;
    //        }
    //    }

       
    //}
}
