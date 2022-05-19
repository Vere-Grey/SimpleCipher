using System;
using System.Collections.Generic;
using System.Linq;

public class SimpleCipher
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    private const int RandomKeyLength = 100;
    private readonly Random random = new();
    private int keyIndex;

    private enum Action
    {
        Encode,
        Decode
    }

    public SimpleCipher() =>
        Key = new string(Enumerable.Repeat(Alphabet, RandomKeyLength)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

    public SimpleCipher(string key) => Key = key;

    public string Key
    {
        get;
    }
    
    public string Encode(string plainText)
    {
        keyIndex = 0;
        return new string(Stream(plainText.ToLower().ToCharArray(), Action.Encode).ToArray());
    } 

    public string Decode(string cipherText)
    {
        keyIndex = 0;
        return new string(Stream(cipherText.ToLower().ToCharArray(), Action.Decode).ToArray());
    }

    private IEnumerable<char> Stream(IEnumerable<char> cipherChars, Action action)
    {
        foreach (var letter in cipherChars)
        {
            var originalCode = Alphabet.IndexOf(letter);
            var keyShift =
                action == Action.Encode
                ? Alphabet.IndexOf(Key[keyIndex])
                : Alphabet.Length - Alphabet.IndexOf(Key[keyIndex]);
            var newCode = (originalCode + keyShift) % Alphabet.Length;
            keyIndex = (keyIndex + 1) % Key.Length;
            yield return Alphabet[newCode];
        }
    }
}