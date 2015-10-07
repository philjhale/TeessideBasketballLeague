using Basketball.Common.Util;
using NUnit.Framework;

namespace Basketball.Tests.Common.Util
{
    [TestFixture]
    public class HtmlTests
    {
        [Test]
        public void ConvertToPlainText_ContainsParagraphTags_ParagraphsConvertedToNewLines()
        {
            Assert.That(Html.ConvertToPlainText("<p>Hello</p><p>Pies</p>"), Is.EqualTo("\r\nHello\r\n\r\nPies\r\n"));
        }

        [Test]
        public void ConvertToPlainText_ContainsValidBreakTag_BreakConvertedToNewLines()
        {
            Assert.That(Html.ConvertToPlainText("Large<br />bogies"), Is.EqualTo("Large\r\nbogies"));
        }

        [Test]
        public void ConvertToPlainText_ContainsTwoValidBreakTags_BreakConvertedToNewLines()
        {
            Assert.That(Html.ConvertToPlainText("Large<br /><br />bogies"), Is.EqualTo("Large\r\n\r\nbogies"));
        }

        [Test]
        public void ConvertToPlainText_ContainsInvalidBreakTag_BreakConvertedToNewLines()
        {
            Assert.That(Html.ConvertToPlainText("Large<br >bogies"), Is.EqualTo("Large\r\nbogies"));
        }

        [Test]
        public void ConvertToPlainText_ContainsNbsp_EntityRemove()
        {
            Assert.That(Html.ConvertToPlainText("Five&nbsp;ten&nbsp;"), Is.EqualTo("Five ten "));
        }

        [Test]
        public void ConvertToPlainText_HtmlNull_ReturnsNull()
        {
            Assert.That(Html.ConvertToPlainText(null), Is.Null);
        }

        [Test]
        public void ConvertToPlainText_HtmlZeroLength_ReturnsNull()
        {
            Assert.That(Html.ConvertToPlainText(""), Is.Null);
        }

        [Test]
        public void ConvertToPlainText_HtmlEmpty_ReturnsNull()
        {
            Assert.That(Html.ConvertToPlainText(" "), Is.Null);
        }
    }
}
