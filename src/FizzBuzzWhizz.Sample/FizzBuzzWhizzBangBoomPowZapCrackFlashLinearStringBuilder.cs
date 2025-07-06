namespace FizzBuzzWhizz.Sample;

public class FizzBuzzWhizzBangBoomPowZapCrackFlashLinearStringBuilder
{
    public string Identity(long n)
    {
        if (n == 0) return "0";
        System.Span<char> result = stackalloc char[37];
        var index = 0;
        var hasMatch = false;
        if (n % 3 == 0)
        {
            "Fizz".CopyTo(result[index..]);
            index += 4;
            hasMatch = true;
        }
        if (n % 5 == 0)
        {
            "Buzz".CopyTo(result[index..]);
            index += 4;
            hasMatch = true;
        }
        if (n % 7 == 0)
        {
            "Whizz".CopyTo(result[index..]);
            index += 5;
            hasMatch = true;
        }
        if (n % 11 == 0)
        {
            "Bang".CopyTo(result[index..]);
            index += 4;
            hasMatch = true;
        }
        if (n % 13 == 0)
        {
            "Boom".CopyTo(result[index..]);
            index += 4;
            hasMatch = true;
        }
        if (n % 17 == 0)
        {
            "Pow".CopyTo(result[index..]);
            index += 3;
            hasMatch = true;
        }
        if (n % 19 == 0)
        {
            "Zap".CopyTo(result[index..]);
            index += 3;
            hasMatch = true;
        }
        if (n % 23 == 0)
        {
            "Crack".CopyTo(result[index..]);
            index += 5;
            hasMatch = true;
        }
        if (n % 29 == 0)
        {
            "Flash".CopyTo(result[index..]);
            index += 5;
            hasMatch = true;
        }
        if (hasMatch) return result[..index].ToString();
        return n.ToString();
    }
}
