using Aspose.Cells;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetEventDataQuery : IRequest<byte[]>
    {
    }

    public class GetEventDataQueryHandler : IRequestHandler<GetEventDataQuery, byte[]>
    {
        private IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private IReadOnlyRepository<Core.Domain.Model.Team.Team> _teamRepository;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private IUserProfileProvider _profileProvider;

        public GetEventDataQueryHandler(IReadOnlyRepository<Core.Domain.Model.User.User> userRepository,
            IReadOnlyRepository<Core.Domain.Model.Team.Team> teamRepository,
            IUserProfileProvider profileProvider,
            IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
        }

        public async Task<byte[]> Handle(GetEventDataQuery request, CancellationToken cancellationToken)
        {
            var wb = new Workbook();
            var sheet = wb.Worksheets[0];
            var cells = sheet.Cells;

            var users = await _userRepository.ListAsync(
                UserSpecification.GetAllWithFilledProfile(_profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var teams = await _teamRepository.ListAsync(
                TeamSpecification.GetAll(_profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var projects = await _projectRepository.ListAsync(
                ProjectSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            users = users.OrderBy(x => x.LastName).ToArray();

            cells["A1"].PutValue("№");
            cells["B1"].PutValue("Фамилия");
            cells["C1"].PutValue("Имя");
            cells["D1"].PutValue("Отчество");
            cells["E1"].PutValue("Телефон");
            cells["F1"].PutValue("Группа");
            cells["G1"].PutValue("Команда");
            cells["H1"].PutValue("Проект");
            cells["I1"].PutValue("Жизненный сценарий");
            cells["J1"].PutValue("Ключевая технология");
            cells["K1"].PutValue("Заказчик (Организация)");
            cells["L1"].PutValue("Куратор (Наставник)");

            for (var i = 0; i < users.Length; i++)
            {
                var user = users[i];
                cells[$"A{i + 2}"].PutValue(i);
                cells[$"B{i + 2}"].PutValue(user?.LastName);
                cells[$"C{i + 2}"].PutValue(user?.FirstName);
                cells[$"D{i + 2}"].PutValue(user?.MiddleName);
                cells[$"E{i + 2}"].PutValue(user?.PhoneNumber);
                cells[$"F{i + 2}"].PutValue(user?.AcademicGroup);
                var team = teams.SingleOrDefault(x => x.Key == user?.TeamId);
                cells[$"G{i + 2}"].PutValue(team != null ? team.Name ?? "Команда №" + team.Key : "");
                var project = projects.SingleOrDefault(x => x.Key == team?.ProjectId);
                cells[$"H{i + 2}"].PutValue(project?.Name);
                cells[$"I{i + 2}"].PutValue(project?.LifeScenario.Name);
                cells[$"J{i + 2}"].PutValue(project?.KeyTechnology.Name);
                cells[$"K{i + 2}"].PutValue(project?.Organization);
                cells[$"L{i + 2}"].PutValue(project?.Curator);
            }

            var style = cells["A1"].GetStyle();
            style.Font.Size = 13;
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
            sheet.AutoFitRows();

            var stream = new MemoryStream();
            wb.Save(stream, new XlsSaveOptions(SaveFormat.Xlsx));
            return stream.ToArray();
        }
    }
}
