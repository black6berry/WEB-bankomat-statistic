namespace BankApi.ActionClass.HelperClass.ValidateData
{
    public class ConvertertClassMD5
    {
        private string _password { get; set; }
        public ConvertertClassMD5(string password)
        {
            _password = password;
        }

        /// <summary>
        /// Принимаем данные и кодируем в MD5
        /// </summary>
        /// <returns>Возвращаем зашифрованный пароль в формате md5</returns>
        public string CreateMD5()
        {
            try
            {
                using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(_password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
            catch
            {
                return "Данные невозможно обработать";
            }
        }
    }
}
