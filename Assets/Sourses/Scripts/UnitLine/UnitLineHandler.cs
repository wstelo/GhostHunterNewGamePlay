using System.Collections.Generic;
using System.Linq;

public class UnitLineHandler
{
    private ElementTypes[,] _ghostColorList;
    private UnitLineFactory _unitLineFactory;
    private SpawnerHandler<Ghost> _spawnerHandler;
    private List<UnitLine> _lines = new List<UnitLine>();
    private UnitLine _prefab;
    private int _currentLineNumber = 0;

    public UnitLineHandler(LevelConfig config, SpawnerHandler<Ghost> spawnerHandler, UnitLine prefab)
    {
        _ghostColorList = config.ButtonValues;
        _spawnerHandler = spawnerHandler;
        _prefab = prefab;
        _unitLineFactory = new UnitLineFactory(_spawnerHandler, _prefab, config.LevelSpeed);

        CreateLine();
    }

    public Ghost GetFirstTarget()
    {
        return _lines.First().GetFirstUnit();
    }

    public List<ElementTypes> GetCurrentLevelTypes()
    {
        List<ElementTypes> elementTypes = new List<ElementTypes>();

        foreach (var item in _lines)
        {
            List<ElementTypes> lineElementTypes = new List<ElementTypes>();
            lineElementTypes = item.GetElementTypes();

            foreach (var elementType in lineElementTypes)
            {
                elementTypes.Add(elementType);
            }
        }

        return elementTypes;
    }

    private UnitLine CreateLine()
    {
        if (_currentLineNumber < _ghostColorList.GetLength(0))
        {
            List<ElementTypes> elements = new List<ElementTypes>();

            for (int j = 0; j < _ghostColorList.GetLength(1); j++)
            {
                elements.Add(_ghostColorList[_currentLineNumber, j]);
            }

            UnitLine currentLine = _unitLineFactory.GetLine(elements);
            currentLine.SpawnLineWalked += SpawnNewLine;
            currentLine.LineEmtied += RemoveLine;
            _currentLineNumber++;
            _lines.Add(currentLine);

            return currentLine;
        }

        return null;
    }

    private void RemoveLine(UnitLine line)
    {
        _lines.Remove(line);
        line.LineEmtied -= RemoveLine;
    }

    private void SpawnNewLine(UnitLine line)
    {
        CreateLine();
        line.SpawnLineWalked -= SpawnNewLine;
    }
}
