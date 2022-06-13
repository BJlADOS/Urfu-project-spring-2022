using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using MediatR;
using Workshop.Core.Domain.Model.Auditorium;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.GenerateData
{
    public class DownloadScheduleCommand : IRequest<byte[]>
    {
    }

    public class DownloadScheduleCommandHelper : IRequestHandler<DownloadScheduleCommand, byte[]>
    {
        private readonly IUserProfileProvider _userProfile;
        private readonly IAuditoriumRepository _auditoriumRepository;

        public DownloadScheduleCommandHelper(IUserProfileProvider userProfile, IAuditoriumRepository auditoriumRepository)
        {
            _userProfile = userProfile;
            _auditoriumRepository = auditoriumRepository;
        }

        public async Task<byte[]> Handle(DownloadScheduleCommand request, CancellationToken cancellationToken)
        {
            var user = _userProfile.GetProfile().User;

            var workBook = new Workbook();
            var sheet = workBook.Worksheets[0];
            var cells = sheet.Cells;

            var auditoriums = await
                _auditoriumRepository.ListAsync(AuditoriumSpecification.GetAll(user.EventId), cancellationToken);

            cells.ImportArray(new[] { "Аудитория", "Эксперты" }, 0, 0, true);
            cells.ImportArray(new[] { "Время/Команда" }, 2, 0, true);

            var index = 1;
            foreach (var auditorium in auditoriums.OrderBy(x => x.Name))
            {
                var column = new List<string> { auditorium.Name };

                var experts = string.Join(",\n", auditorium.Experts
                    .Select(x => x.User)
                    .Select(x =>
                    {
                        var result = $"{x.LastName} {x.FirstName}";
                        if (!string.IsNullOrEmpty(x.MiddleName))
                        {
                            result += $" {x.MiddleName}";
                        }
                        return result;
                    })
                    .OrderBy(x => x)
                    );
                column.Add(experts);

                var orderedTeamSlots = auditorium.TeamSlots.OrderBy(x => x.Time).ToArray();
                var teams = orderedTeamSlots.Select(x => x.Team != null ? x.Team.Name ?? "Команда №" + x.TeamId : "").ToArray();
                var times = orderedTeamSlots.Select(x => x.Time.ToLocalTime().ToString()).ToArray();
                column.AddRange(teams);

                cells.ImportArray(times, 2, index, true);
                cells.ImportArray(column.ToArray(), 0, index + 1, true);

                cells.Merge(0, index, 1, 2);
                cells.Merge(1, index, 1, 2);

                index += 2;
            }

            var cellsFactory = new CellsFactory();
            var style = cellsFactory.CreateStyle();

            style.IsTextWrapped = true;
            style.VerticalAlignment = TextAlignmentType.Top;

            cells.ApplyStyle(style, new StyleFlag { All = true });

            var options = new AutoFitterOptions()
            {
                IgnoreHidden = true,
                MaxRowHeight = 150,
                AutoFitWrappedTextType = AutoFitWrappedTextType.Paragraph,
                AutoFitMergedCellsType = AutoFitMergedCellsType.EachLine
            };

            sheet.AutoFitColumns(options);
            sheet.AutoFitRows(options);

            var stream = new MemoryStream();
            workBook.Save(stream, new XlsSaveOptions(SaveFormat.Xlsx));
            return stream.ToArray();
        }
    }
}