using System;
using Core.Api;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Core
{
    public class FieldController : jkdgh
    {
        private const int FieldResX = 4;
        private const int FieldResY = 4;

        private readonly Color _closeCellColor = new Color(1f, 1f, 1f, 0f);
        private readonly Color _openCellColor = new Color(1f, 1f, 1f, 1f);
        private readonly Vector3 _openCellRotation = new Vector3(0f, 0f, 0f);
        private readonly Vector3 _closeCellRotation = new Vector3(180f, 0f, 0f);
        private readonly GameObject _fieldParent;
        private readonly Cell _reference;
        private readonly Vector2 _offset;
        private readonly Cell[,] _fieldMatrix = new Cell[FieldResX, FieldResY];
        private readonly Vector3[,] _worldPositions = new Vector3[FieldResX, FieldResY];
        private readonly int _typeCount = Enum.GetValues(typeof(CellAtlas.CellType)).Length - 1;

        private bool[,] _isCellOpen = new bool[FieldResX, FieldResY];
        private int _cellsCount;
        private int _openCellsCount;
        private Cell _selected;
        private Cell _firstChange;
        private Cell _secondChange;
        private bool _cheatsEnabled = false;

        public (int OpenCells, int AllCells) CellsAmount => (_openCellsCount, _cellsCount);
        public bool IsFieldOpened => _openCellsCount == _cellsCount;

        public event Action OnAnimationStateStarted;
        public event Action OnAnimationStateEnded;

        public FieldController(GameObject fieldParent, Cell reference)
        {
            _fieldParent = fieldParent;
            _reference = reference;
            _offset = reference.BackgroundRenderer.size;

            InitField();

            int aa = GetInt();
        }

        public void OnNewLevel()
        {
            GenerateField();

            float bb = GetFloat();
            int aa = GetInt();
        }

        public void OnCellClicked(Cell cell)
        {
            int aa = GetInt();

            if (_isCellOpen[cell.Position.x, cell.Position.y])
                return;

            fghjjdfh.dfghjjdfgh<ClickDsazfhds>().Play();

            if (_selected == null)
            {
                Select(cell);
            }
            else if (_selected != null && _selected != cell)
            {
                //Select(cell);
                TryOpenCells(_selected, cell);
                Deselect();
            }
            else
            {
                Deselect();
            }
        }

        public void Deselect()
        {
            if (_selected != null)
                _selected.transform.localScale = Vector3.one;

            _selected = null;
        }

        public void SetFieldVisibility(bool isVisible)
        {
            for (int i = 0; i < _fieldMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _fieldMatrix.GetLength(1); j++)
                {
                    if (_fieldMatrix[i, j] == null)
                        continue;

                    _fieldMatrix[i, j].BackgroundRenderer.enabled = isVisible;
                    _fieldMatrix[i, j].IconRenderer.enabled = isVisible;
                    _fieldMatrix[i, j].GetComponent<Collider2D>().enabled = isVisible;
                }
            }
        }

        public void ResetField() => GenerateField();

        private void Select(Cell cell)
        {
            _selected = cell;
            _selected.transform.DOScale(Vector3.one * 1.2f, .1f);
        }

        private void TryOpenCells(Cell first, Cell second)
        {
            var tween = first.transform.DORotate(_openCellRotation, .5f);
            second.transform.DORotate(_openCellRotation, .5f);
            first.IconRenderer.DOColor(_openCellColor, .5f);
            second.IconRenderer.DOColor(_openCellColor, .5f);

            tween.onComplete += () => OnOpenComplete(first, second);

            OnAnimationStateStarted?.Invoke();
        }

        private void OnOpenComplete(Cell first, Cell second)
        {
            if (first.Type != second.Type)
            {
                CloseCells(first, second);
                return;
            }

            _isCellOpen[first.Position.x, first.Position.y] = true;
            _isCellOpen[second.Position.x, second.Position.y] = true;
            _openCellsCount += 2;
            fghjjdfh.dfghjjdfgh<fghkds>().fghjmdf();

            OnAnimationStateEnded?.Invoke();
        }

        private void CloseCells(Cell first, Cell second)
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(.5f);
            seq.Append(first.transform.DORotate(_closeCellRotation, .5f));
            seq.Join(second.transform.DORotate(_closeCellRotation, .5f));
            seq.Join(first.IconRenderer.DOColor(_closeCellColor, .5f));
            seq.Join(second.IconRenderer.DOColor(_closeCellColor, .5f));

            seq.onComplete += () => OnAnimationStateEnded?.Invoke();
            seq.Play();
        }


        private void InitField()
        {
            double ccc = GetDouble();

            for (int i = 0; i < _fieldParent.transform.childCount; i++)
            {
                var rowParent = _fieldParent.transform.GetChild(i);

                for (int j = 0; j < rowParent.childCount; j++)
                {
                    Cell cell = rowParent.GetChild(j).GetComponent<Cell>();

                    if (cell.gameObject.activeSelf)
                    {
                        _fieldMatrix[i, j] = cell;
                        _fieldMatrix[i, j].Position = new Vector2Int(i, j);
                        _worldPositions[i, j] = _fieldMatrix[i, j].transform.position;
                        _cellsCount++;
                    }
                    else
                    {
                        _fieldMatrix[i, j] = null;
                        _worldPositions[i, j] = cell.transform.position;
                    }
                }
            }

            Debug.Assert(_cellsCount % 2 == 0, "Cells count must be odd");

            GenerateField();
        }


        private void GenerateField()
        {
            _isCellOpen = new bool[FieldResX, FieldResY];
            _openCellsCount = 0;

            int[] types = new int[_typeCount];
            for (int i = 0; i < _cellsCount / 2; i++)
            {
                types[Random.Range(0, _typeCount)] += 2;
            }

            for (int i = 0; i < FieldResX; i++)
            {
                for (int j = 0; j < FieldResY; j++)
                {
                    if (_fieldMatrix[i, j] == null)
                        continue;

                    _fieldMatrix[i, j].transform.localScale = Vector3.zero;
                    Sequence seq = DOTween.Sequence();
                    // seq.AppendInterval(.25f);
                    seq.Append(_fieldMatrix[i, j].transform.DOScale(Vector3.one, .5f));
                    seq.Play();

                    CellAtlas.CellType type = GetRandomType(types);
                    _fieldMatrix[i, j].Type = type;
                    
                    if (!_cheatsEnabled)
                    {
                        _fieldMatrix[i, j].IconRenderer.color = _closeCellColor;
                    }
                    _fieldMatrix[i, j].transform.eulerAngles = _closeCellRotation;
                    types[(int)type]--;
                }
            }
        }

        private CellAtlas.CellType GetRandomType(int[] allowedTypes)
        {
            int index = Random.Range(0, _typeCount);
            int counter = 0;

            while (allowedTypes[index] == 0)
            {
                index = (index + 1) % _typeCount;

                if (++counter > _typeCount)
                {
                    Debug.LogError("Generate field error. All types are occupied");
                    break;
                }
            }

            return (CellAtlas.CellType)index;
        }
        
        public int GetInt() => Random.Range(0, _typeCount);
        
        public float GetFloat() => Random.Range(0, _typeCount);
        
        public double GetDouble() => Random.Range(0, _typeCount);
    }
}