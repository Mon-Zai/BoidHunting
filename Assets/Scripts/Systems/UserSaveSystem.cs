using System.IO;
using UnityEngine;

public static class UserSaveSystem
{
    private const string FileName = "user_save.json";

    private static string SavePath
    {
        get { return Path.Combine(Application.persistentDataPath, FileName); }
    }

    public static void Save(User user)
    {
        if (user == null)
        {
            Debug.LogError("UserSaveSystem.Save: user is null.");
            return;
        }

        UserData data = user.ToData();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static bool TryLoad(User user)
    {
        if (user == null)
        {
            Debug.LogError("UserSaveSystem.TryLoad: user is null.");
            return false;
        }

        if (!File.Exists(SavePath))
        {
            return false;
        }

        string json = File.ReadAllText(SavePath);
        UserData data = JsonUtility.FromJson<UserData>(json);

        if (data == null)
        {
            Debug.LogError("UserSaveSystem.TryLoad: invalid JSON.");
            return false;
        }

        user.LoadFromData(data, true);
        return true;
    }

    public static bool HasSave()
    {
        return File.Exists(SavePath);
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }
}