using UnityEngine;
using System.Collections;
using Grid;

namespace MyGame.Objects
{
    /// <summary>
    /// Wrapper for a projector that casts a colored triangle over a certain grid position.
    /// This is used to highlight the specific tile
    /// </summary>
    class TileHighlighter
    {
        //
        // Member variables
        //

        GameObject gameObject;
        GridPosition gridPos;

        //
        // Member Accessors
        //

        // change the highlighted color
        public Color Color
        {
            get
            {
                Projector projSettings = (Projector)gameObject.GetComponent("Projector");
                return projSettings.material.color;
            }
            set
            {
                Projector projSettings = (Projector)gameObject.GetComponent("Projector");
                projSettings.material.color = value;
            }
        }

        // enable or disable errythang
        public bool Enabled
        {
            get
            {
                return gameObject.activeSelf;
            }
            set
            {
                gameObject.SetActive(value);
            }
        }

        //
        // Member functions
        //

        // empty constructor
        public TileHighlighter()
            : this(new GridPosition(0,0))
        {

        }

        // normal use constructor
        public TileHighlighter(GridPosition gridPos)
        {
            // instantiate the gameobject from the template
            GameObject template = GameObject.FindWithTag("selectedProjector");
            gameObject = (GameObject)GameObject.Instantiate(template);

            // instantiate the material as well, so everything doesn't get screwed up
            Projector projSettings = (Projector)gameObject.GetComponent("Projector");
            Projector templateProjSettings = (Projector)template.GetComponent("Projector");
            projSettings.material = (Material)Material.Instantiate(templateProjSettings.material);

            // set the position
            this.gridPos = gridPos;

            // update the position
            PlanePosAngle pps = GridConverter.MainConverter.gridPosToWorldspace(gridPos);
            gameObject.transform.localPosition = new Vector3(pps.x, gameObject.transform.localPosition.y, pps.z);
            gameObject.transform.localEulerAngles = new Vector3(
                gameObject.transform.localEulerAngles.x,
                pps.angle,
                gameObject.transform.localEulerAngles.z);
        }

        //
        // Static variables
        //

        public static Color defaultBlue = new Color(48, 85, 255);      // default blue highlighted color
        public static Color defaultRed = new Color(255, 0, 0);
    }
}