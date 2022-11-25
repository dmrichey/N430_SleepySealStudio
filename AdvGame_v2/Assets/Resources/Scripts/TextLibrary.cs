using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLibrary : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    public struct Text {
        public Text(int id, string textContents, bool containsNext)
        {
            ID = id;
            TC = textContents;
            CN = containsNext;
        }

        public int ID { get; }
        public string TC { get; }
        public bool CN { get; }
    }

    Text[] library = {
        new Text(0, "No Listed Text", false),
        new Text(1, "Test Element 1", false),
        new Text(2, "Test Element 2a", true),
        new Text(3, "Test Element 2b", false)
    };

    Text currentText;

    public void DisplayText(int itemID)
    {
        // Search Library for Text with itemID
        currentText = library[0];
        for (int i = 0; i < library.Length; i++) {
            if (library[i].ID == itemID) {
                currentText = library[i];
                i = library.Length;
            }
        }
        
        Debug.Log(currentText.TC);

        // Set TextMeshPro Text = currentText.TC

        textBox.text = currentText.TC;
    }

    public bool ProgressText()
    {
        if (currentText.CN) {
            DisplayText(currentText.ID + 1);
            return true;
        } else {
            return false;
        }
    }

    

}
