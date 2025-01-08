using System.Text.RegularExpressions;

namespace PlexRichPresence.Tools.Helpers;

public static class StringHelper
{
    public static double CompareStrings(this string str1, string str2, bool substituteSimilar = true)
    {
        if (substituteSimilar)
        {
            str1 = SubstituteSimilarCharacters(str1);
            str2 = SubstituteSimilarCharacters(str2);
        }
        
        var pairs1 = WordLetterPairs(str1.ToUpper());
        var pairs2 = WordLetterPairs(str2.ToUpper());

        var intersection = 0;
        var union = pairs1.Count + pairs2.Count;

        foreach (var pair1 in pairs1)
        {
            for (var j = 0; j < pairs2.Count; j++)
            {
                if (pair1 != pairs2[j]) 
                    continue;
                
                intersection++;
                pairs2.RemoveAt(j);
                break;
            }
        }

        return 2.0 * intersection / union;
    }
    
    private static List<string> WordLetterPairs(string str)
    {
        var pairs = new List<string>();
        var words = Regex.Split(str, @"\s");
        
        foreach (var word in words)
        {
            if (string.IsNullOrEmpty(word)) 
                continue;
            
            var pairsInWord = LetterPairs(word);
            pairs.AddRange(pairsInWord);
        }
        return pairs;
    }
    
    private static string[] LetterPairs(string str)
    {
        var numPairs = str.Length - 1;
        var pairs = new string[numPairs];

        for (var i = 0; i < numPairs; i++)
            pairs[i] = str.Substring(i, 2);
        
        return pairs;
    }

    public static string SubstituteSimilarCharacters(string str)
    {
        return str.Replace('-', '‐');
    }
}