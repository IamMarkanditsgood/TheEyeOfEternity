using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JSG.WordPalace
{
    public class FixFrameRate : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Resolution currentResolution = Screen.currentResolution;
            float refreshRate = currentResolution.refreshRate;

            Application.targetFrameRate = Mathf.RoundToInt(refreshRate);
        }


    }
}