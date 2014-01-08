using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using BrandBookModel;


namespace BrandBook.Helper
{
    public static class CookieManager
    {
        //Adds the specified cookie to the cookie collection 
        public static void AddCookie(string keyName, string keyValue)
        {
            try
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(keyName, keyValue));
            }
            catch (Exception ex) { }
        }

        //Adds the specified cookie to the cookie collection 
        public static void AddCookieList(Dictionary<String, String> httpCookieDictionay)
        {
            //string keyName = "";
            //string keyValue = "";
            foreach (KeyValuePair<string, string> httpCookie in httpCookieDictionay)
            {
                //keyName = httpCookie.Key;
                //keyValue = httpCookie.Value;
                try
                {
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(httpCookie.Key, httpCookie.Value));
                }
                catch (Exception ex) { }
            }
        }
        
        //Updates the value of an existing cookie in a cookie collection
        public static void UpdateCookie(string keyName, string keyValue)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[keyName] != null)
                {
                    HttpContext.Current.Response.Cookies.Set(new HttpCookie(keyName, keyValue));
                }
            }
            catch (Exception ex) { }
        }

        //Gets an individual cookie value
        public static string GetCookie(string keyName)
        {
            string keyValue = "";
            try
            {
                if (HttpContext.Current.Request.Cookies[keyName] != null)
                {
                    keyValue = HttpContext.Current.Request.Cookies[keyName].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return keyValue;
        }

        //Gets all the cookies from a cookie collection
        public static Dictionary<String, String> GetCookieList()
        {
            Dictionary<String, String> httpCookieDictionay = new Dictionary<string, string>();
            string keyName = "";
            string keyValue = "";
            try
            {
                int cookieLimit = HttpContext.Current.Request.Cookies.Count;
                for (int i = 0; i < cookieLimit; i++)
                {
                    keyName = HttpContext.Current.Request.Cookies[i].Name;
                    if (HttpContext.Current.Request.Cookies[keyName] != null)
                    {
                        keyValue = HttpContext.Current.Request.Cookies[keyName].Value.ToString();
                        httpCookieDictionay.Add(keyName, keyValue);
                    }
                }
            }
            catch (Exception ex) { }

            return httpCookieDictionay;
        }

        //Set the timeout to instruct the client browser to remove the cookie
        public static void RemoveCookie(string keyName)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[keyName] != null)
                {
                    HttpCookie httpCookie = new HttpCookie(keyName);
                    httpCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
            }
            catch (Exception ex) { }
        }

        //Find all the cookies in a cookie collection 
        //And set the timeout instructing the client browser to remove all cookies from the collection
        public static void RemoveAllCookies()
        {
            HttpCookie httpCookie;
            string cookieName;
            try
            {
                int limit = HttpContext.Current.Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    cookieName = HttpContext.Current.Request.Cookies[i].Name;
                    httpCookie = new HttpCookie(cookieName);
                    httpCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
            }
            catch (Exception ex) { }
        }

        
        //encrypt and decrypt cookie data using the AesCryptoServiceProvider class
        public static void EncryptDecrypt()
        {
            try
            {
                string original = "Here is some data to encrypt!";
                // Create a new instance of the AesCryptoServiceProvider 
                // class.  This generates a new key and initialization  
                // vector (IV). 
                using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
                {
                    // Encrypt the string to an array of bytes. 
                    byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                    // Decrypt the bytes to a string. 
                    string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                    //Display the original data and the decrypted data.
                    Console.WriteLine("Original:   {0}", original);
                    Console.WriteLine("Round Trip: {0}", roundtrip);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }


        public static UserModel ReloadSessionFromCookie()
        {
            UserModel userModel = new UserModel();
            if (CookieManager.GetCookie("UserID").ToString().Trim().Length > 0)
            {
                userModel.UserID = CookieManager.GetCookie("UserID").ToString();
                userModel.UserDetailsID = Convert.ToInt32(CookieManager.GetCookie("UserDetaisID"));
                userModel.UserName = CookieManager.GetCookie("UserName").ToString();
                userModel.FirstName = CookieManager.GetCookie("FirstName").ToString();
            }

            return userModel;
        }
    }
}