using System;
using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class GameHistoryValidatorTest
    {
        [Test]
        [TestCase("lll", true)]
        [TestCase("d", false)]
        [TestCase("k", false)]
        [TestCase("llllll", true)]
        [TestCase("lllllll", false)]
        public void TestValidate(string commands, bool expected)
        {
            var validator = new GameHistoryValidator();

            var board = Board.Create(new[]
            {
                "...",
                "...",
                "..."
            });
            var unit = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var unit2 = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var unit3 = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var game = new Game(board, null, new[] { unit, unit2, unit3 }, 0, 0, 0, -1, -1, string.Empty, 0).TrySpawnNew();

            var actual = validator.Validate(game, commands);
            Assert.AreEqual(expected, actual.IsValid);
        }

        [Test]
        public void TestValidateInputJson()
        {
            var game = new GameBuilder().Build("Problems\\problem_0.json")[0];
            var answer = "ia! ia!lei!llbbbbbbbeiia! ia!lei!llbbbbbbeiia! ia!lei!kklbbbbbbkkyugia! ia!r'lyehbdddeiia! ia!lei!dlbyuia! ia!in hiia! ia!lcthulhuddeiia! ia!lcthulhukiia! ia!lbbbyuggia! ia!lbbbblblr'ia! ia!lbyuyuggothdapinyuggothdlbbbdlliia! ia!klbbbllbei!ia! ia!lei!dlir'lyehyuggothppaaklir'lyehyuggothr'lyehleiyuggothyuggotia! ia!lakyuggoia! ia!ldlbbbdinyuggothllbcthulia! ia!kklbbbbbbbklllkiei!r'lyehcthulhublinia! ia!lcthulhuddeiia! ia!lkkiia! ia!dlbbbbbldinyuggothdlbbdldr'ia! ia!r'lyehbddr'ei!r'lyehcthulhulbyuggia! ia!r'lyehppddr'r'lyehyuggothr'lyeheiei!r'lyehyuggothliin his hoia! ia!lbbbbblbblliei!r'lyehr'lyehdei!pkapinia! ia!laakei!ia! ia!llblliia! ia!dlbbdlbbblir'lyehyuggothei!ppia!ia! ia!llklbbbbei!ia! ia!r'lyehr'lyia! ia!lddlbbdlyugia! ia!in r'lyehyuggothr'lyehbei!ia! ia!ldinyuggothcthulhubbbdliia! ia!lyuggyuggothyuggothkr'lia! ia!klblblbleiei!r'lyehyuggothdlyugei!r'lyehr'lyehdlbdlbliyuggothcthulhubbbllkiia! ia!r'lyehbblr'ia! ia!linyuggothyuggothpkiia! ia!llei!yuggothyuggothkkiia! ia!lbbbbldinia! ia!dlbbbdlbblkiia! ia!dlbbbdei!yuggothdei!ppkiei!r'lyehcthulhuddlbbbdliia! ia!r'lyehbbbbbbldar'lei!r'lyehyuggothei!r'lyr'lyehyuggothr'lyehiia! ia!lei!klbbkei!ia! ia!dlbbbbbbbddliia! ia!lei!llbeiia! ia!r'lyehpdddiia! ia!r'lyehpinia! ia!lei!klbbblbbeiei!r'lyehyuggothcthulhia! ia!r'lyehpinia! ia!lr'lyehyuggia! ia!lei!lbbbei!ia! ia!lakklbbbblkyugei!r'lyehcthulhulblbddliia! ia!lei!lkinia!ia!lei!klbbbbbei!ia! ia!ldlblbyur'lyehyuggothcthulhia! ia!r'lyehbbbblblyugia! ia!r'lyehkkicthulhudddei!ppppddr'lyia! ia!ldlbbbbbdei!ia! ia!linyuggothcthulhubblbliei!r'lyehyuggothbbbdlinia! ia!r'lyehbldeiia! ia!in hia! ia!ldr'ia! ia!r'lyehbblbdeiia! ia!lbbblr'ia! ia!r'lyehbbbblbblir'lyehin his hoia! ia!lei!kklbbbbbbbkkyugia! ia!r'lyehcthulia! ia!lpr'lyia! ia!r'lyehbbbbdddeiia! ia!lbblbdei";

            var result = new GameHistoryValidator().Validate(game, answer);

            Console.WriteLine(result.IsValid);
            Console.WriteLine(result.Reason);
            Console.WriteLine(result.WrongMoveNumber);
            Assert.IsTrue(result.IsValid);
        }
    }
}