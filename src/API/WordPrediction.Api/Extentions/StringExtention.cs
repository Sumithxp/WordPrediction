namespace WordPrediction.Api.Extentions
{
    public static class StringExtention
    {
        public static string Base64Encode(this string plainText)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        }
    }
}
