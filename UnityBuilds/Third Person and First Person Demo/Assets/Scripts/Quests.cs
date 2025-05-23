using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Quests : MonoBehaviour
{
    [SerializeField] private int zombiesKilled = 0;
   
    [SerializeField] private GameObject backPackMarker;
    [SerializeField] GameObject ellieMarker;
    [SerializeField] GameObject zombiesMarker;
    [SerializeField] GameObject backpackMapMarker;

    [SerializeField] GameObject zombies;
    [SerializeField] GameObject backpackMap;
    [SerializeField] GameObject backPack;

    [SerializeField] GameObject mapImage;
    [SerializeField] GameObject backPackImage;
    [SerializeField] bool tookBackpack;
    [SerializeField] TextMeshProUGUI objectivesText;
    [SerializeField] TextMeshProUGUI ammoText;

    [SerializeField] GameObject interactionText;
    [SerializeField] GameObject cam3;
    [SerializeField] bool interacted;
    [SerializeField] bool triggerActivated;
    [SerializeField] GameObject talkText;
    [SerializeField] GameObject thankYouText;

    enum QuestState
    {
        TalkToEllie,
        FindZombies,
        KillZombies,
        pickUpMap,
        FindBackpack,
        ReturnToEllie,
        Complete
    }

    QuestState myQuest = QuestState.TalkToEllie;

    void Start()
    {
        tookBackpack = false;
        myQuest = QuestState.TalkToEllie;
        ellieMarker.SetActive(true);

        zombies.SetActive(false);
        backpackMap.SetActive(false);
        backPack.SetActive(false);

        zombiesMarker.SetActive(false);
        backPackMarker.SetActive(false);
        backpackMapMarker.SetActive(false);
        backPackImage.SetActive(false);
        mapImage.SetActive(false);
        talkText.SetActive(false);
        thankYouText.SetActive(false);
        
        cam3.SetActive(false);
        interactionText.SetActive(false);
        triggerActivated = false;
        interacted = false;

        Events.FoundEllie.AddListener(FoundEllie);
        Events.FoundZombies.AddListener(FoundZombies);
        Events.KilledZombies.AddListener(KilledZombies);
        Events.FoundBackpack.AddListener(FoundBackpack);
        Events.PickedUpMap.AddListener(PickedUpMap);
        Events.ReloadStuff.AddListener(UpdateAmmo);
    }

    private void Update()
    {
        if (myQuest == QuestState.TalkToEllie)
        {
            objectivesText.text = "Talk to Ellie";
        }
        else if (myQuest == QuestState.FindZombies)
        {
            objectivesText.text = "Find Zombies";
        }
        else if (myQuest == QuestState.KillZombies)
        {
            objectivesText.text = "Kill Zombies";
        }
        else if (myQuest == QuestState.pickUpMap)
        {
            objectivesText.text = "Pick up the Map";
        }
        else if (myQuest == QuestState.FindBackpack)
        {
            objectivesText.text = "Find and Retrieve the Backpack";
        }
        else if (myQuest == QuestState.ReturnToEllie)
        {
            objectivesText.text = "Return the Backpack to Ellie";
        }
        else if(myQuest == QuestState.Complete)
        {
            objectivesText.text = "Quest Completed";
        }

        if (triggerActivated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interacted = true;
                StartCoroutine(TurnOffCameraAfterDelay());
                FoundEllie();
            }
        }
    }
    private void FoundEllie()
    {
        if(tookBackpack)
        {
            triggerActivated = true;
            interactionText.SetActive(true);
            ellieMarker.SetActive(false);
            if (triggerActivated && interacted)
            {
                thankYouText.SetActive(true);
                cam3.SetActive(true);
                interactionText.SetActive(false);
                myQuest = QuestState.Complete;
                backPackImage.SetActive(false);
            }         
        }
        else
        {     
            triggerActivated = true;
            interactionText.SetActive(true);
            ellieMarker.SetActive(false);
            if (triggerActivated && interacted)
            {
                cam3.SetActive(true);
                talkText.SetActive(true);
                interactionText.SetActive(false);              
                myQuest = QuestState.FindZombies;
                zombiesMarker.SetActive(true);
                zombies.SetActive(true);
            } 
        }  
    }

    private void FoundZombies()
    {
        zombiesMarker.SetActive(false);
        myQuest = QuestState.KillZombies;
    }

    private void KilledZombies()
    {
        zombiesKilled++;
        if (zombiesKilled >=3)
        {
            zombies.SetActive(false);
            myQuest = QuestState.pickUpMap;
            backpackMap.SetActive(true);
            backpackMapMarker.SetActive(true);
        }
    }

    private void PickedUpMap()
    {
        backpackMap.SetActive(false);
        backpackMapMarker.SetActive(false);
        myQuest = QuestState.FindBackpack;
        mapImage.SetActive(true);
        backPack.SetActive(true);
        backPackMarker.SetActive(true);
    }

    private void FoundBackpack()
    {
        backPackImage.SetActive(true);
        myQuest = QuestState.ReturnToEllie;
        backPack.SetActive(false);
        backPackMarker.SetActive(false);
        ellieMarker.SetActive(true);
        RawImage ellieImage = ellieMarker.GetComponentInChildren<RawImage>();
        if (ellieImage != null)
        {
            ellieImage.enabled = true;
        }
        tookBackpack = true;
    }

    private void UpdateAmmo(int x)
    {
        ammoText.text = x.ToString();
    }

    private IEnumerator TurnOffCameraAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        cam3.SetActive(false);
        interacted = false;
        triggerActivated = false;
        thankYouText.SetActive(false);
        talkText.SetActive(false);
    }
}
