using UnityEngine;
using System.Collections;

namespace MyGame
{
    class MyInput : MonoBehaviour
    {
        //
        // Member functions
        //

        void Start()
        {

        }

        void Update()
        {
            mouseRaycast = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
        }

        //
        // Static variables
        //

        static Ray mouseRaycast;

        //
        // Static accessors
        //

        public static Ray MouseRaycast
        {
            get { return mouseRaycast; }
        }

    }
}