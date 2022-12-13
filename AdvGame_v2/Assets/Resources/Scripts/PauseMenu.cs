using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public GameObject pauseScreen;
    public GameObject buttonScreen;
    public GameObject journalScreen;
    public GameObject[] journalEntries;
    public GameObject pauseMenu;
    GameObject player;

    public struct JournalEntry
    {
        public JournalEntry(int id, string text)
        {
            entryID = id;
            entryText = text;
        }

        public int entryID { get; }
        public string entryText { get;  }

    }

    JournalEntry[] library =
    {
        new JournalEntry(0, "While the planet’s air is breathable, the temperature of the planet is proving problematic. Current temperature measurements show a trend of approximately -10c during the day. During the night these temperatures will drop to as low as -40c. Even worse, it appears that during the winter cycle of the orbit these temperatures will drop past even that."),
        new JournalEntry(1, "As the winter approaches, the excavation project continues well. Despite the robots, we are still having to use human labor in order to meet the necessary timeline. Based on our current rate, we will have a large enough excavation in order to house all 6500 colonists. Once the winter hits, we will cease excavation but plans are being discussed to continue excavations in order to create a space large enough to use as a permanent city."),
        new JournalEntry(2, "I miss the sky. Even in the warmer seasons, we seldom stray far from the city. There is an entire other half of the planet that no human has ever set foot on. We have journeyed from Earth, yet we refuse to even explore past the land that our children have ever set foot."),
        new JournalEntry(3, "A local man in our assembly plant has passed away after falling into the line. He was operating the rivet machine during the incident. He received injuries incompatible with life. During the 4 days preceding the incident, he was reportedly acting increasingly erratic, zoning out for minutes at a time as well as demonstrating issues in motor control."),
        new JournalEntry(4, "I can’t sleep. It’s been 4 days now. My eyes hurt. I close them and lay down but no sleep comes. I cannot feel my hand. It feels like it has gone to sleep and won’t wake up. I wish I could join it."),
        new JournalEntry(5, "The number of insomnia cases has been rapidly increasing. Cases are being quarantined but given the widespread distribution of the cases,  I question the effectiveness of a quarantine at this point. Of the observed patients, it appears that the insomnia abates after 4 to 5 days. The entire concern appears to be a reaction to the initial case that resulted in a death. While it certainly isn’t good for the patients, proper medical attention can help get them through the period of insomnia."),
        new JournalEntry(6, "There has been no conclusive evidence of insomnia being contagious between humans, neither by air nor contact. Initial theories suggested that it could be environmental but there is no trend of cases by location. We are still baffled as to the root of this disease that is plaguing us."),
        new JournalEntry(7, "The queen was permitted to leave quarantine today. She required a cane to walk to visit Rosa today. The disease has reached her left leg. The queen winced when Rosa hugged it but Rosa didn’t seem to notice the queen's reaction. Rosa’s infection still has not spread past her hand."),
        new JournalEntry(8, "Previously thought recovered patients are now experiencing degeneration in the nervous system, starting from the extremities. Necrosis starts a variable amount of time after the degeneration starts, ranging from 7 to 21 days. We have attempted amputations of the affected limbs but have achieved inconsistent results."),
        new JournalEntry(9, "“Mother and father have left. I asked them why they did not take me. Father cried. Mother walked away from me. She walked and hugged a strange metal box. She did not look at me after that.”"),
        new JournalEntry(10, "“I want to explore but I can’t leave the city. I can’t even see the stars, let alone visit them. I read the list of our charted stars. There are so many of them yet I will never see any of them. Why could I not have been born on one of them?”"),
        new JournalEntry(11, "“Everyone has gone now. Why did they leave me behind? I miss Mother and Father. Why won’t you come back for me?”"),
        new JournalEntry(12, "“I know that I was left behind but I can no longer remember who. My memories are fading...”"),
        new JournalEntry(13, "“Why am I here”"),
    };
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(pauseMenu);
    }

    public void FoundEntry(int id)
    {
        journalEntries[id].SetActive(true);
    }

    public void PauseGame(GameObject player)
    {
        this.player = player;

        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        journalScreen.SetActive(false);
        buttonScreen.SetActive(true);
        pauseScreen.SetActive(false);

        player.GetComponentInParent<TopDownController>().EnableMovement();
    }

    public void OpenJournal()
    {
        buttonScreen.SetActive(false);
        journalScreen.SetActive(true);
    }

    public void ShowText(int id)
    {
        textBox.text = library[id].entryText;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
