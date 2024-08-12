using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FakeItEasy;
using fixDate;
using fixDate.interfaces;
using NUnit.Framework;
using FluentAssertions;

namespace fixDateTests
{
    public class FileListFilterTests
    {

        [Test]
        [TestCaseSource(nameof(fakeFileNames))]
        public void TestRegExFolderExcludePatterns(List<string> fakeListFileNames)
        {
            var fakeFileNameProvider = A.Fake<IFileNameProvider>();
            var fakeConfigReader = A.Fake<IConfigurationReader>();

            A.CallTo(() => fakeFileNameProvider.GetFileNames(A<string>.Ignored)).Returns(fakeListFileNames);
            A.CallTo(() => fakeConfigReader.GetExcludedFoldersPatterns()).Returns(new List<string>
            {
                "regex1",
                "regex2"
            });

            IFileListFilter sut = new FileListFilter(fakeFileNameProvider, fakeConfigReader);
            var filteredFileNames = sut.GetAllFileNames("");
            filteredFileNames.Should().HaveCount(3);
        }

        static IEnumerable<List<string>> fakeFileNames
        {
            get
            {
                yield return new List<string> { "hansi.txt", "sepp.txt", "jakob.txt" };
                yield return new List<string> { "hansi2.txt", "sepp2.txt", "jakob2.txt" };
            }
        }
    }
}
