using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Cells;

namespace Workshop.Core.Domain.Model.Project
{
    public class ProjectData
    {
        public long EventId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Curator { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public string Organization { get; set; }
        public string Contacts { get; set; }
        public string Roles { get; set; }
        public List<string> HardSkills { get; set; }
        public List<string> SoftSkills { get; set; }
        public string LifeScenario { get; set; }
        public long LifeScenarioId { get; set; }
        public string KeyTechnology { get; set; }
        public long KeyTechnologyId { get; set; }
        public int TeamsSize { get; set; }
        public int TeamLimit { get; set; }
        public bool IsAvailable { get; set; }

        private readonly Dictionary<CellType, Cell> projectData;

        public ProjectData()
        {
        }

        public ProjectData(Dictionary<CellType, Cell> projectData)
        {
            this.projectData = projectData;

            TeamsSize = ParseCellData(CellType.TeamSize, GetIntValue, 5);
            TeamLimit = ParseCellData(CellType.TeamLimit, GetIntValue, 1);

            ProjectName = ParseCellData(CellType.ProjectName, GetStringValue, "Не указано");
            Description = ParseCellData(CellType.ProjectDescription, GetStringValue, "Не указано");
            Curator = ParseCellData(CellType.Curator, GetStringValue, "Не указано");
            Purpose = ParseCellData(CellType.ProjectPurpose, GetStringValue, "Не указано");
            Result = ParseCellData(CellType.Result, GetStringValue, "Не указано");
            Result = ParseCellData(CellType.Result, GetStringValue, "Не указано");
            // TODO: We should return List not string.
            Roles = ParseCellData(CellType.Roles, GetStringValue, "Не указано");

            Organization = ParseCellData(CellType.Organization, GetStringValue, "Не указано");
            Contacts = ParseCellData(CellType.Contacts, GetStringValue, "Не указано");
            LifeScenario = ParseCellData(CellType.LifeScenario, GetStringValue, "Не указано");
            KeyTechnology = ParseCellData(CellType.KeyTechnology, GetStringValue, "Не указано");

            SoftSkills = ParseCellData(CellType.SoftSkills, GetCompetencies, new List<string>());
            HardSkills = ParseCellData(CellType.HardSkills, GetCompetencies, new List<string>());
        }

        private T ParseCellData<T>(CellType cellType, Func<Cell, T> parseFunction, T defaultValue)
        {
            try
            {
                return parseFunction(projectData[cellType]);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        private int GetIntValue(Cell cell)
        {
            return int.Parse(cell.StringValue
                .Split(new[] {',', ' ', '-', '.'}, StringSplitOptions.RemoveEmptyEntries)
                .Last());
        }

        private string GetStringValue(Cell cell)
        {
            var trimmed = cell.StringValue.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                throw new Exception();
            }

            return trimmed;
        }

        private List<string> GetCompetencies(Cell skillsCell)
        {
            return skillsCell.StringValue
                .Split(new[] {'.', ','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(competency => competency.ToLower().Trim())
                .Distinct()
                .Where(competency => competency.Length > 0)
                .ToList();
        }
    }
}