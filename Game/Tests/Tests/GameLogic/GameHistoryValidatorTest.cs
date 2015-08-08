using System;
using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class GameHistoryValidatorTest
    {
        [Test]
        [TestCase("lll", false)]
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
            var answer =
                "ia ia!plei!lei!pia ia!plei!lbei!pyuggothr'lyehei!ppppkbbbbbbbdpdbbdbdppppppppkbbbbkppppyuggothyuggothbbbkpppppyuggothr'lyehbbei!dbbdpplbdppkppyuggothr'lyehbr'lyehei!pyuggothr'lyehei!pkbbbbei!dblbyuggothr'lyehei!ppppalkbyuggothr'lyehbbbdpppppabyuggothr'lyehei!dbbbei!dpyuggothei!dbbbbbaabyuggothbbbbdpppppppplabyuggothei!plbdpdbbdei!ppyuggothr'lyehbbbkbkppppppppdbbbbbblpabyuggothbbbbdpppppppdbbbbbbbblalbia ia!lbbbbbbkbkbkppppppdbbbdpppppabr'lyehyuggothbbbbkbkpppppdpldbr'lyehbr'lyehbabyuggothr'lyehlbbkbkkppdpdabr'lyehr'lyehbei!dblbyuggothbbbbdpppppppdbbadabyuggothr'lyehpkbbei!kblkpei!r'lyehr'lyehbbbdppppppdbbdpalbyuggothr'lyehbbbkbaabr'lyehr'lyehbkpppppppplabyuggothbbei!dbbbdppplbr'lyehei!ppppplbdbr'lyehr'lyehei!pdbbdpppei!r'lyehr'lyehpdbbbbdplbdppr'lyehei!ppppplbbbbbdppppppdppr'lyehr'lyehbdei!ppdbbbbdppppppabei!r'lyehr'lyehppei!pplei!dbbbbr'lyehppabei!ppplbbbbbbei!lbblalppei!pr'lyehpldpr'lyehbbei!kbbddbdppppdbbbdppppaabei!ppplbbbbbbei!lbblalbr'lyehei!pdbbbdddbdbalbr'lyehei!dppldbbbbr'lyehbadppyuggothbbbbbaaabyuggothpdbdbdei!pdppkpklbyuggothr'lyehei!ppppakbklbyuggothr'lyehei!dbbdbdpppppppkbabia ia!lei!kbbbr'lyehbyuggothr'lyehbbbdplpyuggothr'lyehppdbbbdbbdalbyuggothr'lyehbdpppppadpyuggothyuggothbkbkppkpyuggothyuggothbbbadpdpia ia!r'lyehbbdbdbbkabia ia!r'lyehbbdbdpppakbyuggothr'lyehppdbbbbbdppppppppdbbbbbbblkbalbyuggothr'lyehbbbbdpppppppppdbbbbbbbbdppppppppdldbyuggothr'lyehpplpyuggothr'lyehbbkppppppppkbbbbbei!dbbbkbkpppldabyuggothr'lyehbbbdbdppdbbdppppppppdbbbdppppdbblbyuggothbei!dadbbbbdppppppppdbbbbaabyuggothppppkbbbkbbbei!lbbkppkbbbabyuggothbbei!akpkkpppplkbyuggothbbbdpppppppdbbbbbbbdpppppppdbbbbbbbdppppppppplabyuggothbbdbei!dei!pia ia!dbbbdbdbbr'lyehkabyuggothr'lyehpppr'lyehpdblbia ia!r'lyehei!kbbkppia ia!lbbbbei!kei!abyuggothr'lyehbei!kbbei!lbkbkppyuggothbr'lyehbblbia ia!plei!lblppyuggothr'lyehbbdpppdbbbdpppppppplkbbkbyuggothr'lyehbbdpppldbbdpppdbyuggothr'lyehbbei!lbkbkpplbia ia!lpdadpyuggothei!dbbbbbdpppppppdaabia ia!pkbbei!dbbbbbbdbdppdbbdpppppplblbia ia!lbbbbbbei!lpyuggothr'lyehbbbabia ia!lbbbbbbdbdppppppkpllbia ia!r'lyehbbbbbbdbdppppppppdbbei!dbbbbbdppdbyuggothr'lyehei!ppppkbbbbbbbei!kbblbyuggothr'lyehbbbadbdpppppppplbia ia!lei!kbbbei!lbbkpppyuggothyuggothei!pyuggothr'lyehbbadbdppabyuggothbbbr'lyehdpdabyuggothei!dbbbbbdpppppdbbbbdpppppppaddbyuggothei!lbyuggothbei!ddbbbbdpppppppplblbei!r'lyehr'lyehbbei!dbdppkabr'lyehr'lyehei!pkbbbei!pr'lyehr'lyehbbdldbr'lyehbbdppppppkbbbkbbbbkpppppppadpdabr'lyehr'lyehbbkpppppdbblppr'lyehei!pkbbbbbkkbyuggothbdbdbbdppppppppdbbbei!dbbkbkppppr'lyehei!kbei!lkbbkpppbyuggothbei!pr'lyehbbblpkblbbbbbkppppppplpldpdppei!dbbbbkppkbkpppkbbbkppppppplddbbei!lb";

            var result = new GameHistoryValidator().Validate(game, answer);

            Console.WriteLine(result.IsValid);
        }
    }
}