using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSG.WordPalace.ScriptableObjects;
namespace JSG.WordPalace
{
    public class CategoryButton : MonoBehaviour
    {
        [HideInInspector]
        public int m_LevelPackNum;
        public Text m_CategoryName;
        public Image m_LockImage;
        public Image m_PassedImage;

        [SerializeField]
        private Contents m_Contents;
        [SerializeField]
        public DataStorage m_DataStorage;
        [SerializeField]
        public GameplayData m_GameplayData;

        // Start is called before the first frame update
        void Start()
        {
            m_CategoryName.text = m_Contents.m_LevelPacks[m_LevelPackNum].m_Title;

            if (m_Contents.m_LevelPacks[m_LevelPackNum].m_Unlocked)
            {
                GetComponent<Button>().interactable = true;
                m_LockImage.gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
                //GetComponent<Button>().interactable = false;
                m_LockImage.gameObject.SetActive(true);


            }

            if (m_Contents.m_LevelPacks[m_LevelPackNum].m_LastLevel >= m_Contents.m_LevelPacks[m_LevelPackNum].m_Levels.Length - 1)
            {
                m_PassedImage.gameObject.SetActive(true);
            }
            else
            {
                m_PassedImage.gameObject.SetActive(false);
            }

        }
        public void BtnClicked()
        {
            if (m_Contents.m_LevelPacks[m_LevelPackNum].m_Unlocked)
            {
                m_GameplayData.m_LevelPackNumber = m_LevelPackNum;
                UISystem.RemoveUI("CategoryUI");
                UISystem.ShowUI("LevelsUI");
            }

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}