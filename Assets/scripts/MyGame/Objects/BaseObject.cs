using UnityEngine;
using System.Collections;
using MyGame;
using Grid;

namespace MyGame.Objects
{
    class BaseObject
    {
        //
        // Member variables
        //
        
        protected GameObject gameObject;
        protected GridPosition gridPosition;

        //
        // Member accessors
        //

        //
        // Member functions
        //

        // default constructor
        private BaseObject()
        {
            gridPosition = new GridPosition();
        }

        // uses pre-loaded GameObject
        public BaseObject(GameObject go)
            : this()
        {
            gameObject = go;
        }

        // loads a template GameObject
        public BaseObject(string templateName)
            : this()
        {
            gameObject = (GameObject)GameObject.Instantiate(GameObject.FindWithTag(templateName));
        }

        public void Translate(int xAmount, int yAmount)
        {
            // update position
            gridPosition.x += xAmount;
            gridPosition.y += yAmount;

            SyncPosition(gridPosition);
        }

        // set the position of the object
        public void SetGridPosition(GridPosition gPos)
        {
            SetGridPosition(gPos.x, gPos.y);
        }

        // set the position of the object
        public void SetGridPosition(int xValue, int yValue)
        {
            gridPosition.x = xValue;
            gridPosition.y = yValue;

            SyncPosition(gridPosition);
        }

        void SyncPosition(GridPosition newPosition)
        {
            // get real world coords of position
            PlanePosAngle pps = Grid.MainGrid.gridPosToWorldspace(gridPosition);
            
            Vector3 newPos = new Vector3(pps.x, 1000.0f, pps.z);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(newPos, Vector3.down, out hit);
            newPos.y = hit.point.y;

            // and set the position
            gameObject.transform.localPosition = newPos;
        }
    }
}