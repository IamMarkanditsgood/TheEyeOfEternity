using System.Collections;
using System.Collections.Generic;
using JSG.WordPalace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
namespace JSG.WordPalace
{
    public class CategoryUI : MonoBehaviour
    {
        public Image m_RemoveAdsBtn;
        public Transform m_BtnParent;
        public GameObject m_CategoryBtnPrefab1;
        [SerializeField, Space]
        private DataStorage m_DataStorage;
        [SerializeField, Space]

        private UIGraphicContents m_UIGraphicContents;
        [SerializeField, Space]
        private UITextContents m_UITextContentsContents;
        [SerializeField]
        private Contents m_Contents;
        // Start is called before the first frame update
        void Start()
        {

            for (int i = 0; i < m_Contents.m_LevelPacks.Length; i++)
            {
                GameObject btn = Instantiate(m_CategoryBtnPrefab1);
                btn.transform.parent = m_BtnParent;
                CategoryButton b = btn.GetComponent<CategoryButton>();
                b.m_LevelPackNum = i;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void BtnBack()
        {
            UISystem.RemoveUI("CategoryUI");
            UISystem.ShowUI("MainMenUI");
        }


    }
}
