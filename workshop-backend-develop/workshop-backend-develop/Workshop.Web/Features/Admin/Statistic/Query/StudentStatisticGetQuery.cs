using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Admin.Statistic;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class StudentStatisticGetQuery : IRequest<byte[]>
    {
    }

    public class
        StudentStatisticGetQueryHandler : IRequestHandler<StudentStatisticGetQuery, byte[]>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserProfileProvider _profileProvider;
        private readonly UserRepository _userRepository;
        private readonly TeamRepository _teamRepository;

        public StudentStatisticGetQueryHandler(IProjectRepository projectRepository,
            IUserProfileProvider profileProvider, UserRepository users, TeamRepository teams)
        {
            _projectRepository = projectRepository;
            _profileProvider = profileProvider;
            _userRepository = users;
            _teamRepository = teams;
        }

        public async Task<byte[]> Handle(StudentStatisticGetQuery request,
            CancellationToken cancellationToken)
        {
            var wb = new Workbook();
            var sheet = wb.Worksheets[0];
            var cells = sheet.Cells;

            var usersProject = await _projectRepository
                .ListAsync(
                    ProjectSpecification.GetByOrganization("ИРИТ-РТФ", _profileProvider.GetProfile().User.EventId),
                    cancellationToken);
            var firstRow = new[]
            {
                "№",
                "Название проекта",
                "Описание проекта",
                "Цель проекта",
                "Результат",
                "Тимлид"
            };
            cells.ImportArray(firstRow, 0, 0, false);

            for (var i = 0; i < usersProject.Length; i++)
            {

                var project = usersProject[i];
                var row = new[]
                {
                    (i+1).ToString(),
                    project.Name ?? "Не указано",
                    project.Description ?? "Не указано",
                    project.Purpose ?? "Не указано",
                    project.Result ?? "Не указано",
                    project.Curator ?? "Не указано"
                };

                cells.ImportArray(row, i+1, 0, false);
            }
            var cf = new CellsFactory();
            var style = cf.CreateStyle();
            style.IsTextWrapped = true;
            style.VerticalAlignment = TextAlignmentType.Top;

            var firstRowStyle = cells["A1"].GetStyle();
            firstRowStyle.Font.Size = 14;
            firstRowStyle.Font.IsBold = true;

            
           
            cells.ApplyStyle(style, new StyleFlag() { All = true });

            sheet.Cells.Rows[0].ApplyStyle(firstRowStyle, new StyleFlag() { All = true });

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