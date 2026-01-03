using BowlingGame;

namespace TestBowlingGame
{
    public class TestGame
    {
        private Game g;

        public TestGame()
        {
            g = new Game();
        }


        [Fact]
        public void testScoreNoThrows()
        {
            Assert.Equal(0, g.score());
        }

        [Fact]
        public void testAddOneThrowNoMark()
        {
            g.add(5);
            g.add(4);
            Assert.Equal(9, g.score());
            Assert.Equal(2, g.getCurrentFrame());
        }

        [Fact]
        public void testFourThrowsNoMark()
        {
            g.add(5);
            g.add(4);
            g.add(7);
            g.add(2);
            Assert.Equal(18, g.score());
            Assert.Equal(9, g.scoreForFrame(1));
            Assert.Equal(18, g.scoreForFrame(2));
            Assert.Equal(3, g.getCurrentFrame());
        }

        [Fact]
        public void testSimpleSpare()
        {
            g.add(3);
            g.add(7);
            g.add(3);
            Assert.Equal(13, g.scoreForFrame(1));
            Assert.Equal(2, g.getCurrentFrame());
        }

        [Fact]
        public void testSimpleFrameAfterSpare()
        {
            g.add(3);
            g.add(7);
            g.add(3);
            g.add(2);
            Assert.Equal(13, g.scoreForFrame(1));
            Assert.Equal(18, g.scoreForFrame(2));
            Assert.Equal(18, g.score());
            Assert.Equal(3, g.getCurrentFrame());
        }

        [Fact]
        public void testTwoThrows()
        {
            g.add(5);
            g.add(4);
            Assert.Equal(9, g.score());
            Assert.Equal(2, g.getCurrentFrame());
        }

        [Fact]
        public void testSimpleStrike()
        {
            g.add(10);
            g.add(3);
            g.add(6);
            Assert.Equal(19, g.scoreForFrame(1));
            Assert.Equal(28, g.score());
            Assert.Equal(3, g.getCurrentFrame());
        }
    }
}
