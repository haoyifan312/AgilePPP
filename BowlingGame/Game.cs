namespace BowlingGame
{
    public class Game
    {
        //private int _itsScore;
        private int[] _itsThrows;
        private int _itsCurrentThrow;
        private int _itsCurrentFrame;
        private bool _firstThrow;
        
        public Game() 
        {
            //_itsScore = 0;
            _itsThrows = new int[21];
            _itsCurrentThrow = 0;
            _itsCurrentFrame = 1;
            _firstThrow = true;
        }

        

        public int score()
        {
            return scoreForFrame(getCurrentFrame() - 1); 
        }

        public void add(int pins)
        {
            //_itsScore += pins;
            _itsThrows[_itsCurrentThrow++] = pins;
            adjustCurrentFrame(pins);
        }

        private void adjustCurrentFrame(int pins)
        {
            if (_firstThrow)
            {
                if (pins == 10)
                    _itsCurrentFrame++;
                else
                    _firstThrow = false;
            }
            else
            {
                _firstThrow = true;
                _itsCurrentFrame++;
            }
        }

        public int scoreForFrame(int theFrame)
        {
            int score = 0;
            int ball = 0;
            for (int currentFrame = 0; 
                currentFrame < theFrame; 
                currentFrame++)
            {
                int firstThrow = _itsThrows[ball++];
                if (firstThrow == 10)
                {
                    score += firstThrow + _itsThrows[ball] + _itsThrows[ball + 1];
                }
                else
                {
                    int secondThrow = _itsThrows[ball++];
                    int framescore = firstThrow + secondThrow;

                    if (framescore == 10)   //spare
                        score += framescore + _itsThrows[ball];
                    else
                        score += framescore;
                }
            }

            return score;
        }

        public int getCurrentFrame()
        {
            return _itsCurrentFrame;
        }
    }
}
