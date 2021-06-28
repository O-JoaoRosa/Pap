using System;
using System.Text.RegularExpressions;

namespace CRUD.ClassesEntidades
{
    class Validações
    {
        //this function Convert to Encord your Password 
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        /// <summary>
        /// verifica se o input tem o formato certo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidaColor(string input)
        {
            string namepattern = @"^#(([0-9a-fA-F]{2}){3}|([0-9a-fA-F]){3})$";

            // vai verificar se o input respeita as regex expression
            if (Regex.IsMatch(input, namepattern) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// verifica se o texto não tem caracteres especiais
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidaTexto(string input)
        {
            string namepattern = @"[a-zA-Z]+$";
            // vai verificar se o input respeita as regex expression
            if (Regex.IsMatch(input, namepattern) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// verifica se o input contem apenas numeros
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidaNumero(string input)
        {
            string numberpattern = @"^[0-9]";
            // vai verificar se o input respeita as regex expression
            if (Regex.IsMatch(input, numberpattern) == true)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// verififca se o input é um email valido
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidaEmail(string input)
        {
            string mailpattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            // vai verificar se o input respeita as regex expression
            if (Regex.IsMatch(input, mailpattern) == true)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        /// <summary>
        /// valida contra sql injection
        /// </summary>
        /// <returns></returns>
        static internal bool ValidaSQLInjection(String texto)
        {
            bool isSQLInjection = false;

            string[] sqlCheckList = {   "--",

                                        ";--",

                                        ";",

                                        "/*",

                                        "*/",

                                        "@@",

                                        "char",

                                        "nchar",

                                        "varchar",

                                        "nvarchar",

                                        "alter",

                                        "begin",

                                        "cast",

                                        "create",

                                        "cursor",

                                        "declare",

                                        "delete",

                                        "drop",

                                        "end",

                                        "exec",

                                        "execute",

                                        "fetch",

                                        "insert",

                                        "kill",

                                        "select",

                                        "sys",

                                        "sysobjects",

                                        "syscolumns",

                                        "table",

                                        "update"

                                       };

            string CheckString = texto.Replace("'", "''");

            for (int i = 0; i <= sqlCheckList.Length - 1; i++)
            {
                if ((CheckString.IndexOf(sqlCheckList[i],

                     StringComparison.OrdinalIgnoreCase) >= 0))

                { isSQLInjection = true; }
            }

            return isSQLInjection;
        }

        //FileStream stream = File.Open(FilePath, FileMode.Open);
        //FirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig("API Key"));
        //FirebaseAuthLink a = await auth.SignInWithEmailAndPasswordAsync("Email", "Pass");
        //CancellationTokenSource cancellation = new CancellationTokenSource();
        //FirebaseStorageTask task = new FirebaseStorage(
        //    "pap-t05filipe.appspot.com",
        //    new FirebaseStorageOptions
        //    {
        //        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
        //        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
        //                            })
        //    .Child("Users")
        //    .Child(textboxUsername.Text)
        //    .Child(textboxUsername.Text + "ImgP.png")
        //    .PutAsync(stream, cancellation.Token);

        //user.ProfielPicture = await task;
    }
}
