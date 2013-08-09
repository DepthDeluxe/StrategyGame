using UnityEngine;
using System.Collections;
using MyGame.Objects;
using Grid;

namespace MyGame
{
    /// <summary>
    /// This is a more complete version of GridConverter with tools that
    /// will be used in the actual game
    /// </summary>
    class Grid : GridConverter
    {
        //
        // Member variables
        //

        TileHighlighter[,] highlighters;

        //
        // Member functions
        //

        public Grid(Vector3 center, int numX, int numZ, int orthoSize)
            : base(center, numX, numZ, orthoSize)
        {
            highlighters = new TileHighlighter[numX, numZ];

            // assign the proper grid positions to each of the tiles
            for (int z = 0; z < numZ; z++)
                for (int x = 0; x < numX; x++)
                {
                    highlighters[x, z] = new TileHighlighter(new GridPosition(x, z));
                    highlighters[x, z].Enabled = false;
                }

            // set mainGrid if it's blank
            if (mainGrid == null)
                mainGrid = this;                
        }

        public void ActivateHighlighter(bool truthValue, GridPosition pos)
        {
            if (isInRange(pos))
                highlighters[pos.x, pos.y].Enabled = truthValue;
        }

        public void ChangeHighligherColor(Color newColor, GridPosition pos)
        {
            if (isInRange(pos))
                highlighters[pos.x, pos.y].Color = newColor;
        }

        // checks to see if given grid position is within range
        protected bool isInRange(GridPosition pos)
        {
            return (
                pos.x > -1 &&
                pos.x < xTriangles &&
                pos.y > -1 &&
                pos.y < yTriangles
                );
        }

        //
        // Static variables
        //

        static Grid mainGrid = null;

        //
        // Static accessors
        //

        public static Grid MainGrid { get { return mainGrid; } }
    }
}