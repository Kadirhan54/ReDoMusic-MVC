using ReDoMusic.Shared.Interfaces;
using System.Text;

namespace ReDoMusic.MVC.Services
{
    public class PasswordGenerator
    {
        private readonly ITextService _textService;

        public int GeneratedPasswordsCount { get; set; } = 0;

        private readonly Random _random;

        private const string Numbers = "0123456789";
        private const string SpecialChars = "!@#$%^&*()";
        private const string LowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UpperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Full = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()";

        public PasswordGenerator(ITextService textService)
        {
            _textService = textService;

            _random = new Random();
        }

        public string Generate(int passwordLength, bool includeNumbers, bool includeLowerCase, bool includeUpperCase, bool includeSpecialChars)
        {
            var charsBuilder = new StringBuilder();

            if (includeNumbers)
                charsBuilder.Append(Numbers);

            if (includeLowerCase)
                charsBuilder.Append(LowerCaseChars);

            if (includeUpperCase)
                charsBuilder.Append(UpperCaseChars);

            if (includeSpecialChars)
                charsBuilder.Append(SpecialChars);

            var acceptedChars = charsBuilder.ToString();

            var passwordBuilder = new StringBuilder();


            for (int i = 0; i < passwordLength; i++)
            {
                var randomIndex = _random.Next(0, acceptedChars.Length);

                passwordBuilder.Append(acceptedChars[randomIndex]);
            }

            GeneratedPasswordsCount++;

            var password = passwordBuilder.ToString();

            _textService.Save(password);

            return password;
        }
    }
}
