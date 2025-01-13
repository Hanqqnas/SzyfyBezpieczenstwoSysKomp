using Microsoft.AspNetCore.Mvc;

namespace SzyfyBezpieczenstwoSysKomp.Controllers
{
    public class VigenereController : Controller
    {
        private static readonly char[] PolishAlphabet = 
        {
            'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 
            'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 
            'v', 'w', 'x', 'y', 'z', 'ź', 'ż'
        };

        public IActionResult Index()
        {
            return View("~/Views/SzyfrVigenere/Vigenere.cshtml");
        }

        [HttpPost]
        public IActionResult Index(string message, string key, bool isEncrypting)
        {
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(key))
            {
                ViewBag.Result = isEncrypting
                    ? Encrypt(message, key)
                    : Decrypt(message, key);
            }
            else
            {
                ViewBag.Result = "Wprowadź poprawne dane!";
            }
            return View("~/Views/SzyfrVigenere/Vigenere.cshtml");
        }

        private string Encrypt(string text, string key)
        {
            string result = "";
            key = RepeatKey(key, text.Length).ToLower(); 

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = char.ToLower(text[i]);
                int charIndex = Array.IndexOf(PolishAlphabet, currentChar);

                if (charIndex != -1) 
                {
                    int keyIndex = Array.IndexOf(PolishAlphabet, key[i]);
                    int newIndex = (charIndex + keyIndex) % 35;

                    char newChar = PolishAlphabet[newIndex];
                    result += char.IsUpper(text[i]) ? char.ToUpper(newChar) : newChar;
                }
                else
                {
                    result += text[i]; 
                }
            }

            return result;
        }

        private string Decrypt(string text, string key)
        {
            string result = "";
            key = RepeatKey(key, text.Length).ToLower(); 

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = char.ToLower(text[i]);
                int charIndex = Array.IndexOf(PolishAlphabet, currentChar);

                if (charIndex != -1) 
                {
                    int keyIndex = Array.IndexOf(PolishAlphabet, key[i]);
                    int newIndex = (charIndex - keyIndex + 35) % 35;

                    char newChar = PolishAlphabet[newIndex];
                    result += char.IsUpper(text[i]) ? char.ToUpper(newChar) : newChar;
                }
                else
                {
                    result += text[i]; 
                }
            }

            return result;
        }

        private string RepeatKey(string key, int length)
        {
            var repeatedKey = new List<char>();
            int keyLength = key.Length;

            for (int i = 0; i < length; i++)
            {
                repeatedKey.Add(key[i % keyLength]);
            }

            return new string(repeatedKey.ToArray());
        }
    }
}
