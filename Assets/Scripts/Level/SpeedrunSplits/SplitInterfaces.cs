using System.Collections;
using UnityEngine;
using FrogScripts;

namespace LevelScripts
{
    interface INotifyOnReachedSplit
    {
        void OnReachedSplit();
    }

    public interface ISplitReferencer
    {
        void ReachedSplit();
        Frog Frog { get; set; }
    }
}