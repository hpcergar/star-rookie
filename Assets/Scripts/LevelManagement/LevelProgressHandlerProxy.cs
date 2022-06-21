using UnityEngine;

public class LevelProgressHandlerProxy : MonoBehaviour
{
    private LevelProgressHandler levelProgressHandler;

    private LevelProgressHandler GetLevelProgressHandler()
    {
        if(null != this.levelProgressHandler) {
            return this.levelProgressHandler;
        }

        LevelProgressHandler[] levelProgressHandlers = FindObjectsOfType<LevelProgressHandler>();
        if (levelProgressHandlers.Length == 1) {
            this.levelProgressHandler = levelProgressHandlers[0];
            return this.levelProgressHandler;
        } else {
            throw new System.Exception();
        }
    }

    public void SetCheckpointIndex(int index)
    {
        this.GetLevelProgressHandler().SetCheckpointIndex(index);
    }
}
