using UnityEngine;
using System.Collections;

namespace Grid
{
    public class GridConverter
    {
        //
        // Member variables
        //

        // raw-ish input
        protected Vector3 projectorPos;
        protected int xTriangles;
        protected int yTriangles;
        protected int gridWidth;

        // calculated values
        int numTrianglesWide;
        float triangleEdgeLen;
        float triangleHeight;
        float bottomLineHeight;

        //
        // Member Functions
        //

        public GridConverter(Vector3 projPos, int numX, int numZ, int orthoSize)
        {
            projectorPos = projPos;
            xTriangles = numX;
            yTriangles = numZ;
            gridWidth = 2 * orthoSize;

            // calculate values
            numTrianglesWide = (int)Mathf.Floor((float)xTriangles / 2.0f) + xTriangles % 2;
            triangleEdgeLen = (float)gridWidth / (float)numTrianglesWide;
            triangleHeight = triangleEdgeLen * Mathf.Sqrt(3) / 2.0f;

            bottomLineHeight = ((float)gridWidth - (float)yTriangles * triangleHeight) / 2.0f;

            // assign the main converter pointer
            if (mainConverter == null)
            {
                mainConverter = this;
            }
        }

        public GridPosition castHitToGridPos(Vector2 globalPos)
        {
            Vector3 position = this.toLocalCoords(globalPos);

            // subtract bottom line height from position
            position.y -= bottomLineHeight;

            // find the sectors
            float xSector = Mathf.Floor(position.x / triangleEdgeLen);
            float ySector = Mathf.Floor(position.y / triangleHeight);

            // find normalized position
            Vector2 normalizedPos = new Vector2();
            normalizedPos.x = position.x - xSector * triangleEdgeLen;
            normalizedPos.y = position.y - ySector * triangleHeight;

            // calculate the triangle mouse is in
            float slope = Mathf.Sqrt(3);                  // will always be this slope since equilateral

            // get general values
            GridPosition gridPos = new GridPosition();
            gridPos.x = 2 * (int)xSector;
            gridPos.y = (int)ySector;

            // fix the x if needed
            if (ySector % 2 == 0)
            {
                if (normalizedPos.y < (triangleHeight - slope * normalizedPos.x))
                {
                    gridPos.x -= 1;
                }
                else if (normalizedPos.y < (slope * (normalizedPos.x - 0.5f * triangleEdgeLen)))
                {
                    gridPos.x += 1;
                }
            }
            else
            {
                if (normalizedPos.y > slope * normalizedPos.x)
                {
                    gridPos.x -= 1;
                }
                else if (normalizedPos.y > (triangleHeight - slope * (normalizedPos.x - 0.5f * triangleEdgeLen)))
                {
                    gridPos.x += 1;
                }
            }

            return gridPos;
        }

        public PlanePosAngle gridPosToWorldspace(GridPosition inPos)
        {
            PlanePosAngle outPos = new PlanePosAngle();

            outPos.x = 0.5f * (1.0f + (float)inPos.x) * triangleEdgeLen;

            // average the top and bottom positions cuz of line width
            outPos.z = triangleHeight * inPos.y;
            outPos.z += triangleHeight * (inPos.y + 1);
            outPos.z *= 0.5f;
            outPos.z += bottomLineHeight;

            // convert to global coords
            outPos.FlatVector = this.toGlobalCoords(outPos.FlatVector);

            outPos.angle = 180 * (1 + inPos.x % 2 + inPos.y % 2);

            return outPos;
        }

        public bool isPosValid(GridPosition pos)
        {
            return (pos.x > -1) & (pos.y > -1) & (pos.x < xTriangles) & (pos.y < yTriangles);
        }

        public bool isUpTriangle(GridPosition pos)
        {
            bool isUp = false;
            if (pos.x % 2 == 1)         // check left/right
                isUp = true;
            if (pos.y % 2 == 1)         // check up/down
                isUp = !isUp;

            return isUp;
        }

        // converts coordinates to local coordinates
        Vector2 toLocalCoords(Vector2 globalPos)
        {
            Vector2 localPos = new Vector2();

            localPos.x = globalPos.x + gridWidth/2 - projectorPos.x;
            localPos.y = globalPos.y + gridWidth/2 - projectorPos.z;

            return localPos;
        }

        // converts coordinates to global coordinates
        Vector2 toGlobalCoords(Vector2 localPos)
        {
            Vector2 globalPos = new Vector2();

            globalPos.x = localPos.x - gridWidth/2 + projectorPos.x;
            globalPos.y = localPos.y - gridWidth/2 + projectorPos.z;

            return globalPos;
        }

        //
        // Static variables
        //

        static GridConverter mainConverter = null;

        //
        // Static Accessors
        //

        public static GridConverter MainConverter
        {
            get { return mainConverter; }
        }

        //
        // Static functions
        //
    }
}
