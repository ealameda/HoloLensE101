using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour {

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Use this for initialization
    void Start() {
        keywords.Add("Reset world", () =>
        { 
            // call the onReset method on every descendent object.
            this.BroadcastMessage("OnReset");
        });

        keywords.Add("Drop Sphere", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                // call the onDrop method on just the focused object
                focusObject.SendMessage("OnDrop");
            }
        });

        // tell the keyword recognizer about our words
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // register a callback for the keyword recognizer and start recognizing
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
 
    }
	
}
