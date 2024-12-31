using System;
using System.Collections.Generic;
using System.Linq;
using Core.Api;
using Core.PlayerInput;
using Core.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class dfgshdfghs : jkdgh
    {
        private readonly hdfgj _input;
        private readonly FieldController _field;
        private bool _playerControl;

        public dfgshdfghs()
        {
            _input = fghjjdfh.dfghjjdfgh<hdfgj>();
            _field = fghjjdfh.dfghjjdfgh<FieldController>();

            _input.OnCellClicked += fghmk;
            _input.OnDeselected += uyk;

            _playerControl = true;

            _field.OnAnimationStateStarted += kluy;
            _field.OnAnimationStateEnded += dfghjj;
        }

        public void OnTurn()
        {
            _playerControl = true;
        }

        private void dfghjj()
        {
            OnTurn();
        }

        private void kluy()
        {
            _playerControl = false;
        }

        private void uyk()
        {
            if (!_playerControl)
                return;

            _field.Deselect();
        }

        private void fghmk(Cell clicked)
        {
            if (!_playerControl)
                return;

            _field.OnCellClicked(clicked);
        }
    }

    public class tytd : jkdgh
    {
        private const int InitialAmount = 5;

        private readonly Cell _reference;
        private readonly List<Cell> _pool = new List<Cell>();

        public tytd(Cell reference)
        {
            _reference = reference;
            for (int i = 0; i < InitialAmount; i++)
            {
                Cell instance = Object.Instantiate(reference);
                instance.gameObject.SetActive(false);

                _pool.Add(instance);
            }
        }

        public void fgj(Cell cell)
        {
            cell.Type = CellAtlas.CellType.None;
            cell.gameObject.SetActive(false);
            cell.Position = Vector2Int.one * -10;
        }

        public Cell dfghj()
        {
            if (_pool.Count == 0)
                return Object.Instantiate(_reference);

            Cell instance = _pool[0];
            _pool.RemoveAt(0);
            instance.gameObject.SetActive(true);

            return instance;
        }
    }

    public class ghtyk : jkdgh
    {
        private readonly FieldController _field;
        private readonly tyk _tyk;
        private readonly fghkds _fghkds;
        private readonly UpdateProcessor _processor;
        private rtuyitk _listener;

        public int CurrentLevelIndex { get; private set; }


        public event Action OnLoad;

        public ghtyk()
        {
            _field = fghjjdfh.dfghjjdfgh<FieldController>();
            _tyk = fghjjdfh.dfghjjdfgh<tyk>();
            _fghkds = fghjjdfh.dfghjjdfgh<fghkds>();
            _processor = fghjjdfh.dfghjjdfgh<UpdateProcessor>();
        }

        public void hygktfg(int index)
        {
            CurrentLevelIndex = index;

            _tyk.rftyj();
            _processor.SetPauseState(isPaused: false);
            _field.SetFieldVisibility(isVisible: true);
            _field.OnNewLevel();
            _tyk.fghmfgh();
            _fghkds.ghykfgh();
            _listener.ghbmghv();

            OnLoad?.Invoke();
        }

        public void tyjj(rtuyitk listener)
        {
            _listener = listener;
        }
    }

    public class fghkds : jkdgh
    {
        private const float MatchExtraCellLinearModifier = 1.1f;
        private const int DefaultMatchValue = 100;

        private int _score;

        public int fyjdfyjty => _score;

        public event Action<int> OnValueChanged;

        public void fghjmdf()
        {
            _score += DefaultMatchValue;

            OnValueChanged?.Invoke(_score);
        }

        public void ghykfgh()
        {
            _score = 0;
            OnValueChanged?.Invoke(_score);
        }
    }

    public class tyk : IUpdateListener
    {
        private float _timeElapsed;
        private bool _isStarted;

        public int Current => Mathf.RoundToInt(_timeElapsed);

        void IUpdateListener.OnUpdate()
        {
            if (!_isStarted)
                return;

            _timeElapsed += Time.deltaTime;
        }

        public void rftyj() => _isStarted = true;
        public void fyghjmdfg() => _isStarted = false;
        public void fghmfgh() => _timeElapsed = 0;
    }

    public class dfgjdfnhxx : jkdgh
    {
        private readonly List<BaseWindow> _windows;
        private readonly Canvas _canvas;
        private readonly Dictionary<Type, BaseWindow> _onScene = new Dictionary<Type, BaseWindow>();

        public dfgjdfnhxx(List<BaseWindow> windows, Canvas canvas)
        {
            _windows = windows;
            _canvas = canvas;
        }

        public T Open<T>() where T : BaseWindow
        {
            if (!_onScene.ContainsKey(typeof(T)))
                _onScene[typeof(T)] = Object.Instantiate(_windows.First(window => window.GetType() == typeof(T)),
                    _canvas.transform);

            _onScene[typeof(T)].gameObject.SetActive(true);

            return _onScene[typeof(T)] as T;
        }

        public T Get<T>() where T : BaseWindow
        {
            if (!_onScene.ContainsKey(typeof(T)))
                _onScene[typeof(T)] = Object.Instantiate(_windows.First(window => window.GetType() == typeof(T)),
                    _canvas.transform);

            return _onScene[typeof(T)] as T;
        }
    }

    public class rtuyitk : IUpdateListener
    {
        public const int TimeConstraintInSeconds = 90;

        private readonly LevelScoreConstraints _constraints;
        private readonly tyk _tyk;
        private bool _isInvoked;
        private readonly ghtyk _ghtyk;

        public event Action OnGameEnd;

        public rtuyitk(ghtyk ghtyk)
        {
            fghjjdfh.dfghjjdfgh<fghkds>().OnValueChanged += CheckWin;
            _tyk = fghjjdfh.dfghjjdfgh<tyk>();
            _constraints = fghjjdfh.dfghjjdfgh<LevelScoreConstraints>();
            _ghtyk = ghtyk;
        }

        private void CheckWin(int _)
        {
            if (!_isInvoked && fghjjdfh.dfghjjdfgh<FieldController>().IsFieldOpened)
            {
                OnGameEnd?.Invoke();
                _isInvoked = true;
                fghjjdfh.dfghjjdfgh<tyk>().fyghjmdfg();
            }
        }

        void IUpdateListener.OnUpdate()
        {
            if (!_isInvoked && _tyk.Current > TimeConstraintInSeconds)
            {
                OnGameEnd?.Invoke();
                _isInvoked = true;
                fghjjdfh.dfghjjdfgh<tyk>().fyghjmdfg();
            }
        }

        public void ghbmghv() => _isInvoked = false;
    }

    public class vbcm : jkdgh
    {
        private readonly rtuyitk _listener;
        private readonly LevelScoreConstraints _constraints;
        private readonly ghtyk _level;
        private readonly fghkds _fghkds;

        public enum GameResult
        {
            Win,
            Lose
        }

        public event Action<GameResult> OnGameResult;

        public vbcm()
        {
            _listener = fghjjdfh.dfghjjdfgh<rtuyitk>();
            _constraints = fghjjdfh.dfghjjdfgh<LevelScoreConstraints>();
            _level = fghjjdfh.dfghjjdfgh<ghtyk>();
            _fghkds = fghjjdfh.dfghjjdfgh<fghkds>();

            _listener.OnGameEnd += GetResult;
        }

        private void GetResult()
        {
            if (fghjjdfh.dfghjjdfgh<tyk>().Current >= rtuyitk.TimeConstraintInSeconds)
            {
                OnGameResult?.Invoke(GameResult.Lose);
            
                return;
            }

            OnGameResult?.Invoke(GameResult.Win);
        }
    }

    public class dsfhjnd : jkdgh
    {
        private readonly LevelScoreConstraints _constraints;
        private readonly ghtyk _loader;
        private readonly vbcm _resolver;
        private readonly dfgjdfnhxx _ui;
        private readonly fghkds _fghkds;
        private bool[] _openedLevels;

        public bool[] OpenedLevels => _openedLevels;
        public event Action OnNewlevelOpened;

        public dsfhjnd()
        {
            _constraints = fghjjdfh.dfghjjdfgh<LevelScoreConstraints>();
            _loader = fghjjdfh.dfghjjdfgh<ghtyk>();
            _resolver = fghjjdfh.dfghjjdfgh<vbcm>();
            _ui = fghjjdfh.dfghjjdfgh<dfgjdfnhxx>();
            _fghkds = fghjjdfh.dfghjjdfgh<fghkds>();
            _openedLevels = new bool[_constraints.Map.Count];
            _openedLevels[0] = true;

            _resolver.OnGameResult += TryOpenLevel;
        }

        private void TryOpenLevel(vbcm.GameResult result)
        {
            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(false);
            EndGameWindow endWindow = _ui.Open<EndGameWindow>();
            endWindow.SetPrev(_ui.Open<GameWindow>());
            endWindow.SetResult(result);
            endWindow.Score = _fghkds.fyjdfyjty.ToString();

            if (result == vbcm.GameResult.Win)
            {
                if (_loader.CurrentLevelIndex + 1 >= _constraints.Map.Count)
                {
                    endWindow.ButtonText = "To level menu";
                    endWindow.Init(vbcm.GameResult.Win, true);
                }
                else
                {
                    endWindow.Init(vbcm.GameResult.Win, false);
                    _openedLevels[_loader.CurrentLevelIndex + 1] = true;
                    OnNewlevelOpened?.Invoke();
                }
                
                return;
            }
            
            
            endWindow.Init(vbcm.GameResult.Lose, false);
        }
    }

    public class dsazfhds : jkdgh
    {
        protected readonly AudioSource Source;
        private readonly float _startVolume;

        public float Volume
        {
            get => Source.volume / _startVolume;
            set => Source.volume = value * _startVolume;
        }

        public dsazfhds(AudioSource source)
        {
            Source = source;
            _startVolume = Source.volume;
        }
    }

    public class ClickDsazfhds : dsazfhds
    {
        public ClickDsazfhds(AudioSource source) : base(source)
        {
        }

        public void Play()
        {
            Source.Play();
        }
    }
}