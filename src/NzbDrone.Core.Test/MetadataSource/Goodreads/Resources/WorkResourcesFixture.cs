using System;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using NzbDrone.Core.MetadataSource.Goodreads;
using NzbDrone.Core.Test.Framework;
using NzbDrone.Test.Common;

namespace NzbDrone.Core.Test.MetadataSource.Goodreads.Resources
{
    [TestFixture]
    public class WorkResourceFixture : CoreTest<WorkResource>
    {
        [Test]
        public void parse_non_work()
        {
            var element = new XElement("Dummy", "entry");
            var work = new WorkResource();

            Assert.Throws<NullReferenceException>(() => work.Parse(element));

            work.OriginalTitle.Should().Be(null);

            ExceptionVerification.IgnoreWarns();
        }

        [Test]
        public void parse_minimal_work()
        {
            var element = new XElement("work",
                new XElement("original_title", "Book Title"),
                new XElement("id", "123456789"));

            var work = new WorkResource();

            work.Parse(element);

            work.OriginalTitle.Should().Be("Book Title");
            work.Id.Should().Be(123456789);

            ExceptionVerification.IgnoreWarns();
        }

        [Test]
        public void parse_minimal_work_with_surrounding_tags()
        {
            var element = new XElement("series_works",
                new XElement("work",
                    new XElement("original_title", "Book Title"),
                    new XElement("id", "123456789")));

            var work = new WorkResource();

            work.Parse(element);

            work.OriginalTitle.Should().Be("Book Title");
            work.Id.Should().Be(123456789);

            ExceptionVerification.IgnoreWarns();
        }
    }
}
