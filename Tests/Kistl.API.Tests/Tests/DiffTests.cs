
namespace Kistl.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Utils;
    using NUnit.Framework;

    public class DiffTests
    {
        // copied from Kistl.API/Utils/Diff.cs
        // see license there
        [Test]
        public void Test()
        {
            StringBuilder ret = new StringBuilder();
            string a, b;

            // test all changes
            a = "a,b,c,d,e,f,g,h,i,j,k,l".Replace(',', '\n');
            b = "0,1,2,3,4,5,6,7,8,9".Replace(',', '\n');
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo("12.10.0.0*"),
                "all-changes test failed.");

            // test all same
            a = "a,b,c,d,e,f,g,h,i,j,k,l".Replace(',', '\n');
            b = a;
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo(""),
                "all-same test failed.");

            // test snake
            a = "a,b,c,d,e,f".Replace(',', '\n');
            b = "b,c,d,e,f,x".Replace(',', '\n');
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo("1.0.0.0*0.1.6.5*"),
                "snake test failed.");

            // 2002.09.20 - repro
            a = "c1,a,c2,b,c,d,e,g,h,i,j,c3,k,l".Replace(',', '\n');
            b = "C1,a,C2,b,c,d,e,I1,e,g,h,i,j,C3,k,I2,l".Replace(',', '\n');
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo("1.1.0.0*1.1.2.2*0.2.7.7*1.1.11.13*0.1.13.15*"),
                "repro20020920 test failed.");

            // 2003.02.07 - repro
            a = "F".Replace(',', '\n');
            b = "0,F,1,2,3,4,5,6,7".Replace(',', '\n');
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo("0.1.0.0*0.7.1.2*"),
                "repro20030207 test failed.");

            // Muegel - repro
            a = "HELLO\nWORLD";
            b = "\n\nhello\n\n\n\nworld\n";
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo("2.8.0.0*"),
                "repro20030409 test failed.");

            // test some differences
            a = "a,b,-,c,d,e,f,f".Replace(',', '\n');
            b = "a,b,x,c,e,f".Replace(',', '\n');
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo("1.1.2.2*1.0.4.4*1.0.7.6*"),
                "some-changes test failed.");

            // test one change within long chain of repeats
            a = "a,a,a,a,a,a,a,a,a,a".Replace(',', '\n');
            b = "a,a,a,a,-,a,a,a,a,a".Replace(',', '\n');
            Assert.That(TestHelper(Diff.DiffText(a, b, false, false, false)),
                Is.EqualTo("0.1.4.4*1.0.9.10*"),
                "long chain of repeats test failed.");
        }

        public static string TestHelper(Diff.Item[] f)
        {
            StringBuilder ret = new StringBuilder();
            for (int n = 0; n < f.Length; n++)
            {
                ret.Append(f[n].deletedA.ToString() + "." + f[n].insertedB.ToString() + "." + f[n].StartA.ToString() + "." + f[n].StartB.ToString() + "*");
            }
            // Debug.Write(5, "TestHelper", ret.ToString());
            return (ret.ToString());
        }
    }
}
