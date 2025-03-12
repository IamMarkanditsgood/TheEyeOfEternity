using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSG.WordPalace.ScriptableObjects;
using JSG.WordPalace.Gameplay;
namespace JSG.WordPalace
{
    public class Tutorial_C : MonoBehaviour
    {
        [SerializeField, Space]
        private GameplayData m_GameplayData;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Co_TutorialLoop1());

        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator Co_TutorialLoop1()
        {
            Image back = UISystem.FindImage(gameObject, "img-background");
            Image img, img1;

            //tut-1 -- corns
            yield return new WaitForSeconds(15);
            img = UISystem.FindImage(gameObject, "img-tut-1");
            img.gameObject.SetActive(true);
            yield return new WaitForSeconds(5);
            img.gameObject.SetActive(false);

        }

    }
}