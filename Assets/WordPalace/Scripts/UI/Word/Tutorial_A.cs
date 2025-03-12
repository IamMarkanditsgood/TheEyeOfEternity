using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSG.WordPalace.ScriptableObjects;
using JSG.WordPalace.Gameplay;
namespace JSG.WordPalace
{
    public class Tutorial_A : MonoBehaviour
    {
        public Image m_Hand1;
        [SerializeField, Space]
        private GameplayData m_GameplayData;
        // Start is called before the first frame update
        void Start()
        {
            m_Hand1.gameObject.SetActive(false);
            StartCoroutine(Co_TutorialLoop1());

        }

        // Update is called once per frame
        void Update()
        {
            if (GameControl.Current.m_WordFound[0])
            {
                StopAllCoroutines();
                gameObject.SetActive(false);
            }
        }

        IEnumerator Co_TutorialLoop1()
        {

            yield return new WaitForSeconds(1);

            m_Hand1.gameObject.SetActive(true);
            Vector3[] pos = new Vector3[3];

            pos[0] = GameControl.Current.m_LetterBoxes[2].transform.position;
            pos[1] = GameControl.Current.m_LetterBoxes[3].transform.position;
            pos[2] = GameControl.Current.m_LetterBoxes[1].transform.position;

            Vector3 p = Helper.WorldToUIPosition2(pos[0], CameraControl.Current.GetComponent<Camera>(), UISystem.m_Main.m_GeneralCanvasSize);
            m_Hand1.transform.localPosition = p;

            int num = 0;
            while (true)
            {
                Vector3 p1 = Helper.WorldToUIPosition2(pos[num], CameraControl.Current.GetComponent<Camera>(), UISystem.m_Main.m_GeneralCanvasSize);
                //m_Hand1.transform.localPosition = p1;

                BaseScriptAnim.MoveFromTo(m_Hand1.transform, m_Hand1.transform.localPosition, p1, .5f);
                yield return new WaitForSeconds(.5f);
                num++;
                if (num > 2)
                {
                    num = 0;
                    p1 = Helper.WorldToUIPosition2(pos[num], CameraControl.Current.GetComponent<Camera>(), UISystem.m_Main.m_GeneralCanvasSize);
                    m_Hand1.transform.localPosition = p1;
                }
            }






        }


    }
}
