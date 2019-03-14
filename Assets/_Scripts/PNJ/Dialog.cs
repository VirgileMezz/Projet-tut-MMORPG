using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Dialog
{
    private string[] texts;
    private int cursor;

    public Dialog() { }

    public Dialog(string[] texts)
    {
        this.texts = texts;
        this.cursor = 1;
    }

    public bool HasNext()
    {
        return (cursor < texts.Length );
    }

    public bool HasPrev()
    {
        return cursor >= 2;
    }

    public string Next()
    {
        int i = cursor;
        cursor++;
        return texts[i];
    }

    public string Prev()
    {
        int i = cursor;
        cursor--;
        return texts[i];
    }

    public void Reset()
    {
        cursor = 1;
    }

    public string Name()
    {
        return this.texts[0];
    }

}
