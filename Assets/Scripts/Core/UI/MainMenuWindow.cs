using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _rulesButton;
        [SerializeField] private Button _privacyButton;

        protected override void OnAwake()
        {
            _playButton?.onClick.AddListener(OpenLevelMenu);
            _settingsButton?.onClick.AddListener(OpenSettings);
            _rulesButton?.onClick.AddListener(OpenRules);
            _privacyButton?.onClick.AddListener(OpenPrivacy);
        }

        protected override void Close()
        {
            var exitWindow = fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<ExitWindow>();
            exitWindow.SetPrevWindow(this);
            
            CloseWindow();
        }

        private void OpenSettings()
        {
            var settings = fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<SettingsWindow>();
            settings.SetPrev(this);

            CloseWindow();
        }

        private void OpenPrivacy()
        {
            LongTextWindow textWindow = fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<LongTextWindow>();
            textWindow.Label = fghjjdfh.dfghjjdfgh<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Privacy).Label;
            textWindow.Text = fghjjdfh.dfghjjdfgh<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Privacy).Text;
            textWindow.SetPrevWindow(this);

            CloseWindow();
        }

        private void OpenRules()
        {
            LongTextWindow textWindow = fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<LongTextWindow>();
            textWindow.Label = fghjjdfh.dfghjjdfgh<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Rules).Label;
            textWindow.Text = fghjjdfh.dfghjjdfgh<TextAtlas>().Map.First(text => text.TypeId == TextAtlas.TextType.Rules).Text;
            textWindow.SetPrevWindow(this);

            CloseWindow();
        }

        private void OpenLevelMenu()
        {
            fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<LevelMenuWindow>();

            CloseWindow();
        }

        private void CloseWindow() => gameObject.SetActive(false);
    }
}