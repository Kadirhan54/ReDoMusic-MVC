using ReDoMusic.Shared.Interfaces;

namespace ReDoMusic.MVC.Services
{
    public class TextService : ITextService
    {
        private readonly string _path;
        public TextService()
        {
            _path = "C:\\Users\\kado\\Desktop\\passwords.txt";
        }

        public void Save(string text)
        {         
            File.WriteAllText(_path, text); 
        }
    }
}
