using UnityEngine;
using System.Collections;

using Grid;
using MyGame.Objects;

namespace MyGame
{
	class CharacterBehavior : MonoBehaviour
	{
        public int gridXStart;
        public int gridYStart;

        GridPosition position;
        TileHighlighter highlighter;

        void Start()
        {
            position = new GridPosition(gridXStart, gridYStart);

            // highlight the position that character is in
            highlighter = new TileHighlighter(position);
            highlighter.Color = Color.red;
        }

        void Update()
        {
            //PlanePosAngle pps = GridConverter.MainConverter.gridPosToWorldspace(position);
            //gameObject.transform.localPosition = PlanePosAngle.getV3Pos(pps, 1.3f);

            // change the angle
            Vector3 cameraAngles = Camera.mainCamera.transform.localEulerAngles;
            gameObject.transform.localEulerAngles = new Vector3(-cameraAngles.x, cameraAngles.y - 180, 270);
        }
	}
}
