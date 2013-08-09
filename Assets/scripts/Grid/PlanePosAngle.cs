using UnityEngine;
using System.Collections;

namespace Grid
{
    public class PlanePosAngle
    {
        //
        // Member variables
        //

        public float x;
        public float z;
        public float angle;

        //
        // Member accessors
        //

        public Vector2 FlatVector
        {
            get { return new Vector2(x, z); }
            set
            {
                x = value.x;
                z = value.y;
            }
        }

        //
        // Member Functions
        //

        public PlanePosAngle()
        {
            x = 0;
            z = 0;
            angle = 0;
        }

        //
        // Static functions
        //

        public static Vector3 getV3Pos(PlanePosAngle pps, float yValue)
        {
            return new Vector3(pps.x, yValue, pps.z);
        }
    }
}