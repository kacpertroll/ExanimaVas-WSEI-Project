using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton

    public TextMeshProUGUI textMessage;
    public AudioSource soundSource;
    public RawImage background;
    public GameObject displayMessage;
    public Coroutine currentCoroutine;

    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 1.0f;

    private Queue<string> messageQueue = new Queue<string>(); // Kolejka puszczanych wiadomosci po u�yciu CommentDelayMessage
    public bool isMessageDisplaying = false; // Sprawdza, czy jest aktualnie wy�wietlana wiadomo��

    void Awake()
    {
        // Ustawia na start singletona jako instancj�
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
    }

    // Tego u�ywamy jako pierwsz� wiadomo�c w kolejce. To tworzy kolejk� aktualnych wiadomo�ci, kt�r� kontynuujemy poprzez u�yhcie CommentDelayMessage
    public void CommentMessage(string message)
    {
        // Na start czy�cimy kolejke, niepotrzebne jako� bardzo, ale na wszelki.
        messageQueue.Clear();
        if (!isMessageDisplaying)
        {
            messageQueue.Enqueue(message);

            // Jak nie ma �adnej innej wiadomo�ci wy�wietlanej to zaczyna wy�wietla� aktualn� kolejkl� wiadomo�ci
            if (!isMessageDisplaying)
            {
                StartCoroutine(DisplayNextMessage());
            }
        }
    }

    private IEnumerator DisplayNextMessage()
    {
        while (messageQueue.Count > 0)
        {
            isMessageDisplaying = true;
            string message = messageQueue.Dequeue(); // Kolejna wiadomo�c w kolejce

            // Wywo�uje korutyne, meessage to wiadomo�� a na drugim miejscu czas wy�wietlania wiadomo�ci
            yield return StartCoroutine(DisplayMessageRoutine(message, 2f));
        }
        isMessageDisplaying = false; // Kasujemy wy�wietlanie
    }

    // wywo�anie DelayedCommentMessageRoutine
    public void CommentDelayMessage(string message)
    {
        StartCoroutine(DelayedCommentMessageRoutine(message));
    }

    private IEnumerator DelayedCommentMessageRoutine(string message)
    {
        // Delayed czeka na zako�czenie aktualnej korutyny
        if (currentCoroutine != null)
        {
            yield return currentCoroutine;
        }

        messageQueue.Enqueue(message);

        // Jak nie ma �adnej innej wiadomo�ci wy�wietlanej to zaczyna wy�wietla� aktualn� kolejkl� wiadomo�ci
        if (!isMessageDisplaying)
        {
            StartCoroutine(DisplayNextMessage());
        }
    }

    private IEnumerator DisplayMessageRoutine(string message, float displayTime)
    {
        textMessage.text = "";
        textMessage.alpha = 1;
        displayMessage.SetActive(true);
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0.3f);

        // I cyk jeb jeb jeb z dzidy laserowej litera po literze
        foreach (char c in message)
        {
            textMessage.text += c;
            soundSource.pitch = Random.Range(minPitch, maxPitch);
            soundSource.Play();
            if (c.Equals(',') || c.Equals('.') || c.Equals('?') || c.Equals('!'))
            {
                yield return new WaitForSeconds(0.4f);
            }
            else yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(displayTime);


        // No i po zako�czeniu p�ynny lerpik do prze�roczysto�ci
        float elapsedTime = 0f;
        float fadeDuration = 0.2f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            textMessage.alpha = alpha;
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // a tu zerujemy ju� tekst i wy��czamy aktywno�� wiadomo�ci.
        displayMessage.SetActive(false);
        textMessage.text = "";
    }
}