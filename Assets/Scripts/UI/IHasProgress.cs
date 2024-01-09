using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;//进度条事件
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
