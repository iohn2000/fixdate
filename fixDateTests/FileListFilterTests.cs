using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using fixDate;
using fixDate.interfaces;
using NUnit.Framework;
using FluentAssertions;
using fixDate.FileOperations;

namespace fixDateTests;

public class FileListFilterTests
{

    [Test]
    [TestCaseSource(nameof(fakeFileNames))]
    public void TestRegExFolderExcludePatterns(List<string> fakeListFileNames)
    {
        var fakeFileNameProvider = A.Fake<IFileManager>();
        var fakeConfigReader = A.Fake<IConfigurationReader>();

        A.CallTo(() => fakeFileNameProvider.GetFileNames(A<string>.Ignored)).Returns(fakeListFileNames);
        A.CallTo(() => fakeConfigReader.GetExcludedFoldersPatterns()).Returns(new List<string>
        {
            "sub2",
            "sub3",
        });

        IFileListFilter sut = new FileListFilter(fakeFileNameProvider, fakeConfigReader);
        var filteredFileNames = sut.GetAllFileNames("");
        filteredFileNames.Where(w=>w.IsIncluded).Should().HaveCount(4);
    }

    static IEnumerable<List<string>> fakeFileNames
    {
        get
        {
            yield return new List<string> 
                { 
                    "hansi.txt", "sub1/sepp_1.txt", "sub2/jakob_2.txt", "sub3/asdf_3.txt",
                    "sub2_in_name.txt", "sub1/sub4/sub3/auch_excluded.txt",
                    "sub1/sub2.txt"
                };
        }
    }
}
