using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Grumpy.Common.Extensions;
using Grumpy.Common.UnitTests.Helper;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class StringExtensionsTests
    {
        private const string SomeText = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...";

        [Fact]
        public void DeserializeXml_Work()
        {
            const string str = "<TestPerson><Age>8</Age><Name>Sara</Name></TestPerson>";

            var result = str.DeserializeFromXml<TestPerson>();

            result.Age.Should().Be(8);
            result.Name.Should().Be("Sara");
        }

        [Fact]
        public void NullOrEmpty_Null_True()
        {
            ((string)null).NullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void NullOrEmpty_Empty_True()
        {
            "".NullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void NullOrEmpty_Space_False()
        {
            " ".NullOrEmpty().Should().BeFalse();
        }

        [Fact]
        public void NullOrEmpty_NonEmpty_False()
        {
            "Hallo".NullOrEmpty().Should().BeFalse();
        }

        [Fact]
        public void NullOrWhiteSpace_Null_True()
        {
            ((string)null).NullOrWhiteSpace().Should().BeTrue();
        }

        [Fact]
        public void NullOrWhiteSpace_Empty_True()
        {
            "".NullOrWhiteSpace().Should().BeTrue();
        }

        [Fact]
        public void NullOrWhiteSpace_Space_True()
        {
            " ".NullOrWhiteSpace().Should().BeTrue();
        }

        [Fact]
        public void NullOrWhiteSpace_NonEmpty_False()
        {
            "Hallo".NullOrWhiteSpace().Should().BeFalse();
        }

        [Fact]
        public void ContainsAnyOf_CharactersNotFound_False()
        {
            char[] a = { 'O', ':', 'D' };

            "Hallo there".ContainsAnyOf(a).Should().BeFalse();
        }

        [Fact]
        public void ContainsAnyOf_OneOfCharactersFound_True()
        {
            char[] a = { 'O', ':', 'D' };

            "HallO there".ContainsAnyOf(a).Should().BeTrue();
        }

        [Fact]
        public void ContainsAnyOf_EmptyString_False()
        {
            char[] a = { 'A', ':', 'D' };

            Assert.False("".ContainsAnyOf(a));
        }

        [Fact]
        public void ContainsAnyOf_EmptyList_False()
        {
            char[] b = { };

            Assert.False("Hallo".ContainsAnyOf(b));
        }

        [Fact]
        public void ContainsAnyOf_EmptyListInEmptyString_False()
        {
            char[] b = { };

            Assert.False("".ContainsAnyOf(b));
        }

        [Fact]
        public void StartWith_StringHasPrefix_SameString()
        {
            Assert.Equal("Hi There", "Hi There".StartWith("H"));
        }

        [Fact]
        public void StartWith_StringDoesNotHavePrefix_DifferentString()
        {
            Assert.Equal("gHi There", "Hi There".StartWith("g"));
        }

        [Fact]
        public void EndWith_StringHasSuffix_SameString()
        {
            Assert.Equal("Hi There", "Hi There".EndWith("e"));
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void EndWith_StringDoNotHaveSuffix_DifferentString()
        {
            Assert.Equal("Alberts", "Albert".EndWith("s"));
        }

        [Fact]
        public void StartsWith_True()
        {
            Assert.StartsWith("H", "Hi There");
        }

        [Fact]
        public void StartsWith_False()
        {
            Assert.False("Hi There".StartsWith("g"));
        }

        [Fact]
        public void Less_LargerString_False()
        {
            Assert.False("Hi There".Less("Gi There"));
        }

        [Fact]
        public void Less_SmallerString_True()
        {
            Assert.True("Hi There".Less("Ji There"));
        }

        [Fact]
        public void Less_EmptyString_False()
        {
            Assert.False("Hi There".Less(""));
        }

        [Fact]
        public void Less_StringEqual_False()
        {
            Assert.False("Hi There".Less("Hi There"));
        }

        [Fact]
        public void LessOrEqual_LargerString_False()
        {
            Assert.False("Hi There".LessOrEqual("Gi There"));
        }

        [Fact]
        public void LessOrEqual_SmallerString_True()
        {
            Assert.True("Hi There".LessOrEqual("Ji There"));
        }

        [Fact]
        public void LessOrEqual_EmptyString_False()
        {
            Assert.False("Hi There".LessOrEqual(""));
        }

        [Fact]
        public void LessOrEqual_StringEqual_True()
        {
            Assert.True("Hi There".LessOrEqual("Hi There"));
        }

        [Fact]
        public void Greater_LargerString_True()
        {
            Assert.True("Hi There".Greater("Gi There"));
        }

        [Fact]
        public void Greater_SmallerString_False()
        {
            Assert.False("Hi There".Greater("Ji There"));
        }

        [Fact]
        public void Greater_EmptyString_True()
        {
            Assert.True("Hi There".Greater(""));
        }

        [Fact]
        public void Greater_SameString_False()
        {
            Assert.False("Hi There".Greater("Hi There"));
        }

        [Fact]
        public void GreaterOrEqual_LargerString_True()
        {
            Assert.True("Hi There".GreaterOrEqual("Gi There"));
        }

        [Fact]
        public void GreaterOrEqual_SmallerString_False()
        {
            Assert.False("Hi There".GreaterOrEqual("Ji There"));
        }

        [Fact]
        public void GreaterOrEqual_EmptyString_True()
        {
            Assert.True("Hi There".GreaterOrEqual(""));
        }

        [Fact]
        public void GreaterOrEqual_SameString_True()
        {
            Assert.True("Hi There".GreaterOrEqual("Hi There"));
        }

        [Fact]
        public void ContainsAnyOf_OneCharInString_True()
        {
            Assert.True("Hi There".ContainsAnyOf('a', 'b', 'e', 'f'));
        }

        [Fact]
        public void ContainsAnyOf_MoreCharsInString_True()
        {
            Assert.True("Hi There".ContainsAnyOf('a', 'b', 'e', 'f', 'T'));
        }

        [Fact]
        public void ContainsAnyOf_NoCharsInString_False()
        {
            Assert.False("Hi There".ContainsAnyOf('a', 'b', 'E', 'f', 't'));
        }

        [Fact]
        public void Left_NullString()
        {
            Assert.Null(((string)null).Left(10));
        }

        [Fact]
        public void Left_EmptyString()
        {
            Assert.Equal(string.Empty, string.Empty.Left(10));
        }

        [Fact]
        public void Left_LongString()
        {
            var ss = SomeText.Left(50);

            Assert.Equal(50, ss.Length);
            Assert.Equal(SomeText.Substring(0, 50), ss);
        }

        [Fact]
        public void Left_StringToShort()
        {
            var s = SomeText.Left(120);

            Assert.Equal(94, s.Length);
            Assert.Equal(SomeText, s);
        }

        [Fact]
        public void Left_NegativePart()
        {
            Assert.Equal(SomeText.Substring(0, SomeText.Length - 1), SomeText.Left(-1));
        }

        [Fact]
        public void Left_NegativeAll()
        {
            Assert.Equal("", SomeText.Left(-120));
        }

        [Fact]
        public void Right_NullString()
        {
            Assert.Null(((string)null).Right(10));
        }

        [Fact]
        public void Right_EmptyString()
        {
            Assert.Equal(string.Empty, string.Empty.Right(10));
        }

        [Fact]
        public void Right_LongString()
        {
            var s = SomeText.Right(50);

            Assert.Equal(50, s.Length);
            Assert.Equal(SomeText.Substring(44, 50), s);
        }

        [Fact]
        public void Right_StringToShort()
        {
            var s = SomeText.Right(120);

            Assert.Equal(94, s.Length);
            Assert.Equal(SomeText, s);
        }

        [Fact]
        public void Right_NegativePart()
        {
            Assert.Equal(SomeText.Substring(1), SomeText.Right(-1));
        }

        [Fact]
        public void Right_NegativeAll()
        {
            Assert.Equal("", SomeText.Right(-120));
        }

        [Fact]
        public void ToStream_Text_Match()
        {
            var act = SomeText.ToStream();

            using (var reader = new StreamReader(act, Encoding.UTF8))
                Assert.Equal(SomeText, reader.ReadToEnd());
        }

        [Fact]
        public void ToStream_Null_Null()
        {
            var act = ((string)null).ToStream();

            Assert.Null(act);
        }

        [Fact]
        public void StartsWith_Match_True()
        {
            Assert.StartsWith("H", "Hi");
        }

        [Fact]
        public void StartsWith_NonMatch_False()
        {
            Assert.False("Hi".StartsWith("h"));
        }

        [Fact]
        public void StartsWith_Empty_False()
        {
            Assert.False("".StartsWith("h"));
        }

        [Fact]
        public void StartsWith_Null_False()
        {
            Assert.False(StringExtensions.StartsWith(null, "h"));
        }

        [Fact]
        public void StartsWith_Any_True()
        {
            Assert.True("Hi".StartsWith("h", "H"));
        }

        [Fact]
        public void StartsWith_None_False()
        {
            Assert.False("Hi".StartsWith("d", "D"));
        }

        [Fact]
        public void ContainsAllOf_Char_True()
        {
            Assert.True("Hi there you lovely person".ContainsAllOf('o', 'p', 'H'));
        }

        [Fact]
        public void ContainsAllOf_Char_False()
        {
            Assert.False("Hi there you lovely person".ContainsAllOf('o', 'p', 'G'));
        }

        [Fact]
        public void ContainsAnyOf_Char_True()
        {
            Assert.True("Hi there you lovely person".ContainsAnyOf('O', 'p', 'G'));
        }

        [Fact]
        public void ContainsAnyOf_Char_False()
        {
            Assert.False("Hi there you lovely person".ContainsAnyOf('O', 'P', 'g'));
        }

        [Fact]
        public void ContainsAllOf_String_True()
        {
            Assert.True("Hi there you lovely person".ContainsAllOf("ove", "per", "Hi "));
        }

        [Fact]
        public void ContainsAllOf_String_False()
        {
            Assert.False("Hi there you lovely person".ContainsAllOf("ove", "per", "G"));
        }

        [Fact]
        public void ContainsAnyOf_String_True()
        {
            Assert.True("Hi there you lovely person".ContainsAnyOf("O", "per", "G"));
        }

        [Fact]
        public void ContainsAnyOf_String_False()
        {
            Assert.False("Hi there you lovely person".ContainsAnyOf("op", "yi", "g"));
        }

        [Fact]
        public void To_Enum_Match()
        {
            Assert.Equal(DayOfWeek.Monday, "Monday".To<DayOfWeek>());
        }

        [Fact]
        public void ToMilliseconds_Match()
        {
            Assert.Equal(new TimeSpan(0, 0, 0, 1).TotalMilliseconds, "1s".ToMilliseconds());
        }

        [Fact]
        public void To_Enum_Comma_Match()
        {
            Assert.Throws<InvalidCastException>(() => "Monday, Tuesday".To<DayOfWeek>());
        }

        [Fact]
        public void To_Int_Match()
        {
            Assert.Equal(314, "314".To<int>());
        }

        [Fact]
        public void To_NullableInt_Match()
        {
            Assert.Equal(314, "314".To<int?>());
        }

        [Fact]
        public void To_Double_Match()
        {
            Assert.Equal(314.54, "314.54".To<double>());
        }

        [Fact]
        public void To_String_Match()
        {
            Assert.Equal("314.54", "314.54".To<string>());
        }

        [Fact]
        public void AsRegEx_True()
        {
            Assert.Matches("^\\d{3}-\\d{3}-\\d{4}$".AsRegex(), "123-456-7890");
        }

        [Fact]
        public void AsRegEx_False()
        {
            Assert.DoesNotMatch("^\\d{3}-\\d{3}-\\d{4}$".AsRegex(), "123-456-789D");
        }

        [Fact]
        public void MatchesRegEx_True()
        {
            Assert.True("123-456-7890".MatchesRegex("^\\d{3}-\\d{3}-\\d{4}$"));
        }

        [Fact]
        public void MatchesRegex_False()
        {
            Assert.False("123-456-7D90".MatchesRegex("^\\d{3}-\\d{3}-\\d{4}$"));
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ReplaceAnyOf_Test()
        {
            Assert.Equal("abxxxcd#efxxxgh&ij(kl)m", "ab!cd#ef%gh&ij(kl)m".ReplaceAnyOf("!%", "xxx"));
        }

        [Fact]
        public void Escape_Test()
        {
            Assert.Equal(@"field1\;field2\\field3", @"field1;field2\field3".Escape(";", '\\'));
        }

        [Fact]
        public void SplitWithEscape_Test_1()
        {
            const string s = @"abc\;def;ghi\\jkl;mno\\\pqr";

            var res = s.SplitWithEscape(';', '\\').ToArray();

            Assert.Equal(3, res.Length);
            Assert.Equal(@"abc;def", res[0]);
            Assert.Equal(@"ghi\jkl", res[1]);
            Assert.Equal(@"mno\\pqr", res[2]);
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void SplitWithEscape_Test_2()
        {
            const string s = "US\\;Do\\\\llar;3;4.0000000";

            var res = s.SplitWithEscape(';', '\\').ToArray();

            Assert.Equal(@"US;Do\llar", res[0]);
        }

        [Fact]
        public void SplitWithEscape_Test_3()
        {
            const string s = @"User;U3334;Anders Busted-Janum\;ABJA\;25243705\;anders@busted-janum.dk\;1973-10-25\;2017-11-01";

            var res = s.SplitWithEscape(';', '\\').ToArray();

            Assert.Equal(3, res.Length);
            Assert.Equal("User", res[0]);
            Assert.Equal("U3334", res[1]);
            Assert.Equal(@"Anders Busted-Janum;ABJA;25243705;anders@busted-janum.dk;1973-10-25;2017-11-01", res[2]);
        }

        [Fact]
        public void ToProperName_1()
        {
            Assert.Equal("Albert Einstein", "AlBerT EinSTEIn".ToProperName());
        }

        [Fact]
        public void ToProperName_2()
        {
            Assert.Equal("Albert-Einstein", "alBerT-einSTEIn".ToProperName());
        }

        [Fact]
        public void ToProperName_3()
        {
            Assert.Equal(" Albert-Einstein", " alBerT-einSTEIn".ToProperName());
        }

        [Fact]
        public void ToProperName_4()
        {
            Assert.Equal("Albert  Einstein", "alBerT  einSTEIn".ToProperName());
        }

        [Fact]
        public void ToProperName_5()
        {
            Assert.Equal("Albert- Einstein", "alBerT- einSTEIn".ToProperName());
        }

        [Fact]
        public void ToProperName_6()
        {
            Assert.Equal("Albert -Einstein", "alBerT -einSTEIn".ToProperName());
        }

        [Fact]
        public void ToProperName_7()
        {
            Assert.Equal("Albert--Einstein", "alBerT--einSTEIn".ToProperName());
        }

        [Fact]
        public void ToProperName_8()
        {
            Assert.Equal("Albert-Einstein ", "alBerT-einSTEIn ".ToProperName());
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_1()
        {
            Assert.Equal("Øjvin-Børn", "øjvin-BØrn".ToProperName());
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_2()
        {
            Assert.Equal("Ånders Månd", "ånders Månd".ToProperName());
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_3()
        {
            Assert.Equal("Únders Månd", "únders MÅnd".ToProperName());
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_4()
        {
            Assert.Equal("Ånders Børn", "ånders BØrn".ToProperName(CultureInfo.GetCultureInfo("En-us")));
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_5()
        {
            Assert.Equal("Ånders Børn", "ånders BØrn".ToProperName(CultureInfo.GetCultureInfo("En-in")));
        }

        [Fact]
        public void Quotify_Empty()
        {
            Assert.Equal("", "".Quotify());
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void Quotify_OneWord_NotQuoted()
        {
            Assert.Equal("Iwasheretoday", "Iwasheretoday".Quotify());
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void Quotify_TwoWords_Quoted()
        {
            Assert.Equal("\"I washeretoday\"", "I washeretoday".Quotify());
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void Quotify_DifferentBlankCharacter_Quoted()
        {
            Assert.Equal("'Iwasheretoday'", "Iwasheretoday".Quotify("'", "o"));
        }

        [Fact]
        public void Quotify_AlreadyQuoted_SameString()
        {
            Assert.Equal("\"I was here 'today'\"", "\"I was here 'today'\"".Quotify());
        }

        [Fact]
        public void Quotify_OnlyQuoteAtEnd_QuotedWithDoubleAtEnd()
        {
            Assert.Equal("\"I was here 'today'\"\"", "I was here 'today'\"".Quotify());
        }

        [Fact]
        public void Quotify_OnlyQuoteAtStart_QuotedWithDoubleAtStart()
        {
            Assert.Equal("\"\"I was here 'today'\"", "\"I was here 'today'".Quotify());
        }

        [Fact]
        public void IndexOfNth_FoundInStartOfString_Zero()
        {
            Assert.Equal(0, "#".IndexOfNth("#", 1));
        }

        [Fact]
        public void IndexOfNth_SearchForMoreOccurrenceThanInString_MinusOne()
        {
            Assert.Equal(-1, "#".IndexOfNth("#", 2));
        }

        [Fact]
        public void IndexOfNth_SearchForSecondOccurrence_Found()
        {
            Assert.Equal(1, "##".IndexOfNth("#", 2));
        }

        [Fact]
        public void IndexOfNth_SearchForStringNotFound_MinusOne()
        {
            Assert.Equal(-1, "#".IndexOfNth("##", 1));
        }

        [Fact]
        public void IndexOfNth_Error()
        {
            Assert.Throws<NotSupportedException>(() => "#".IndexOfNth("#", 0));
        }

        [Fact]
        public void ToUpperFirstCharacter_MixCaseText()
        {
            Assert.Equal("Word. WORD. word", "word. WORD. word".ToUpperFirstCharacter());
        }

        [Fact]
        public void ToProperFirst_MultipleParagraph()
        {
            Assert.Equal("Word. word. word", "WORD. WORD. WORD".ToProperFirst());
        }

        [Fact]
        public void ToProperParagraph_UpdateCaseString()
        {
            Assert.Equal("Karin is testing", "KARIN IS TESTING".ToProperFirst());
        }

        [Fact]
        public void ToProperParagraph_MultiplePoints()
        {
            Assert.Equal("Word.. Word.. Word", "WORD.. WORD.. WORD".ToProperParagraph());
        }

        [Fact]
        public void ToProperParagraph_MultipleParagraph()
        {
            Assert.Equal("Word. Word. Word", "WORD. WORD. WORD".ToProperParagraph());
        }

        [Fact]
        public void ToProperParagraph_UpperCaseString()
        {
            Assert.Equal("Karin is testing", "KARIN IS TESTING".ToProperParagraph());
        }

        [Fact]
        public void ToProperParagraph_NordicCharacters()
        {
            Assert.Equal("Åå åå. Ææ ææ. Øø øø. Ää ää. Öö öö", "åå åå. ææ ææ. øø øø. ää ää. öö öö".ToProperParagraph());
        }

        [Fact]
        public void ToProperParagraph_EmptyString()
        {
            Assert.Equal("", "".ToProperParagraph());
        }

        [Fact]
        public void ToProper_UpperCaseString()
        {
            Assert.Equal("Karin Is Testing", "KARIN IS TESTING".ToProper());
        }

        [Fact]
        public void ToProper_NordicCharacters()
        {
            Assert.Equal("Åå Ææ Øø Ää Öö", "ÅÅ ÆÆ ØØ ÄÄ ÖÖ".ToProper());
        }
    }
}