namespace FizzBuzzWhizz.Sample;

[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.NativeAot90)]
[RPlotExporter]
[MemoryDiagnoser]
public class IdentityPerformanceComparison
{
    private static readonly FizzBuzzManualManyModuloOneByOne FizzBuzzManualManyModuloOneByOne = new();
    private static readonly FizzBuzzManualManyModuloUpFront FizzBuzzManualManyModuloUpFront = new();

    private static readonly Consumer Consumer = new();

    private static void Tester(Func<long, string> identityFunction)
    {
        for (long i = 0; i <= 60; i++)
        {
            Consumer.Consume(identityFunction(i));
        }
    }

    [Benchmark]
    public void FizzBuzzManualManyModuloOneByOneTest() =>
        Tester(FizzBuzzManualManyModuloOneByOne.Identity);

    [Benchmark]
    public void FizzBuzzManualManyModuloUpFrontTest() =>
        Tester(FizzBuzzManualManyModuloUpFront.Identity);
}
