using UnityEngine;
using System.Collections;
using Grid;

namespace MyGame.Objects
{
    class Character : BaseObject
    {
        //
        // Member variables
        //

        string name;

        // character features
        int numMoves = 3;               // how far can walk in the game, we'll start w/ 3

        //
        // Member accessors
        //

        //
        // Member functions
        //

        public Character(string name, GridPosition pos)
            : base("characterTemplate")                     // load from the character template
        {
            // save constructor inputs
            this.name = name;

            SetGridPosition(pos);
        }

        public void ShowMoves(bool truthValue)
        {
            // figure out which way the triangle points
            bool pointsUp = Grid.MainGrid.isUpTriangle(gridPosition);

            // direction alternates with evenness of # of moves
            if (numMoves % 2 == 1)
                pointsUp = !pointsUp;

            if (pointsUp)
            {
                // will calculate relative y boundaries
                int yStart, yEnd;
                if (numMoves % 2 == 0)
                {
                    yStart = -numMoves / 2;
                    yEnd = yStart + numMoves + 1;
                }
                else
                {
                    yStart = -(numMoves - 1) / 2;
                    yEnd = yStart + numMoves + 1;
                }

                for (int y = yStart; y < yEnd; y++)
                {
                    // calculate relative x boundaries
                    int xStart = -numMoves + Mathf.Abs(y);
                    int xEnd = numMoves - Mathf.Abs(y) + 1;

                    for (int x = xStart; x < xEnd; x++)
                    {
                        if (y == (yEnd - 1) && shouldBeDeactivated(x, numMoves))
                            continue;

                        // modify the highlighter
                        Grid.MainGrid.ActivateHighlighter(truthValue, new GridPosition(gridPosition.x + x, gridPosition.y + y));
                    }
                }
            }
            else
            {
                // calculate relative y boundaries
                int yStart, yEnd;
                if (numMoves % 2 == 0)
                {
                    yStart = -numMoves / 2;
                    yEnd = yStart + numMoves + 1;
                }
                else
                {
                    yStart = -(numMoves+1) / 2;
                    yEnd = yStart + numMoves + 1;
                }

                for (int y = yStart; y < yEnd; y++)
                {
                    // calculate relative x boundaries
                    int xStart = -numMoves + Mathf.Abs(y);
                    int xEnd = numMoves - Mathf.Abs(y) + 1;

                    for (int x = xStart; x < xEnd; x++)
                    {
                        if (y == yStart && shouldBeDeactivated(x, numMoves))
                            continue;

                        // modify the highlighter
                        Grid.MainGrid.ActivateHighlighter(truthValue, new GridPosition(gridPosition.x + x, gridPosition.y + y));
                    }
                }
            }
        }

        //
        // Static functions
        //

        private static bool shouldBeDeactivated(int x, int nMoves)
        {
            // make even
            if (nMoves % 2 == 1)
                nMoves -= 1;

            // make absolute value
            x = Mathf.Abs(x);

            if ((nMoves / 2) % 2 == 0)
            {
                return (x % 2) == 1;
            }
            else
            {
                return (x % 2) == 0;
            }
        }
    }
}