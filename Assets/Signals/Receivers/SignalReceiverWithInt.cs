using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Events;
using UnityEngine.Playables;
using System;
using System.Linq;

public class SignalReceiverWithInt : MonoBehaviour, INotificationReceiver
{
    public SignalAssetEventPair[] signalAssetEventPairs;

    [Serializable]
    public class SignalAssetEventPair
    {
        public SignalAsset signalAsset;
        public ParameterizedEvent events;

        [Serializable]
        public class ParameterizedEvent : UnityEvent<int> { }
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is ParameterizedEmitter<int> intEmitter)
        {
            var output = JsonUtility.ToJson(this.signalAssetEventPairs[0], true);
            var matches = signalAssetEventPairs.Where(x => ReferenceEquals(x.signalAsset, intEmitter.asset));
            foreach( var m in matches)
            {
                Debug.Log("Match " + m.ToString());
                m.events.Invoke(intEmitter.parameter);
            }
        }
    }
}