// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using System.Collections;

namespace Fungus
{
    /// <summary>
    /// Do multiple say and portrait commands in a single block of text. Format is: [character] [portrait] [stage position] [hide] [<<< | >>>] [clear | noclear] [wait | nowait] [fade | nofade] [: Story text].
    /// </summary>
    [CommandInfo("Narrative", 
                 "Conversation",
                 "Do multiple say and portrait commands in a single block of text. Format is: [character] [portrait] [stage position] [hide] [<<< | >>>] [clear | noclear] [wait | nowait] [fade | nofade] [: Story text]")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class Conversation : Command
    {
        [SerializeField] protected StringDataMulti conversationText;

        protected ConversationManager conversationManager = new ConversationManager();

        [SerializeField] protected BooleanData clearPrevious = new BooleanData(true);
        [SerializeField] protected BooleanData waitForInput = new BooleanData(true);
        [Tooltip("a wait for seconds added to each item of the conversation.")]
        [SerializeField] protected FloatData waitForSeconds = new FloatData(0);
        [SerializeField] protected BooleanData fadeWhenDone = new BooleanData(true);

        protected virtual void Start()
        {
            conversationManager.PopulateCharacterCache();
        }

        protected virtual IEnumerator DoConversation()
        {
            var flowchart = GetFlowchart();
            string subbedText = flowchart.SubstituteVariables(conversationText.Value);

            conversationManager.ClearPrev = clearPrevious;
            conversationManager.WaitForInput = waitForInput;
            conversationManager.FadeDone = fadeWhenDone;
            conversationManager.WaitForSeconds = waitForSeconds;

            yield return StartCoroutine(conversationManager.DoConversation(subbedText));

            Continue();
        }

        #region Public members

        public override void OnEnter()
        {
            StartCoroutine(DoConversation());
        }

        public override string GetSummary()
        {
            return conversationText.Value;
        }

        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion
    }
}