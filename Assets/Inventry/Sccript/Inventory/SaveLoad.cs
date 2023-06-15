using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    public static void OnSave<T>(this T data) where T : class
    {
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/save.bytes", FileMode.Create, FileAccess.Write))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
        }
    }

    public static T OnLoad<T>(this T data) where T : class
    {
        try
        {
            using (FileStream fs = new FileStream(Application.persistentDataPath + "/save.bytes", FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return data = bf.Deserialize(fs) as T;
            }
        }
        catch
        {
            Debug.LogError("ÉfÅ[É^Ç™Ç†ÇËÇ‹ÇπÇÒ");
            return data;
        }
    }
}
