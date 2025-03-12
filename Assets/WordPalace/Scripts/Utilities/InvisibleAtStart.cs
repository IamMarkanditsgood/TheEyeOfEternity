using UnityEngine;
using System.Collections;
namespace JSG.WordPalace
{
    public class InvisibleAtStart : MonoBehaviour
    {


        // Use this for initialization
        void Start()
        {
            GetComponent<Renderer>().enabled = false;

        }
    }
}