using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.GenerateData
{
    public class DownloadResultsCommand : IRequest<byte[]>
    {
    }

    public class DownloadResultsCommandHelper : IRequestHandler<DownloadResultsCommand, byte[]>
    {
        private const int MarkMaxValue = 4;
        private readonly IUserProfileProvider _userProfile;
        private readonly TeamRepository _teamRepository;

        public DownloadResultsCommandHelper(IUserProfileProvider userProfile, TeamRepository teamRepository)
        {
            _userProfile = userProfile;
            _teamRepository = teamRepository;
        }

        public async Task<byte[]> Handle(DownloadResultsCommand request, CancellationToken cancellationToken)
        {
            var user = _userProfile.GetProfile().User;

            var workBook = new Workbook();
            var sheet = workBook.Worksheets[0];

            var teams = await
                _teamRepository.TeamsWithReviewListAsync(TeamSpecification.GetAll(user.EventId), cancellationToken);

            SetHeaders(sheet);
            SetTeamReviews(teams, sheet.Cells);
            SetStyle(sheet);

            var stream = new MemoryStream();
            workBook.Save(stream, new XlsSaveOptions(SaveFormat.Xlsx));
            return stream.ToArray();
        }

        private static void SetHeaders(Worksheet sheet)
        {
            sheet.Cells.ImportArray(
                new[]
                {
                    "Команда",
                    "Эксперты",
                    "Формулировка цели и задач",
                    "Обоснование выбранного решения",
                    "Презентация проекта",
                    "Техническая проработанность",
                    "Соответствие результата поставленной цели",
                    "Знание предметной области",
                    "Итоговая оценка"
                },
                0, 0, false);
        }

        private static void SetTeamReviews(IEnumerable<Core.Domain.Model.Team.Team> teams, Cells cells)
        {
            var index = 1;
            foreach (var team in teams)
            {
                cells.ImportArray(new[] { team.Name ?? "Команда №" + team.Key }, index, 0, false);

                var tempIndex = index;
                foreach (var teamReview in team.TeamReviews)
                {
                    var expertName = new[]
                    {
                        string.Join(' ', teamReview.Expert.LastName, teamReview.Expert.FirstName,
                            teamReview.Expert.MiddleName)
                    };
                    cells.ImportArray(expertName, index, 1, false);

                    var marks = new List<int>
                    {
                        teamReview.GoalsAndTasks ?? 0,
                        teamReview.Solution ?? 0,
                        teamReview.Presentation ?? 0,
                        teamReview.Technical ?? 0,
                        teamReview.Result ?? 0,
                        teamReview.Knowledge ?? 0
                    };
                    marks.Add((int)Math.Round(marks.Average() / MarkMaxValue * 100));
                    cells.ImportArray(marks.ToArray(), index, 2, false);
                    index++;
                }
                if (team.TeamReviews != null && team.TeamReviews.Count > 0)
                    cells.Merge(tempIndex, 0, team.TeamReviews.Count, 1);
            }
        }

        private static void SetStyle(Worksheet sheet)
        {
            var cellsFactory = new CellsFactory();
            var style = cellsFactory.CreateStyle();

            style.IsTextWrapped = true;
            style.VerticalAlignment = TextAlignmentType.Top;

            sheet.Cells.ApplyStyle(style, new StyleFlag { All = true });

            sheet.AutoFitColumns(new AutoFitterOptions()
            {
                IgnoreHidden = true,
                MaxRowHeight = 100,
                AutoFitWrappedTextType = AutoFitWrappedTextType.Paragraph
            });
            sheet.AutoFitRows();
        }
    }
}