
namespace Flappy.Data
{
    [System.Serializable]
    public enum GameState {
        MainMenu,
        Playing,
        GameOver
    }

    [System.Serializable]
    public class BestScoreData
    {
        public int highestScore;
    }
}