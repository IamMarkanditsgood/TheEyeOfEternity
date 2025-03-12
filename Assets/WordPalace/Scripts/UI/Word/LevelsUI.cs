using System.Collections;
using System.Collections.Generic;
using JSG.WordPalace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace JSG.WordPalace.UI
{
    public class LevelsUI : MonoBehaviour
    {
        public Image m_RemoveAdsBtn;
        public int m_SelectedCategory;
        public Transform BtnParent;
        public GameObject LevelBtnPrefab1;
        [SerializeField]
        private Contents m_Contents;
        [SerializeField]
        public GameplayData m_GameplayData;
        [SerializeField, Space]
        private DataStorage m_DataStorage;
        [SerializeField, Space]

        private UIGraphicContents m_UIGraphicContents;
        [SerializeField, Space]
        private UITextContents m_UITextContentsContents;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels.Length; i++)
            {
                GameObject btn = Instantiate(LevelBtnPrefab1);
                btn.transform.parent = BtnParent;
                LevelButton b = btn.GetComponent<LevelButton>();
                b.m_LevelNum = i;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void BtnBack()
        {
            UISystem.RemoveUI("LevelsUI");
            UISystem.ShowUI("CategoryUI");
        }


    }
}