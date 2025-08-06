using System.Security.Cryptography;
using System.Text;

namespace SchoolSaas.Infrastructure.Identity
{
    public class PasswordGenerator
    {
        private readonly static Random _rand = new Random();

        public static string Generate(int length = 15)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%&*";

            // Get cryptographically random sequence of bytes
            var bytes = RandomNumberGenerator.GetBytes(length);

            // Build up a string using random bytes and character classes
            StringBuilder res;
            do
            {
                res = new StringBuilder();
                foreach (byte b in bytes)
                {
                    // Randomly select a character class for each byte
                    switch (_rand.Next(4))
                    {
                        // In each case use mod to project byte b to the correct range
                        case 0:
                            res.Append(lower[b % lower.Count()]);
                            break;
                        case 1:
                            res.Append(upper[b % upper.Count()]);
                            break;
                        case 2:
                            res.Append(number[b % number.Count()]);
                            break;
                        case 3:
                            res.Append(special[b % special.Count()]);
                            break;
                    }
                }
            } while (!res.ToString().Any(char.IsLower) || !res.ToString().Any(char.IsUpper) || !res.ToString().Any(char.IsDigit) || res.ToString().All(char.IsLetterOrDigit));
            return res.ToString();
        }
    }
}
