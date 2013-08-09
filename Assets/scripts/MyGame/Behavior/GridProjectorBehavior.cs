using UnityEngine;
using System.Collections;
using MyGame.Objects;
using Grid;

namespace MyGame.Behavior
{
    public class GridProjectorBehavior : MonoBehaviour
    {
        public int xTriangles;
        public int yTriangles;

        Grid grid;

        GameObject selectedProjector;
        Camera mainCamera;

        // initialize the converter before anything else
        void Awake()
        {
            Projector projSettings = (Projector)gameObject.GetComponent("Projector");

            // set up the converter
            grid = new Grid(gameObject.transform.localPosition, xTriangles, yTriangles, (int)projSettings.orthographicSize);
        }

        // initialize rest of stuff before update routine called
        void Start()
        {
            // get game objects
            selectedProjector = GameObject.FindWithTag("selectedProjector");
            mainCamera = Camera.mainCamera;
        }

        // Update is called once per frame
        void Update()
        {
            // initialize raycast stuffz
            Ray ray = MyInput.MouseRaycast;
            RaycastHit hit;

            // perform raycast math if it hit
            if (Physics.Raycast(ray, out hit))
            {
                Vector2 hitPos = new Vector2(hit.point.x, hit.point.z);

                GridPosition gridPos = grid.castHitToGridPos(hitPos);
                PlanePosAngle pps = grid.gridPosToWorldspace(gridPos);

                if (grid.isPosValid(gridPos))
                {
                    // set the new position
                    setSelectedProjPos(pps);
                }
                else
                {
                    // send far, far away
                    selectedProjector.transform.localPosition = new Vector3(1000, 0, 1000);
                }
            }
            else
            {
                // send far, far away
                selectedProjector.transform.localPosition = new Vector3(1000, 0, 1000);
            }
        }

        // moves the selected point
        void setSelectedProjPos(PlanePosAngle newLoc)
        {
            selectedProjector.transform.localPosition = PlanePosAngle.getV3Pos(newLoc, 10);
            Vector3 angles = selectedProjector.transform.localEulerAngles;
            angles.y = newLoc.angle;
            selectedProjector.transform.localEulerAngles = angles;
        }
    }
}