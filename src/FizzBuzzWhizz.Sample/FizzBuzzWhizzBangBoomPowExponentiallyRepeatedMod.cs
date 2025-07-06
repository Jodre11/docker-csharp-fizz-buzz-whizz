namespace FizzBuzzWhizz.Sample;

public class FizzBuzzWhizzBangBoomPowExponentiallyRepeatedMod
{
    public string Identity(long n)
    {
        if (n == 0) return "0";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0 && n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzBuzzWhizzBangBoomPow";
        if (n % 5 == 0 && n % 7 == 0 && n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "BuzzWhizzBangBoomPow";
        if (n % 3 == 0 && n % 7 == 0 && n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzWhizzBangBoomPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzBuzzBangBoomPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzBuzzWhizzBoomPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0 && n % 11 == 0 && n % 17 == 0) return "FizzBuzzWhizzBangPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0 && n % 11 == 0 && n % 13 == 0) return "FizzBuzzWhizzBangBoom";
        if (n % 7 == 0 && n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "WhizzBangBoomPow";
        if (n % 5 == 0 && n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "BuzzBangBoomPow";
        if (n % 5 == 0 && n % 7 == 0 && n % 13 == 0 && n % 17 == 0) return "BuzzWhizzBoomPow";
        if (n % 5 == 0 && n % 7 == 0 && n % 11 == 0 && n % 17 == 0) return "BuzzWhizzBangPow";
        if (n % 5 == 0 && n % 7 == 0 && n % 11 == 0 && n % 13 == 0) return "BuzzWhizzBangBoom";
        if (n % 3 == 0 && n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzBangBoomPow";
        if (n % 3 == 0 && n % 7 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzWhizzBoomPow";
        if (n % 3 == 0 && n % 7 == 0 && n % 11 == 0 && n % 17 == 0) return "FizzWhizzBangPow";
        if (n % 3 == 0 && n % 7 == 0 && n % 11 == 0 && n % 13 == 0) return "FizzWhizzBangBoom";
        if (n % 3 == 0 && n % 5 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzBuzzBoomPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 11 == 0 && n % 17 == 0) return "FizzBuzzBangPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 11 == 0 && n % 13 == 0) return "FizzBuzzBangBoom";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0 && n % 17 == 0) return "FizzBuzzWhizzPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0 && n % 13 == 0) return "FizzBuzzWhizzBoom";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0 && n % 11 == 0) return "FizzBuzzWhizzBang";
        if (n % 11 == 0 && n % 13 == 0 && n % 17 == 0) return "BangBoomPow";
        if (n % 7 == 0 && n % 13 == 0 && n % 17 == 0) return "WhizzBoomPow";
        if (n % 7 == 0 && n % 11 == 0 && n % 17 == 0) return "WhizzBangPow";
        if (n % 7 == 0 && n % 11 == 0 && n % 13 == 0) return "WhizzBangBoom";
        if (n % 5 == 0 && n % 13 == 0 && n % 17 == 0) return "BuzzBoomPow";
        if (n % 5 == 0 && n % 11 == 0 && n % 17 == 0) return "BuzzBangPow";
        if (n % 5 == 0 && n % 11 == 0 && n % 13 == 0) return "BuzzBangBoom";
        if (n % 5 == 0 && n % 7 == 0 && n % 17 == 0) return "BuzzWhizzPow";
        if (n % 5 == 0 && n % 7 == 0 && n % 13 == 0) return "BuzzWhizzBoom";
        if (n % 5 == 0 && n % 7 == 0 && n % 11 == 0) return "BuzzWhizzBang";
        if (n % 3 == 0 && n % 13 == 0 && n % 17 == 0) return "FizzBoomPow";
        if (n % 3 == 0 && n % 11 == 0 && n % 17 == 0) return "FizzBangPow";
        if (n % 3 == 0 && n % 11 == 0 && n % 13 == 0) return "FizzBangBoom";
        if (n % 3 == 0 && n % 7 == 0 && n % 17 == 0) return "FizzWhizzPow";
        if (n % 3 == 0 && n % 7 == 0 && n % 13 == 0) return "FizzWhizzBoom";
        if (n % 3 == 0 && n % 7 == 0 && n % 11 == 0) return "FizzWhizzBang";
        if (n % 3 == 0 && n % 5 == 0 && n % 17 == 0) return "FizzBuzzPow";
        if (n % 3 == 0 && n % 5 == 0 && n % 13 == 0) return "FizzBuzzBoom";
        if (n % 3 == 0 && n % 5 == 0 && n % 11 == 0) return "FizzBuzzBang";
        if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0) return "FizzBuzzWhizz";
        if (n % 13 == 0 && n % 17 == 0) return "BoomPow";
        if (n % 11 == 0 && n % 17 == 0) return "BangPow";
        if (n % 11 == 0 && n % 13 == 0) return "BangBoom";
        if (n % 7 == 0 && n % 17 == 0) return "WhizzPow";
        if (n % 7 == 0 && n % 13 == 0) return "WhizzBoom";
        if (n % 7 == 0 && n % 11 == 0) return "WhizzBang";
        if (n % 5 == 0 && n % 17 == 0) return "BuzzPow";
        if (n % 5 == 0 && n % 13 == 0) return "BuzzBoom";
        if (n % 5 == 0 && n % 11 == 0) return "BuzzBang";
        if (n % 5 == 0 && n % 7 == 0) return "BuzzWhizz";
        if (n % 3 == 0 && n % 17 == 0) return "FizzPow";
        if (n % 3 == 0 && n % 13 == 0) return "FizzBoom";
        if (n % 3 == 0 && n % 11 == 0) return "FizzBang";
        if (n % 3 == 0 && n % 7 == 0) return "FizzWhizz";
        if (n % 3 == 0 && n % 5 == 0) return "FizzBuzz";
        if (n % 17 == 0) return "Pow";
        if (n % 13 == 0) return "Boom";
        if (n % 11 == 0) return "Bang";
        if (n % 7 == 0) return "Whizz";
        if (n % 5 == 0) return "Buzz";
        if (n % 3 == 0) return "Fizz";
        return n.ToString();
    }
}
