using UnityEngine;
using System.Collections;

namespace Grid
{
    public class GridPosition
    {
        //
        // Member variables
        //

        public int x;
        public int y;

        //
        // Member functions
        //

        // blank constructor
        public GridPosition()
            : this(0, 0)
        {

        }

        // standard constructor
        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}