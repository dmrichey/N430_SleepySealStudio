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
        new Text(1, "While the planet’s air is breathable, the temperature of the planet is proving problematic. Current temperature measurements show a trend of approximately -10c during the day. During the night these temperatures will drop to as low as -40c.", true),
        new Text(2, "Even worse, it appears that during the winter cycle of the orbit these temperatures will drop past even that.", false),
        new Text(3, "As the winter approaches, the excavation project continues well. Despite the robots, we are still having to use human labor in order to meet the necessary timeline. Based on our current rate, we will have a large enough excavation in order to house all 6500 colonists.", true),
        new Text(4, "Once the winter hits, we will cease excavation but plans are being discussed to continue excavations in order to create a space large enough to use as a permanent city.", false),
        new Text(5, "A local man in our assembly plant has passed away after falling into the line. He was operating the rivet machine during the incident. He received injuries incompatible with life.", true),
        new Text(6, "During the 4 days preceding the incident, he was reportedly acting increasingly erratic, zoning out for minutes at a time as well as demonstrating issues in motor control.", false),
        new Text(7, "“Mother and father have left. I asked them why they did not take me. Father cried. Mother walked away from me. She walked and hugged a strange metal box. She did not look at me after that.”", false),
        new Text(8, "I miss the sky. Even in the warmer seasons, we seldom stray far from the city. There is an entire other half of the planet that no human has ever set foot on. We have journeyed from Earth, yet we refuse to even explore past the land that our children have ever set foot.", false),
        new Text(9, "“I want to explore but I can’t leave the city. I can’t even see the stars, let alone visit them. I read the list of our charted stars. There are so many of them yet I will never see any of them. Why could I not have been born on one of them?”", false),
        new Text(10, "I can’t sleep. It’s been 4 days now. My eyes hurt. I close them and lay down but no sleep comes. I cannot feel my hand. It feels like it has gone to sleep and won’t wake up. I wish I could join it.", false),
        new Text(11, "The number of insomnia cases has been rapidly increasing. Cases are being quarantined but given the widespread distribution of the cases, I question the effectiveness of a quarantine at this point. Of the observed patients, it appears that the insomnia abates after 4 to 5 days.", true),
        new Text(12, "The entire concern appears to be a reaction to the initial case that resulted in a death. While it certainly isn’t good for the patients, proper medical attention can help get them through the period of insomnia.", false),
        new Text(13, "“Everyone has gone now. Why did they leave me behind? I miss Mother and Father. Why won’t you come back for me?”", false),
        new Text(14, "The queen was permitted to leave quarantine today. She required a cane to walk to visit Rosa today. The disease has reached her left leg. The queen winced when Rosa hugged it but Rosa didn’t seem to notice the queen's reaction. Rosa’s infection still has not spread past her hand.", false),
        new Text(15, "There has been no conclusive evidence of insomnia being contagious between humans, neither by air nor contact. Initial theories suggested that it could be environmental but there is no trend of cases by location. We are still baffled as to the root of this disease that is plaguing us.", false),
        new Text(16, "“I know that I was left behind but I can no longer remember who. My memories are fading...”", false),
        new Text(17, "Previously thought recovered patients are now experiencing degeneration in the nervous system, starting from the extremities. Necrosis starts a variable amount of time after the degeneration starts, ranging from 7 to 21 days.", true),
        new Text(18, "We have attempted amputations of the affected limbs but have achieved inconsistent results.", false),
        new Text(19, "“Why am I here”", false)
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
