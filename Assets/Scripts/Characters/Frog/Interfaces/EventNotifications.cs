using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts
{
    public interface INotifyOnDeath
    {
        void OnDeath();
    }

    public interface INotifyOnRestart
    {
        void OnRestart();
    }

    public interface INotifyBeforeRestart
    {
        void BeforeRestart();
    }

    public interface INotifyOnSetback
    {
        void OnSetback();
    }

    public interface INotifyOnAnyRespawn
    {
        void OnAnyRespawn();
    }

    public interface INotifyOnEndLevel
    {
        void OnEndLevel();
    }
}
