using MBS_Task.Domain.Enums;
using MBS_Task.Entities.Models;

namespace MBS_Task.Infrastructure.DataLoaders
{
    public class TextGridLoader
    {
        private readonly string _defaultPath;

        public TextGridLoader(string defaultPath = null)
        {
            _defaultPath = defaultPath;
        }

        // Загрузка из файла
        public Grid Load(string path = null)
        {
            path ??= _defaultPath;
            if (path == null || !File.Exists(path))
                throw new FileNotFoundException("Файл сетки не найден", path);

            using var reader = new StreamReader(path);
            return Load(reader);
        }

        // Загрузка из TextReader (stdin или StreamReader)
        public Grid Load(TextReader reader)
        {
            var firstLine = reader.ReadLine()?.Trim();
            if (firstLine == null)
                throw new InvalidDataException("Пустая первая строка");

            var split = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 2)
                throw new InvalidDataException("Некорректная первая строка сетки");

            if (!int.TryParse(split[0], out int rows) || !int.TryParse(split[1], out int cols))
                throw new InvalidDataException("Некорректные размеры сетки");

            var cells = new Cell[rows, cols];
            var portals = new Dictionary<char, List<Position>>();
            Position start = null;
            Position end = null;

            for (int i = 0; i < rows; i++)
            {
                string line = reader.ReadLine()?.TrimEnd();
                if (line == null || line.Length != cols)
                    throw new InvalidDataException($"Строка {i + 1} имеет некорректную длину");

                for (int j = 0; j < cols; j++)
                {
                    var pos = new Position(i, j);
                    var cell = new Cell(pos, line[j]);
                    cells[i, j] = cell;

                    if (cell.Type == CellType.Start) start = pos;
                    if (cell.Type == CellType.End) end = pos;

                    if (cell.Type == CellType.Portal)
                    {
                        if (!portals.ContainsKey(cell.Raw))
                            portals[cell.Raw] = new List<Position>();
                        portals[cell.Raw].Add(pos);
                    }
                }
            }

            if (start == null || end == null)
                throw new InvalidDataException("Сетка должна содержать Start и End");

            return new Grid(cells, start, end, portals);
        }
    }
}
