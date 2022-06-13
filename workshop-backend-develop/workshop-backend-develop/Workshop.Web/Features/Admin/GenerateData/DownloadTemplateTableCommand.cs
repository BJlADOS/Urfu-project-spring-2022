using Aspose.Cells;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Web.Features.Admin.GenerateData
{
    public class DownloadTemplateTableCommand : IRequest<byte[]>
    {
    }

    public class DownloadTemplateTableHandler : IRequestHandler<DownloadTemplateTableCommand, byte[]>
    {
        public DownloadTemplateTableHandler()
        { }

        public async Task<byte[]> Handle(DownloadTemplateTableCommand request, CancellationToken cancellationToken)
        {
            var wb = new Workbook();
            var sheet = wb.Worksheets[0];
            var cells = sheet.Cells;

            var firstRow = new string[]
            {
                "Название проекта",
                "Описание проекта",
                "Куратор (Наставник)",
                "Цель проекта",
                "Результат",
                "Организация заказчика",
                "Контакты представителя заказчика",
                "Роли\n(Значения указываются через запятую)",
                "Жизненный сценарий (Проектный трек)",
                "Ключевая технология (Тип продукта)",
                "Размер команды",
                "Ограничение по командам",
                "Soft skills\n(Значения указываются через запятую; можно оставить пустым)",
                "Hard skills\n(Значения указываются через запятую; можно оставить пустым)",
            };

            var secondRow = new string[]
            {
                "Демонстрация",
                "В этом разделе находится подробное описание проекта",
                "Иванов Иван Иванович",
                "В этом разделе описана цель проекта",
                "В этом разделе указан результат проекта и итоговый продукт",
                "ООО Организация",
                "Алексеев Алексей Алексеевич, +7123456789",
                "Backend разработчик, Frontend разработчик, Дизайнер, Аналитик",
                "Профессиональный",
                "Web",
                "4",
                "1",
                "Коммуникабельность",
                "Базы данных, Анализ данных, Frontend, Backend, Интерфесы",
            };

            CellsFactory cf = new CellsFactory();
            Style style = cf.CreateStyle();

            style.IsTextWrapped = true;
            style.VerticalAlignment = TextAlignmentType.Top;

            cells.ApplyStyle(style, new StyleFlag() { All = true });

            cells.ImportArray(firstRow, 0, 0, false);
            cells.ImportArray(secondRow, 1, 0, false);

            sheet.AutoFitColumns(new AutoFitterOptions()
            {
                IgnoreHidden = true,
                MaxRowHeight = 100,
                AutoFitWrappedTextType = AutoFitWrappedTextType.Paragraph
            });
            sheet.AutoFitRows();

            var stream = new MemoryStream();
            wb.Save(stream, new XlsSaveOptions(SaveFormat.Xlsx));
            return stream.ToArray();
        }
    }
}
