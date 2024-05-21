using System;
using System.Collections;
using UnityEngine;

namespace MageVsMonsters.Extensions
{
    public static class MonoBehaviourExtensions
    {
        [HideInCallstack]
        public static Coroutine InvokeActionAfterFrames(this MonoBehaviour monoBehaviour,
            Action action, int framesCount)
        {
            var invokeActionAfterFirstFrameCoroutine =
                monoBehaviour.StartCoroutine(InvokeActionAfterFramesCoroutine(action, framesCount));

            return invokeActionAfterFirstFrameCoroutine;
        }

        [HideInCallstack]
        private static IEnumerator InvokeActionAfterFramesCoroutine(Action action, int framesCount)
        {
            for (int i = 0; i < framesCount; i++)
            {
                yield return new WaitForEndOfFrame();
            }

            action?.Invoke();
        }
    }
}
