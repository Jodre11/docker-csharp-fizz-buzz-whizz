namespace FizzBuzzWhizz.Tests;

[Collection("Performance Tests")]
public class PerformanceTests
{
    private const string PerformanceTestClassText = @"
namespace TestNamespace;

[FizzBuzzWhizz.FizzBuzzWhizz(""Fizz"", ""3"", ""Buzz"", ""5"", ""Whizz"", ""7"", ""Bang"", ""11"", ""Boom"", ""13"")]
public partial class PerformanceTest
{
}";

    [Fact]
    public void IdentityMethod_ShouldHaveConstantTimeComplexity()
    {
        // Arrange
        var (instance, method) = GeneratePerformanceTestClass();
        var inputSizes = new[] { 100, 1000, 10000, 100000 };
        var measurements = new List<double>();

        // Act - Measure time for different input sizes
        foreach (var size in inputSizes)
        {
            var testCases = GenerateTestCases(size);
            var stopwatch = new Stopwatch();

            // Warmup
            for (int i = 0; i < Math.Min(100, size); i++)
            {
                method.Invoke(instance, [testCases[i]]);
            }

            stopwatch.Start();
            foreach (var testCase in testCases)
            {
                method.Invoke(instance, [testCase]);
            }
            stopwatch.Stop();

            var timePerCall = stopwatch.Elapsed.TotalMilliseconds / size;
            measurements.Add(timePerCall);
        }

        // Assert - Time per call should remain roughly constant (O(1) complexity)
        var firstMeasurement = measurements[0];
        var lastMeasurement = measurements[^1];
        var ratio = lastMeasurement / firstMeasurement;

        // For O(1) complexity, the ratio should be close to 1 (within reasonable bounds)
        // Allow for some variance due to JIT, GC, etc., but not exponential growth
        Assert.True(ratio < 3, $"Time complexity appears non-constant: {firstMeasurement:F6}ms -> {lastMeasurement:F6}ms (ratio: {ratio:F2})");
    }

    [Fact]
    public void IdentityMethod_ShouldHaveConstantSpaceComplexity()
    {
        // Arrange
        var (instance, method) = GeneratePerformanceTestClass();
        var inputSizes = new[] { 100, 1000, 10000 };
        var memoryMeasurements = new List<double>();

        // Act - Measure memory usage for different input sizes
        foreach (var size in inputSizes)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var initialMemory = GC.GetTotalMemory(true);
            var testCases = GenerateTestCases(size);

            // Process all test cases
            for (int i = 0; i < size; i++)
            {
                method.Invoke(instance, [testCases[i]]);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var finalMemory = GC.GetTotalMemory(true);
            var memoryPerCall = (double)(finalMemory - initialMemory) / size;
            memoryMeasurements.Add(memoryPerCall);
        }

        // Assert - Memory per call should remain roughly constant (O(1) space complexity)
        var firstMeasurement = memoryMeasurements[0];
        var lastMeasurement = memoryMeasurements[^1];
        var ratio = lastMeasurement / firstMeasurement;

        // For O(1) space complexity, memory per call should remain constant
        Assert.True(ratio < 2, $"Space complexity appears non-constant: {firstMeasurement:F2} bytes -> {lastMeasurement:F2} bytes per call (ratio: {ratio:F2})");
    }

    [Fact]
    public void IdentityMethod_ShouldScaleLinearlyWithNumberOfRules()
    {
        // Arrange - Test with different numbers of rules
        var ruleConfigurations = new[]
        {
            ("Simple", "[FizzBuzzWhizz.FizzBuzzWhizz(\"Fizz\", \"3\", \"Buzz\", \"5\")]"),
            ("Medium", "[FizzBuzzWhizz.FizzBuzzWhizz(\"Fizz\", \"3\", \"Buzz\", \"5\", \"Whizz\", \"7\", \"Bang\", \"11\")]"),
            ("Complex", "[FizzBuzzWhizz.FizzBuzzWhizz(\"Fizz\", \"3\", \"Buzz\", \"5\", \"Whizz\", \"7\", \"Bang\", \"11\", \"Boom\", \"13\", \"Pow\", \"17\", \"Zap\", \"19\")]")
        };

        var performanceResults = new Dictionary<string, double>();
        var testCases = GenerateTestCases(1000);

        // Act
        foreach (var (name, attributeText) in ruleConfigurations)
        {
            var classText = $@"
namespace TestNamespace;

{attributeText}
public partial class {name}Test
{{
}}";

            var (instance, method) = Generate(classText, $"{name}Test");
            var stopwatch = new Stopwatch();

            // Warmup
            for (int i = 0; i < 100; i++)
            {
                method.Invoke(instance, [testCases[i]]);
            }

            stopwatch.Start();
            foreach (var testCase in testCases)
            {
                method.Invoke(instance, [testCase]);
            }
            stopwatch.Stop();

            var timePerCall = stopwatch.Elapsed.TotalMilliseconds / testCases.Length;
            performanceResults[name] = timePerCall;
        }

        // Assert - Performance should scale reasonably with rule complexity
        // The time increase should not be exponential (which would indicate poor algorithm)
        var simpleTime = performanceResults["Simple"];
        var mediumTime = performanceResults["Medium"];
        var complexTime = performanceResults["Complex"];

        // Calculate rule counts (pairs of word/divisor)
        var simpleRules = 2; // Fizz/3, Buzz/5
        var mediumRules = 4; // Fizz/3, Buzz/5, Whizz/7, Bang/11
        var complexRules = 7; // 7 pairs

        // Check that performance scales reasonably with rule count
        var mediumRatio = mediumTime / simpleTime;
        var complexRatio = complexTime / simpleTime;
        var expectedMediumRatio = (double)mediumRules / simpleRules;
        var expectedComplexRatio = (double)complexRules / simpleRules;

                // Allow for some overhead but should scale roughly linearly
        Assert.True(mediumRatio < expectedMediumRatio * 1.5,
            $"Medium complexity scales poorly: {mediumRatio:F2}x vs expected ~{expectedMediumRatio:F2}x");
        Assert.True(complexRatio < expectedComplexRatio * 2,
            $"Complex complexity scales poorly: {complexRatio:F2}x vs expected ~{expectedComplexRatio:F2}x");
    }

    [Fact]
    public void IdentityMethod_ShouldScaleEfficientlyWithRuleCount()
    {
        // Arrange - Test rule counts from 1 to 10 pairs (more reasonable range)
        var ruleCounts = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        var performanceResults = new Dictionary<int, double>();
        var testCases = GenerateTestCases(500); // Use fewer test cases for faster execution

        // Act - Measure performance for each rule count
        foreach (var ruleCount in ruleCounts)
        {
            try
            {
                var attributeText = GenerateAttributeText(ruleCount);
                var className = $"RuleCount{ruleCount}Test";

                var classText = $@"
namespace TestNamespace;

{attributeText}
public partial class {className}
{{
}}";

                var (instance, method) = Generate(classText, className);
                var stopwatch = new Stopwatch();

                // Warmup
                for (int i = 0; i < Math.Min(50, testCases.Length); i++)
                {
                    method.Invoke(instance, [testCases[i]]);
                }

                stopwatch.Start();
                foreach (var testCase in testCases)
                {
                    method.Invoke(instance, [testCase]);
                }
                stopwatch.Stop();

                var timePerCall = stopwatch.Elapsed.TotalMilliseconds / testCases.Length;
                performanceResults[ruleCount] = timePerCall;
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to generate or test rule count {ruleCount}: {ex.Message}");
            }
        }

        // Assert - Performance should scale reasonably with rule count
        var baseTime = performanceResults[1]; // 1 rule as baseline

        foreach (var (ruleCount, timePerCall) in performanceResults)
        {
            if (ruleCount == 1) continue; // Skip baseline

            var ratio = timePerCall / baseTime;
            var expectedRatio = ruleCount; // Linear scaling

            // Allow for some overhead but should not scale exponentially
            // For 10 rules, we expect roughly 10x the time, but allow up to 15x for overhead
            var maxAllowedRatio = expectedRatio * 1.5;

            Assert.True(ratio < maxAllowedRatio,
                $"Rule count {ruleCount} scales poorly: {ratio:F2}x vs expected ~{expectedRatio:F2}x (max allowed: {maxAllowedRatio:F2}x)");
        }

        // Additional analysis: Check for exponential growth patterns
        var times = ruleCounts.Select(rc => performanceResults[rc]).ToArray();
        var growthRates = new List<double>();

        for (int i = 1; i < times.Length; i++)
        {
            var growthRate = times[i] / times[i - 1];
            growthRates.Add(growthRate);
        }

                // Average growth rate should be reasonable (not exponential)
        var avgGrowthRate = growthRates.Average();
        Assert.True(avgGrowthRate < 1.5,
            $"Average growth rate {avgGrowthRate:F2} suggests exponential scaling");
    }

    [Fact]
    public void IdentityMethod_ShouldHandleAllInputSizesConsistently()
    {
        // Arrange
        var (instance, method) = GeneratePerformanceTestClass();
        var inputSizes = new[] { 1, 10, 100, 1000 };
        var results = new List<bool>();

        // Act - Test that all input sizes work correctly
        foreach (var size in inputSizes)
        {
            var testCases = GenerateTestCases(size);
            var success = true;

            try
            {
                foreach (var testCase in testCases)
                {
                    var result = method.Invoke(instance, [testCase]) as string;
                    if (string.IsNullOrEmpty(result))
                    {
                        success = false;
                        break;
                    }
                }
            }
            catch
            {
                success = false;
            }

            results.Add(success);
        }

        // Assert - All input sizes should work correctly
        Assert.All(results, result => Assert.True(result, "Method failed for some input size"));
    }

        [Fact]
    public void IdentityMethod_ShouldMaintainPerformanceBaseline()
    {
        // Arrange - Test cases that should maintain consistent performance
        var baselineTests = new[]
        {
            ("FizzBuzz", "[FizzBuzzWhizz.FizzBuzzWhizz(\"Fizz\", \"3\", \"Buzz\", \"5\")]", typeof(FizzBuzzModel)),
            ("FizzBuzzWhizz", "[FizzBuzzWhizz.FizzBuzzWhizz(\"Fizz\", \"3\", \"Buzz\", \"5\", \"Whizz\", \"7\")]", typeof(FizzBuzzWhizzModel))
        };
        
        var testCases = GenerateTestCases(1000);
        var performanceResults = new Dictionary<string, (double generated, double model, double stdDev)>();
        
        // Act - Measure performance for baseline test cases with multiple runs
        foreach (var (name, attributeText, modelType) in baselineTests)
        {
            var className = $"{name}BaselineTest";
            var classText = $@"
namespace TestNamespace;

{attributeText}
public partial class {className}
{{
}}";
            
            var (instance, method) = Generate(classText, className);
            var modelInstance = Activator.CreateInstance(modelType);
            var modelMethod = modelType.GetMethod("Identity");
            Assert.NotNull(modelMethod);
            
            // Warmup
            for (int i = 0; i < 100; i++)
            {
                method.Invoke(instance, [testCases[i]]);
                modelMethod.Invoke(modelInstance, [testCases[i]]);
            }
            
            // Multiple runs to establish baseline and variance
            var generatedRuns = new List<double>();
            var modelRuns = new List<double>();
            const int numRuns = 5;
            
            for (int run = 0; run < numRuns; run++)
            {
                // Measure generated
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                foreach (var testCase in testCases)
                {
                    method.Invoke(instance, [testCase]);
                }
                stopwatch.Stop();
                var generatedTimePerCall = stopwatch.Elapsed.TotalMilliseconds / testCases.Length;
                generatedRuns.Add(generatedTimePerCall);
                
                // Measure model
                stopwatch.Restart();
                foreach (var testCase in testCases)
                {
                    modelMethod.Invoke(modelInstance, [testCase]);
                }
                stopwatch.Stop();
                var modelTimePerCall = stopwatch.Elapsed.TotalMilliseconds / testCases.Length;
                modelRuns.Add(modelTimePerCall);
            }
            
            var generatedAvg = generatedRuns.Average();
            var modelAvg = modelRuns.Average();
            var modelStdDev = CalculateStandardDeviation(modelRuns);
            
            performanceResults[name] = (generatedAvg, modelAvg, modelStdDev);
        }
        
        // Assert - Generated code should not be more than 2.5 sigma above model average
        foreach (var (name, (generated, model, stdDev)) in performanceResults)
        {
            var threshold = model + (2.5 * stdDev);
            Assert.True(generated < threshold, 
                $"{name} generated code is too slow: {generated:F6}ms per call vs model {model:F6}ms ± {stdDev:F6}ms (threshold: {threshold:F6}ms)");
        }
    }

    private static double CalculateStandardDeviation(List<double> values)
    {
        var mean = values.Average();
        var sumOfSquares = values.Sum(x => Math.Pow(x - mean, 2));
        return Math.Sqrt(sumOfSquares / values.Count);
    }

    private static string GenerateAttributeText(int ruleCount)
    {
        var rules = new List<string>();
        var primes = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71 };
        var words = new[] { "Fizz", "Buzz", "Whizz", "Bang", "Boom", "Pow", "Zap", "Crack", "Flash", "Thunder",
                           "Lightning", "Storm", "Rain", "Wind", "Fire", "Ice", "Earth", "Water", "Air", "Energy" };

        for (int i = 0; i < ruleCount; i++)
        {
            rules.Add($"\"{words[i]}\"");
            rules.Add($"\"{primes[i]}\"");
        }

        return $"[FizzBuzzWhizz.FizzBuzzWhizz({string.Join(", ", rules)})]";
    }

    private static (object instance, MethodInfo method) GeneratePerformanceTestClass()
    {
        return Generate(PerformanceTestClassText, "PerformanceTest");
    }

    private static (object instance, MethodInfo method) Generate(
        string classText,
        string className)
    {
        // Create an instance of the source generator.
        var generator = new FizzBuzzWhizz();

        // Source generators should be tested using 'GeneratorDriver'.
        var driver = Microsoft.CodeAnalysis.CSharp.CSharpGeneratorDriver.Create(generator);

        // We need to create a compilation with the required source code.
        var compilation = Microsoft.CodeAnalysis.CSharp.CSharpCompilation.Create(nameof(PerformanceTests),
            [Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(classText, cancellationToken: TestContext.Current.CancellationToken)],
            [
                // To support 'System.Attribute' inheritance, add reference to 'System.Private.CoreLib'.
                Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            ],
            new Microsoft.CodeAnalysis.CSharp.CSharpCompilationOptions(Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary)
                .WithOptimizationLevel(Microsoft.CodeAnalysis.OptimizationLevel.Release)
                .WithDeterministic(false));

        // Run generators and retrieve all results.
        var runResult = driver.RunGenerators(compilation, TestContext.Current.CancellationToken).GetRunResult();

        // All generated files can be found in 'RunResults.GeneratedTrees'.
        var generatedAttributeSyntax = runResult.GeneratedTrees.Single(t => t.FilePath.EndsWith("FizzBuzzWhizzAttribute.g.cs"));
        var generatedClassSyntax = runResult.GeneratedTrees.Single(t => t.FilePath.EndsWith($"{className}.g.cs"));

        // Add the generated tree to the compilation
        var updatedCompilation = compilation.AddSyntaxTrees(generatedAttributeSyntax, generatedClassSyntax);

        // Emit the assembly to a stream
        using var ms = new MemoryStream();
        var emitResult = updatedCompilation.Emit(ms, cancellationToken: TestContext.Current.CancellationToken);
        Assert.True(emitResult.Success, "Compilation failed");

        // Load the assembly
        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        // Get the generated type and instantiate it
        var type = assembly.GetType($"TestNamespace.{className}");
        Assert.NotNull(type);

        var instance = Activator.CreateInstance(type);
        Assert.NotNull(instance);

        var method = instance.GetType().GetMethod("Identity");
        Assert.NotNull(method);

        return (instance, method);
    }

    private static long[] GenerateTestCases(int count)
    {
        var random = new Random(42); // Fixed seed for reproducible tests
        var testCases = new long[count];

        for (int i = 0; i < count; i++)
        {
            // Generate a mix of small and large numbers
            if (i % 10 == 0)
            {
                // Large numbers
                testCases[i] = random.Next(1000000, 10000000);
            }
            else
            {
                // Small numbers
                testCases[i] = random.Next(1, 1000);
            }
        }

        return testCases;
    }

    public class FizzBuzzModel
    {
        public string Identity(long n)
        {
            if (n == 0) return "0";
            if (n % 3 == 0 && n % 5 == 0) return "FizzBuzz";
            if (n % 3 == 0) return "Fizz";
            if (n % 5 == 0) return "Buzz";
            return n.ToString();
        }
    }

    public class FizzBuzzWhizzModel
    {
        public string Identity(long n)
        {
            if (n == 0) return "0";
            if (n % 3 == 0 && n % 5 == 0 && n % 7 == 0) return "FizzBuzzWhizz";
            if (n % 3 == 0 && n % 5 == 0) return "FizzBuzz";
            if (n % 3 == 0 && n % 7 == 0) return "FizzWhizz";
            if (n % 5 == 0 && n % 7 == 0) return "BuzzWhizz";
            if (n % 3 == 0) return "Fizz";
            if (n % 5 == 0) return "Buzz";
            if (n % 7 == 0) return "Whizz";
            return n.ToString();
        }
    }
}
