using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GameWindow : BaseWindow
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _rulesButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _levelText;

        private fghkds _fghkds;
        private tyk _tyk;
        private LevelScoreConstraints _constraints;
        private ghtyk _level;
        private bool _isPaused;

        public bool IsPaused
        {
            get => _isPaused;
            private set
            {
                fghjjdfh.dfghjjdfgh<UpdateProcessor>().SetPauseState(isPaused: value);
                _isPaused = value;
            }
        }

        public int LevelText
        {
            set
            {
                if (_levelText != null) _levelText.text = $"level {value}";
            }
        }

        public float Time
        {
            set
            {
                int minutes = (int)value / 60;
                int seconds = Mathf.Clamp((int)value % 60, 0, 59);

                if (seconds < 10)
                    _timeText.text = $"{minutes}:0{seconds}";
                else
                    _timeText.text = $"{minutes}:{seconds}";
            }
        }

        public int Score
        {
            set
            {
                //int neededScore = _constraints.Map.First(pair => pair.LevelId.Equals(_level.CurrentLevelIndex)).Score;
                _scoreText.text = $"{value}";
            }
        }

        public void Unpause()
        {
            IsPaused = false;
            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(true);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _settingsButton?.onClick.AddListener(OpenSettings);
            _pauseButton?.onClick.AddListener(Pause);
            _rulesButton?.onClick.AddListener(OpenRules);
            _fghkds = fghjjdfh.dfghjjdfgh<fghkds>();
            _tyk = fghjjdfh.dfghjjdfgh<tyk>();
            _fghkds.OnValueChanged += SetFghkds;
            _constraints = fghjjdfh.dfghjjdfgh<LevelScoreConstraints>();
            _level = fghjjdfh.dfghjjdfgh<ghtyk>();
            Score = 0;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            Time = 90 - _tyk.Current;
        }

        protected override void Close()
        {
            fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<LevelMenuWindow>();
            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(false);

            gameObject.SetActive(false);
        }

        private void Pause()
        {
            IsPaused = true;

            fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<PauseWindow>();
            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(false);

            gameObject.SetActive(false);
        }

        private void OpenSettings()
        {
            IsPaused = true;

            fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<SettingsWindow>();
            fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Get<SettingsWindow>().SetPrev(this);
            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(false);

            gameObject.SetActive(false);
        }

        private void OpenRules()
        {
            IsPaused = true;

            var textWindow = fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<LongTextWindow>();
            textWindow.Label = fghjjdfh.dfghjjdfgh<TextAtlas>().Map
                .First(text => text.TypeId == TextAtlas.TextType.Rules).Label;
            textWindow.Text = fghjjdfh.dfghjjdfgh<TextAtlas>().Map
                .First(text => text.TypeId == TextAtlas.TextType.Rules).Text;
            textWindow.SetPrevWindow(this);

            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(false);
            gameObject.SetActive(false);
        }

        private void SetFghkds(int val)
        {
            Score = val;
        }
    }
}