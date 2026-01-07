using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace BowlingGame
{
    internal class Scorer
    {
        private int ball;
        private int[] itsThrows;
        private int itsCurrentThrow;

        public Scorer()
        {
            ball = 0;
            itsThrows = new int[21];
            itsCurrentThrow = 0;
        }

        public void addThrow(int pins)
        {
            itsThrows[itsCurrentThrow++] = pins;
        }

        public int scoreForFrame(int theFrame)
        {
            int score = 0;
            ball = 0;
            for (int currentFrame = 0;
                currentFrame < theFrame;
                currentFrame++)
            {
                if (strike())
                {
                    score += 10 + nextTwoBallsForStrike();
                    ball++;
                }
                else if (spare())
                {
                    score += 10 + nextBallForSpare();
                    ball += 2;
                }
                else
                {
                    score += twoBallsInFrame();
                    ball += 2;
                }
            }

            return score;
        }
        private bool strike()
        {
            return itsThrows[ball] == 10;
        }
        private int nextTwoBallsForStrike()
        {
            return itsThrows[ball + 1] + itsThrows[ball + 2];
        }
        private bool spare()
        {
            return (itsThrows[ball] + itsThrows[ball + 1]) == 10;
        }

        private int nextBallForSpare()
        {
            return itsThrows[ball + 2];
        }
        private int twoBallsInFrame()
        {
            return itsThrows[ball] + itsThrows[ball + 1];
        }
    }
}
