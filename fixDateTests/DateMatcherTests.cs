using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using FakeItEasy;
using fixDate;
using fixDate.interfaces;
using fixDate.Models;
using NUnit.Framework;

namespace fixDateTests;

public class DateMatcherTests
{
    [SetUp]
    public void Setup()
    {
    }

    private const string dateYYYYmmdd = @"\d{4}-\d{1,2}-\d{1,2}";
    private const string dateYYYYmmddHHmmss = @"\d{4}-\d{1,2}-\d{1,2} \d{1,2}[\.]\d{1,2}[\.]\d{1,2}";
    private const string dateYYYYmmddHHmm = @"\d{4}-\d{1,2}-\d{1,2} \d{1,2}[\.]\d{1,2}";


    [Test]
    [TestCase("mypicture-2012-11-23 22.17.22_1","mypicture-2012-11-23 22.17.22")]
    [TestCase("mypicture-2012-11-23 22.17.22_14","mypicture-2012-11-23 22.17.22")]
    [TestCase("mypicture-2012-11-23 22.17.22_","mypicture-2012-11-23 22.17.22")]
    [TestCase("mypic-2012-11-23 22.17 _1","mypic-2012-11-23 22.17 ")]
    public void CleanFileNameTest(string orig, string clean)
    {
        IDateMatch sut = new DateMatcher();
        var result = sut.TryCleaningUpFilename(orig);
        Assert.AreEqual(clean,result);
    }
        
    [Test]
    [TestCase("date-2022-11-23 23.45.37", dateYYYYmmddHHmmss, true, "2022-11-23 23.45.37")]
    [TestCase("date-2022-1-9 5.3.9", dateYYYYmmddHHmmss, true, "2022-1-9 5.3.9")]
    [TestCase("date-2022-11-23 22.39", dateYYYYmmddHHmm, true, "2022-11-23 22.39")]
    public void TestDateMatchingExcatSingleMatches(string fileName, string pattern, bool isADate,
        string extractedDate)
    {
        IDateMatch sut = new DateMatcher();
        TheMatchResult findDateFormatMatch = sut.FindDateFormatMatch(fileName, new SortedList<int, string> {{ 1, pattern}});

        Assert.AreEqual(extractedDate, findDateFormatMatch.TheValue);
        Assert.AreEqual(isADate, findDateFormatMatch.Success);
            
    }

    private const string dateYYYYmmddAppendix = @"\d{4}-\d{1,2}-\d{1,2} \d{1,2}[\.]\d{1,2}";

    [Test]
    [TestCase("testname-1976-12-12 14.44-bb-2022-5-17 13.49 2_1.jpg", dateYYYYmmddAppendix, true, "2022-5-17 13.49")]
    [TestCase("testname-2022-5-17 13.49 3_1.jpg", dateYYYYmmddAppendix, true, "2022-5-17 13.49")]
    [TestCase("testname-2022-5-17 13.49_1.jpg", dateYYYYmmddAppendix, true, "2022-5-17 13.49")]
    public void TestUnderscoreAppendixClearScannerExcatSingleMatches(string fileName, string pattern, bool isADate,
        string extractedDate)
    {
        IDateMatch sut = new DateMatcher();
        TheMatchResult findDateFormatMatch = sut.FindDateFormatMatch(fileName, new SortedList<int, string> {{ 1, pattern}});
        Assert.AreEqual(isADate, findDateFormatMatch.Success);
        Assert.AreEqual(extractedDate, findDateFormatMatch.TheValue);
    }

    [Test]
    [TestCase("testname-1976-12-12 14.44.jpg", "1976-12-12 14.44")]
    [TestCase("testname-2022-5-17 13.49.jpg", "2022-5-17 13.49")]
    [TestCase("testname-2022-5-17 13.49.11.jpg", "2022-5-17 13.49.11")]
    [TestCase("date-2022-11-23", "2022-11-23")]
    [TestCase("date-2022-1-9", "2022-1-9")]
    [TestCase("mypic-2012-11-23 22.17 _1","2012-11-23 22.17")]
    [TestCase("mypicture-2012-11-23 22.17.22_1","2012-11-23 22.17.22")]
    [TestCase("mypicture-2012-11-23 22.17.22_14","2012-11-23 22.17.22")]
    [TestCase("mypicture-2012-11-23 22.17.22_","2012-11-23 22.17.22")]
    public void DateMatchingWithConfigListOfFormats(string fileName, string result)
    {
        IConfigurationReader cfgReader = new ConfigurationReaderHardCoded();
        IDateMatch sut = new DateMatcher();

        TheMatchResult findDateFormatMatch = sut.FindDateFormatMatch(fileName, cfgReader.GetDateTimeMatchingPatterns());
        Assert.IsTrue(findDateFormatMatch.Success);
        Assert.AreEqual(result, findDateFormatMatch.TheValue);
    }
}