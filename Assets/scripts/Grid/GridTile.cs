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

        public bool IsZUp
        {
            get
            {
                // find which two have the same z values
                float z0 = edges[0].z;
                float z1 = edges[1].z;
                float z2 = edges[2].z;

                // put it through case switch
                if (z0 == z1)
                {
                    return z0 < z2;
                }
                else if (z1 == z2)
                {
                    return z1 < z0;
                }
                else if (z0 == z2)
                {
                    return z0 < z1;
                }
                else
                {
                    throw new Exception("There seems to be something wrong with the edges");
                }
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

        ~GridTile()
        {

        }
	}
}
