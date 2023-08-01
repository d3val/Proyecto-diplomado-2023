using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessagesFromList : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI showedText;
    [SerializeField] Image showedImage;
    [SerializeField] TextMeshProUGUI paginatedLabel;
    int index = 0;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] List<MessageContainer> messagges;
    private void OnEnable()
    {
        index = 0;
        showedText.text = messagges[index].message;
        showedImage.sprite = messagges[index].imageMessage;
        rightButton.SetActive(true);
        leftButton.SetActive(false);
        UpdatePaginated();
    }

    public void ShowNextMessage()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(true);

        index++;
        if (index >= messagges.Count)
        {
            index--;
            return;
        }

        if (index == messagges.Count - 1)
            rightButton.SetActive(false);

        showedText.text = messagges[index].message;
        showedImage.sprite = messagges[index].imageMessage;
        UpdatePaginated();
    }

    public void ShowPreviousMessage()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        index--;
        if (index < 0)
        {
            index++;
            return;
        }

        if (index == 0)
            leftButton.SetActive(false);

        showedText.text = messagges[index].message;
        showedImage.sprite = messagges[index].imageMessage;
        UpdatePaginated();
    }

    public void UpdatePaginated()
    {
        paginatedLabel.text = index + 1 + " / " + messagges.Count;
    }

    [Serializable]
    public class MessageContainer
    {
        [TextArea] public string message = "";
        public Sprite imageMessage = null;
        public MessageContainer(string str, Sprite img)
        {
            message = str;
            imageMessage = img;
        }
    }
}
