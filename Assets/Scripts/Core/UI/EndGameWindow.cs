using System.Collections;
using System.Collections.Generic;
using Core.PlayerInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class EndGameWindow : BaseWindow
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private List<GameObject> _activeOnWin;
        [SerializeField] private List<GameObject> _activeOnLose;

        private vbcm.GameResult _result;
        private BaseWindow _prev;
        private bool _isLastLevel;

        public string Label
        {
            get => _label.text;
            set => _label.text = value;
        }

        public void SetResult(vbcm.GameResult res)
        {
            if (res == vbcm.GameResult.Win)
                SetWin();
            else
                SetLose();

            foreach (var o in _activeOnWin)
            {
                o.SetActive(res == vbcm.GameResult.Win);
            }
            foreach (var o in _activeOnLose)
            {
                o.SetActive(res == vbcm.GameResult.Lose);
            }
        }

        public string Score
        {
            get => _scoreText.text;
            set => _scoreText.text = $"{value}";
        }

        public string ButtonText
        {
            get => _buttonText.text;
            set => _buttonText.text = value;
        }

        public void Init(vbcm.GameResult res, bool isLastLevel)
        {
            _result = res;
            _isLastLevel = isLastLevel;
            StartCoroutine(HideFieldRoutine());
        }

        public void SetPrev(BaseWindow prev)
        {
            _prev = prev;
        }

        protected override void OnAwake()
        {
            _menuButton?.onClick.AddListener(OpenMenu);
            _nextLevelButton?.onClick.AddListener(OpenLevel);
            _settingsButton?.onClick.AddListener(OpenSettings);
        }

        protected override void OnEnableWindow()
        {
            StartCoroutine(HideFieldRoutine());
        }

        private IEnumerator HideFieldRoutine()
        {
            yield return null;
            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(false);
        }

        private void OpenMenu()
        {
            fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<LevelMenuWindow>();
            Close();
        }
        
        private void OpenSettings()
        {
            var settings = fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<SettingsWindow>();
            settings.SetPrev(this);

            Close();
        }

        private void SetLose()
        {
            Label = "Lose";
            ButtonText = "Repeat";
        }

        private void SetWin()
        {
            Label = "you\nwin!";
            ButtonText = "Next level";
        }

        private void OnEnable()
        {
            fghjjdfh.dfghjjdfgh<hdfgj>().Disable();
            StartCoroutine(ClosePrefRoutine());
        }

        private void OnDisable()
        {
            fghjjdfh.dfghjjdfgh<hdfgj>().Enable();
        }

        private IEnumerator ClosePrefRoutine()
        {
            yield return null;

            _prev.gameObject.SetActive(false);
        }

        private void OpenLevel()
        {
            var loader = fghjjdfh.dfghjjdfgh<ghtyk>();

            if (_result == vbcm.GameResult.Lose)
            {
                loader.hygktfg(loader.CurrentLevelIndex);
                _prev.gameObject.SetActive(true);
                Close();
                // ServiceLocator.Get<Score>().Reset();
                // ServiceLocator.Get<Timer>().Reset();
            }
            else
            {
                if (_isLastLevel)
                {
                    fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<LevelMenuWindow>();
                    Close();
                    return;
                }

                loader.hygktfg(loader.CurrentLevelIndex + 1);
                if (_prev is GameWindow gameWindow)
                {
                    gameWindow.LevelText = loader.CurrentLevelIndex + 1;
                }

                _prev.gameObject.SetActive(true);
                Close();
            }
        }
    }
}