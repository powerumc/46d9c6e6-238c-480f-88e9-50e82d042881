namespace Diff.Tests;

public class LcsAlgorithmTests
{
    [Fact]
    public void BasicTest()
    {
        // given
        var source = (DiffString)"ABCDE";
        var dest = (DiffString)"ACDEFG";
        var diffSet = new DiffSet(source, dest);
        var algorithm = new LcsAlgorithm(diffSet);

        // when LCS 메트릭스 출력
        _ = diffSet.Diff(algorithm);
        using var writer = new StringWriter();
        algorithm.PrintMetrics(writer);

        // then 검증
        Assert.Equal(
            @"0 0 0 0 0 0 0
0 1 1 1 1 1 1
0 1 1 1 1 1 1
0 1 2 2 2 2 2
0 1 2 3 3 3 3
0 1 2 3 4 4 4",
            writer.ToString());

        Assert.True(true);
    }
}
