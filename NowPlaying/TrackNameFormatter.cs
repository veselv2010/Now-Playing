﻿using System.Collections.Generic;
using System.Linq;

namespace NowPlaying
{
    public static class TrackNameFormatter
    {
        public static string ToLatin(string name)
        {
            return TrackNameFormatter.Ru2En.Aggregate(name, (current, value) => current.Replace(value.Key, value.Value));
        }

        #region Translit Dictionary

        private static readonly Dictionary<string, string> Ru2En = new Dictionary<string, string>
        {
            { "ё", "yo"},
            { "ж", "zh"},
            { "ч", "ch"},
            { "ш", "sh"},
            { "щ", "sch"},
            { "ю", "yu"},
            { "я", "ya"},
            { "Ё", "Yo"},
            { "Ж", "Zh"},
            { "Ч", "Ch"},
            { "Ш", "Sh"},
            { "Щ", "Sch"},
            { "Ю", "Yu"},
            { "Я", "Ya"},
            { "а", "a"},
            { "б", "b"},
            { "в", "v"},
            { "г", "g"},
            { "д", "d"},
            { "е", "e"},
            { "з", "z"},
            { "и", "i"},
            { "й", "j"},
            { "к", "k"},
            { "л", "l"},
            { "м", "m"},
            { "н", "n"},
            { "о", "o"},
            { "п", "p"},
            { "р", "r"},
            { "с", "s"},
            { "т", "t"},
            { "у", "u"},
            { "ф", "f"},
            { "х", "h"},
            { "ц", "c"},
            { "ъ", "j"},
            { "ы", "i"},
            { "ь", "j"},
            { "э", "e"},
            { "А", "A"},
            { "Б", "B"},
            { "В", "V"},
            { "Г", "G"},
            { "Д", "D"},
            { "Е", "E"},
            { "З", "Z"},
            { "И", "I"},
            { "Й", "J"},
            { "К", "K"},
            { "Л", "L"},
            { "М", "M"},
            { "Н", "N"},
            { "О", "O"},
            { "П", "P"},
            { "Р", "R"},
            { "С", "S"},
            { "Т", "T"},
            { "У", "U"},
            { "Ф", "F"},
            { "Х", "H"},
            { "Ц", "C"},
            { "Ъ", "J"},
            { "Ы", "I"},
            { "Ь", "J"},
            { "Э", "E"}
        };
        #endregion
    }
}