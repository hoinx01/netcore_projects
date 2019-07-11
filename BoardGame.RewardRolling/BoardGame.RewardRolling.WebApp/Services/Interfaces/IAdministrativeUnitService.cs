using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;

namespace BoardGame.RewardRolling.WebApp.Services.Interfaces
{
    public interface IAdministrativeUnitService
    {
        Task ImportExcelAdministrativeUnit(
            List<ExcelStandardAdministrativeUnitModel> excelModels
        );

    }
}
