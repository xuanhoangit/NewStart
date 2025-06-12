using System.Text.RegularExpressions;

namespace IVY.Domain.Libs;
public class StringValid
{   
    public static string CapitalizeEachWord(string input)
{
    if (string.IsNullOrEmpty(input))
        return input;

    var words = input.Split(' ');
    for (int i = 0; i < words.Length; i++)
    {
        if (!string.IsNullOrWhiteSpace(words[i]))
        {
            words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1).ToLower();
        }
    }
    return string.Join(" ", words);
}

    public static string ConvertToValidString(string input="")
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        // Loại bỏ khoảng trắng thừa và chỉ giữ 1 khoảng trắng giữa các từ
        string result = Regex.Replace(input.Trim(), @"\s+", " ");
        return CapitalizeEachWord(result);
    }
}