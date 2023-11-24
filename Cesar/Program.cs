using System.Text;

namespace Cesar
{
    internal class Program
    {
        private static string Alphavit = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        static void Main(string[] args)
        {
            bool actionDone = false;

            do {
                Console.Write("Зашифровать(1)/взломать(2) шифр Цезаря [1/2] (Enter-выйти): ");
                var actionCode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(actionCode))
                    return;
                string? text;

                switch (actionCode)
                {
                    case "1":
                    {
                        Console.Write("Введите сдвиг для шифра Цезаря: ");
                        var deltaStr = Console.ReadLine();
                        Console.Write("Введите текст для зашифровки: ");
                        text = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            text = "тестовый Русский текст";
                            Console.WriteLine($"Будет зашифрован тестовый текст: {text}");
                        }

                        if (Int32.TryParse(deltaStr, out var delta))
                        {
                            var encryptedText = EncryptCesar(text, delta);
                            Console.WriteLine(encryptedText);
                        }
                        actionDone = true;
                        break;
                    }
                    case "2":
                        Console.Write("Введите текст для взлома: ");
                        text = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            text = "щлшщхиврчъшшспрщлсшщ";
                            Console.WriteLine($"Будет взломан тестовый текст: {text}");
                        }

                        var openTexts = BruteForceCesar(text);
                        foreach (var tuple in openTexts)
                        {
                            Console.WriteLine($"Сдвиг: {tuple.delta} Текст: {tuple.text}");
                        }
                        actionDone = true;
                        break;
                    default:
                        Console.WriteLine("Введен неизвестный код операции.");
                        break;
                }
            } while (!actionDone);
        }

        private static string EncryptCesar(string text, int delta)
        {
            var lowercaseText = text.ToLower();
            var module = Alphavit.Length;
            var sb = new StringBuilder();
            for (var i = 0; i < lowercaseText.Length; i++)
            {
                var oldIndex = Alphavit.IndexOf(lowercaseText[i]);
                if (oldIndex == -1) continue;

                var newIndex = (Alphavit.IndexOf(lowercaseText[i]) + delta + module) % module;
                sb.Append(Alphavit[newIndex]);
            }
            return sb.ToString();
        }

        private static List<(int delta, string text)> BruteForceCesar(string cryptoText)
        {
            List<(int delta, string text)> openTexts = new();

            for (var i = 0; i < Alphavit.Length; i++)
            {
                openTexts.Add((i, EncryptCesar(cryptoText, i*(-1))));
            }

            return openTexts;
        }
    }
}