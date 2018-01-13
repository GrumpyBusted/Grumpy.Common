using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Grumpy.Common.Extensions;
using Grumpy.Common.UnitTests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        private const string SomeText = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...";

        [TestMethod]
        public void DeserializeXml_Work()
        {
            const string str = "<TestPerson><Age>8</Age><Name>Sara</Name></TestPerson>";

            var result = str.DeserializeFromXml<TestPerson>();

            result.Age.Should().Be(8);
            result.Name.Should().Be("Sara");
        }

        [TestMethod]
        public void NullOrEmpty_Null_True()
        {
            ((string)null).NullOrEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void NullOrEmpty_Empty_True()
        {
            "".NullOrEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void NullOrEmpty_Space_False()
        {
            " ".NullOrEmpty().Should().BeFalse();
        }

        [TestMethod]
        public void NullOrEmpty_NonEmpty_False()
        {
            "Hallo".NullOrEmpty().Should().BeFalse();
        }

        [TestMethod]
        public void NullOrWhiteSpace_Null_True()
        {
            ((string)null).NullOrWhiteSpace().Should().BeTrue();
        }

        [TestMethod]
        public void NullOrWhiteSpace_Empty_True()
        {
            "".NullOrWhiteSpace().Should().BeTrue();
        }

        [TestMethod]
        public void NullOrWhiteSpace_Space_True()
        {
            " ".NullOrWhiteSpace().Should().BeTrue();
        }

        [TestMethod]
        public void NullOrWhiteSpace_NonEmpty_False()
        {
            "Hallo".NullOrWhiteSpace().Should().BeFalse();
        }

        [TestMethod]
        public void ContainsAnyOf_CharactersNotFound_False()
        {
            char[] a = { 'O', ':', 'D' };

            Assert.IsFalse("Hallo there".ContainsAnyOf(a));
        }

        [TestMethod]
        public void ContainsAnyOf_OneOfCharactersFound_True()
        {
            char[] a = { 'O', ':', 'D' };

            Assert.IsTrue("HallO there".ContainsAnyOf(a));
        }

        [TestMethod]
        public void ContainsAnyOf_EmptyString_False()
        {
            char[] a = { 'A', ':', 'D' };

            Assert.IsFalse("".ContainsAnyOf(a));
        }

        [TestMethod]
        public void ContainsAnyOf_EmptyList_False()
        {
            char[] b = { };

            Assert.IsFalse("Hallo".ContainsAnyOf(b));
        }

        [TestMethod]
        public void ContainsAnyOf_EmptyListInEmptyString_False()
        {
            char[] b = { };

            Assert.IsFalse("".ContainsAnyOf(b));
        }

        [TestMethod]
        public void StartWith_StringHasPrefix_SameString()
        {
            Assert.AreEqual("Hi There".StartWith("H"), "Hi There");
        }

        [TestMethod]
        public void StartWith_StringDoesNotHavePrefix_DifferentString()
        {
            Assert.AreEqual("Hi There".StartWith("g"), "gHi There");
        }

        [TestMethod]
        public void EndWith_StringHasSuffix_SameString()
        {
            Assert.AreEqual("Hi There".EndWith("e"), "Hi There");
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void EndWith_StringDoNotHaveSuffix_DifferentString()
        {
            Assert.AreEqual("Albert".EndWith("s"), "Alberts");
        }

        [TestMethod]
        public void StartsWith_True()
        {
            Assert.IsTrue("Hi There".StartsWith("H"));
        }

        [TestMethod]
        public void StartsWith_False()
        {
            Assert.IsFalse("Hi There".StartsWith("g"));
        }

        [TestMethod]
        public void Less_LargerString_False()
        {
            Assert.IsFalse("Hi There".Less("Gi There"));
        }

        [TestMethod]
        public void Less_SmallerString_True()
        {
            Assert.IsTrue("Hi There".Less("Ji There"));
        }

        [TestMethod]
        public void Less_EmptyString_False()
        {
            Assert.IsFalse("Hi There".Less(""));
        }

        [TestMethod]
        public void Less_StringEqual_False()
        {
            Assert.IsFalse("Hi There".Less("Hi There"));
        }

        [TestMethod]
        public void LessOrEqual_LargerString_False()
        {
            Assert.IsFalse("Hi There".LessOrEqual("Gi There"));
        }

        [TestMethod]
        public void LessOrEqual_SmallerString_True()
        {
            Assert.IsTrue("Hi There".LessOrEqual("Ji There"));
        }

        [TestMethod]
        public void LessOrEqual_EmptyString_False()
        {
            Assert.IsFalse("Hi There".LessOrEqual(""));
        }

        [TestMethod]
        public void LessOrEqual_StringEqual_True()
        {
            Assert.IsTrue("Hi There".LessOrEqual("Hi There"));
        }

        [TestMethod]
        public void Greater_LargerString_True()
        {
            Assert.IsTrue("Hi There".Greater("Gi There"));
        }

        [TestMethod]
        public void Greater_SmallerString_False()
        {
            Assert.IsFalse("Hi There".Greater("Ji There"));
        }

        [TestMethod]
        public void Greater_EmptyString_True()
        {
            Assert.IsTrue("Hi There".Greater(""));
        }

        [TestMethod]
        public void Greater_SameString_False()
        {
            Assert.IsFalse("Hi There".Greater("Hi There"));
        }

        [TestMethod]
        public void GreaterOrEqual_LargerString_True()
        {
            Assert.IsTrue("Hi There".GreaterOrEqual("Gi There"));
        }

        [TestMethod]
        public void GreaterOrEqual_SmallerString_False()
        {
            Assert.IsFalse("Hi There".GreaterOrEqual("Ji There"));
        }

        [TestMethod]
        public void GreaterOrEqual_EmptyString_True()
        {
            Assert.IsTrue("Hi There".GreaterOrEqual(""));
        }

        [TestMethod]
        public void GreaterOrEqual_SameString_True()
        {
            Assert.IsTrue("Hi There".GreaterOrEqual("Hi There"));
        }

        [TestMethod]
        public void ContainsAnyOf_OneCharInString_True()
        {
            Assert.IsTrue("Hi There".ContainsAnyOf('a', 'b', 'e', 'f'));
        }

        [TestMethod]
        public void ContainsAnyOf_MoreCharsInString_True()
        {
            Assert.IsTrue("Hi There".ContainsAnyOf('a', 'b', 'e', 'f', 'T'));
        }

        [TestMethod]
        public void ContainsAnyOf_NoCharsInString_False()
        {
            Assert.IsFalse("Hi There".ContainsAnyOf('a', 'b', 'E', 'f', 't'));
        }

        [TestMethod]
        public void Left_NullString()
        {
            Assert.IsNull(((string)null).Left(10));
        }

        [TestMethod]
        public void Left_EmptyString()
        {
            Assert.AreEqual(string.Empty, string.Empty.Left(10));
        }

        [TestMethod]
        public void Left_LongString()
        {
            var ss = SomeText.Left(50);

            Assert.AreEqual(50, ss.Length);
            Assert.AreEqual(SomeText.Substring(0, 50), ss);
        }

        [TestMethod]
        public void Left_StringToShort()
        {
            var s = SomeText.Left(120);

            Assert.AreEqual(94, s.Length);
            Assert.AreEqual(SomeText, s);
        }

        [TestMethod]
        public void Left_NegativePart()
        {
            Assert.AreEqual(SomeText.Substring(0, SomeText.Length - 1), SomeText.Left(-1));
        }

        [TestMethod]
        public void Left_NegativeAll()
        {
            Assert.AreEqual("", SomeText.Left(-120));
        }

        [TestMethod]
        public void Right_NullString()
        {
            Assert.IsNull(((string)null).Right(10));
        }

        [TestMethod]
        public void Right_EmptyString()
        {
            Assert.AreEqual(string.Empty, string.Empty.Right(10));
        }

        [TestMethod]
        public void Right_LongString()
        {
            var s = SomeText.Right(50);

            Assert.AreEqual(50, s.Length);
            Assert.AreEqual(SomeText.Substring(44, 50), s);
        }

        [TestMethod]
        public void Right_StringToShort()
        {
            var s = SomeText.Right(120);

            Assert.AreEqual(94, s.Length);
            Assert.AreEqual(SomeText, s);
        }

        [TestMethod]
        public void Right_NegativePart()
        {
            Assert.AreEqual(SomeText.Substring(1), SomeText.Right(-1));
        }

        [TestMethod]
        public void Right_NegativeAll()
        {
            Assert.AreEqual("", SomeText.Right(-120));
        }

        [TestMethod]
        public void ToStream_Text_Match()
        {
            var act = SomeText.ToStream();

            using (var reader = new StreamReader(act, Encoding.UTF8))
                Assert.AreEqual(SomeText, reader.ReadToEnd());
        }

        [TestMethod]
        public void ToStream_Null_Null()
        {
            var act = ((string)null).ToStream();

            Assert.IsNull(act);
        }

        [TestMethod]
        public void StartsWith_Match_True()
        {
            Assert.IsTrue("Hi".StartsWith("H"));
        }

        [TestMethod]
        public void StartsWith_NonMatch_False()
        {
            Assert.IsFalse("Hi".StartsWith("h"));
        }

        [TestMethod]
        public void StartsWith_Empty_False()
        {
            Assert.IsFalse("".StartsWith("h"));
        }

        [TestMethod]
        public void StartsWith_Null_False()
        {
            Assert.IsFalse(StringExtensions.StartsWith(null, "h"));
        }

        [TestMethod]
        public void StartsWith_Any_True()
        {
            Assert.IsTrue("Hi".StartsWith("h", "H"));
        }

        [TestMethod]
        public void StartsWith_None_False()
        {
            Assert.IsFalse("Hi".StartsWith("d", "D"));
        }

        [TestMethod]
        public void ContainsAllOf_Char_True()
        {
            Assert.IsTrue("Hi there you lovely person".ContainsAllOf('o', 'p', 'H'));
        }

        [TestMethod]
        public void ContainsAllOf_Char_False()
        {
            Assert.IsFalse("Hi there you lovely person".ContainsAllOf('o', 'p', 'G'));
        }

        [TestMethod]
        public void ContainsAnyOf_Char_True()
        {
            Assert.IsTrue("Hi there you lovely person".ContainsAnyOf('O', 'p', 'G'));
        }

        [TestMethod]
        public void ContainsAnyOf_Char_False()
        {
            Assert.IsFalse("Hi there you lovely person".ContainsAnyOf('O', 'P', 'g'));
        }

        [TestMethod]
        public void ContainsAllOf_String_True()
        {
            Assert.IsTrue("Hi there you lovely person".ContainsAllOf("ove", "per", "Hi "));
        }

        [TestMethod]
        public void ContainsAllOf_String_False()
        {
            Assert.IsFalse("Hi there you lovely person".ContainsAllOf("ove", "per", "G"));
        }

        [TestMethod]
        public void ContainsAnyOf_String_True()
        {
            Assert.IsTrue("Hi there you lovely person".ContainsAnyOf("O", "per", "G"));
        }

        [TestMethod]
        public void ContainsAnyOf_String_False()
        {
            Assert.IsFalse("Hi there you lovely person".ContainsAnyOf("op", "yi", "g"));
        }

        [TestMethod]
        public void To_Enum_Match()
        {
            Assert.AreEqual(DayOfWeek.Monday, "Monday".To<DayOfWeek>());
        }

        [TestMethod]
        public void ToMilliseconds_Match()
        {
            Assert.AreEqual(new TimeSpan(0, 0, 0, 1).TotalMilliseconds, "1s".ToMilliseconds());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void To_Enum_Comma_Match()
        {
            Assert.AreEqual(DayOfWeek.Monday, "Monday, Tuesday".To<DayOfWeek>());
        }

        [TestMethod]
        public void To_Int_Match()
        {
            Assert.AreEqual(314, "314".To<int>());
        }

        [TestMethod]
        public void To_NullableInt_Match()
        {
            Assert.AreEqual(314, "314".To<int?>());
        }

        [TestMethod]
        public void To_Double_Match()
        {
            Assert.AreEqual(314.54, "314.54".To<double>());
        }

        [TestMethod]
        public void To_String_Match()
        {
            Assert.AreEqual("314.54", "314.54".To<string>());
        }

        [TestMethod]
        public void AsRegEx_True()
        {
            Assert.IsTrue("^\\d{3}-\\d{3}-\\d{4}$".AsRegex().IsMatch("123-456-7890"));
        }

        [TestMethod]
        public void AsRegEx_False()
        {
            Assert.IsFalse("^\\d{3}-\\d{3}-\\d{4}$".AsRegex().IsMatch("123-456-789D"));
        }

        [TestMethod]
        public void MatchesRegEx_True()
        {
            Assert.IsTrue("123-456-7890".MatchesRegex("^\\d{3}-\\d{3}-\\d{4}$"));
        }

        [TestMethod]
        public void MatchesRegex_False()
        {
            Assert.IsFalse("123-456-7D90".MatchesRegex("^\\d{3}-\\d{3}-\\d{4}$"));
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ReplaceAnyOf_Test()
        {
            Assert.AreEqual("abxxxcd#efxxxgh&ij(kl)m", "ab!cd#ef%gh&ij(kl)m".ReplaceAnyOf("!%", "xxx"));
        }

        [TestMethod]
        public void Escape_Test()
        {
            Assert.AreEqual(@"field1\;field2\\field3", @"field1;field2\field3".Escape(";", '\\'));
        }

        [TestMethod]
        public void SplitWithEscape_Test_1()
        {
            const string s = @"abc\;def;ghi\\jkl;mno\\\pqr";

            var res = s.SplitWithEscape(';', '\\').ToArray();

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(@"abc;def", res[0]);
            Assert.AreEqual(@"ghi\jkl", res[1]);
            Assert.AreEqual(@"mno\\pqr", res[2]);
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void SplitWithEscape_Test_2()
        {
            const string s = "US\\;Do\\\\llar;3;4.0000000";

            var res = s.SplitWithEscape(';', '\\').ToArray();

            Assert.AreEqual(@"US;Do\llar", res[0]);
        }

        [TestMethod]
        public void SplitWithEscape_Test_3()
        {
            const string s = @"User;U3334;Anders Busted-Janum\;ABJA\;25243705\;anders@busted-janum.dk\;1973-10-25\;2017-11-01";

            var res = s.SplitWithEscape(';', '\\').ToArray();

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("User", res[0]);
            Assert.AreEqual("U3334", res[1]);
            Assert.AreEqual(@"Anders Busted-Janum;ABJA;25243705;anders@busted-janum.dk;1973-10-25;2017-11-01", res[2]);
        }

        [TestMethod]
        public void ToProperName_1()
        {
            Assert.AreEqual("Albert Einstein", "AlBerT EinSTEIn".ToProperName());
        }

        [TestMethod]
        public void ToProperName_2()
        {
            Assert.AreEqual("Albert-Einstein", "alBerT-einSTEIn".ToProperName());
        }

        [TestMethod]
        public void ToProperName_3()
        {
            Assert.AreEqual(" Albert-Einstein", " alBerT-einSTEIn".ToProperName());
        }

        [TestMethod]
        public void ToProperName_4()
        {
            Assert.AreEqual("Albert  Einstein", "alBerT  einSTEIn".ToProperName());
        }

        [TestMethod]
        public void ToProperName_5()
        {
            Assert.AreEqual("Albert- Einstein", "alBerT- einSTEIn".ToProperName());
        }

        [TestMethod]
        public void ToProperName_6()
        {
            Assert.AreEqual("Albert -Einstein", "alBerT -einSTEIn".ToProperName());
        }

        [TestMethod]
        public void ToProperName_7()
        {
            Assert.AreEqual("Albert--Einstein", "alBerT--einSTEIn".ToProperName());
        }

        [TestMethod]
        public void ToProperName_8()
        {
            Assert.AreEqual("Albert-Einstein ", "alBerT-einSTEIn ".ToProperName());
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_1()
        {
            Assert.AreEqual("Øjvin-Børn", "øjvin-BØrn".ToProperName());
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_2()
        {
            Assert.AreEqual("Ånders Månd", "ånders Månd".ToProperName());
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_3()
        {
            Assert.AreEqual("Únders Månd", "únders MÅnd".ToProperName());
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_4()
        {
            Assert.AreEqual("Ånders Børn", "ånders BØrn".ToProperName(CultureInfo.GetCultureInfo("En-us")));
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToProperName_Danish_5()
        {
            Assert.AreEqual("Ånders Børn", "ånders BØrn".ToProperName(CultureInfo.GetCultureInfo("En-in")));
        }

        [TestMethod]
        public void Quotify_Empty()
        {
            Assert.AreEqual("", "".Quotify());
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void Quotify_OneWord_NotQuoted()
        {
            Assert.AreEqual("Iwasheretoday", "Iwasheretoday".Quotify());
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void Quotify_TwoWords_Quoted()
        {
            Assert.AreEqual("\"I washeretoday\"", "I washeretoday".Quotify());
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void Quotify_DifferentBlankCharacter_Quoted()
        {
            Assert.AreEqual("'Iwasheretoday'", "Iwasheretoday".Quotify("'", "o"));
        }

        [TestMethod]
        public void Quotify_AlreadyQuoted_SameString()
        {
            Assert.AreEqual("\"I was here 'today'\"", "\"I was here 'today'\"".Quotify());
        }

        [TestMethod]
        public void Quotify_OnlyQuoteAtEnd_QuotedWithDoubleAtEnd()
        {
            Assert.AreEqual("\"I was here 'today'\"\"", "I was here 'today'\"".Quotify());
        }

        [TestMethod]
        public void Quotify_OnlyQuoteAtStart_QuotedWithDoubleAtStart()
        {
            Assert.AreEqual("\"\"I was here 'today'\"", "\"I was here 'today'".Quotify());
        }

        [TestMethod]
        public void IndexOfNth_FoundInStartOfString_Zero()
        {
            Assert.AreEqual(0, "#".IndexOfNth("#", 1));
        }

        [TestMethod]
        public void IndexOfNth_SearchForMoreOccurrenceThanInString_MinusOne()
        {
            Assert.AreEqual(-1, "#".IndexOfNth("#", 2));
        }

        [TestMethod]
        public void IndexOfNth_SearchForSecondOccurrence_Found()
        {
            Assert.AreEqual(1, "##".IndexOfNth("#", 2));
        }

        [TestMethod]
        public void IndexOfNth_SearchForStringNotFound_MinusOne()
        {
            Assert.AreEqual(-1, "#".IndexOfNth("##", 1));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void IndexOfNth_Error()
        {
            Assert.AreEqual(0, "#".IndexOfNth("#", 0));
        }

        [TestMethod]
        public void ToUpperFirstCharacter_MixCaseText()
        {
            Assert.AreEqual("Word. WORD. word", "word. WORD. word".ToUpperFirstCharacter());
        }

        [TestMethod]
        public void ToProperFirst_MultipleParagraph()
        {
            Assert.AreEqual("Word. word. word", "WORD. WORD. WORD".ToProperFirst());
        }

        [TestMethod]
        public void ToProperParagraph_UpdateCaseString()
        {
            Assert.AreEqual("Karin is testing", "KARIN IS TESTING".ToProperFirst());
        }

        [TestMethod]
        public void ToProperParagraph_MultiplePoints()
        {
            Assert.AreEqual("Word.. Word.. Word", "WORD.. WORD.. WORD".ToProperParagraph());
        }

        [TestMethod]
        public void ToProperParagraph_MultipleParagraph()
        {
            Assert.AreEqual("Word. Word. Word", "WORD. WORD. WORD".ToProperParagraph());
        }

        [TestMethod]
        public void ToProperParagraph_UpperCaseString()
        {
            Assert.AreEqual("Karin is testing", "KARIN IS TESTING".ToProperParagraph());
        }

        [TestMethod]
        public void ToProperParagraph_NordicCharacters()
        {
            Assert.AreEqual("Åå åå. Ææ ææ. Øø øø. Ää ää. Öö öö", "åå åå. ææ ææ. øø øø. ää ää. öö öö".ToProperParagraph());
        }

        [TestMethod]
        public void ToProperParagraph_EmptyString()
        {
            Assert.AreEqual("", "".ToProperParagraph());
        }

        [TestMethod]
        public void ToProper_UpperCaseString()
        {
            Assert.AreEqual("Karin Is Testing", "KARIN IS TESTING".ToProper());
        }

        [TestMethod]
        public void ToProper_NordicCharacters()
        {
            Assert.AreEqual("Åå Ææ Øø Ää Öö", "ÅÅ ÆÆ ØØ ÄÄ ÖÖ".ToProper());
        }
    }
}