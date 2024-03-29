// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
//
// OptionSetTest.cs
//
// Authors:
//  Jonathan Pryor <jpryor@novell.com>
//
// Copyright (C) 2008 Novell (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Zetbox.API.Utils.Tests
{

    class FooConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                CultureInfo culture, object value)
        {
            string v = value as string;
            if (v != null)
            {
                switch (v)
                {
                    case "A": return Foo.A;
                    case "B": return Foo.B;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }

    [TypeConverter(typeof(FooConverter))]
    class Foo
    {
        public static readonly Foo A = new Foo("A");
        public static readonly Foo B = new Foo("B");
        string s;
        Foo(string s) { this.s = s; }
        public override string ToString() { return s; }
    }

    [TestFixture]
    public class OptionSetTest
    {
        static IEnumerable<string> _(params string[] a)
        {
            return a;
        }

        [Test]
        public void BundledValues()
        {
            var defines = new List<string>();
            var libs = new List<string>();
            bool debug = false;
            var p = new OptionSet() {
                { "D|define=",  v => defines.Add (v) },
                { "L|library:", v => libs.Add (v) },
                { "Debug",      v => debug = v != null },
                { "E",          v => { /* ignore */ } },
            };
            p.Parse(_("-DNAME", "-D", "NAME2", "-Debug", "-L/foo", "-L", "/bar", "-EDNAME3"));
            Assert.AreEqual(defines.Count, 3);
            Assert.AreEqual(defines[0], "NAME");
            Assert.AreEqual(defines[1], "NAME2");
            Assert.AreEqual(defines[2], "NAME3");
            Assert.AreEqual(debug, true);
            Assert.AreEqual(libs.Count, 2);
            Assert.AreEqual(libs[0], "/foo");
            Assert.AreEqual(libs[1], null);

            Utils.AssertException(typeof(OptionException),
                    "Cannot bundle unregistered option '-V'.",
                    p, v => { v.Parse(_("-EVALUENOTSUP")); });
        }

        [Test]
        public void RequiredValues()
        {
            string a = null;
            int n = 0;
            OptionSet p = new OptionSet() {
                { "a=", v => a = v },
                { "n=", (int v) => n = v },
            };
            List<string> extra = p.Parse(_("a", "-a", "s", "-n=42", "n"));
            Assert.AreEqual(extra.Count, 2);
            Assert.AreEqual(extra[0], "a");
            Assert.AreEqual(extra[1], "n");
            Assert.AreEqual(a, "s");
            Assert.AreEqual(n, 42);

            extra = p.Parse(_("-a="));
            Assert.AreEqual(extra.Count, 0);
            Assert.AreEqual(a, "");
        }

        [Test]
        public void OptionalValues()
        {
            string a = null;
            int n = -1;
            Foo f = null;
            OptionSet p = new OptionSet() {
                { "a:", v => a = v },
                { "n:", (int v) => n = v },
                { "f:", (Foo v) => f = v },
            };
            p.Parse(_("-a=s"));
            Assert.AreEqual(a, "s");
            p.Parse(_("-a"));
            Assert.AreEqual(a, null);
            p.Parse(_("-a="));
            Assert.AreEqual(a, "");

            p.Parse(_("-f", "A"));
            Assert.AreEqual(f, null);
            p.Parse(_("-f"));
            Assert.AreEqual(f, null);
            p.Parse(_("-f=A"));
            Assert.AreEqual(f, Foo.A);
            f = null;
            p.Parse(_("-fA"));
            Assert.AreEqual(f, Foo.A);

            p.Parse(_("-n42"));
            Assert.AreEqual(n, 42);
            p.Parse(_("-n", "42"));
            Assert.AreEqual(n, 0);
            p.Parse(_("-n=42"));
            Assert.AreEqual(n, 42);
            p.Parse(_("-n"));
            Assert.AreEqual(n, 0);
        }

        [Test]
        public void BooleanValues()
        {
            bool a = false;
            OptionSet p = new OptionSet() {
                { "a", v => a = v != null },
            };
            p.Parse(_("-a"));
            Assert.AreEqual(a, true);
            p.Parse(_("-a+"));
            Assert.AreEqual(a, true);
            p.Parse(_("-a-"));
            Assert.AreEqual(a, false);
        }

        [Test]
        public void CombinationPlatter()
        {
            int a = -1, b = -1;
            string av = null, bv = null;
            Foo f = null;
            int help = 0;
            int verbose = 0;
            OptionSet p = new OptionSet() {
                { "a=", v => { a = 1; av = v; } },
                { "b", "desc", v => {b = 2; bv = v;} },
                { "f=", (Foo v) => f = v },
                { "v", v => { ++verbose; } },
                { "h|?|help", (v) => { switch (v) {
                    case "h": help |= 0x1; break;
                    case "?": help |= 0x2; break;
                    case "help": help |= 0x4; break;
                } } },
            };
            List<string> e = p.Parse(new string[]{"foo", "-v", "-a=42", "/b-",
                "-a", "64", "bar", "--f", "B", "/h", "-?", "--help", "-v"});

            Assert.AreEqual(e.Count, 2);
            Assert.AreEqual(e[0], "foo");
            Assert.AreEqual(e[1], "bar");
            Assert.AreEqual(a, 1);
            Assert.AreEqual(av, "64");
            Assert.AreEqual(b, 2);
            Assert.AreEqual(bv, null);
            Assert.AreEqual(verbose, 2);
            Assert.AreEqual(help, 0x7);
            Assert.AreEqual(f, Foo.B);
        }

        [Test]
        public void Exceptions()
        {
            string a = null;
            var p = new OptionSet() {
                { "a=", v => a = v },
                { "b",  v => { } },
                { "c",  v => { } },
                { "n=", (int v) => { } },
                { "f=", (Foo v) => { } },
            };
            // missing argument
            Utils.AssertException(typeof(OptionException),
                    "Missing required value for option '-a'.",
                    p, v => { v.Parse(_("-a")); });
            // another named option while expecting one -- follow Getopt::Long
            Utils.AssertException(null, null,
                    p, v => { v.Parse(_("-a", "-a")); });
            Assert.AreEqual(a, "-a");
            // no exception when an unregistered named option follows.
            Utils.AssertException(null, null,
                    p, v => { v.Parse(_("-a", "-b")); });
            Assert.AreEqual(a, "-b");
            Assert.That(() => p.Add(null), Throws.InstanceOf<ArgumentNullException>());

            // bad type
            Utils.AssertException(typeof(OptionException),
                    "Could not convert string `value' to type Int32 for option `-n'.",
                    p, v => { v.Parse(_("-n", "value")); });
            Utils.AssertException(typeof(OptionException),
                    "Could not convert string `invalid' to type Foo for option `--f'.",
                    p, v => { v.Parse(_("--f", "invalid")); });

            // try to bundle with an option requiring a value
            Utils.AssertException(typeof(OptionException),
                    "Cannot bundle unregistered option '-z'.",
                    p, v => { v.Parse(_("-cz", "extra")); });

            Assert.That(() => p.Add("foo", (Action<string>)null), Throws.InstanceOf<ArgumentNullException>());
            Assert.That(() => p.Add("foo", (k, val) => { return Task.CompletedTask; /* ignore */}), Throws.ArgumentException);
        }

        [Test]
        public void WriteOptionDescriptions()
        {
            var p = new OptionSet() {
                { "p|indicator-style=", "append / indicator to directories",    v => {} },
                { "color:",             "controls color info",                  v => {} },
                { "color2:",            "set {color}",                          v => {} },
                { "rk=",                "required key/value option",            (k, v) => {return Task.CompletedTask;} },
                { "rk2=",               "required {{foo}} {0:key}/{1:value} option",    (k, v) => {return Task.CompletedTask;} },
                { "ok:",                "optional key/value option",            (k, v) => {return Task.CompletedTask;} },
                { "long-desc",
                    "This has a really\nlong, multi-line description that also\ntests\n" +
                        "the-builtin-supercalifragilisticexpialidicious-break-on-hyphen.  " +
                        "Also, a list:\n" +
                        "  item 1\n" +
                        "  item 2",
                    v => {} },
                { "long-desc2",
                    "IWantThisDescriptionToBreakInsideAWordGeneratingAutoWordHyphenation.",
                    v => {} },
                { "h|?|help",           "show help text",                       v => {} },
                { "version",            "output version information and exit",  v => {} },
                { "<>", v => {} },
            };

            StringWriter expected = new StringWriter();
            expected.WriteLine("  -p, --indicator-style=VALUE");
            expected.WriteLine("                             append / indicator to directories");
            expected.WriteLine("      --color[=VALUE]        controls color info");
            expected.WriteLine("      --color2[=color]       set color");
            expected.WriteLine("      --rk=VALUE1:VALUE2     required key/value option");
            expected.WriteLine("      --rk2=key:value        required {foo} key/value option");
            expected.WriteLine("      --ok[=VALUE1:VALUE2]   optional key/value option");
            expected.WriteLine("      --long-desc            This has a really");
            expected.WriteLine("                               long, multi-line description that also");
            expected.WriteLine("                               tests");
            expected.WriteLine("                               the-builtin-supercalifragilisticexpialidicious-");
            expected.WriteLine("                               break-on-hyphen.  Also, a list:");
            expected.WriteLine("                                 item 1");
            expected.WriteLine("                                 item 2");
            expected.WriteLine("      --long-desc2           IWantThisDescriptionToBreakInsideAWordGenerating-");
            expected.WriteLine("                               AutoWordHyphenation.");
            expected.WriteLine("  -h, -?, --help             show help text");
            expected.WriteLine("      --version              output version information and exit");

            StringWriter actual = new StringWriter();
            p.WriteOptionDescriptions(actual);

            Assert.AreEqual(actual.ToString(), expected.ToString());
        }

        [Test]
        public void OptionBundling()
        {
            string a, b, c, f;
            a = b = c = f = null;
            var p = new OptionSet() {
                { "a", v => a = "a" },
                { "b", v => b = "b" },
                { "c", v => c = "c" },
                { "f=", v => f = v },
            };
            List<string> extra = p.Parse(_("-abcf", "foo", "bar"));
            Assert.AreEqual(extra.Count, 1);
            Assert.AreEqual(extra[0], "bar");
            Assert.AreEqual(a, "a");
            Assert.AreEqual(b, "b");
            Assert.AreEqual(c, "c");
            Assert.AreEqual(f, "foo");
        }

        [Test]
        public void HaltProcessing()
        {
            var p = new OptionSet() {
                { "a", v => {} },
                { "b", v => {} },
            };
            List<string> e = p.Parse(_("-a", "-b", "--", "-a", "-b"));
            Assert.AreEqual(e.Count, 2);
            Assert.AreEqual(e[0], "-a");
            Assert.AreEqual(e[1], "-b");
        }

        [Test]
        public void KeyValueOptions()
        {
            var a = new Dictionary<string, string>();
            var b = new Dictionary<int, char>();
            var p = new OptionSet() {
                { "a=", (k,v) => { a.Add (k, v); return Task.CompletedTask; } },
                { "b=", (int k, char v) => { b.Add (k, v); return Task.CompletedTask; } },
                { "c:", (k, v) => {if (k != null) a.Add (k, v);return Task.CompletedTask;} },
                { "d={=>}{-->}", (k, v) => { a.Add (k, v);return Task.CompletedTask; } },
                { "e={}", (k, v) => {a.Add (k, v); return Task.CompletedTask; } },
                { "f=+/", (k, v) => {a.Add (k, v); return Task.CompletedTask; } },
            };
            p.Parse(_("-a", "A", "B", "-a", "C", "D", "-a=E=F", "-a:G:H", "-aI=J", "-b", "1", "a", "-b", "2", "b"));
            AssertDictionary(a,
                    "A", "B",
                    "C", "D",
                    "E", "F",
                    "G", "H",
                    "I", "J");
            AssertDictionary(b,
                    "1", "a",
                    "2", "b");

            a.Clear();
            p.Parse(_("-c"));
            Assert.AreEqual(a.Count, 0);
            p.Parse(_("-c", "a"));
            Assert.AreEqual(a.Count, 0);
            p.Parse(_("-ca"));
            AssertDictionary(a, "a", null);
            a.Clear();
            p.Parse(_("-ca=b"));
            AssertDictionary(a, "a", "b");

            a.Clear();
            p.Parse(_("-dA=>B", "-d", "C-->D", "-d:E", "F", "-d", "G", "H", "-dJ-->K"));
            AssertDictionary(a,
                    "A", "B",
                    "C", "D",
                    "E", "F",
                    "G", "H",
                    "J", "K");

            a.Clear();
            p.Parse(_("-eA=B", "-eC=D", "-eE", "F", "-e:G", "H"));
            AssertDictionary(a,
                    "A=B", "-eC=D",
                    "E", "F",
                    "G", "H");

            a.Clear();
            p.Parse(_("-f1/2", "-f=3/4", "-f:5+6", "-f7", "8", "-f9=10", "-f11=12"));
            AssertDictionary(a,
                    "1", "2",
                    "3", "4",
                    "5", "6",
                    "7", "8",
                    "9=10", "-f11=12");
        }

        static void AssertDictionary<TKey, TValue>(Dictionary<TKey, TValue> dict, params string[] set)
        {
            TypeConverter k = TypeDescriptor.GetConverter(typeof(TKey));
            TypeConverter v = TypeDescriptor.GetConverter(typeof(TValue));

            Assert.AreEqual(dict.Count, set.Length / 2);
            for (int i = 0; i < set.Length; i += 2)
            {
                TKey key = (TKey)k.ConvertFromString(set[i]);
                Assert.AreEqual(dict.ContainsKey(key), true);
                if (set[i + 1] == null)
                    Assert.AreEqual(dict[key], default(TValue));
                else
                    Assert.AreEqual(dict[key], (TValue)v.ConvertFromString(set[i + 1]));
            }
        }

        class CustomOption : Option
        {
            Action<OptionValueCollection> action;

            public CustomOption(string p, string d, int c, Action<OptionValueCollection> a)
                : base(p, d, c)
            {
                this.action = a;
            }

            protected override void OnParseComplete(OptionContext c)
            {
                action(c.OptionValues);
            }
        }

        [Test]
        public void CustomKeyValue()
        {
            var a = new Dictionary<string, string>();
            var b = new Dictionary<string, string[]>();
            var p = new OptionSet() {
                new CustomOption ("a==:", null, 2, v => a.Add (v [0], v [1])),
                new CustomOption ("b==:", null, 3, v => b.Add (v [0], new string[]{v [1], v [2]})),
            };
            p.Parse(_("-a=b=c", "-a=d", "e", "-a:f=g", "-a:h:i", "-a", "j=k", "-a", "l:m"));
            Assert.AreEqual(a.Count, 6);
            Assert.AreEqual(a["b"], "c");
            Assert.AreEqual(a["d"], "e");
            Assert.AreEqual(a["f"], "g");
            Assert.AreEqual(a["h"], "i");
            Assert.AreEqual(a["j"], "k");
            Assert.AreEqual(a["l"], "m");

            Utils.AssertException(typeof(OptionException),
                    "Missing required value for option '-a'.",
                    p, v => { v.Parse(_("-a=b")); });

            p.Parse(_("-b", "a", "b", "c", "-b:d:e:f", "-b=g=h:i", "-b:j=k:l"));
            Assert.AreEqual(b.Count, 4);
            Assert.AreEqual(b["a"][0], "b");
            Assert.AreEqual(b["a"][1], "c");
            Assert.AreEqual(b["d"][0], "e");
            Assert.AreEqual(b["d"][1], "f");
            Assert.AreEqual(b["g"][0], "h");
            Assert.AreEqual(b["g"][1], "i");
            Assert.AreEqual(b["j"][0], "k");
            Assert.AreEqual(b["j"][1], "l");
        }

        [Test]
        public void Localization()
        {
            var p = new OptionSet(f => "hello!") {
                { "n=", (int v) => { } },
            };
            Utils.AssertException(typeof(OptionException), "hello!",
                    p, v => { v.Parse(_("-n=value")); });

            StringWriter expected = new StringWriter();
            expected.WriteLine("  -nhello!                   hello!");

            StringWriter actual = new StringWriter();
            p.WriteOptionDescriptions(actual);

            Assert.AreEqual(actual.ToString(), expected.ToString());
        }

        class CiOptionSet : OptionSet
        {
            protected override void InsertItem(int index, Option item)
            {
                if (item.Prototype.ToLower() != item.Prototype)
                    throw new ArgumentException("prototypes must be null!");
                base.InsertItem(index, item);
            }

            protected override bool Parse(string option, OptionContext c)
            {
                if (c.Option != null)
                    return base.Parse(option, c);
                string f, n, s, v;
                if (!GetOptionParts(option, out f, out n, out s, out v))
                {
                    return base.Parse(option, c);
                }
                return base.Parse(f + n.ToLower() + (v != null && s != null ? s + v : ""), c);
            }

            public void CheckOptionParts(string option, bool er, string ef, string en, string es, string ev)
            {
                string f, n, s, v;
                bool r = GetOptionParts(option, out f, out n, out s, out v);
                Assert.AreEqual(r, er);
                Assert.AreEqual(f, ef);
                Assert.AreEqual(n, en);
                Assert.AreEqual(s, es);
                Assert.AreEqual(v, ev);
            }
        }

        [Test]
        public void DerivedType()
        {
            bool help = false;
            var p = new CiOptionSet() {
                { "h|help", v => help = v != null },
            };
            p.Parse(_("-H"));
            Assert.AreEqual(help, true);
            help = false;
            p.Parse(_("-HELP"));
            Assert.AreEqual(help, true);

            Assert.AreEqual(p["h"], p[0]);
            Assert.AreEqual(p["help"], p[0]);
            Assert.Throws(Is.AssignableTo(typeof(KeyNotFoundException)), () => { var tmp = p["invalid"]; Console.Write(tmp); });

            Utils.AssertException(typeof(ArgumentException), "prototypes must be null!",
                    p, v => { v.Add("N|NUM=", (int n) => { }); });
            Assert.That(() => { var ignore = p[null]; Console.Write(ignore); }, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void OptionParts()
        {
            var p = new CiOptionSet();
            p.CheckOptionParts("A", false, null, null, null, null);
            p.CheckOptionParts("A=B", false, null, null, null, null);
            p.CheckOptionParts("-A=B", true, "-", "A", "=", "B");
            p.CheckOptionParts("-A:B", true, "-", "A", ":", "B");
            p.CheckOptionParts("--A=B", true, "--", "A", "=", "B");
            p.CheckOptionParts("--A:B", true, "--", "A", ":", "B");
            p.CheckOptionParts("/A=B", true, "/", "A", "=", "B");
            p.CheckOptionParts("/A:B", true, "/", "A", ":", "B");
            p.CheckOptionParts("-A=B=C", true, "-", "A", "=", "B=C");
            p.CheckOptionParts("-A:B=C", true, "-", "A", ":", "B=C");
            p.CheckOptionParts("-A:B:C", true, "-", "A", ":", "B:C");
            p.CheckOptionParts("--A=B=C", true, "--", "A", "=", "B=C");
            p.CheckOptionParts("--A:B=C", true, "--", "A", ":", "B=C");
            p.CheckOptionParts("--A:B:C", true, "--", "A", ":", "B:C");
            p.CheckOptionParts("/A=B=C", true, "/", "A", "=", "B=C");
            p.CheckOptionParts("/A:B=C", true, "/", "A", ":", "B=C");
            p.CheckOptionParts("/A:B:C", true, "/", "A", ":", "B:C");
            p.CheckOptionParts("-AB=C", true, "-", "AB", "=", "C");
            p.CheckOptionParts("-AB:C", true, "-", "AB", ":", "C");
        }

        class ContextCheckerOption : Option
        {
            string eName, eValue;
            int index;

            public ContextCheckerOption(string p, string d, string eName, string eValue, int index)
                : base(p, d)
            {
                this.eName = eName;
                this.eValue = eValue;
                this.index = index;
            }

            protected override void OnParseComplete(OptionContext c)
            {
                Assert.AreEqual(c.OptionValues.Count, 1);
                Assert.AreEqual(c.OptionValues[0], eValue);
                Assert.AreEqual(c.OptionName, eName);
                Assert.AreEqual(c.OptionIndex, index);
                Assert.AreEqual(c.Option, this);
                Assert.AreEqual(c.Option.Description, base.Description);
            }
        }

        [Test]
        public void OptionContext()
        {
            var p = new OptionSet() {
                new ContextCheckerOption ("a=", "a desc", "/a",   "a-val", 1),
                new ContextCheckerOption ("b",  "b desc", "--b+", "--b+",  2),
                new ContextCheckerOption ("c=", "c desc", "--c",  "C",     3),
                new ContextCheckerOption ("d",  "d desc", "/d-",  null,    4),
            };
            Assert.AreEqual(p.Count, 4);
            p.Parse(_("/a", "a-val", "--b+", "--c=C", "/d-"));
        }

        [Test]
        public void DefaultHandler()
        {
            var extra = new List<string>();
            var p = new OptionSet() {
                { "<>", v => extra.Add (v) },
            };
            var e = p.Parse(_("-a", "b", "--c=D", "E"));
            Assert.AreEqual(e.Count, 0);
            Assert.AreEqual(extra.Count, 4);
            Assert.AreEqual(extra[0], "-a");
            Assert.AreEqual(extra[1], "b");
            Assert.AreEqual(extra[2], "--c=D");
            Assert.AreEqual(extra[3], "E");
        }

        [Test]
        public void MixedDefaultHandler()
        {
            var tests = new List<string>();
            var p = new OptionSet() {
                { "t|<>=", v => tests.Add (v) },
            };
            var e = p.Parse(_("-tA", "-t:B", "-t=C", "D", "--E=F"));
            Assert.AreEqual(e.Count, 0);
            Assert.AreEqual(tests.Count, 5);
            Assert.AreEqual(tests[0], "A");
            Assert.AreEqual(tests[1], "B");
            Assert.AreEqual(tests[2], "C");
            Assert.AreEqual(tests[3], "D");
            Assert.AreEqual(tests[4], "--E=F");
        }

        [Test]
        public void DefaultHandlerRuns()
        {
            var formats = new Dictionary<string, List<string>>();
            string format = "foo";
            var p = new OptionSet() {
                { "f|format=", v => format = v },
                { "<>",
                    v => {
                        List<string> f;
                        if (!formats.TryGetValue (format, out f)) {
                            f = new List<string> ();
                            formats.Add (format, f);
                        }
                        f.Add (v);
                } },
            };
            var e = p.Parse(_("a", "b", "-fbar", "c", "d", "--format=baz", "e", "f"));
            Assert.AreEqual(e.Count, 0);
            Assert.AreEqual(formats.Count, 3);
            Assert.AreEqual(formats["foo"].Count, 2);
            Assert.AreEqual(formats["foo"][0], "a");
            Assert.AreEqual(formats["foo"][1], "b");
            Assert.AreEqual(formats["bar"].Count, 2);
            Assert.AreEqual(formats["bar"][0], "c");
            Assert.AreEqual(formats["bar"][1], "d");
            Assert.AreEqual(formats["baz"].Count, 2);
            Assert.AreEqual(formats["baz"][0], "e");
            Assert.AreEqual(formats["baz"][1], "f");
        }
    }
}

