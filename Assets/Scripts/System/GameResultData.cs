using UnityEngine;

public static class GameResultData
{
    public static bool IsClear { get; private set; }
    public static string ResultTitle { get; private set; } = "No RESULT";
    public static string ResultMessage { get; private set; } = "No Result Data";

    public static void SetClearResult(string message)
    {
        IsClear = true;
        ResultTitle = "MISSION CLEAR";
        ResultMessage = message;
    }

    public static void SetGameOverResult(string message)
    {
        IsClear = false;
        ResultTitle = "GAME OVER";
        ResultMessage = message;
    }

    public static void ResetResult()
    {
        IsClear = false;
        ResultTitle = "NO RESULT";
        ResultMessage = "No Game Result";
    }
}
