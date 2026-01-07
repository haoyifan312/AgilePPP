namespace BowlingGame
{
    public class Game
    {
        private int itsCurrentFrame;
        private bool firstThrowInFrame;
        private Scorer itsScorer;

        public Game() 
        {
            itsCurrentFrame = 1;
            firstThrowInFrame = true;
            itsScorer = new Scorer();
        }

        

        public int score()
        {
            return scoreForFrame(itsCurrentFrame); 
        }

        public void add(int pins)
        {
            itsScorer.addThrow(pins);
            adjustCurrentFrame(pins);
        }

        private void adjustCurrentFrame(int pins)
        {
            if (lastBallInFrame(pins))
            {
                advanceFrame();
            }
            else
            {
                firstThrowInFrame = false;
            }
        }

        private bool lastBallInFrame(int pins)
        {
            return strike(pins) || (!firstThrowInFrame);
        }

        private bool strike(int pins)
        {
            return (firstThrowInFrame && pins == 10);
        }

        private void advanceFrame()
        {
            itsCurrentFrame = Math.Min(10, itsCurrentFrame+1);
        }

        public int scoreForFrame(int theFrame)
        {
            return itsScorer.scoreForFrame(theFrame);
        }

    }
}
