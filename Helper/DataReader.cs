using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TeamsWindowsApp.Driver;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TeamsWindowsApp.Helper
{
    public class DataReader
    {

        public static String getDetail(string label, string type)
        {
            string path = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location).Split("bin")[0];
            string sYamlUserFile = File.ReadAllText(path + "\\TestData\\TestAccounts\\" + Environment.GetEnvironmentVariable("ENV") +"_UserAccounts.yaml");
            sYamlUserFile = sYamlUserFile + File.ReadAllText(path + "\\TestData\\TestAccounts\\" + Environment.GetEnvironmentVariable("ENV") + "_MIB_UserAccounts.yaml");

            var objDeserialize = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithTypeMapping<IUser, User>()
                .Build();

            var accounts = objDeserialize.Deserialize<Accounts>(sYamlUserFile);

            int iUser = accounts.UserAccounts.Count;
            for (int i = 0; i <= iUser - 1; i++)
            {
                if (accounts.UserAccounts[i].label.Equals(label))
                {
                    switch (type)
                    {
                    case "userName":
                        return accounts.UserAccounts[i].userName;
                    case "email":
                        return accounts.UserAccounts[i].email;
                    case "password":
                        return accounts.UserAccounts[i].password;
                    case "fullName":
                            return accounts.UserAccounts[i].fullName;
                    case "userType":
                           return accounts.UserAccounts[i].userType;
                    }
                }
            }
            return null;
        }    

        public static String getUserName(string label)
        {
            return getDetail(label, "userName");
        }

        public static String getEmail(string label)
        {
            return getDetail(label, "email");
        }

        public static String getPassword(string label)
        {
            return getDetail(label, "password");
        }

        public static String getFullName(string label)
        {
            return getDetail(label, "fullName");
        }

        public static String getUserType(string label)
        {
            return getDetail(label, "userType");
        }


        public class Accounts
        {
            public IList<IUser> UserAccounts { get; private set; }
        }

        public class User : IUser
        {
            public string label { get; private set; }
            public string userName { get; private set; }

            public string email { get; private set; }

            public string password { get; private set; }

            public string fullName { get; private set; }
            
            public string userType { get; private set; }
        }

        public interface IUser
        {
            string label { get; }
            string userName { get; }

            string email { get; }

            string password { get; }

            string fullName { get; }

            string userType { get; }

        }

            
    }
    
}
