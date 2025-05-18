using System;
using System.Linq;
using fixDate;
using fixDate.interfaces;
using fixDate.Models;
using FluentAssertions;
using NUnit.Framework;

namespace fixDateTests;

public class DateParsingTests
{
    private IDateParsing sut = new DateParsing();
    private IConfigurationReader cfgReader = new ConfigurationReaderHardCoded();
    [SetUp]
    public void Setup()
    {
    }
    
    private const string dateYYYYmmdd = "\\d{4}-\\d{1,2}-\\d{1,2}";
    private const string dateYYYYmmddHHmmss = @"\d{4}-\d{1,2}-\d{1,2} \d{1,2}[\.]\d{1,2}[\.]\d{1,2}";
    private const string dateYYYYmmddHHmm = @"\d{4}-\d{1,2}-\d{1,2} \d{1,2}[\.]\d{1,2}";
    [Test]
    [TestCase("2022-11-23",0,0,0)]
    [TestCase("2022-1-9",0,0,0)]
    [TestCase("2022-11-23 23.45.37",23,45,37)]
    [TestCase("2022-1-9 5.3.9",5,3,9)]
    [TestCase("2022-11-23 22.39",22,39,0)]
    public void TestMinutesSeconds(string extractedDate, int h, int m, int s)
    {
        
        TheMatchResult result = new TheMatchResult {Success = true, TheValue = extractedDate};
        DateTime fdate;
        sut.TryParseDateExact(result, cfgReader.GetDateTimeParsingFormats().Values.ToList(), out fdate);

        fdate.Second.Should().Be(s);
        fdate.Minute.Should().Be(m);
        fdate.Hour.Should().Be(h);
    }
}