using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetEventResultQuery : IRequest<byte[]>
    {
    }

    public class GetEventResultQueryHandler : IRequestHandler<GetEventResultQuery, byte[]>
    {
        private IReadOnlyRepository<Core.Domain.Model.Team.Team> _teamRepository;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private IUserProfileProvider _profileProvider;

        public GetEventResultQueryHandler(IReadOnlyRepository<Core.Domain.Model.Team.Team> teamRepository,
            IUserProfileProvider profileProvider,
            IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
        }

        public async Task<byte[]> Handle(GetEventResultQuery request, CancellationToken cancellationToken)
        {
            var wb = new Workbook();
            var sheet = wb.Worksheets[0];
            var cells = sheet.Cells;

            var teams = await _teamRepository.ListAsync(
                TeamSpecification.GetAll(_profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var projects = await _projectRepository.ListAsync(
                ProjectSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            teams = teams.OrderBy(x => x.Key).ToArray();

            cells["A1"].PutValue("Номер команды");
            cells["B1"].PutValue("Тема проекта");
            cells["C1"].PutValue("Список участников");
            cells["D1"].PutValue("Коментарий");
            cells["E1"].PutValue("Оценка");

            for (var i = 0; i < teams.Length; i++)
            {
                cells[$"A{i + 2}"].PutValue(teams[i].Key);
                cells[$"B{i + 2}"].PutValue(
                    projects.SingleOrDefault(x => x.Key == teams[i].ProjectId)?.Name ?? "");
                cells[$"C{i + 2}"].PutValue(teams[i].Users != null
                    ? string.Join("\n",
                        teams[i].Users?.Select(x =>
                            $"{x.LastName} {x.FirstName} {x.MiddleName}"))
                    : string.Empty);
                cells[$"D{i + 2}"].PutValue(teams[i].Comment ?? "");
                cells[$"E{i + 2}"].PutValue(teams[i].Mark.ToString() ?? "");
            }

            var style = cells["A1"].GetStyle();
            style.Font.Size = 14;
            style.Font.IsBold = true;

            sheet.Cells.Rows[0].ApplyStyle(style, new StyleFlag() { All = true });

            var defaultCellsStyle = cells["A2"].GetStyle();
            defaultCellsStyle.IsTextWrapped = true;
            defaultCellsStyle.Font.Size = 12;

            for (var i = 1; i < sheet.Cells.Rows.Count; i++)
            {
                sheet.Cells.Rows[i].ApplyStyle(defaultCellsStyle, new StyleFlag() { All = true });
            }

            sheet.AutoFitColumns(new AutoFitterOptions()
            {
                IgnoreHidden = true,
                MaxRowHeight = 100,
                AutoFitWrappedTextType = AutoFitWrappedTextType.Paragraph
            });

            sheet.Cells.Columns[1].Width = 50;
            sheet.AutoFitRows();

            var stream = new MemoryStream();
            wb.Save(stream, new XlsSaveOptions(SaveFormat.Xlsx));
            return stream.ToArray();
        }
    }
}