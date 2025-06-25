using System.Text;

namespace Game.Gameplay.TypingGameplay
{
    public class WordGeneratorService
    {
        private readonly string _allowedCharacters;
        
        public WordGeneratorService(string allowedCharacters)
        {
            _allowedCharacters = allowedCharacters;
        }

        public string GenerateWord(int length)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
                sb.Append(_allowedCharacters[UnityEngine.Random.Range(0, _allowedCharacters.Length)]);

            return sb.ToString();
        }
    }
}