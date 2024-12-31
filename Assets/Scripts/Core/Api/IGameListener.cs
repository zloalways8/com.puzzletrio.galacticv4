namespace Core.Api
{
    public interface jkdgh {}

    public interface IGameListener : jkdgh {}

    public interface IUpdateListener : IGameListener
    {
        void OnUpdate();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }
}