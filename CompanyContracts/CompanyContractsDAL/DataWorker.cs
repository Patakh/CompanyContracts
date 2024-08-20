using CompanyContracts.Data;
using System.Text.Json;
using System.Xml.Serialization;
namespace CompanyContractsDAL;

public static class DataWorker
{
    private static CompanyContractsContext _companyContractsContext;

    static DataWorker()
    {
        _companyContractsContext = new CompanyContractsContext();
    }

    /// <summary>
    /// Метод для получения списка договоров за текущий год
    /// </summary> 
    public static List<Contract> GetContractsForCurrentYear() =>
        _companyContractsContext.Contracts.Where(c => c.SigningDate.Year == DateTime.Now.Year).ToList();


    /// <summary>
    /// Метод для получения суммы договоров за текущий год
    /// </summary> 
    public static decimal GetTotalContractAmountForCurrentYear()
    {
        return GetContractsForCurrentYear().Sum(c => c.ContractAmount);
    }

    /// <summary>
    /// Метод для получения списка контрагентов из России
    /// </summary> 
    public static List<LegalEntity> GetRussianContractors() =>
        _companyContractsContext.LegalEntities.Where(l => l.Country == "Россия").ToList();

    /// <summary>
    /// Метод для получения суммы договоров по каждому контрагенту из России
    /// </summary>
    public static Dictionary<string, decimal> GetTotalContractAmountByRussianContractor()
    {
        var result = new Dictionary<string, decimal>();
        foreach (var contractor in GetRussianContractors())
        {
            var totalAmount = _companyContractsContext.Contracts
                .Where(c => c.ContractorId == contractor.Id)
                .Sum(c => c.ContractAmount);

            result.Add(contractor.Name, totalAmount);
        }
        return result;
    }

    /// <summary>
    /// Метод для получения списка e-mail уполномоченных лиц, заключивших договоры за последние 30 дней, на сумму больше 40000
    /// </summary>
    public static List<string> GetEmailsOfAuthorizedPersonsForRecentContracts()
    {
        var recentContracts = _companyContractsContext.Contracts.
            Where(c => c.SigningDate >= DateOnly.FromDateTime(DateTime.Now).AddDays(-30) && c.ContractAmount > 40000).ToList();
        return recentContracts
            .Select(c => _companyContractsContext.PhysicalPersons.FirstOrDefault(p => p.Id == c.AuthorizedPersonId))
            .Where(p => p != null)
            .Select(p => p.Email)
            .ToList();
    }

    /// <summary>
    /// Метод для изменения статуса договора на "Расторгнут" для физических лиц старше 60 лет
    /// </summary>
    public static void TerminateContractsForElderlyPersons()
    {
        var elderlyPersons = _companyContractsContext.PhysicalPersons.Where(p => p.Age >= 60).ToList();
        foreach (var person in elderlyPersons)
        {
            var contracts = _companyContractsContext.Contracts.Where(c => c.AuthorizedPersonId == person.Id && c.Status != "Расторгнут");
            foreach (var contract in contracts)
            {
                contract.Status = "Расторгнут";
            }
        }
        _companyContractsContext.SaveChanges();
    }

    /// <summary>
    /// Метод для создания отчета в формате JSON
    /// </summary>
    public static void CreateReportToJson(string fileName)
    {
        var reportData = _companyContractsContext.PhysicalPersons
            .Where(p => _companyContractsContext.Contracts.Any(c => c.AuthorizedPersonId == p.Id && c.Status != "Расторгнут"))
            .Where(p => _companyContractsContext.LegalEntities.Any(l => l.Id == _companyContractsContext.Contracts.FirstOrDefault(c => c.AuthorizedPersonId == p.Id).ContractorId && l.City == "Москва"))
                .Select(p => new
                {
                    FullName = $"{p.FirstName} {p.LastName} {p.MiddleName}",
                    Email = p.Email,
                    Phone = p.Phone,
                    DateOfBirth = p.BirthDate.HasValue ? p.BirthDate.Value.ToString("dd-MM-yyyy") : null,
                })
                .ToList();
        var json = JsonSerializer.Serialize(reportData);
        File.WriteAllText(fileName, json);
    }

    /// <summary>
    /// Метод для создания отчета в формате XML
    /// </summary>
    public static void CreateReportToXml(string fileName)
    {
        var reportData = _companyContractsContext.PhysicalPersons
            .Where(p => _companyContractsContext.Contracts.Any(c => c.AuthorizedPersonId == p.Id && c.Status != "Расторгнут"))
            .Where(p => _companyContractsContext.LegalEntities.Any(l => l.Id == _companyContractsContext.Contracts.FirstOrDefault(c => c.AuthorizedPersonId == p.Id).ContractorId && l.City == "Москва"))
            .Select(p => new
            {
                FullName = $"{p.FirstName} {p.LastName} {p.MiddleName}",
                Email = p.Email,
                Phone = p.Phone,
                DateOfBirth = p.BirthDate.HasValue ? p.BirthDate.Value.ToString("dd-MM-yyyy") : null,
            })
            .ToList();

        var serializer = new XmlSerializer(typeof(List<object>));
        var writer = new StreamWriter(fileName);
        serializer.Serialize(writer, reportData);
        
    }
}