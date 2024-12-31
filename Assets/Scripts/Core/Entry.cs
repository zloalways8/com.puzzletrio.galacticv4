using System.Collections.Generic;
using Core.PlayerInput;
using Core.UI;
using UnityEngine;

namespace Core
{
    [DefaultExecutionOrder(-10000)]
    public class Entry : MonoBehaviour
    {
        [SerializeField] private List<BaseWindow> _ui;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _fieldParent;
        [SerializeField] private GameObject _fieldBg;
        [SerializeField] private Cell _cellReference;
        [SerializeField] private CellAtlas _cellAtlas;
        [SerializeField] private TextAtlas _textAtlas;
        [SerializeField] private LevelScoreConstraints _levelConstraints;
        [SerializeField] private AudioSource _sound;
        [SerializeField] private AudioSource _clickSound;

        private UpdateProcessor _updateProcessor;

        private void Awake()
        {
            _updateProcessor = GetComponent<UpdateProcessor>();

            InstallBindings();
        }

        private void Start()
        {
            //ServiceLocator.Get<InterfaceDispatcher>().Open<MainMenuWindow>();
            fghjjdfh.dfghjjdfgh<dfgjdfnhxx>().Open<PrivacyDialogWindow>();
            fghjjdfh.dfghjjdfgh<FieldController>().SetFieldVisibility(false);
        }

        private void InstallBindings()
        {
            fghjjdfh.Bind<CellAtlas>(_cellAtlas);
            fghjjdfh.Bind<TextAtlas>(_textAtlas);
            fghjjdfh.Bind<LevelScoreConstraints>(_levelConstraints);
            fghjjdfh.Bind<UpdateProcessor>(_updateProcessor);
            fghjjdfh.Bind<FieldController>(new FieldController(_fieldParent, _cellReference));
            fghjjdfh.Bind<hdfgj>(new hdfgj());
            fghjjdfh.Bind<dfgshdfghs>(new dfgshdfghs());
            fghjjdfh.Bind<fghkds>(new fghkds());
            fghjjdfh.Bind<tyk>(new tyk());
            
            var levelLoader = new ghtyk();
            fghjjdfh.Bind<ghtyk>(levelLoader);
            fghjjdfh.Bind<rtuyitk>(new rtuyitk(levelLoader));
            levelLoader.tyjj(fghjjdfh.dfghjjdfgh<rtuyitk>());
            
            fghjjdfh.Bind<tytd>(new tytd(_cellReference));
            fghjjdfh.Bind<dfgjdfnhxx>(new dfgjdfnhxx(_ui, _canvas));
            fghjjdfh.Bind<vbcm>(new vbcm());
            fghjjdfh.Bind<dsfhjnd>(new dsfhjnd());
            fghjjdfh.Bind<dsazfhds>(new dsazfhds(_sound));
            fghjjdfh.Bind<ClickDsazfhds>(new ClickDsazfhds(_clickSound));

            _updateProcessor.Bind(fghjjdfh.dfghjjdfgh<tyk>()).AsUpdateListener();
            _updateProcessor.Bind(fghjjdfh.dfghjjdfgh<hdfgj>()).AsUpdateListener();
            _updateProcessor.Bind(fghjjdfh.dfghjjdfgh<rtuyitk>()).AsUpdateListener();
        }
    }
}