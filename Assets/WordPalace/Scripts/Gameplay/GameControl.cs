using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSG.WordPalace.ScriptableObjects;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using JSG.WordPalace.Gameplay;
using System.Drawing;
using JSG.WordPalace.UI;
using System.Runtime.InteropServices;
using UnityEngine.TextCore.Text;
namespace JSG.WordPalace
{
    public class GameControl : MonoBehaviour
    {
        public Camera m_Camera;

        [HideInInspector]
        public List<LetterBox> m_SelectedBoxes;

        [HideInInspector]
        public char[] m_SelectedLetters;

        [HideInInspector]
        public LetterBox[] m_LetterBoxes;

        [HideInInspector]
        public int m_SelectState = 0;

        [HideInInspector]
        public string[] m_Words;
        [HideInInspector]
        public bool[] m_WordFound;
        [HideInInspector]
        public bool[] m_FirstLetterShown;
        [HideInInspector]
        public bool[] m_WordShown;

        [HideInInspector]
        public Level m_CurrentLevel;

        public Transform m_TableBase;

        public SpriteRenderer m_TableFrameSprite;

        public GameObject m_LetterBoxPrefab;
        public GameObject m_LetterBoxShadowPrefab;

        [HideInInspector]
        public int m_State = 0;

        public const int State_Start = 0;
        public const int State_Game = 1;
        public const int State_Win = 2;
        public const int State_Lose = 3;

        [SerializeField, Space]
        private Contents m_Contents;
        [SerializeField]
        private GameplayData m_GameplayData;

        [SerializeField]
        private DataStorage m_DataStorage;

        public static GameControl Current;

        void Awake()
        {
            Current = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_CurrentLevel = m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels[m_GameplayData.m_LevelNumber];
            m_SelectedBoxes = new List<LetterBox>();
            m_Words = m_CurrentLevel.m_Words;
            m_WordFound = new bool[m_CurrentLevel.m_Words.Length];
            m_FirstLetterShown = new bool[m_CurrentLevel.m_Words.Length];
            m_WordShown = new bool[m_CurrentLevel.m_Words.Length];

            switch (m_CurrentLevel.m_TableSize)
            {
                case 2:
                    CameraControl.Current.GetComponent<Camera>().orthographicSize = 110;
                    break;
                case 3:
                    CameraControl.Current.GetComponent<Camera>().orthographicSize = 115;
                    break;
                case 4:
                    CameraControl.Current.GetComponent<Camera>().orthographicSize = 120;
                    break;
                case 5:
                    CameraControl.Current.GetComponent<Camera>().orthographicSize = 130;
                    break;

                case 6:
                    CameraControl.Current.GetComponent<Camera>().orthographicSize = 140;
                    break;
            }

            CreateTable();

            UISystem.ShowUI("GameUI");

            if (m_GameplayData.m_LevelPackNumber == 0 && m_GameplayData.m_LevelNumber == 0)
            {
                UISystem.ShowUI("TutorialUI_A");
            }
        }

        public void CreateTable()
        {
            int letterCount = m_CurrentLevel.m_TableSize * m_CurrentLevel.m_TableSize;
            m_LetterBoxes = new LetterBox[letterCount];
            int x = 0; int y = 0;

            Vector3 start = new Vector3(40, 40, 0);
            Vector3 sum = Vector3.zero;

            for (int i = 0; i < letterCount; i++)
            {
                GameObject obj = Instantiate(m_LetterBoxPrefab);
                obj.transform.SetParent(m_TableBase);
                obj.transform.localPosition = start + new Vector3(-x * 20, -y * 20, 0);
                sum += obj.transform.localPosition;
                m_LetterBoxes[i] = obj.GetComponent<LetterBox>();
                m_LetterBoxes[i].m_NearBoxes = new List<LetterBox>();



                x++;
                if (x > m_CurrentLevel.m_TableSize - 1)
                {
                    x = 0;
                    y++;
                }
            }

            sum = sum / (float)letterCount;

            for (int i = 0; i < letterCount; i++)
            {
                m_LetterBoxes[i].transform.localPosition -= sum;

                GameObject obj = Instantiate(m_LetterBoxShadowPrefab);
                obj.transform.SetParent(m_TableBase);
                obj.transform.localPosition = m_LetterBoxes[i].transform.localPosition + new Vector3(0, 0, 4);
            }


            char[] characters = m_CurrentLevel.m_Letters.ToCharArray();
            for (int i = 0; i < letterCount; i++)
            {
                m_LetterBoxes[i].m_Character = characters[i];
                m_LetterBoxes[i].m_TextMesh.text = characters[i].ToString();

                if (i % m_CurrentLevel.m_TableSize < m_CurrentLevel.m_TableSize - 1)
                {
                    m_LetterBoxes[i].m_NearBoxes.Add(m_LetterBoxes[i + 1]);
                }

                if (i % m_CurrentLevel.m_TableSize > 0)
                {
                    m_LetterBoxes[i].m_NearBoxes.Add(m_LetterBoxes[i - 1]);
                }

                if (i >= m_CurrentLevel.m_TableSize)
                {
                    m_LetterBoxes[i].m_NearBoxes.Add(m_LetterBoxes[i - m_CurrentLevel.m_TableSize]);
                }

                if (i < (letterCount - m_CurrentLevel.m_TableSize))
                {
                    m_LetterBoxes[i].m_NearBoxes.Add(m_LetterBoxes[i + m_CurrentLevel.m_TableSize]);
                }
            }

            m_TableFrameSprite.size = new Vector2(m_CurrentLevel.m_TableSize * 20 + 20, m_CurrentLevel.m_TableSize * 20 + 20);

            StartCoroutine(Co_StartTalbe());
        }

        IEnumerator Co_StartTalbe()
        {
            for (int i = 0; i < m_LetterBoxes.Length; i++)
            {
                m_LetterBoxes[i].gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < m_LetterBoxes.Length; i++)
            {
                m_LetterBoxes[i].gameObject.SetActive(true);
                BaseScriptAnim.Grow(m_LetterBoxes[i].transform, .1f);
                yield return new WaitForSeconds(.06f);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (m_SelectState == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    LetterBox box = CheckBoxClick(Input.mousePosition);
                    if (box != null)
                    {
                        m_SelectState = 1;
                        m_SelectedBoxes.Clear();
                    }
                }
            }
            else if (m_SelectState == 1)
            {
                if (Input.GetMouseButton(0))
                {
                    LetterBox box = CheckBoxClick(Input.mousePosition);
                    if (box != null && !m_SelectedBoxes.Contains(box))
                    {
                        m_SelectedBoxes.Add(box);
                        box.Select();
                    }
                }
                else
                {
                    HandleSelectionEnd();
                }
            }
        }

        public LetterBox CheckBoxClick(Vector3 clickPos)
        {
            Vector3 pos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapPointAll(pos);
            for (int i = 0; i < colliders.Length; i++)
            {
                LetterBox box = colliders[i].gameObject.GetComponent<LetterBox>();
                if (box != null)
                {
                    return box;
                }
            }

            return null;
        }

        public void HandleSelectionEnd()
        {
            m_SelectedLetters = new char[m_SelectedBoxes.Count];
            for (int i = 0; i < m_SelectedBoxes.Count; i++)
            {
                m_SelectedLetters[i] = m_SelectedBoxes[i].m_Character;
            }

            bool correct = false;
            bool all = true;
            int foundWord = -1;
            for (int i = 0; i < m_Words.Length; i++)
            {
                if (m_Words[i].Length != m_SelectedLetters.Length)
                    continue;

                all = true;
                char[] chars = m_Words[i].ToCharArray();
                for (int j = 0; j < m_Words[i].Length; j++)
                {
                    if (chars[j] != m_SelectedLetters[j])
                    {
                        all = false;
                        break;
                    }
                }

                if (all)
                {
                    correct = true;
                    foundWord = i;
                    break;
                }
            }

            if (correct)
            {
                m_WordFound[foundWord] = true;
                GameUI.m_Main.UpdateWord(foundWord);
                //for (int i = 0; i < m_SelectedBoxes.Count; i++)
                //{
                //    m_SelectedBoxes[i].BreakBox();
                //}
                StartCoroutine(Co_BreakBoxes());
                CameraControl.Current.StartShake(.2f, 10);
                string word1 = m_Words[foundWord];
                GameUI.m_Main.ShowNewWord(word1);

            }
            else
            {
                for (int i = 0; i < m_SelectedBoxes.Count; i++)
                {
                    m_SelectedBoxes[i].Deselect();
                }
                if (m_SelectedBoxes.Count > 1)
                {
                    CameraControl.Current.StartShake(.4f, 5);
                    GameUI.m_Main.ShakeHint();
                }
            }

            bool allwords = true;
            for (int i = 0; i < m_WordFound.Length; i++)
            {
                if (!m_WordFound[i])
                {
                    allwords = false;
                }
            }

            if (allwords)
            {
                StartCoroutine(Co_HandleWin());
            }


            m_SelectState = 0;
        }

        IEnumerator Co_BreakBoxes()
        {
            for (int i = 0; i < m_SelectedBoxes.Count; i++)
            {
                m_SelectedBoxes[i].BreakBox();
                yield return new WaitForSeconds(.1f);
            }
            yield return new WaitForSeconds(.1f);
        }

        public void RestartLevel()
        {
            Invoke("LoadLevelDelayed", 1f);
        }
        public void LoadLevelDelayed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        IEnumerator Co_HandleWin()
        {
            m_GameplayData.m_GameEnded = true;

            yield return new WaitForSeconds(1);

            m_GameplayData.m_LevelNumber++;
            if (m_GameplayData.m_LevelNumber > m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Levels.Length - 1)
            {
                m_GameplayData.m_LevelNumber = 0;

                if (m_GameplayData.m_LevelPackNumber < 3)
                {
                    m_GameplayData.m_LevelPackNumber++;
                    m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_Unlocked = true;
                }
                else
                {
                    m_GameplayData.m_LevelPackNumber = 0;
                }
            }
            else
            {
                if (m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_LastLevel < m_GameplayData.m_LevelNumber)
                {
                    m_Contents.m_LevelPacks[m_GameplayData.m_LevelPackNumber].m_LastLevel = m_GameplayData.m_LevelNumber;

                }
            }
            //CurrentLevel.m_StarCount = 3;



            //CurrentLevel.m_StarCount = Mathf.Clamp(CurrentLevel.m_StarCount, 0, 3);

            //CameraControl.Current.ZoomCamera(460, 2);
            //InGameUI.Current.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);
            //MusicPlayer.Current.StopMusic();
            UISystem.ShowUI("WinUI");


        }

        IEnumerator Co_HandleLose()
        {
            yield return new WaitForSeconds(1);
            UISystem.ShowUI("LoseUI");
            //InGameUI.Current.gameObject.SetActive(false);
        }

        public void UseHint(int num)
        {
            switch (num)
            {
                case 0:
                    for (int i = 0; i < m_WordFound.Length; i++)
                    {
                        if (!m_WordFound[i] && !m_WordShown[i] && !m_FirstLetterShown[i])
                        {
                            GameUI.m_Main.ShowWordLetter(i);
                            m_FirstLetterShown[i] = true;
                            break;
                        }
                    }
                    break;

                case 1:
                    for (int i = 0; i < m_WordFound.Length; i++)
                    {
                        if (!m_WordFound[i] && !m_WordShown[i])
                        {
                            GameUI.m_Main.UpdateWord(i);
                            m_WordShown[i] = true;
                            break;
                        }
                    }
                    break;

                case 2:
                    PassLevel();
                    break;
            }
        }

        public void PassLevel()
        {
            for (int i = 0; i < m_LetterBoxes.Length; i++)
            {
                if (m_LetterBoxes[i].gameObject.activeSelf)
                {
                    m_LetterBoxes[i].BreakBox();
                }
            }

            for (int i = 0; i < m_Words.Length; i++)
            {
                GameUI.m_Main.UpdateWord(i);
            }

            StartCoroutine(Co_HandleWin());

        }

        public void FindWords()
        {
            for (int i = 0; i < m_Words.Length; i++)
            {
                char[] chars = m_Words[i].ToCharArray();
                int counter = 0;
                int nearCounter = 0;

                for (int j = 0; j < m_LetterBoxes.Length; j++)
                {
                    LetterBox startBox = m_LetterBoxes[j];
                    while (counter < chars.Length)
                    {
                        if (chars[counter] == startBox.m_Character)
                        {
                            counter++;
                        }

                        for (int k = 0; k < startBox.m_NearBoxes.Count; k++)
                        {

                        }
                    }

                }
            }
        }
    }
}