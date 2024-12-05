namespace AdventOfCodeDay3.Test;

[TestClass]
public class DayThreeTests
{
    [TestMethod]
    public void Multiply_ShouldMultiplyCorrectly()
    {
        string input = "mul(44,46)";
        int expected = 2024;

        string tempTestFile = "tempTestFileMultiply.txt";
        File.WriteAllText(tempTestFile, input);

        DayThree dayThree = new DayThree(tempTestFile);
        int actual = dayThree.Multiply(44, 46);

        Assert.AreEqual(expected, actual);

        File.Delete(tempTestFile);
    }

    [TestMethod]
    public void PartOne_ShouldReturnCorrectSum()
    {
        string input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        int expected = 161;

        string tempTestFile = "tempTestFilePartOne.txt";
        File.WriteAllText(tempTestFile, input);

        DayThree dayThree = new DayThree(tempTestFile);
        int actual = dayThree.PartOne();

        Assert.AreEqual(expected, actual);

        File.Delete(tempTestFile);
    }

    [TestMethod]
    public void PartTwo_ShouldReturnCorrectSum()
    {
        string input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        int expected = 48;

        string tempTestFile = "tempTestFilePartTwo.txt";
        File.WriteAllText(tempTestFile, input);

        DayThree dayThree = new DayThree(tempTestFile);
        int actual = dayThree.PartTwo();

        Assert.AreEqual(expected, actual);

        File.Delete(tempTestFile);
    }
}