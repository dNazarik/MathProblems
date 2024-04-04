namespace Gameplay.Common
{
    /// <summary>
    /// Created to decrease dependencies amount in GameController
    /// </summary>
    public class GameUIDataContainer
    {
        public TopPanelView TopPanelView { get; private set; }
        public MiddlePanelView MiddlePanelView { get; private set; }
        public BottomPanelView BottomPanelView { get; private set; }
        public PausePanelView PausePanelView { get; private set; }
        public GameOverView GameOverView { get; private set; }
        public SaveRecordPanelView SaveRecordPanelView { get; private set; }

        public GameUIDataContainer(TopPanelView topPanelView, MiddlePanelView middlePanelView,
            BottomPanelView bottomPanelView, PausePanelView pausePanelView, GameOverView gameOverView,
            SaveRecordPanelView saveRecordPanelView)
        {
            TopPanelView = topPanelView;
            MiddlePanelView = middlePanelView;
            BottomPanelView = bottomPanelView;
            PausePanelView = pausePanelView;
            GameOverView = gameOverView;
            SaveRecordPanelView = saveRecordPanelView;
        }
    }
}