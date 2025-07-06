namespace FizzBuzzWhizz.Sample;

[SimpleJob(RuntimeMoniker.Net90)]
[RPlotExporter]
[MemoryDiagnoser]
public class IdentityPerformanceComparison
{
    private static readonly FizzBuzzWhizzBangBoomPowExponentiallyRepeatedMod FizzBuzzWhizzBangBoomPowExponentiallyRepeatedMod = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapExponentiallyRepeatedMod FizzBuzzWhizzBangBoomPowZapExponentiallyRepeatedMod = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapCrackExponentiallyRepeatedMod FizzBuzzWhizzBangBoomPowZapCrackExponentiallyRepeatedMod = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapCrackFlashExponentiallyRepeatedMod FizzBuzzWhizzBangBoomPowZapCrackFlashExponentiallyRepeatedMod = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapCrackFlashThunderExponentiallyRepeatedMod FizzBuzzWhizzBangBoomPowZapCrackFlashThunderExponentiallyRepeatedMod = new();
    private static readonly FizzBuzzWhizzBangBoomPowLinearStringBuilder FizzBuzzWhizzBangBoomPowLinearStringBuilder = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapLinearStringBuilder FizzBuzzWhizzBangBoomPowZapLinearStringBuilder = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapCrackLinearStringBuilder FizzBuzzWhizzBangBoomPowZapCrackLinearStringBuilder = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapCrackFlashLinearStringBuilder FizzBuzzWhizzBangBoomPowZapCrackFlashLinearStringBuilder = new();
    private static readonly FizzBuzzWhizzBangBoomPowZapCrackFlashThunderLinearStringBuilder FizzBuzzWhizzBangBoomPowZapCrackFlashThunderLinearStringBuilder = new();

    private static readonly Consumer Consumer = new();

    private static void Tester(Func<long, string> identityFunction, int lcm, int step = 1)
    {
        for (long i = 0; i <= lcm; i += step)
        {
            Consumer.Consume(identityFunction(i));
        }
    }

    [Benchmark]
    public void FizzBuzzWhizzBangBoomPowExponentiallyRepeatedModTest() =>
        Tester(FizzBuzzWhizzBangBoomPowExponentiallyRepeatedMod.Identity, 1021020);

    [Benchmark]
    public void FizzBuzzWhizzBangBoomPowZapExponentiallyRepeatedModTest() =>
        Tester(FizzBuzzWhizzBangBoomPowZapExponentiallyRepeatedMod.Identity, 9699690, 10);

    [Benchmark]
    public void FizzBuzzWhizzBangBoomPowZapCrackExponentiallyRepeatedModTest() =>
        Tester(FizzBuzzWhizzBangBoomPowZapCrackExponentiallyRepeatedMod.Identity, 111546435, 112);

    [Benchmark]
    public void FizzBuzzWhizzBangBoomPowLinearStringBuilderTest() =>
        Tester(FizzBuzzWhizzBangBoomPowLinearStringBuilder.Identity, 1021020);

    [Benchmark]
    public void FizzBuzzWhizzBangBoomPowZapLinearStringBuilderTest() =>
        Tester(FizzBuzzWhizzBangBoomPowZapLinearStringBuilder.Identity, 9699690, 10);

    [Benchmark]
    public void FizzBuzzWhizzBangBoomPowZapCrackLinearStringBuilderTest() =>
        Tester(FizzBuzzWhizzBangBoomPowZapCrackLinearStringBuilder.Identity, 111546435, 112);
}
