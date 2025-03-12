using JSG.WordPalace.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JSG.WordPalace.UI
{
    public class GameUI : MonoBehaviour
    {
        public Image m_HintPanel;
        public GameObject m_LetterPrefab;

        public RectTransform[] m_WordBoxes;

        private LetterBox m_Hint;

        public Text m_LevelNumText;
        public Text m_CategoryText;

        public Text m_NewWordText;
        public Image m_NewWordPanel;
        public Image m_NewWordBox;

        public Image m_BolbImage;

        [SerializeField, Space]
        private GameplayData m_GameplayData;
        [SerializeField, Space]
        private Contents m_Contents;
        [SerializeField, Space]
        private DataStorage m_DataStorage;


        public static GameUI m_Main;
        [SerializeField, Space]
        private UIGraphicContents m_UIGraphicContents;
        [SerializeField, Space]
        private UITextContents m_UITextContentsContents;

        private void Awake()
        {
            m_Main = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            Level level = m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels[m_GameplayData.m_LevelNumber];

            for (int i = 0; i < level.m_Words.Length; i++)
            {
                for (int j = 0; j < level.m_Words[i].Length; j++)
                {
                    GameObject obj = Instantiate(m_LetterPrefab);
                    obj.transform.SetParent(m_WordBoxes[i]);
                    obj.GetComponent<UILetterBox>().m_TextLetter.gameObject.SetActive(false);
                }
            }

            m_LevelNumText.text = "Level " + (m_GameplayData.m_LevelNumber + 1).ToString();
            m_CategoryText.text = (level.m_Words.Length).ToString() + " " + m_Contents.m_CategoryTitles[(int)level.m_Theme].ToString();
            m_HintPanel.gameObject.SetActive(false);
            //TapsellPlusControl.m_Current.m_VideoObjectsList["Tapsell_Hint"].OnHandleReward += HandleGetFreeHint;

            m_NewWordPanel.gameObject.SetActive(false);
        }
        void OnDestroy()
        {
            // TapsellPlusControl.m_Current.m_VideoObjectsList["Tapsell_Hint"].OnHandleReward -= HandleGetFreeHint;
        }

        public void UpdateWord(int num)
        {
            Level level = m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels[m_GameplayData.m_LevelNumber];
            char[] characters = level.m_Words[num].ToCharArray();

            UILetterBox[] boxes = m_WordBoxes[num].GetComponentsInChildren<UILetterBox>();
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].m_TextLetter.gameObject.SetActive(true);
                boxes[i].m_TextLetter.text = characters[i].ToString();
                boxes[i].GetComponent<Image>().color = new Color(0, 0, 0, .7f);
            }
        }

        public void ShowWordLetter(int num)
        {
            Level level = m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels[m_GameplayData.m_LevelNumber];
            char[] characters = level.m_Words[num].ToCharArray();

            UILetterBox[] boxes = m_WordBoxes[num].GetComponentsInChildren<UILetterBox>();
            boxes[0].m_TextLetter.gameObject.SetActive(true);
            boxes[0].m_TextLetter.text = characters[0].ToString();
            boxes[0].GetComponent<Image>().color = new Color(0, 0, 0, .7f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BtnHint(int num)
        {
            switch (num)
            {
                case 0:
                    if (Application.platform != RuntimePlatform.Android)
                    {
                        HandleGetFreeHint();
                    }
                    else
                    {
                        if (!m_DataStorage.CheckInternet())
                        {
                            UIMessage_A msg = UISystem.ShowMessage("UIMessage_A", 1, m_UITextContentsContents.m_Messages[0], m_UIGraphicContents.m_Graphics[5]);
                        }
                        else
                        {

                        }
                    }
                    break;
                case 1:
                    if (m_DataStorage.Coin >= 70)
                    {
                        m_DataStorage.Coin -= 70;
                        m_DataStorage.SaveData();
                        GameControl.Current.UseHint(1);
                        BtnCloseHintPanel();
                    }
                    else
                    {
                        UIMessage_A msg = UISystem.ShowMessage("UIMessage_A", 0, m_UITextContentsContents.m_Messages[2], m_UIGraphicContents.m_Graphics[7]);
                        msg.f_Clicked_Yes = ShowCoinShop;
                    }
                    break;
                case 2:
                    if (m_DataStorage.Coin >= 100)
                    {
                        m_DataStorage.Coin -= 100;
                        m_DataStorage.SaveData();
                        GameControl.Current.UseHint(2);
                        BtnCloseHintPanel();
                    }
                    else
                    {
                        UIMessage_A msg = UISystem.ShowMessage("UIMessage_A", 0, m_UITextContentsContents.m_Messages[2], m_UIGraphicContents.m_Graphics[7]);
                        msg.f_Clicked_Yes = ShowCoinShop;
                    }
                    break;
            }

        }

        public void FindWord()
        {
            for (int i = 0; i < m_Contents.m_LevelPacks[0].m_Levels[m_GameplayData.m_LevelNumber].m_Words.Length; i++)
            {
                if (!GameControl.Current.m_WordFound[i])
                {
                    GameControl.Current.m_WordFound[i] = true;
                    UpdateWord(i);
                    //for (int j = 0; j < GameControl.Current.m_LetterBoxes.Length; j++)
                    //{
                    //    if(GameControl.Current.m_LetterBoxes[j].m_Character == m_Contents.m_LevelPacks[0].m_Levels[m_GameplayData.m_LevelNumber].m_Words[i].ToCharArray()[0])
                    //    {

                    //    }
                    //}
                    break;
                }
            }
        }

        IEnumerator ClearHint()
        {
            yield return new WaitForSeconds(3);
            m_Hint.Deselect();
        }
        public void BtnShowHintPanel()
        {
            m_HintPanel.gameObject.SetActive(true);
        }
        public void BtnCloseHintPanel()
        {
            m_HintPanel.gameObject.SetActive(false);
        }
        public void HandleGetFreeHint()
        {
            GameControl.Current.UseHint(0);
            BtnCloseHintPanel();
        }
        public bool ShowCoinShop()
        {
            UISystem.ShowUI("CoinShopUI");
            return true;
        }

        public void ShowNewWord(string word)
        {
            m_NewWordText.text = word;
            StartCoroutine(Co_ShowNewWord());
        }

        IEnumerator Co_ShowNewWord()
        {
            yield return new WaitForSeconds(.2f);
            m_NewWordPanel.gameObject.SetActive(true);

            BaseScriptAnim.MoveFromTo(m_NewWordBox.transform, new Vector3(0, 900, 0), new Vector3(0, 300, 0), .1f);

            yield return new WaitForSeconds(1.5f);
            m_NewWordPanel.gameObject.SetActive(false);

        }
        public void BtnBack()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ShakeHint()
        {
            StartCoroutine(Co_ShakeHint());
        }

        IEnumerator Co_ShakeHint()
        {
            yield return new WaitForSeconds(.2f);
            float lerp = 0;
            while (lerp < 2)
            {

                m_BolbImage.transform.localScale = (1 + (0.2f * Mathf.Sin(20 * lerp))) * Vector3.one;
                lerp += Time.deltaTime;
                yield return null;
            }

            m_BolbImage.transform.localScale = Vector3.one;
        }
    }
}