using System.Text;

namespace Cesar
{
    internal class Program
    {
        private static string Alphavit = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        static void Main(string[] args)
        {
            Console.Write("Введите сдвиг для шифра Цезаря: ");
            var deltaStr = Console.ReadLine();
            if (Int32.TryParse(deltaStr, out var delta))
            {
                var text = "тестовый Русский текст";
                var encryptedText = EncryptCesar(text, delta);
                Console.WriteLine(encryptedText);
            }
        }

        private static string EncryptCesar(string text, int delta)
        {
            var lowercaseText = text.ToLower();
            var module = Alphavit.Length;
            var sb = new StringBuilder();
            for (int i = 0; i < lowercaseText.Length; i++)
            {
                var oldIndex = Alphavit.IndexOf(lowercaseText[i]);
                if (oldIndex == -1) continue;

                var newIndex = (Alphavit.IndexOf(lowercaseText[i]) + delta) % module;
                sb.Append(Alphavit[newIndex]);
            }
            return sb.ToString();
        }
    }
}