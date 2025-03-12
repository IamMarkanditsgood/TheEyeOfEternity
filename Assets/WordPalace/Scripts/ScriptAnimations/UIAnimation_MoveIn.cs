using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JSG.WordPalace
{
    public class UIAnimation_MoveIn : MonoBehaviour
    {
        public UIData m_UIData;
        // Start is called before the first frame update
        public float m_AnimSpeed = 2;
        public Vector3 m_Offset;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Co_MoveIn());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator Co_MoveIn()
        {
            //yield return new WaitForSeconds(.1f);
            Vector3 start = transform.localPosition + m_Offset;
            Vector3 end = transform.localPosition;
            transform.localPosition = start;
            float lerp = 0;
            while (lerp < 1)
            {
                transform.localPosition = Vector3.Lerp(start, end, m_UIData.m_AnimInCurve_1.Evaluate(lerp));
                lerp += m_AnimSpeed * Time.deltaTime;
                yield return null;
            }
            transform.localPosition = end;
        }
    }
}