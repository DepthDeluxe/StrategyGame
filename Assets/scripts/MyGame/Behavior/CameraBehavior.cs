using UnityEngine;
using System.Collections;

namespace MyGame.Behavior
{
    public class CameraBehavior : MonoBehaviour
    {
        // positions for the camera
        Vector3[] pts = {
	        new Vector3(0f, 6f, 0f),
	        new Vector3(10f, 6f, 0f),
	        new Vector3(10f, 6f, 10f),
	        new Vector3(1.75f, 5f, 10f-1.75f)
	    };

        // angles for the camera
        int[] rotAngles = {
	        45,
	        315,
	        225,
	        135,
	    };

        int currentSelected = 0;
        int prevSelected = 0;
        float timeConstant = 0.1f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // handle camera rotation
            cameraRotationFunc();
        }

        void cameraRotationFunc()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                prevSelected = currentSelected;

                currentSelected++;
                if (currentSelected == 4)
                    currentSelected = 0;
            }

            // with no lerping
            gameObject.transform.localPosition = pts[currentSelected];
            gameObject.transform.localEulerAngles = new Vector3(35, rotAngles[currentSelected], 0);
        }
    }
}