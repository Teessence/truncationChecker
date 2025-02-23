using NUnit.Framework;
using System.Diagnostics;

namespace MyConsoleApp
{
    [TestFixture]
    public class MyTests
    {
        private static List<string> reportLines = [];
        private StreamWriter _logWriter;
        private TextWriter _originalConsoleOutput;

        [SetUp]
        public void Setup()
        {
            // Save the original Console output
            _originalConsoleOutput = Console.Out;

            // Open the log file in append mode and auto-flush enabled
            string logFileName = $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";

            _logWriter = new StreamWriter(logFileName, append: true)
            {
                AutoFlush = true  // Ensure immediate writing
            };

            // Redirect Console output to the log file
            Console.SetOut(_logWriter);
        }

        [TearDown]
        public void TearDown()
        {
            // Restore original console output
            Console.SetOut(_originalConsoleOutput);

            // Close the log writer
            _logWriter.Close();
            _logWriter.Dispose();
        }

        public void RunTestCase(string targetStringTest, List<List<string>> AllPossibilitiesTest, bool ExpectedValue)
        {
            var stopwatch = Stopwatch.StartNew();

            var IsPassing = Program.IsTruncated(targetStringTest, AllPossibilitiesTest) == ExpectedValue;
            stopwatch.Stop();

            if (IsPassing)
            {
                LogTestResult(TestContext.CurrentContext.Test.Name, stopwatch.Elapsed.TotalSeconds, true);
                Assert.Pass();
            }
            else
            {
                LogTestResult(TestContext.CurrentContext.Test.Name, stopwatch.Elapsed.TotalSeconds, false);
                Assert.Fail();
            }
        }

        [Test]
        public void TestCase1()
        {
            RunTestCase
            (
                "witamy",
                 [
                    ["wi", "mit", "w"],
                                            ["ta", "tam", "tamm"],
                                            ["y", "my"]
                ],
                false
            );
        }


        [Test]
        public void TestCase2()
        {
            RunTestCase
            (
                "Pełne skanowanie 2876 0 08.08.2024 09:50 Pełne skanowanie 2023 0 08.08.2024 09:26 Pełne skanowanie 2544 0 08.08.2024 09:02 Pełne skanowanie 1983 0 08.08.2024 08:38 Pełne skanowanie 2854 0 08.08.2024 08:14 Pełne skanowanie 2626 0 08.08.2024 07:50 Pełne skanowanie 2577 0 08.08.2024 07:26 Pełne skanowanie 2619 0 08.08.2024 07:02 Pełne skanowanie 2831 0 08.08.2024 06:39 Pełne skanowanie 2431 0 08.08.2024 04:26 Pełne skanowanie 1946 0 08.08.2024 04:02",
                [
                    ["vv vv vv vy vy vy vyj j j oe oe je os hs. ho. ho.2 2 2 2 2 2 2 2 2 2 qo5 3 3 3 3 3 3 3 3 3 3© © © © © © © © © oo[22] [22] [22] [22] [22] [22] [22] [22] [22] [22] [22]= = = = = = = = = = =[e] [e] [e] [e] [e] [e] [e] [e] [e] [e] [e]5 3 3 3 3 3 3 3 3 3 3[¢] [¢] [¢] [¢] [¢] [¢] [¢] [¢] [¢] [¢] [¢]= = = = = = = = = =z =[o] [o] [o] [o] [o] [o] [o] [o] [o] [o] [o]3 | 2 (2 |2 2 [22/2222© © oo oo oo oo ® @® @® @® ©", "are varese 2 va ee var va vo er va var ee vypetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowanie", "ave a \"va ee var va vee ve va vspetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowanie", "vviviiviiv iv ivivivi|v]|v |vpełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowanie", "vviviiviiv iv ivivivi|v]|v |vpetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowanie", "\\vv vv vy vy vy vy vy vy vypetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowanie", "x ty ]y|v|v[yv]yv|y]|y|y|vpełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowanie", "\\vv vv vy vy vy vy vy nnpetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowanie", "> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie>  pelne skanowanie", "nzesz uz sz sz sz sz szpełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowanie", ",yyy ny ny nn ny on nnpełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowanie", "> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie> petne skanowanie", "nzesz uszu sz sz sz szpełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowanie", "> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie> pelne skanowanie", "nnnnnnn nn ny wypełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowanie", "ave a \"va sz szpetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowanie", "vv vv vv vv vvpełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowaniepełne skanowanie", "vv vv vv vv vvpetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowaniepetne skanowanie", ".a eere vars\" va va va va a vy", "*nzesz uszu sz sz sz sz", "22222222222289222222225", ".a eere vars\" va va sz"],
                                                    ["~~", "hd", "4", "[", "-"],
                                                    ["wt", "ek", "6", "."],
                                                    ["ie", "=", "m"],
                                                    ["[@] [@] [@] [@] [@] [@] [@] [@] [@] [@] [@]", "[e][e][e][e][e][e][e][e][e][e][e]", "00000000000", "ooooooooooo"],
                                                    ["2577pijepiski", "nnn® @ a® © j", "257726192831", "2577pije2831", "vivisie]2831", "02831", "228", "."],
                                                    ["1946", "1846", "©5>»", "2"],
                                                    ["[= [= [= [=© ® © ©[=] [=] [=] [=]© oo © ©[&) [&) [&) [&)[=} [=} [=} [=}[ [ [ [+» +» +» +»[=] [=] [=] [=]ey h [4 n[=] nn [5 [=]n [&] [ze] no", "08.08.2024 07:0208.08.2024 06:3908.08.2024 04:2608.08.2024 04:02", "080820240702080820240639080820240426080820240402", "08082024104260", "26822299681"],
                                                    ["pzeji", "2431", "1243", "[nj«", "5"],
                                                    ["[= [= [=© © ©[= [= [=© © ©[&) [&) [&)[= [= [=no no noen en en=} =} =}~n n ©[¥) 0 =oo =} »", "08.08.2024 08:1408.08.2024 07:50(o) sk oj o p boys)", "08.08.2024 08:1408.08.2024 07:5008.08.2024 07:26", "0808202408140808202407500808202410726", "080820240814080820240750080820240726", "28562"],
                                                    ["nd[o][>][¢2]", "pisyjsj", "2626"],
                                                    ["nd®(61)»", "pisjsy!", "12854", "3"],
                                                    ["1983", "©®[#"],
                                                    ["nd(61)»»", "pay vs", "12544", "3"],
                                                    ["oo ooo ooo oo© | © 0 ®oo oo o o© | © oo oo[cine cee con cyoo oo oo ono no no noen ne nnoo o o ow ooo nn ase =)", "08.08.2024 09:5008.08.2024 09:2608.08.2024 09:0208.08.2024 08:38", "080820240950080820240926080820240902080820240838", "080820240850080820240926080820240802080820240838", "268222996826809", "1"],
                                                    ["[odo[[8]", "piski", "2023"],
                                                    ["nd[e+]~[)]", "płsy/e)", "2876", "0"]
                ],
                false
            );
        }

        [Test]
        public void TestCase3()
        {
            RunTestCase
            (
                "witamy2344",
                [
                    ["wi", "mit", "w"],
                                    ["ta", "tam", "tamm"],
                                    ["y", "my"]
                ],
                true
            );
        }

        [Test]
        public void TestCase4()
        {
            RunTestCase
            (
                "witamyyy",
                [
                            ["ywi", "mit", "w"],
                                    ["ta", "tam", "tamm"],
                                    ["y", "myy"]
                ],
                false
            );
        }

        [Test]
        public void TestCase5()
        {
            RunTestCase
            (
                "łwitamyyyłł",
                [
                            ["ływi", "mit", "w"],
                            ["ta", "tam", "tamm"],
                            ["y", "myyłł"]
                ],
                false
            );
        }

        [Test]
        public void TestCase6()
        {
            RunTestCase
            (
                "Ѱ",
                [
                            ["ływi", "mit", "w"],
                            ["ta", "tam", "tamm"],
                            ["yѰ", "myyłłѰ"]
                ],
                false
            );
        }

        [Test]
        public void TestCase7()
        {
            RunTestCase
            (
                "ѰѰ",
                [
                            ["ływi", "mit", "w"],
                            ["ta", "tam", "tamm"],
                            ["yѰ", "myyłłѰ"]
                ],
                true
            );
        }

        [Test]
        public void TestCase8()
        {
            RunTestCase
            (
                "ѰѰѰ",
                [
                            ["ływiѰ", "mit", "w"],
                            ["taѰ", "tam", "tamm"],
                            ["yѰ", "myyłłѰ"]
                ],
                false
            );
        }


        [Test]
        public void TestCase9()
        {
            RunTestCase
            (
                "より安全なデジタルライフを 楽しむために",
                [
                            ["より安全なデジタルライフを", "mit", "w"],
                            ["taѰ", "楽しむために", "tamm"],
                            ["yѰ", "myyłłѰ"]
                ],
                false
            );
        }

        [Test]
        public void TestCase10()
        {
            RunTestCase
            (
                "Welcome",
                [
                            ["", "", ""],
                            ["", "", ""],
                            ["", ""]
                ],
                true
            );
        }

        [Test]
        public void TestCase11()
        {
            RunTestCase
            (
                "Welcome",
                [
                            ["Welcome", "", ""],
                            ["", "", ""],
                            ["", ""]
                ],
                false
            );
        }

        [Test]
        public void TestCase12()
        {
            RunTestCase
            (
                "Welcome1",
                [
                            ["Welcome", "", ""],
                            ["", "", ""],
                            ["", ""]
                ],
                true
            );
        }


        [Test]
        public void TestCase13()
        {
            RunTestCase
            (
                "WelcomeWelcome",
                [
                            ["Welcome", "", ""],
                            ["", "Welcome", ""],
                            ["", ""]
                ],
                false
            );
        }

        private void LogTestResult(string testCaseName, double duration, bool passed)
        {
            var result = passed ? "Passed" : "Failed";
            reportLines.Add($"{testCaseName}, {result}, {duration} seconds");
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
            var reportPath = "TestReport.html";

            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine("<html>");
                writer.WriteLine("<head><title>Test Report</title></head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<h1>Test Case Results</h1>");
                writer.WriteLine("<table border='1'><tr><th>Test Case</th><th>Status</th><th>Duration (seconds)</th></tr>");

                foreach (var line in reportLines)
                {
                    var columns = line.Split(',');
                    writer.WriteLine($"<tr><td>{columns[0]}</td><td>{columns[1]}</td><td>{columns[2]}</td></tr>");
                }

                writer.WriteLine("</table>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }
        }
    }
}
