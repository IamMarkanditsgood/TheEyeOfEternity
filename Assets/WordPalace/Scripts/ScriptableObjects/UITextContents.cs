using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JSG.WordPalace
{
    [CreateAssetMenu(fileName = "UITextContents", menuName = "CustomObjects/UITextContents", order = 1)]
    public class UITextContents : ScriptableObject
    {
        [TextAreaAttribute]
        public string[] m_Messages;
        //public Dictionary<string, Sprite> d_Graphics;
    }
}