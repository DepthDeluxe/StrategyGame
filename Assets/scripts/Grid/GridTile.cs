using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;

namespace Grid
{
	public class GridTile : GridPosition
	{
        // private class variables
        //
        Vector3[] edges;             // holds each edge of grid
        GridTile[] adjacentTiles;    // holds adjacent tiles

        // Properties
        //

        public bool IsUp
        {
            get
            {
                throw new NotImplementedException();      // not impelmented
            }
        }

        public Vector3 CenterPosition
        {
            get
            {
                // calculate the average
                Vector3 avgPos = new Vector3(0, 0, 0);
                for (int n = 0; n < 3; n++)
                {
                    avgPos = (n * avgPos + edges[n]) / (n + 1);
                }

                return avgPos;
            }
        }

        // Member Functions
        //

        // main constructor
        public GridTile()
            : base()
        {
            edges = new Vector3[3];
        }

        public ~GridTile()
        {

        }
	}
}
