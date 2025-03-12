using JSG.WordPalace.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JSG.WordPalace
{
    public class LetterBox : MonoBehaviour
    {
        public SpriteRenderer m_MainSprite;

        public Sprite[] m_StateSprites;

        public char m_Character;

        public TextMesh m_TextMesh;

        public List<LetterBox> m_NearBoxes;

        public GameObject m_Particle1;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Select()
        {
            m_MainSprite.sprite = m_StateSprites[1];
        }

        public void Deselect()
        {
            m_MainSprite.sprite = m_StateSprites[0];
        }

        public void BreakBox()
        {
            GameObject obj = Instantiate(m_Particle1);
            obj.transform.position = transform.position + new Vector3(0, 0, -2);
            Destroy(obj, 5);

            gameObject.SetActive(false);
        }
    }
}