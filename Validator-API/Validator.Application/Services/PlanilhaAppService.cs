using ExcelDataReader;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Application.Interfaces;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Application.Models;
using System.ComponentModel.DataAnnotations;
using ValidationResult = Validator.Domain.Core.ValidationResult;

namespace Validator.Application.Services
{
    public class PlanilhaAppService : AppBaseService, IPlanilhaAppService
    {
        public PlanilhaAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<dynamic> Upload(FileStream objPlanilha)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(objPlanilha);
                var asDataTable = reader.AsDataSet().Tables[2];

                reader.Close();

                var inconsistentData = new List<dynamic>();

                for (int rowIndex = 1; rowIndex < asDataTable.Rows.Count; rowIndex++)
                {
                    DataRow row = asDataTable.Rows[rowIndex];

                    DateTime dataAdmissao;
                    DateTime.TryParse(row[5].ToString(), out dataAdmissao);
                    var data = new PlanilhaImport(rowIndex)
                    {
                        Nome = row[0].ToString(),
                        Email = row[1].ToString(),
                        CPF = row[2].ToString(),
                        Cargo = row[3].ToString(),
                        Nivel = row[4].ToString(),
                        DataAdmissao = dataAdmissao,
                        CentroCusto = row[6].ToString(),
                        NumeroCentroCusto = row[7].ToString(),
                        Unidade = row[8].ToString(),
                        SuperiorImediato = row[9].ToString(),
                        EmailSuperior = row[10].ToString(),
                        Divisoes =
                        !Convert.IsDBNull(row[11]) ? row[11].ToString() :
                        !Convert.IsDBNull(row[12]) ? row[12].ToString() :
                        !Convert.IsDBNull(row[13]) ? row[13].ToString() :
                        !Convert.IsDBNull(row[14]) ? row[14].ToString() :
                        !Convert.IsDBNull(row[15]) ? row[15].ToString() :
                        !Convert.IsDBNull(row[16]) ? row[16].ToString() :
                        !Convert.IsDBNull(row[17]) ? row[17].ToString() :
                        !Convert.IsDBNull(row[18]) ? row[18].ToString() :
                        !Convert.IsDBNull(row[19]) ? row[19].ToString() :
                        !Convert.IsDBNull(row[20]) ? row[20].ToString() :
                        !Convert.IsDBNull(row[21]) ? row[21].ToString() :
                        !Convert.IsDBNull(row[22]) ? row[22].ToString() :
                        !Convert.IsDBNull(row[23]) ? row[23].ToString() :""

                    };


                    if (data.IsValid == false)
                    {
                        inconsistentData.Add(data);
                    }

                }

                return inconsistentData;

            }
            catch (Exception err)
            {

                throw err;
            }



        }
    }
}
