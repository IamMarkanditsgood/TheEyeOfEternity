using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JSG.WordPalace.ScriptableObjects;
namespace JSG.WordPalace.UI
{
    public class LevelButton : MonoBehaviour
    {
        [HideInInspector]
        public int m_LevelNum;

        public Text m_LevelNumText;
        public Image m_LockImage;
        public Image m_PassedImage;
        public Image m_LevelImage;


        [HideInInspector]
        public bool LevelUnlocked = false;
        // Start is called before the first frame update
        [SerializeField, Space]
        private GameplayData m_GameplayData;
        [SerializeField, Space]
        private Contents m_Contents;
        [SerializeField, Space]
        private DataStorage m_DataStorage;

        private void Awake()
        {

        }

        void Start()
        {

            m_LevelNumText.text = (m_LevelNum + 1).ToString();
            m_LevelImage.sprite = m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels[m_LevelNum].m_Icon;
            LevelUnlocked = (m_LevelNum <= m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_LastLevel);

            int levelSize = m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels[m_LevelNum].m_TableSize;


            switch (levelSize)
            {
                case 2:
                    m_LevelImage.sprite = m_Contents.m_TableSizeIcons[0];
                    break;
                case 3:
                    m_LevelImage.sprite = m_Contents.m_TableSizeIcons[1];
                    break;
                case 4:
                    m_LevelImage.sprite = m_Contents.m_TableSizeIcons[2];
                    break;
                case 5:
                    m_LevelImage.sprite = m_Contents.m_TableSizeIcons[3];
                    break;
            }


            if (LevelUnlocked)
            {
                GetComponent<Button>().interactable = true;
                m_LockImage.gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
                // GetComponent<Button>().interactable = false;
                // m_LockImage.gameObject.SetActive(true);
                // m_PassedImage.gameObject.SetActive(false);

            }

        }


        // Update is called once per frame
        void Update()
        {

        }

        public void BtnClicked()
        {
            if (LevelUnlocked)
            {
                m_GameplayData.m_LevelNumber = m_LevelNum;
                SceneManager.LoadScene("Levels");
            }
        }
    }
}
