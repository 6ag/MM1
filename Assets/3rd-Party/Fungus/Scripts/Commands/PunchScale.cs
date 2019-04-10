// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

namespace Fungus
{
    /// <summary>
    /// Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.
    /// </summary>
    [CommandInfo("iTween", 
                 "Punch Scale", 
                 "Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class PunchScale : iTweenCommand
    {
        [Tooltip("A scale offset in space the GameObject will animate to")]
        [SerializeField] protected Vector3Data _amount;

        #region Public members

        public override void DoTween()
        {
            Hashtable tweenParams = new Hashtable();
            tweenParams.Add("name", _tweenName.Value);
            tweenParams.Add("amount", _amount.Value);
            tweenParams.Add("time", _duration.Value);
            tweenParams.Add("easetype", easeType);
            tweenParams.Add("looptype", loopType);
            tweenParams.Add("oncomplete", "OniTweenComplete");
            tweenParams.Add("oncompletetarget", gameObject);
            tweenParams.Add("oncompleteparams", this);
            iTween.PunchScale(_targetObject.Value, tweenParams);
        }

        #endregion

        #region Backwards compatibility

        [HideInInspector] [FormerlySerializedAs("amount")] public Vector3 amountOLD;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (amountOLD != default(Vector3))
            {
                _amount.Value = amountOLD;
                amountOLD = default(Vector3);
            }
        }

        #endregion
    }
}