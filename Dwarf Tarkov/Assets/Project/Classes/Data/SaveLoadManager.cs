using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface SaveLoadManager<T> where T : class
{
    public void Save(T obj);

    public T Load();
}
