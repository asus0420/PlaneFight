using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEditor;
public class FileReadWirte : MonoBehaviour
{
    private string _path;
    private static FileReadWirte instance;
    public static FileReadWirte Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        _path = Application.persistentDataPath +"/GameInfo.xml";
        CreateFile();
    }
    void CreateFile()
    {
        if ( !File.Exists(_path) )
        {
            File.Create( _path  );
        }
    }
    public void WriteFileOfScore(float score)
    {
        XmlDocument gameInfo = new XmlDocument();
        XmlDeclaration xmlDex = gameInfo.CreateXmlDeclaration( "1.0" , "UTF-8" , "yes" );
        gameInfo.AppendChild( xmlDex );
        XmlElement xmlEle = gameInfo.CreateElement( "历史最高" );
        gameInfo.AppendChild( xmlEle );
        xmlEle.InnerText = score.ToString();
        gameInfo.Save( _path );
    }

    public float ReadFileOfScore()
    {
        XmlDocument gameInfo  = new XmlDocument();
        gameInfo.Load( _path );
        XmlNode scoreNode  = gameInfo.SelectSingleNode( "历史最高" );
        return float.Parse( scoreNode.InnerText );
    }

    public void CheckScore()
    {
        try
        {
            ReadFileOfScore();
        }
        catch 
        {
            WriteFileOfScore(0);
        }
    }
}
